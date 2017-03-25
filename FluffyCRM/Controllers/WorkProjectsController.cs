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

namespace FluffyCRM.Controllers
{
    public class WorkProjectsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: WorkProjects
        //public ActionResult Index()
        //{
        //    return View(db.WorkProjects.ToList());
        //}

        public ActionResult Projects(string searchString, string sortOption, int page = 1)
        {
            int pageSize = 10;

           

            var lst = db.WorkProjects.AsQueryable();

            if (!String.IsNullOrEmpty(searchString))
            {
                lst = db.WorkProjects.Where(p => p.Name.ToLower().Contains(searchString));
            }

            ViewBag.CurrentFilter = searchString;
            if (sortOption == null) sortOption = "name_acs";
            ViewBag.sortOption = sortOption;
    

            if (!String.IsNullOrEmpty(searchString))
            {
                lst = lst.Where(s => s.Name.StartsWith(searchString) || s.Description.Contains(searchString));
            }
            switch (sortOption)
            {
                case "name_acs":
                    lst = lst.OrderBy(p => p.Name);
                    ViewBag.sortOption = "name_desc";
                    break;
                case "name_desc":
                    lst = lst.OrderByDescending(p => p.Name);
                    ViewBag.sortOption = "name_asc";
                    break;
               
                default:
                    lst = lst.OrderBy(p => p.LocalTime);
                    ViewBag.sortOption = "name_desc";
                    break;

            }

            
            


            return Request.IsAjaxRequest()
               ? (ActionResult)PartialView("_Projects", lst.ToPagedList(page, pageSize))
               : View(lst.ToPagedList(page, pageSize));


        }

        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)

        {
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

            var WorkProjects = from s in db.WorkProjects
                               select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                WorkProjects = WorkProjects.Where(s => s.Name.StartsWith(searchString) ||   s.Description.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    WorkProjects = WorkProjects.OrderByDescending(s => s.Name);
                    break;
 
                default:  // Name ascending 
                    WorkProjects = WorkProjects.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 10;
            int pageNumber = (page ?? 1);

                return View(WorkProjects.ToPagedList(pageNumber, pageSize));
            
            
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
