﻿using FluffyCRM.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FluffyCRM.Controllers
{
    public class HomeController : Controller
    {
        Settings setting = new Settings();
        public ActionResult Index()
        {
            ViewBag.Title = setting.AppTitle;
            ViewBag.Publisher = setting.Publisher;
            return View();
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