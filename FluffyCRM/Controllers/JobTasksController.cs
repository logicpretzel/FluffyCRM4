using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FluffyCRM.Models;

namespace FluffyCRM.Controllers
{
    public class JobTasksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: JobTasks
        public ActionResult Index()
        {
            return View(db.JobTasks.ToList());
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
        public ActionResult Edit([Bind(Include = "Id,Name,Description,ProdId,ProjectId,ParentTaskId,Level,ClientId,ContactUserId,CreatedBy,StartDate,CompletedDate,DueDate,LocalTime")] JobTask jobTask)
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
