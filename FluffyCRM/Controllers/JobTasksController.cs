using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FluffyCRM.Models;
using PagedList;
using FluffyCRM.DAL;
using FluffyCRM.utils;
using Microsoft.AspNet.Identity;
using FluffyCRM.ViewModels;

namespace FluffyCRM.Controllers
{
    [Authorize]
    public class JobTasksController : Controller
    {
        private const int MAXPICWIDTH = 300;
        private const int MAXPICHEIGHT = 500;
        private DataRepository _repos = new DataRepository();
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: JobTasks
        //public ActionResult Index()
        //{
        //    return View(db.JobTasks.ToList());
        //}

        [Authorize(Roles = "Admin,Staff")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)

        {
            ViewBag.Title = "Task Listing";
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            // ViewBag.ZipSortParm = sortOrder == "Zip" ? "Zip_desc" : "Zip";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            if (searchString == null) { searchString = ""; }
            var JobTasks = _repos.GetTaskList(0,"", searchString);
           
            switch (sortOrder)
            {
                case "name_desc":
                    JobTasks = JobTasks.OrderByDescending(s => s.Name);
                    break;

                default:  // Name ascending 
                    JobTasks = JobTasks.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 50;
            int pageNumber = (page ?? 1);

            if (Request.IsAjaxRequest() == true) {
                return PartialView(JobTasks.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return View(JobTasks.ToPagedList(pageNumber, pageSize));
            }
        }

        // GET: JobTasks/Details/5
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index");
            }
            JobTask jobTask = db.JobTasks.Find(id);
            if (jobTask == null)
            {
                return null;
            }

            if (Request.IsAjaxRequest() == true)
            {
                return PartialView(jobTask);
            } else
            {
                return View(jobTask);
            }
                
        }

        #region NOTES
        // GET: TaskNotes
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult _Notes(int? id, int? page)
        {

            var lst = _repos.TaskNoteListing(id ?? 0, "", "", "");

            return View(lst);
        }

        public ActionResult AssignedTo(int? taskId)
        {
            if (taskId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lst = _repos.AssigneeList((int)taskId);
            if (lst == null)
            {
                return null;
            }
            return View(lst);
        }


        // GET: TaskNotes/Edit/5
        [Authorize(Roles = "Admin,Staff")]
        [Route("JobTasks/EditNote/{id ?}")]
        public ActionResult EditNote(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.CustList = new SelectList(_repos.GetClientListAll(), "ClientId", "CompanyName", null);
            TaskNote taskNote = _repos.GetTaskNote(id);
            if (taskNote == null)
            {
                return RedirectToAction("Index", "JobTasks");
            }
            return View(taskNote);
        }

        // POST: TaskNotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Staff")]
        [Route("JobTasks/EditNote/{id ?}")]
        public ActionResult EditNote([Bind(Include = "Id,CategoryId,Subject,Comment,JobTask_Id,CreatedBy,CreateDate,Status,DeleteInd,ClientId,StartDate,CompletedDate,DueDate,LocalTime")] TaskNote taskNote)
        {
            ViewBag.CustList = new SelectList(_repos.GetClientListAll(), "ClientId", "CompanyName", null);
            if (ModelState.IsValid)
            {
                db.Entry(taskNote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "JobTasks");
            }
            return View(taskNote);
        }


        // GET: TaskNotes/Delete/5
        [Authorize(Roles = "Admin,Staff")]
        [Route("JobTasks/DeleteNote/{id ?}/{taskId ?}")]
        public ActionResult DeleteNote(int id = 0, int taskId = 0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            if (taskId == 0)
            {
                return RedirectToAction("Index");
            }

            TaskNote taskNote = db.TaskNotes.Find((int)id);
            if (taskNote == null)
            {
                return RedirectToAction("Index");
            }
            db.TaskNotes.Remove(taskNote);
            db.SaveChanges();

            //   var lst = _repos.TaskNoteListing(taskId, "", "", "");

            return RedirectToAction("Index", "JobTasks");
        }

        #endregion

        // GET: JobTasks/Create
        public ActionResult Assignment(int? taskId)
        {
            if (taskId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.Employees = new SelectList(_repos.EmpList(), "UserId", "Name", null);

            return View();
        }

        // POST: JobTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult Assignment([Bind(Include = "TaskId, UserId")] TaskAssignment model)
        {
            Employee emp = new Employee();
            JobTask jt = new JobTask();

          

            if (ModelState.IsValid)
            {
                if (Request.Form["submit"] == "Create")
                {
                    emp = db.Employees.Where(m => m.UserId == model.UserId).SingleOrDefault();
                    if (emp != null)
                    {
                        model.LastName = emp.LastName;
                        model.FirstName = emp.FirstName;
                        model.Initials = emp.Initials;
                        db.TaskAssignments.Add(model);
                        db.SaveChanges();

                        jt = db.JobTasks.Find(model.TaskId);
                        if (jt != null)
                        {
                            var rc = sendAssignTaskNotify(emp, jt);
                        }
                    }
                }  else if (Request.Form["delete"] == "Remove")
                {
                    model = db.TaskAssignments.Where(m => m.UserId == model.UserId && m.TaskId == model.TaskId).Take(1).SingleOrDefault();
                    if (model != null)
                    {
                        db.Entry(model).State = EntityState.Deleted;
                        db.SaveChanges();
                    }

                }
                return RedirectToAction("Index");
            }

            return View(model);
        }


        public bool sendAssignTaskNotify(Employee emp, JobTask jt) {
           
            try
            {
                bool rc = false;
                var es = new EmailSender();
                string subj = "You've been assigned a Fluffy New Task!";
                string baseurl = "http://www.ccssllc.com/JobTasks/details/";
                string surl = String.Format("<a href=\"{0}{1}\">{0}{1}</a>", baseurl, jt.Id.ToString());
                var email = _repos.GetEmailByUID(emp.UserId);
                string sTask = "You've been assigned a new task: " + utils.HtmlEncodeDecode.Encode(jt.Name) + "<br /><br />To access directly, go to: "  + surl;
                var sBody = es.GetNotifyMsgBody("FluffyCRM", sTask);

                es.Send(email, subj, sBody, true, null);

                es = null;
                return rc;
            }
            catch { return false; }
        }


        // GET: JobTasks/Create
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: JobTasks/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,ProdId,ProjectId,ParentTaskId,Level,ClientId,ContactUserId,CreatedBy,StartDate,CompletedDate,DueDate,LocalTime")] JobTask jobTask)
        {
            if (ModelState.IsValid)
            {
                db.JobTasks.Add(jobTask);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jobTask);
        }

        // GET: JobTasks/Edit/5
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            ViewBag.TaskCategories = new SelectList(_repos.GetCategoryList(FLCatType.Task), "id", "Name", null);

            var proj_lst = db.WorkProjects.ToList();
            ViewBag.WorkProjects = new SelectList(db.WorkProjects.OrderBy(o=>o.Name).ToList(), "id", "Name", null);
            JobTask jobTask = db.JobTasks.Find(id);
            if (jobTask == null)
            {
                return HttpNotFound();
            }
            return View(jobTask);
        }

        // POST: JobTasks/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,ProdId,TaskType,ProjectId,ParentTaskId,Level,ClientId,ContactUserId,CreatedBy,StartDate,CompletedDate,DueDate,LocalTime")] JobTask jobTask)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobTask).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jobTask);
        }

        // GET: JobTasks/Delete/5
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobTask jobTask = db.JobTasks.Find(id);
            if (jobTask == null)
            {
                return HttpNotFound();
            }
            return View(jobTask);
        }

        // POST: JobTasks/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult DeleteConfirmed(int id)
        {
            JobTask jobTask = db.JobTasks.Find(id);
            db.JobTasks.Remove(jobTask);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        // GET: TaskNotes/Create
        [Authorize(Roles = "Admin,Staff")]
        public PartialViewResult CreateNote(int? id)
        {
            ViewBag.CustList = new SelectList(_repos.GetClientListAll(), "ClientId", "CompanyName", null);
            TaskNote model = new TaskNote();
            model.JobTask_Id = id ?? 0;
            ViewBag.TaskId = id;
            return PartialView(model);
        }

        // POST: TaskNotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult CreateNote([Bind(Include = "Id,CategoryId,Subject,Comment,JobTask_Id,CreatedBy,CreateDate,Status,DeleteInd,ClientId,StartDate,CompletedDate,DueDate,LocalTime")] TaskNote taskNote)
        {
            ViewBag.CustList = new SelectList(_repos.GetClientListAll(), "ClientId", "CompanyName", null);
            ViewBag.TaskId = taskNote.JobTask_Id;

            var userName = User.Identity.Name;

            var userId = User.Identity.GetUserId();



            if (ModelState.IsValid)
            {
                taskNote.Id = 0;
                taskNote.CreatedBy = userId;
                taskNote.CategoryId = 1;
                _repos.AddOrUpdateNote(taskNote);
                return RedirectToAction("Index");

            }
             return PartialView(taskNote);




        }

        #region IMAGE_PROCESSING




        public ActionResult Picture(int? id)
        {
            Picture pic = new Picture();
            if (id != null && id != 0) pic = _repos.GetImage((int)id);

            if (pic != null)
            {
                return View(pic);
            }
            else return null;
        }

        //iMAGE UPLOAD
        [Authorize(Roles = "Admin, Contributer")]
        public ActionResult Upload(int? id)
        {
            if (id == null) id = 0;
            TaskNote taskNote = _repos.GetTaskNote(id);
            if (taskNote == null)
            {
                return new HttpNotFoundResult("Note not found");
            }
            ViewBag.TaskID = id;
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Contributer")]
        public ActionResult Upload(PictureAddView image)
        {
            // Apply Validation Here
            int sourceId = image.SourceID;



            if (image.File.ContentLength > (3 * 1024 * 1024))
            {
                ModelState.AddModelError("CustomError", "File size must be less than 3 MB");
                return View();
            }
            if (!(image.File.ContentType == "image/jpeg" || image.File.ContentType == "image/gif" || image.File.ContentType == "image/png"))
            {
                ModelState.AddModelError("CustomError", "Only png, jpeg and gif file types are allowed!");
                return View();
            }

            image.FileName = image.File.FileName;
            image.Size = image.File.ContentLength;
            image.ContentType = image.File.ContentType;


            byte[] data = new byte[image.File.ContentLength];
            image.File.InputStream.Read(data, 0, image.File.ContentLength);


            int w = 0;
            int h = 0;

            utils.StaticImageResizer.GetDimensions(data, out w, out h);

            if (w > MAXPICWIDTH || h > MAXPICHEIGHT)
            {
                //desired max width = 400, want to scale proportionally
                if (w > MAXPICWIDTH)
                {
                    decimal nHeight = MAXPICWIDTH * ((decimal)h / w);
                    //call resize 

                    data = utils.StaticImageResizer.GetScaledDownByteArray(data, MAXPICWIDTH, (int)nHeight);
                }
                else if (h > MAXPICHEIGHT)
                {
                    decimal nWidth = MAXPICHEIGHT * ((decimal)w / h);
                    //call resize 

                    data = utils.StaticImageResizer.GetScaledDownByteArray(data, (int)nWidth, MAXPICHEIGHT);

                }
            }
            Picture pic = new Picture();
            pic.ImageData = data;
            pic.ContentType = image.ContentType;
            pic.FileName = image.FileName;
            pic.Height = image.Height;
            pic.Size = image.Size;
            pic.Title = image.Title;
            pic.Width = image.Width;
            pic.Contents = image.Contents;

            db.Pictures.Add(pic);
            db.SaveChanges();

            image = null;

            TaskNote taskNote = _repos.GetTaskNote(sourceId);

            //return RedirectToAction("Details", new { id = recipeid }); // if recipeid = 0 will return to list
            return View(taskNote);
        }
        #endregion


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
