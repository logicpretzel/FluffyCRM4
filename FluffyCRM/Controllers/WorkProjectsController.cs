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
    public class WorkProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WorkProjects
        public ActionResult Index()
        {
            return View(db.WorkProjects.ToList());
        }

        // GET: WorkProjects/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkProject workProject = db.WorkProjects.Find(id);
            if (workProject == null)
            {
                return HttpNotFound();
            }
            return View(workProject);
        }

        // GET: WorkProjects/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WorkProjects/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,ProdId,Version,ProjType,Description,CreatedBy,StartDate,CompletedDate,DueDate,LocalTime")] WorkProject workProject)
        {
            if (ModelState.IsValid)
            {
                db.WorkProjects.Add(workProject);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(workProject);
        }

        // GET: WorkProjects/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkProject workProject = db.WorkProjects.Find(id);
            if (workProject == null)
            {
                return HttpNotFound();
            }
            return View(workProject);
        }

        // POST: WorkProjects/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,ProdId,Version,ProjType,Description,CreatedBy,StartDate,CompletedDate,DueDate,LocalTime")] WorkProject workProject)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workProject).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(workProject);
        }

        // GET: WorkProjects/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkProject workProject = db.WorkProjects.Find(id);
            if (workProject == null)
            {
                return HttpNotFound();
            }
            return View(workProject);
        }

        // POST: WorkProjects/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            WorkProject workProject = db.WorkProjects.Find(id);
            db.WorkProjects.Remove(workProject);
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
