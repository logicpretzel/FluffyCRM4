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
using PagedList;

namespace FluffyCRM.Controllers
{
    [Authorize]
    public class ClientsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private DataRepository _repos = new DataRepository();

        // GET: Clients
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)

        {
            ViewBag.Title = "Client Listing";
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.ZipSortParm = sortOrder == "Zip" ? "Zip_desc" : "Zip";

            IQueryable<Client> lst;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;
            if (searchString == null) { searchString = ""; }
            if (searchString.Length > 0)
            {
                lst = db.Clients.Where(m => m.CompanyName.Contains(searchString));
            }
            else {
                lst = db.Clients.Take(1000);
            }

            switch (sortOrder)
            {
                case "name_desc":
                    lst = lst.OrderByDescending(s => s.CompanyName);
                    break;

                default:  // Name ascending 
                    lst = lst.OrderBy(s => s.CompanyName);
                    break;
            }

          //  var _clients = (IEnumerable<Client>)lst;

            int pageSize = 50;
            int pageNumber = (page ?? 1);

            if (Request.IsAjaxRequest() == true)
            {
                return PartialView(lst.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return View(lst.ToPagedList(pageNumber, pageSize));
            }
        }


        // GET: Clients/Create
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult Create()
        {
            ViewBag.Title = "Add Clients";

            return View();
        }

        // POST: Clients/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Staff")]
        public async Task<ActionResult> Create([Bind(Include = "ClientId,CompanyName,Address1,Address2,City,State,Zip,Phone1,PhoneType1")] Client client)
        {
            ViewBag.Title = "Clients";
            if (ModelState.IsValid)
            {
                db.Clients.Add(client);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(client);
        }

        // GET: Clients/Details/5
        [Authorize(Roles = "Admin,Staff")]
        public async Task<ActionResult> Details(int? id)
        {
            ViewBag.Title = "Clients";
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


        // GET: Clients/Edit/5
        [Authorize(Roles = "Admin,Staff")]
        public async Task<ActionResult> Edit(int? id)
        {
            ViewBag.Title = "Edit Clients";
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
        [Authorize(Roles = "Admin,Staff")]
        public async Task<ActionResult> Edit([Bind(Include = "ClientId,CompanyName,Address1,Address2,City,State,Zip,Phone1,PhoneType1")] Client client)
        {
            ViewBag.Title = "Clients";
            if (ModelState.IsValid)
            {
                db.Entry(client).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(client);
        }

        // GET: Clients/Delete/5
        [Authorize(Roles = "Admin,Staff")]
        public async Task<ActionResult> Delete(int? id)
        {
            ViewBag.Title = "Delete Clients";
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
        [Authorize(Roles = "Admin,Staff")]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ViewBag.Title = "Clients";
            Client client = await db.Clients.FindAsync(id);
            db.Clients.Remove(client);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public ActionResult GetAddlPhones(int clientID)
        {
            ViewBag.Title = "Clients - Additional Phone Numbers";
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
        [Authorize(Roles = "Admin,Staff")]
        public ActionResult AddClientPhone([Bind(Include = "ParentId,Phone,PhoneType,Comment")] ContactPhone model)
        {
            ViewBag.Title = "Clients - Add Phone";
            model.ParentRecordType = FlParentRecType.Client;

            if (ModelState.IsValid)
            {
                db.ContactPhones.Add(model);
                 db.SaveChanges();
                return RedirectToAction("Details", new { id = model.ParentId });
            }

            return View(model);
        }

        [Authorize(Roles = "Admin,Staff")]
        public RedirectToRouteResult DeletePhone(int id, int clientId)
        {
           // ViewBag.Title = "Delete Phone Number";
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
