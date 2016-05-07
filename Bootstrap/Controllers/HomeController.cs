using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bootstrap.Models;
using System.Threading.Tasks;

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
            System.Diagnostics.Debug.WriteLine("Index Action Result Called", model.Name);
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

            //****************************************

            System.Diagnostics.Debug.WriteLine("Main starting call");
            Task results = Task.Run(() => HaloSharpHelper.printResults());
            results.Wait();
            System.Diagnostics.Debug.WriteLine("MAIN: This doesn't take long in main");
            System.Diagnostics.Debug.WriteLine("MAIN: Here's some more logic while we wait");
            System.Diagnostics.Debug.WriteLine("MAIN: Things are happening on a different thread");
            System.Diagnostics.Debug.WriteLine("MAIN: Going to sleep for 3 seconds");
            System.Threading.Thread.Sleep(3000);
            System.Diagnostics.Debug.WriteLine("MAIN: Woke back up");
            //results.Wait();
            System.Diagnostics.Debug.WriteLine("Only print this at the end");

            //****************************************

            //foreach (string member in gamertags)
            //{
            //}

            return View();
        }

        //public ActionResult AwaitTest()
        //{
        //    Clan clan = new Clan();
        //    return View(clan);
        //}

        //[HttpPost]
        //public async Task<ActionResult> AwaitTest(Clan clan)
        //{
        //    bool saved = await SaveGameAsync(clan);
        //    return View(clan);
        //}

        //public async Task<bool> SaveGameAsync(Clan clan)
        //{
        //    using (var db = ApplicationDbContext.Create())
        //    {
        //        db.Games.Add(game);
        //        return await db.SaveChangesAsync() > 0;
        //    }
        //}


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