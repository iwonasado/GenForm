﻿using System.Web.Mvc;

namespace Informedica.GenForm.Mvc3.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Title = "GenForm Acceptatie";

            return View();
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
