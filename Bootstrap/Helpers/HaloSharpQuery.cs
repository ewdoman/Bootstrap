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
using HaloSharp.Query.Metadata;
using HaloSharp.Validation;

namespace Bootstrap.Helpers
{
    public class HaloSharpQuery
    {
        //Set up intial parameters used in both all functions
        private static List<PlayerStats> matchPlayerStats { set; get; }
        private static List<DeathEventStats> matchDeathEventStats { set; get; }
        private static List<string> teamColors { set; get; }

        //TODO:Encrypt this so others wont mess with it
        private static string developerKey = "5f97d87729cb4c7bbf44687e9b9267f4";

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
                        result.EventName.ToString(),
                        "Killer"));
                }

            }

            return matchDeathEventStats;
        }

        //Get Team color info

        public static List<string> HaloApiGetTeamColors()
        {
            Task asyncResults = Task.Run(() => TeamColorQueryRunAsync());
            asyncResults.Wait();
            return teamColors;
        }

        static async Task<List<string>> TeamColorQueryRunAsync()
        {
            teamColors = new List<string>();

            var client = new HaloClient(developerAccessProduct, cacheSettings);

            using (var session = client.StartSession())
            {

                var query = new GetTeamColors();

                var matchCarnage = await session.Query(query);

                foreach (var result in matchCarnage)
                {
                   teamColors.Add(result.Name);
                    System.Diagnostics.Debug.WriteLine(result.Id + " " + result.Name);
                }

            }

            return teamColors;
        }


    }

}