using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SearchBoxSample.Models;

namespace SearchBoxSample.Controllers
{
    public class HomeController : Controller
    {

        private SampleEntities db = new SampleEntities();

        public ActionResult Index()
        {
            ViewBag.Message = "SearchBox.io Sample Application!";

            var documents = db.Documents;

            return View(documents.Take(3).ToList());
        }

        public ActionResult About()
        {
            return View();
        }
    }
}
