using FluffyCRM.DAL;
using FluffyCRM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FluffyCRM.Controllers
{
    public class CardsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private CardsDAL dal = new CardsDAL();

        // GET: Cards
        public async Task<ActionResult> Index()
        {
            return View(await db.Cards.ToListAsync());
        }

        // GET: Cards/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = await db.Cards.FindAsync(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        //// GET: Cards/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Cards/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "Title,Detail,SprintId,BudgetHours")] CardAddViewModel card)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        return RedirectToAction("Index");
        //    }

        //    return View(card);
        //}



        // GET: Cards/Create
        public ActionResult AddCard()
        {
            return View();
        }

        // POST: Cards/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddCard([Bind(Include = "Title,Detail,SprintId,BudgetHours,ProjectId,ExpStartDt,ExpStopDt,CatId")] CardAddViewModel card)
        {
            int rc = 0;

            card.EntityId = 0;
            card.StatusId = 0;
            

            if (ModelState.IsValid)
            {
                rc = dal.AddCard(card);
                return RedirectToAction("Index");
            }

            return View(card);
        }



        // GET: Cards/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = await db.Cards.FindAsync(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        // POST: Cards/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,CreateDt,ProjectId,EntityId,Title,Detail,SprintId,HasChildren,HasPrereqs,ExpStartDt,ExpStopDt,StartDt,CompletedDt,BudgetHours,ActualHours,ModifyDt,ModifyBy,ParentId,CatId,BackgroundColor,StatusId,DeleteId,DeletedDt,ArchivedInd,ArchivedDt")] Card card)
        {
            if (ModelState.IsValid)
            {
                db.Entry(card).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(card);
        }

        // GET: Cards/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Card card = await db.Cards.FindAsync(id);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        // POST: Cards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Card card = await db.Cards.FindAsync(id);
            db.Cards.Remove(card);
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