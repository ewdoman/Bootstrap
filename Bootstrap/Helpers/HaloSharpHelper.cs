using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bootstrap.Models;

using HaloSharp;
using HaloSharp.Exception;
using HaloSharp.Extension;
using HaloSharp.Model;
using HaloSharp.Query.Stats;
using HaloSharp.Validation;

namespace Bootstrap.Helpers
{
    using Quartermaster;
    public class HaloSharpHelper
    {

        public static List<string> matchIds { set; get; }

        public static List<MatchResultsModel> printResults(string gamertagname, List<MatchResultsModel> currentMatchResults)
        {
            List<string> matchResultsClass = new List<string>();
            //System.Diagnostics.Debug.WriteLine("printResults starting call");
            Task asyncResults = Task.Run(() => RunAsync(gamertagname, currentMatchResults));
            asyncResults.Wait();
            return currentMatchResults;
        }

        static async Task<List<MatchResultsModel>> RunAsync(string gamertagname, List<MatchResultsModel> currentMatchResults)
        {
            //Some "magic numbers..."
            string myGamertag = "Sn1p3r C";
            string spartanCompanyName = "Test"; //"UsingCheatCodes";
            string developerKey = "bddabd5d05f54eb0993eddfdda59b8ac"; 
             matchIds = new List<string>();

            //Obtain gamertags from website.
            List<string> gamertags = Quartermaster.GetGamertagsForCompany(spartanCompanyName);

            //Some setup for the HaloSharp client...
            var developerAccessProduct = new Product
            {
                SubscriptionKey = developerKey,
                RateLimit = new RateLimit
                {
                    RequestCount = 1, //number of requests in timespan
                    TimeSpan = new TimeSpan(0, 0, 0, 1),
                    Timeout = new TimeSpan(0, 0, 10, 0)
                }
            };

            var cacheSettings = new CacheSettings
            {
                MetadataCacheDuration = new TimeSpan(0, 0, 10, 0),         //10 minute cache duration on Metadata.
                ProfileCacheDuration = new TimeSpan(0, 0, 10, 0)       //10 minute cache duration on profiles.
            };

            //Actually running the client;
            System.Diagnostics.Debug.WriteLine("Before: HaloCilent");
            var client = new HaloClient(developerAccessProduct, cacheSettings);
            System.Diagnostics.Debug.WriteLine("Before: StartSession");
            using (var session = client.StartSession())
            {

                System.Diagnostics.Debug.WriteLine("before: foreach member in gamertags");
               // foreach (string member in gamertags)
              //  {
                    /**System.Diagnostics.Debug.WriteLine("");
                    System.Diagnostics.Debug.WriteLine(member);
                    System.Diagnostics.Debug.WriteLine("");
                    **/

                    var query = new GetMatches() //HaloSharpt.Query.Stats.GetMatches()
                    .InGameMode(Enumeration.GameMode.Arena)
                    .ForPlayer(gamertagname)
                    .Take(5);

                    var matchSet = await session.Query(query);
                    //System.Diagnostics.Debug.WriteLine("After: await");

                    System.Diagnostics.Debug.WriteLine("");
                    System.Diagnostics.Debug.WriteLine("Look here for match Data!!!!!!!!!!!!!!!!!");
                    System.Diagnostics.Debug.WriteLine("");

                    //foreach (var result in matchSet.Results)
                    foreach (var result in matchSet.Results)
                    {
                        System.Diagnostics.Debug.WriteLine("******Match******");
                        System.Diagnostics.Debug.WriteLine("MatchID: " + result.Id.MatchId);
                        System.Diagnostics.Debug.WriteLine("GameMode: " + result.Id.GameMode);
                        System.Diagnostics.Debug.WriteLine("MapId: " + result.MapId);
                        System.Diagnostics.Debug.WriteLine("MapVariantId: " + result.MapVariant.ResourceId);
                        System.Diagnostics.Debug.WriteLine("GameVariant: " + result.GameVariant.ResourceId);
                        System.Diagnostics.Debug.WriteLine("MatchDuration: " + result.MatchDuration);
                        System.Diagnostics.Debug.WriteLine("MatchCompletedDate: " + result.MatchCompletedDate.ISO8601Date);

                        currentMatchResults.Add(new MatchResultsModel(result.MapId.ToString(), result.Id.GameMode.ToString(), result.MapVariant.ResourceId.ToString(), result.MatchCompletedDate.ISO8601Date)); 

                        System.Diagnostics.Debug.WriteLine("***Team***");
                        foreach (var team in result.Teams)
                        {
                            System.Diagnostics.Debug.WriteLine("TeamId: " + team.Id);
                            System.Diagnostics.Debug.WriteLine("TeamScore: " + team.Score);
                            System.Diagnostics.Debug.WriteLine("TeamRank: " + team.Rank);
                        }

                        foreach (var player in result.Players)
                        {
                            System.Diagnostics.Debug.WriteLine("Player: " + player.Identity.Gamertag); //this calls what i assume is the gamertag of the player? different from the official halo api
                            System.Diagnostics.Debug.WriteLine("TeamId: " + player.TeamId);
                            System.Diagnostics.Debug.WriteLine("TotalKills: " + player.TotalKills);
                            System.Diagnostics.Debug.WriteLine("TotalDeaths: " + player.TotalDeaths);
                        }

                    //System.Diagnostics.Debug.WriteLine("TeamsScore: " + result.Teams.); //Teams score are lists of ints
                    matchIds.Add(result.Id.MatchId.ToString());
                    }

                    System.Diagnostics.Debug.WriteLine("");
                    System.Diagnostics.Debug.WriteLine("End player match Data!!!!!!!!!!!!!!!!!");
                    System.Diagnostics.Debug.WriteLine("");

                /**System.Diagnostics.Debug.WriteLine("");
                System.Diagnostics.Debug.WriteLine("After: FINAL foreach");
                System.Diagnostics.Debug.WriteLine("");
                **/
                //  }

            }
            //System.Diagnostics.Debug.WriteLine("Before: return");
            return currentMatchResults;
        }


    }
}      
             
//try{

//}
//   catch (HaloSharp.Exception.HaloApiException e)
//{
//    System.Diagnostics.Debug.WriteLine("Error EXCEPTION");
//}