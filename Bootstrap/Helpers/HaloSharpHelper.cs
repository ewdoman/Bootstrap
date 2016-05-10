using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static List<string> printResults()
        {
            //List<string> results = new List<string>();
            System.Diagnostics.Debug.WriteLine("printResults starting call");
            Task otherresults = Task.Run(() => RunAsync());
            otherresults.Wait();

            //foreach (string id in results)
            //{
            //    //Console.WriteLine(id);
            //}

            return matchIds;

        }

        static async Task<List<string>> RunAsync()
        {
            //Some "magic numbers..."
            string myGamertag = "Sn1p3r C";
            string spartanCompanyName = "UsingCheatCodes"; //"UsingCheatCodes";
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
                    RequestCount = 10,
                    TimeSpan = new TimeSpan(0, 0, 0, 10),
                    Timeout = new TimeSpan(1, 0, 0)
                }
            };

            var cacheSettings = new CacheSettings
            {
                MetadataCacheDuration = new TimeSpan(0, 0, 10, 0),         //10 minute cache duration on Metadata.
                ProfileCacheDuration = new TimeSpan(0, 0, 10, 0),       //10 minute cache duration on profiles.
            };

            //Actually running the client;
            System.Diagnostics.Debug.WriteLine("Before: HaloCilent");
            var client = new HaloClient(developerAccessProduct, cacheSettings);
            System.Diagnostics.Debug.WriteLine("Before: StartSession");
            using (var session = client.StartSession())
            {

                System.Diagnostics.Debug.WriteLine("before: foreach member in gamertags");
                foreach (string member in gamertags)
                {
                    System.Diagnostics.Debug.WriteLine("");
                    System.Diagnostics.Debug.WriteLine(member);
                    System.Diagnostics.Debug.WriteLine("");

                    var query = new GetMatches()                //HaloSharpt.Query.Stats.GetMatches()
                    .InGameMode(Enumeration.GameMode.Arena)
                    .ForPlayer(member);

                        var matchSet = await session.Query(query);
                        //System.Diagnostics.Debug.WriteLine("After: await");

                    foreach (var result in matchSet.Results)
                    {
                        System.Diagnostics.Debug.WriteLine(result.Id.MatchId.ToString());
                        matchIds.Add(result.Id.MatchId.ToString());
                    }
                    System.Diagnostics.Debug.WriteLine("");
                    System.Diagnostics.Debug.WriteLine("After: FINAL foreach");
                    System.Diagnostics.Debug.WriteLine("");
                }

            }
            System.Diagnostics.Debug.WriteLine("Before: return");
            return matchIds;
        }


    }
}      
             
//try{

//}
//   catch (HaloSharp.Exception.HaloApiException e)
//{
//    System.Diagnostics.Debug.WriteLine("Error EXCEPTION");
//}