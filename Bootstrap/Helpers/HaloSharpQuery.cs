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
using HaloSharp.Query.Stats.CarnageReport;
using HaloSharp.Validation;

namespace Bootstrap.Helpers
{
    public class HaloSharpQuery
    {

        public static List<PlayerStats> matchPlayerStats { set; get; }

        public static List<PlayerStats>HaloApiGetPlayerStats(string stringMatchID)
        {
            Task asyncResults = Task.Run(() => RunAsync(stringMatchID));
            asyncResults.Wait();
            return matchPlayerStats;
        }

        static async Task<List<PlayerStats>> RunAsync(string stringMatchID)
        {
            string developerKey = "bddabd5d05f54eb0993eddfdda59b8ac";
            Guid matchGuid = Guid.Parse(stringMatchID);
            matchPlayerStats = new List<PlayerStats>();

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

            var client = new HaloClient(developerAccessProduct, cacheSettings);

            using (var session = client.StartSession())
            {

                //TODO: Create ifelse structure for the various gamemodes
                //Currently only supports arena

                var query = new GetArenaMatchDetails()
                    .ForMatchId(matchGuid);

                var matchCarnage = await session.Query(query);

                foreach (var result in matchCarnage.PlayerStats)
                {
                    matchPlayerStats.Add(new PlayerStats(
                        result.Player.Gamertag, 
                        result.TeamId, //TODO: double check this end point, may need a second for loop for matchCarnage.TeamStats.TeamId to fill this point
                        result.Rank, 
                        result.TotalKills, 
                        result.TotalAssists, 
                        result.TotalDeaths));
                }
            }

            return matchPlayerStats;
        }

     }

}