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

namespace FluffyCRM.Controllers
{
    public class JobTasksController : Controller
    {
        private DataRepository _repos = new DataRepository();
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: JobTasks
        //public ActionResult Index()
        //{
        //    return View(db.JobTasks.ToList());
        //}


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
        public ActionResult Assignment([Bind(Include = "TaskId, UserId")] TaskAssignment model)
        {
            Employee emp = new Employee();
            JobTask jt = new JobTask();
            

            if (ModelState.IsValid)
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
        public ActionResult DeleteConfirmed(int id)
        {
            JobTask jobTask = db.JobTasks.Find(id);
            db.JobTasks.Remove(jobTask);
            db.SaveChanges();
            return RedirectToAction("Index");
        }



        // GET: TaskNotes/Create
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
        public ActionResult CreateNote([Bind(Include = "Id,CategoryId,Subject,Comment,JobTask_Id,CreatedBy,CreateDate,Status,DeleteInd,ClientId,StartDate,CompletedDate,DueDate,LocalTime")] TaskNote taskNote)
        {
            ViewBag.CustList = new SelectList(_repos.GetClientListAll(), "ClientId", "CompanyName", null);
            ViewBag.TaskId = taskNote.JobTask_Id;
            if (ModelState.IsValid)
            {
                db.TaskNotes.Add(taskNote);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
             return PartialView(taskNote);




        }




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
