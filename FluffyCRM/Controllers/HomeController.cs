using FluffyCRM.DAL;
using FluffyCRM.Properties;
using FluffyCRM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FluffyCRM.Controllers
{
    public class HomeController : Controller
    {
        DataRepository _rp = new DataRepository();
        Settings setting = new Settings();
        public ActionResult Index()
        {
            ViewBag.Title = setting.AppTitle;
            ViewBag.Publisher = setting.Publisher;
            return View();
        }
        public ActionResult NeedLogon()
        {
            return View();
        }
        public ActionResult DisplayStaffDashboard() {

            StaffDashBoard model = new StaffDashBoard();
            model = _rp.GetUserClientCounts();
            return View(model);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            ViewBag.Title = setting.AppTitle;
            ViewBag.Publisher = setting.Publisher;
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            ViewBag.Title = setting.AppTitle;
            ViewBag.Publisher = setting.Publisher;
            return View();
        }
    }
}