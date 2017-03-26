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
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JobTask jobTask = db.JobTasks.Find(id);
            if (jobTask == null)
            {
                return HttpNotFound();
            }
            return View(jobTask);
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
