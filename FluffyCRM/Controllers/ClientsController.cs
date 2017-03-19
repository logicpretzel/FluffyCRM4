using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FluffyCRM.Models;
using FluffyCRM.DAL;

namespace FluffyCRM.Controllers
{
    public class ClientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private DataRepository _repos = new DataRepository();
        // GET: Clients
        public async Task<ActionResult> Index()
        {
            return View(await db.Clients.ToListAsync());
        }

        // GET: Clients/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = await db.Clients.FindAsync(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // GET: Clients/Create
        public ActionResult Create()
        {
           
            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ClientId,CompanyName,Address1,Address2,City,State,Zip,Phone1,PhoneType1")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Clients/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = await db.Clients.FindAsync(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ClientId,CompanyName,Address1,Address2,City,State,Zip,Phone1,PhoneType1")] Client client)
        {
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Client client = await db.Clients.FindAsync(id);
            if (client == null)
            {
                return HttpNotFound();
            }
            return View(client);
        }

        // POST: Clients/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Client client = await db.Clients.FindAsync(id);
            db.Clients.Remove(client);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public ActionResult GetAddlPhones(int clientID)
        {
            var model = _repos.ClientPhonesList(clientID);
            // fill some data for your model here
            return PartialView("AddlPhones", model);
        }

        public ActionResult AddClientPhone(int clientID)
        {
            ContactPhone model = new ContactPhone();
            model.ParentId = clientID;
            model.ParentRecordType = FlParentRecType.Client;
            return View(model);
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddClientPhone([Bind(Include = "ParentId,Phone,PhoneType,Comment")] ContactPhone model)
        {
            model.ParentRecordType = FlParentRecType.Client;

            if (ModelState.IsValid)
            {
                db.ContactPhones.Add(model);
                 db.SaveChanges();
                return RedirectToAction("Details", new { id = model.ParentId });
            }

            return View(model);
        }

        public RedirectToRouteResult DeletePhone(int id, int clientId)
        {
           // Client client = await db.Clients.FindAsync(id);
            ContactPhone phone = db.ContactPhones.Find(id);
            if (ModelState.IsValid)
            {
                if (phone.ParentId == clientId)
                {
                    db.ContactPhones.Remove(phone);
                    db.SaveChanges();
                }
                return RedirectToAction("Details", new { id = clientId });
            }
            return RedirectToAction("Details", new { id = clientId });
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
