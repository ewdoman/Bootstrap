using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Quartermaster;

namespace Bootstrap.Controllers
{
    public class HelloWorldController : Controller
    {
        public ActionResult Index(string companyname)
        {
            List<string> gamertags = Quartermaster.Quartermaster.GetGamertagsForCompany(companyname);
            ViewBag.ProgList = gamertags;
            ViewBag.Message = "Company: " + companyname;

            foreach (string member in gamertags)
            {
                System.Diagnostics.Debug.WriteLine(member);
            }

            return View();
        }

    }
}