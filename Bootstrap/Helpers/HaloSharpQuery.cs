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
using HaloSharp.Query.Stats.Lifetime;
using HaloSharp.Query.Stats.CarnageReport;
using HaloSharp.Validation;

namespace Bootstrap.Helpers
{
    public class HaloSharpQuery
    {
        //Set up intial parameters used in both all functions
        private static List<PlayerStats> matchPlayerStats { set; get; }
        private static List<DeathEventStats> matchDeathEventStats { set; get; }

        //TODO:Encrypt this so others wont mess with it
        private static string developerKey = "bddabd5d05f54eb0993eddfdda59b8ac";

        //Some setup for the HaloSharp client...
        private static Product developerAccessProduct = new Product
        {
            SubscriptionKey = developerKey,
            RateLimit = new RateLimit
            {
                RequestCount = 1, //number of requests in timespan
                TimeSpan = new TimeSpan(0, 0, 0, 1),
                Timeout = new TimeSpan(0, 0, 10, 0)
            }
        };

        private static CacheSettings cacheSettings = new CacheSettings
        {
            MetadataCacheDuration = new TimeSpan(0, 0, 10, 0),         //10 minute cache duration on Metadata.
            ProfileCacheDuration = new TimeSpan(0, 0, 10, 0)       //10 minute cache duration on profiles.
        };

        //Get match Player stats

        public static List<PlayerStats>HaloApiGetPlayerStats(string stringMatchID)
        {
            Task asyncResults = Task.Run(() => PlayerStatQueryRunAsync(stringMatchID));
            asyncResults.Wait();
            return matchPlayerStats;
        }

        static async Task<List<PlayerStats>> PlayerStatQueryRunAsync(string stringMatchID)
        {
            Guid matchGuid = Guid.Parse(stringMatchID);
            matchPlayerStats = new List<PlayerStats>();

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

        //Get match Death Events stats

        public static List<DeathEventStats> HaloApiGetDeathEventStats(string stringMatchID)
        {
            Task asyncResults = Task.Run(() => DeathStatQueryRunAsync(stringMatchID));
            asyncResults.Wait();
            return matchDeathEventStats;
        }

        static async Task<List<DeathEventStats>> DeathStatQueryRunAsync(string stringMatchID)
        {
            Guid matchGuid = Guid.Parse(stringMatchID);
            matchDeathEventStats = new List<DeathEventStats>();

            var client = new HaloClient(developerAccessProduct, cacheSettings);

            using (var session = client.StartSession())
            {

                //TODO: Create ifelse structure for the various gamemodes
                //Currently only supports arena

                var query = new GetMatchEvents()
                    .ForMatchId(matchGuid);

                var matchCarnage = await session.Query(query);

                foreach (var result in matchCarnage.GameEvents)
                {
                    matchDeathEventStats.Add(new DeathEventStats(
                        result.TimeSinceStart,
                        "Victim",
                        "Killer"));
                }

            }

            return matchDeathEventStats;
        }

     }

}