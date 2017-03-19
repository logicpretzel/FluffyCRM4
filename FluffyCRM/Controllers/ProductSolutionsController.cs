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
    public class ProductSolutionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ProductSolutions
        public ActionResult Index()
        {
            return View(db.ProductSolutions.ToList());
        }

        // GET: ProductSolutions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSolution productSolution = db.ProductSolutions.Find(id);
            if (productSolution == null)
            {
                return HttpNotFound();
            }
            return View(productSolution);
        }

        // GET: ProductSolutions/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ProductSolutions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Description,CreateDate,CurrentVersion")] ProductSolution productSolution)
        {
            if (ModelState.IsValid)
            {
                db.ProductSolutions.Add(productSolution);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(productSolution);
        }

        // GET: ProductSolutions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSolution productSolution = db.ProductSolutions.Find(id);
            if (productSolution == null)
            {
                return HttpNotFound();
            }
            return View(productSolution);
        }

        // POST: ProductSolutions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Description,CreateDate,CurrentVersion")] ProductSolution productSolution)
        {
            if (ModelState.IsValid)
            {
                db.Entry(productSolution).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(productSolution);
        }

        // GET: ProductSolutions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ProductSolution productSolution = db.ProductSolutions.Find(id);
            if (productSolution == null)
            {
                return HttpNotFound();
            }
            return View(productSolution);
        }

        // POST: ProductSolutions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ProductSolution productSolution = db.ProductSolutions.Find(id);
            db.ProductSolutions.Remove(productSolution);
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
