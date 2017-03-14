﻿using System;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using FluffyCRM.Models;
using FluffyCRM.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Collections.Generic;
using FluffyCRM.DAL;

namespace FluffyCRM.Controllers
{
    [Authorize]
    public class TicketsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private DataRepository _repos = new DataRepository();

        // GET: Tickets
        [Authorize(Roles = "Admin,Client,Staff")]
        public ActionResult Index()
        {

            IEnumerable<TicketList> lst = new List<TicketList>();

            string userId = User.Identity.GetUserId().ToString();
            if (!(User.IsInRole("Staff") || User.IsInRole("Admin")))
            {
                lst = _repos.GetTicketList(userId);


            }
            else {
                lst = _repos.GetTicketList("");
            }
            


            return View(lst);
        }

        // GET: Tickets/Details/5
        [Authorize(Roles = "Admin,Client,Staff")]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = await db.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // GET: Tickets/Create
        [Authorize(Roles = "Admin,Client,Staff")]
        public ActionResult Create()
        {
            ViewBag.TicketCategories = new SelectList(_repos.GetCategoryList(FLCatType.Ticket), "id", "Name", null);
            ViewBag.CustList = new SelectList(_repos.GetClientListAll(), "ClientId", "CompanyName", null);
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Client,Staff")]
        public async Task<ActionResult> Create([Bind(Include = "Subject,CategoryId,Description,ClientId,DueDate,CreatedBy,Status,LocalTime")] Ticket ticket)
        {
            DateTime now = new DateTime();
            now = DateTime.Now;
            ticket.CreatedBy = User.Identity.GetUserId();
            ticket.Status = ticketStatus.NEW;
            ticket.LocalTime = now;
            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                db.Tickets.Add(ticket);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(ticket);
        }

        // GET: Tickets/Create
        [Authorize(Roles = "Admin,Client,Staff")]
        public ActionResult AddClientTicket()
        {
            return View();
        }

        // POST: Tickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Client,Staff")]
        public async Task<ActionResult> AddClientTicket([Bind(Include = "Subject,Description,ClientId")] ClientTicket clientTicket)
        {
            DateTime now = new DateTime();
            now = DateTime.Now;

            Ticket ticket = new Ticket();
            ticket.LocalTime = now;

            ticket.CategoryId = 0;

            ticket.Subject = clientTicket.Subject;
            ticket.Description = clientTicket.Description;


            ticket.Status = ticketStatus.NEW;
            ticket.CreatedBy = User.Identity.GetUserId();
        
            ticket.ClientId = _repos.GetClientByUID(ticket.CreatedBy);

            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            if (ModelState.IsValid)
            {
                db.Tickets.Add(ticket);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
           }

            return View(clientTicket);
        }

        // GET: Tickets/Edit/5
        [Authorize(Roles = "Admin,Client,Staff")]
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = await db.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Client,Staff")]
        public async Task<ActionResult> Edit([Bind(Include = "TicketId,Subject,CategoryId,CreateDate,Description,Status,DeleteInd,ClientId,StartDate,CompletedDate,DueDate")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                db.Entry(ticket).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(ticket);
        }

        // GET: Tickets/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ticket ticket = await db.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return HttpNotFound();
            }
            return View(ticket);
        }

        // POST: Tickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Ticket ticket = await db.Tickets.FindAsync(id);
            db.Tickets.Remove(ticket);
            await db.SaveChangesAsync();
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
