using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FluffyCRM.Models;
using FluffyCRM.DAL;

namespace FluffyCRM.Controllers
{
    public class TaskNotesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private DataRepository _repos = new DataRepository();

        // GET: TaskNotes
        public ActionResult Index(int? id)
        {
            var lst = _repos.TaskNoteListing(id ?? 0, "", "","");

            return View(lst);
        }

        // GET: TaskNotes
        public ActionResult _Notes(int? id, int? page)
        {
          
            var lst = _repos.TaskNoteListing(id ?? 0, "", "","");

            return View( lst);
        }

        // GET: TaskNotes/Delete/5
        [Route("TaskNotes/DeleteNote/{id ?}/{taskId ?}")]
        public ActionResult DeleteNote(int id =0, int taskId =0)
        {
            if (id == 0)
            {
                return RedirectToAction("Index");
            }
            if (taskId == 0)
            {
                return RedirectToAction("Index");
            }

            TaskNote taskNote = db.TaskNotes.Find(id);
            if (taskNote == null)
            {
                return RedirectToAction("Index");
            }

   
            db.TaskNotes.Remove(taskNote);
            db.SaveChanges();

         //   var lst = _repos.TaskNoteListing(taskId, "", "", "");

            return RedirectToAction( "Index", "JobTasks");
        }


        // GET: TaskNotes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskNote taskNote = db.TaskNotes.Find(id);
            if (taskNote == null)
            {
                return HttpNotFound();
            }
            return View(taskNote);
        }

        // GET: TaskNotes/Create
        public ActionResult Create(int? id)
        {
            TaskNote model = new TaskNote();
            ViewBag.CustList = new SelectList(_repos.GetClientListAll(), "ClientId", "CompanyName", null);
            model.JobTask_Id = id ?? 0;
            return View(model);
        }

        // POST: TaskNotes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CategoryId,Subject,Comment,JobTask_Id,CreatedBy,CreateDate,Status,DeleteInd,ClientId,StartDate,CompletedDate,DueDate,LocalTime")] TaskNote taskNote)
        {
            ViewBag.CustList = new SelectList(_repos.GetClientListAll(), "ClientId", "CompanyName", null);
            if (ModelState.IsValid)
            {
                db.TaskNotes.Add(taskNote);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(taskNote);
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
        public void CreateNote([Bind(Include = "Id,CategoryId,Subject,Comment,JobTask_Id,CreatedBy,CreateDate,Status,DeleteInd,ClientId,StartDate,CompletedDate,DueDate,LocalTime")] TaskNote taskNote)
        {
            ViewBag.CustList = new SelectList(_repos.GetClientListAll(), "ClientId", "CompanyName", null);
            ViewBag.TaskId = taskNote.JobTask_Id;
            if (ModelState.IsValid)
            {
                db.TaskNotes.Add(taskNote);
                db.SaveChanges();
               // return PartialView(taskNote);

            }
           // return PartialView(taskNote);




        }

        // GET: TaskNotes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.CustList = new SelectList(_repos.GetClientListAll(), "ClientId", "CompanyName", null);
            TaskNote taskNote = db.TaskNotes.Find(id);
            if (taskNote == null)
            {
                return HttpNotFound();
            }
            return View(taskNote);
        }

        // POST: TaskNotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CategoryId,Subject,Comment,JobTask_Id,CreatedBy,CreateDate,Status,DeleteInd,ClientId,StartDate,CompletedDate,DueDate,LocalTime")] TaskNote taskNote)
        {
            ViewBag.CustList = new SelectList(_repos.GetClientListAll(), "ClientId", "CompanyName", null);
            if (ModelState.IsValid)
            {
                db.Entry(taskNote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(taskNote);
        }

        // GET: TaskNotes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TaskNote taskNote = db.TaskNotes.Find(id);
            if (taskNote == null)
            {
                return HttpNotFound();
            }
            return View(taskNote);
        }

        // POST: TaskNotes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TaskNote taskNote = db.TaskNotes.Find(id);
            db.TaskNotes.Remove(taskNote);
            db.SaveChanges();
            return RedirectToAction("Index");
        }




        // GET: TaskNotes/Edit/5
        public ActionResult EditNote(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ViewBag.CustList = new SelectList(_repos.GetClientListAll(), "ClientId", "CompanyName", null);
            TaskNote taskNote = db.TaskNotes.Find(id);
            if (taskNote == null)
            {
                return HttpNotFound();
            }
            return View(taskNote);
        }

        // POST: TaskNotes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditNote([Bind(Include = "Id,CategoryId,Subject,Comment,JobTask_Id,CreatedBy,CreateDate,Status,DeleteInd,ClientId,StartDate,CompletedDate,DueDate,LocalTime")] TaskNote taskNote)
        {
            ViewBag.CustList = new SelectList(_repos.GetClientListAll(), "ClientId", "CompanyName", null);
            if (ModelState.IsValid)
            {
                db.Entry(taskNote).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index","JobTasks");
            }
            return View(taskNote);
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
