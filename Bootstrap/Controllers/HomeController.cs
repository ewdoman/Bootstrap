using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Bootstrap.Models;
using Bootstrap.Helpers;
using System.Threading.Tasks;

namespace Bootstrap.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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
            //take companyname and loop through players 
            //List<string> gamertags = Quartermaster.Quartermaster.GetGamertagsForCompany(model.Name);
            List<MatchResultsModel> currentMatchResults = new List<MatchResultsModel>();

            //View Debugging functions
            //ViewBag.ProgList = gamertags;
            //ViewBag.Message = model.Name;

            ViewBag.GamertagLabel = "Player: " + model.Name; 

            /**
            /*    Search page main logic Begin
            **/

            //Async API call debug print statements
            /**
            //System.Diagnostics.Debug.WriteLine("Main starting call");
            //Task finalresults = Task.Run(() => HaloSharpHelper.printResults());
            //finalresults.Wait();
            //System.Diagnostics.Debug.WriteLine("MAIN: This doesn't take long in main");
            //System.Diagnostics.Debug.WriteLine("MAIN: Here's some more logic while we wait");
            //System.Diagnostics.Debug.WriteLine("MAIN: Things are happening on a different thread");
            //System.Diagnostics.Debug.WriteLine("MAIN: Going to sleep for 3 seconds");
            //System.Threading.Thread.Sleep(3000);
            //System.Diagnostics.Debug.WriteLine("MAIN: Woke back up");
            ////results.Wait();
            //System.Diagnostics.Debug.WriteLine("Only print this at the end");
            **/

            //Call HaloSharpHelper class to gather list of matchIds
            List<MatchResultsModel> finalresults = HaloSharpHelper.printResults(model.Name, currentMatchResults);

            //Write gamertag matchIds to debug console
            //foreach (var item in finalresults)
            //{
            //    System.Diagnostics.Debug.WriteLine(item);
            //}

            //Set ViewBag list to gamertag match history IDs
            ViewBag.ProgList = finalresults;

            /**
            /*    Search page main logic End
            **/

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