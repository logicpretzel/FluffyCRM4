using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using FluffyCRM.Models;
using PagedList;
/// <summary>
/// /test   
/// </summary>
namespace FluffyCRM.Controllers
{
    [Authorize]
    public class ZipCodesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ZipCodes
        //public async Task<ActionResult> Index()
        //{
        //    return View(await db.ZipCodes.ToListAsync());
        //}

        public ActionResult Index(string sortOrder, string currentFilter,  string searchString, int? page)

        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "city_desc" : "";
            ViewBag.ZipSortParm = sortOrder == "Zip" ? "Zip_desc" : "Zip";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var zipcodes = from s in db.ZipCodes
                           select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                zipcodes = zipcodes.Where(s => s.City.StartsWith(searchString)
                                       || s.StateAbbrev.Equals(searchString)
                                       || s.Zip.Equals(searchString));
            }
            switch (sortOrder)
            {
                case "city_desc":
                    zipcodes = zipcodes.OrderByDescending(s => s.City);
                    break;
                case "Zip":
                    zipcodes = zipcodes.OrderBy(s => s.Zip);
                    break;
                case "zip_desc":
                    zipcodes = zipcodes.OrderByDescending(s => s.Zip);
                    break;
                default:  // Name ascending 
                    zipcodes = zipcodes.OrderBy(s => s.City);
                    break;
            }

            int pageSize = 50;
            int pageNumber = (page ?? 1);
            return View(zipcodes.ToPagedList(pageNumber, pageSize));
        }




        // GET: ZipCodes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZipCode zipCode = await db.ZipCodes.FindAsync(id);
            if (zipCode == null)
            {
                return HttpNotFound();
            }
            return View(zipCode);
        }

        // GET: ZipCodes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ZipCodes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "ID,Zip,City,StateAbbrev,PostalOrder")] ZipCode zipCode)
        {
            if (ModelState.IsValid)
            {
                db.ZipCodes.Add(zipCode);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(zipCode);
        }

        // GET: ZipCodes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZipCode zipCode = await db.ZipCodes.FindAsync(id);
            if (zipCode == null)
            {
                return HttpNotFound();
            }
            return View(zipCode);
        }

        // POST: ZipCodes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "ID,Zip,City,StateAbbrev,PostalOrder")] ZipCode zipCode)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zipCode).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(zipCode);
        }

        // GET: ZipCodes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ZipCode zipCode = await db.ZipCodes.FindAsync(id);
            if (zipCode == null)
            {
                return HttpNotFound();
            }
            return View(zipCode);
        }

        // POST: ZipCodes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ZipCode zipCode = await db.ZipCodes.FindAsync(id);
            db.ZipCodes.Remove(zipCode);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
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

/*
 * REFERENCES
 * TODO: Pagination reference
 * https://docs.microsoft.com/en-us/aspnet/mvc/overview/getting-started/getting-started-with-ef-using-mvc/sorting-filtering-and-paging-with-the-entity-framework-in-an-asp-net-mvc-application
 * 
 * 
 * 
 */
