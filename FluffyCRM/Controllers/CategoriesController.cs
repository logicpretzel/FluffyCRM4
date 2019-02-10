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
    [Authorize]
    public class CategoriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Categories
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult Index()
        {
            ViewBag.Title = "Categories";
            return View(db.Categories.ToList());
        }

        // GET: Categories/Details/5
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult Details(int? id)
        {
            ViewBag.Title = "Categories";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // GET: Categories/Create
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult Create()
        {
            ViewBag.Title = "Add Categories";
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult Create([Bind(Include = "id,Name,Type,Description")] Category category)
        {
            ViewBag.Title = "Categories";
            DateTime dt = new DateTime();
            dt = DateTime.Now;

            category.delete_ind = 0;
            category.CreateDate = dt;
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(category);
        }

        // GET: Categories/Edit/5
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult Edit(int? id)
        {
            ViewBag.Title = "Edit - Categories";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult Edit([Bind(Include = "id,Name,Type,delete_ind,Description,CreateDate")] Category category)
        {
            ViewBag.Title = "Categories";
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult Delete(int? id)
        {
            ViewBag.Title = "Delete -Categories";
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Category category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult DeleteConfirmed(int id)
        {
            ViewBag.Title = "Categories";
            Category category = db.Categories.Find(id);
            db.Categories.Remove(category);
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
