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
        private Quartermaster.Models.Spartan spartandb = new Quartermaster.Models.Spartan();
        private BattleModel battlemodeldb = new BattleModel();

        public ActionResult Index()
        {
            return View(new ViewModel());
        }

        [HttpPost]
        public ActionResult Index(ViewModel model)
        {
            System.Diagnostics.Debug.WriteLine("Index Action Result Called", model.Gamertag);
            return RedirectToAction("Results", model);
            //return View();
        }

        public ActionResult Results(ViewModel model)
        {

            model.Company = Quartermaster.Quartermaster.GetSpartanCompanyFromGamertag(model.Gamertag);
            string[] gameModeNames = { "Error", "Arena", "Campaign", "Custom", "Warzone" };

            ViewBag.gamertagLabel = model.Gamertag;
            ViewBag.companyLabel = model.Company;

            ViewBag.gameModeNameArray = gameModeNames;

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

            //Create LINQ query
            var battleQry = from cb in battlemodeldb.v_Battle
                        where cb.a_company == model.Company || cb.b_company == model.Company
                        select cb;

            /**
            /*    Search page main logic End
            **/

            //Call HaloSharpHelper class to gather list of matchIds
            //List<MatchResultsModel> finalresults = HaloSharpHelper.printResults(model.Gamertag, currentMatchResults);

            //Set ViewBag list to gamertag match history IDs
            //ViewBag.ProgList = ClanBattleListHelper.GetListofClanBattlesFromCompany(model.Company);

            //return View(battlemodeldb.v_Battle.ToList());
            return View(battleQry);
        }

        public ActionResult MatchResults(string matchid)
        {
            return View(matchid);
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