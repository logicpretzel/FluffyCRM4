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
using Microsoft.AspNet.Identity;

namespace FluffyCRM.Controllers
{
    [Authorize]
    public class TicketCommentsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private DataRepository _repos = new DataRepository();


        // GET: TicketComments
        [Authorize(Roles = "Admin,Staff,Client")]
        public ActionResult Index(int? id)
        {
            int ID = id == null ? 0 : (int)id;
            var lst = _repos.TicketCommentListing(ID, "", "");
            return View(lst);
        }

        // GET: TicketComments/Details/5
        [Authorize(Roles = "Admin,Staff,Client")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Tickets", null);
            }
            TicketComment ticketComment = db.TicketComments.Find(id);
            if (ticketComment == null)
            {
                return HttpNotFound();
            }
            return View(ticketComment);
        }

        // GET: TicketComments/Create
        [Authorize(Roles = "Admin,Staff,Client")]
        public ActionResult Create(int? id)
        {

            int _id = id == null ? 0 : (int)id;

            if (_id == 0)   return RedirectToAction("Index", "Tickets", null);

            var model = new TicketComment()
            {
                CategoryId = 0,
                TicketId = _id
            };


            if (Request.IsAjaxRequest() == true)
            {
                return PartialView(model);
            }
            else
            {
                return View(model);
            }
        }

        // POST: TicketComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Staff,Client")]
        public ActionResult Create([Bind(Include = "Id,TicketId,Subject,Description")] TicketComment ticketComment)
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            //,CreateDate,Status,LocalTime,DeleteInd,CreatedBy

            ticketComment.LocalTime = dt;
            ticketComment.CreateDate = dt;
            ticketComment.CreatedBy = User.Identity.GetUserId();
            ticketComment.Status = CommentStatus.New;
            ticketComment.CategoryId = 0; 
            if (ModelState.IsValid)
            {
                db.TicketComments.Add(ticketComment);
                db.SaveChanges();
                return RedirectToAction("Details","Tickets", new { id = ticketComment.TicketId } );
            }

            return View(ticketComment);
        }

        // GET: TicketComments/Create
        [Authorize(Roles = "Admin,Staff,Client")]
        public ActionResult NewComment(int? id)
        {
            int  _id = id == null ? 0 : (int)id;

            if (_id == 0)   return RedirectToAction("Index", "Tickets", null);

            var model = new TicketComment() {
                CategoryId = 0,
                TicketId = _id
            };
                

            if (Request.IsAjaxRequest() == true)
            {
                return PartialView(model);
            }
            else
            {
                return View(model);
            }
           
        }

        // POST: TicketComments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Staff,Client")]
        public ActionResult NewComment([Bind(Include = "Id,TicketId,Subject,Description")] TicketComment ticketComment)
        {
            DateTime dt = new DateTime();
            dt = DateTime.Now;
            //,CreateDate,Status,LocalTime,DeleteInd,CreatedBy

            ticketComment.LocalTime = dt;
            ticketComment.CreateDate = dt;
            ticketComment.CreatedBy = User.Identity.GetUserId();
            ticketComment.Status = CommentStatus.New;
            ticketComment.CategoryId = 0;
            if (ModelState.IsValid)
            {
                db.TicketComments.Add(ticketComment);
                db.SaveChanges();
               // return RedirectToAction("Index");
            }
            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            if (Request.IsAjaxRequest() == true)
            {
                return PartialView(ticketComment);
            }
            else
            {
                return View(ticketComment);
            }
           
        }
        // GET: TicketComments/Edit/5
        [Authorize(Roles = "Admin,Staff,Client")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Index", "Tickets",null);
            }
            var uid = User.Identity.GetUserId();

            TicketComment ticketComment = db.TicketComments.Find(id);

            if (uid == ticketComment.CreatedBy)
            {
                ViewBag.IsOwner = true;
            }
            else ViewBag.IsOwner = false;

            if (ticketComment == null)
            {
                return HttpNotFound();
            }
            return View(ticketComment);
        }

        // POST: TicketComments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Staff,Client")]
        public ActionResult Edit([Bind(Include = "Id,Subject,Description,Status,TicketId")] TicketComment ticketComment)
        {

  

            if (ModelState.IsValid)
            {
                db.Entry(ticketComment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Details", "Tickets", new { id = ticketComment.TicketId });
            }
            return View(ticketComment);
        }

        // GET: TicketComments/Delete/5
        [Authorize(Roles = "Admin,Staff,Client")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TicketComment ticketComment = db.TicketComments.Find(id);
            if (ticketComment == null)
            {
                return HttpNotFound();
            }
            return View(ticketComment);
        }

        // POST: TicketComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Staff,Client")]
        public ActionResult DeleteConfirmed(int id)
        {
            TicketComment ticketComment = db.TicketComments.Find(id);
            db.TicketComments.Remove(ticketComment);
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
