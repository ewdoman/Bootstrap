using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bootstrap.Models;

namespace Bootstrap.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //TO DO: Search Logic goes here

            //Search by gamertag

            //Print Txt from - form
            //ViewBag.Message = gamertag;

            return View(new ViewModel());
        }

        [HttpPost]
        public ActionResult Index(ViewModel model)
        {
            System.Diagnostics.Debug.WriteLine("Called overloaded method INDEX - {0} - Headered by [HttpPost], [ValidateAntiForgeryToken]", model.Name);
            return RedirectToAction("Results", model);
            //return View();
        }

        public ActionResult Results(ViewModel model)
        {
            //System.Diagnostics.Debug.WriteLine("Called overloaded method INDEX - {0} - Headered by [HttpPost], [ValidateAntiForgeryToken]", model.Name);

            //take companyname and loop through players 
            List<string> gamertags = Quartermaster.Quartermaster.GetGamertagsForCompany(model.Name);
            ViewBag.ProgList = gamertags;
            ViewBag.Message = "Company: " + model.Name;

            //foreach (string member in gamertags)
            //{

            //}

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}