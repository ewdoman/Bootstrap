using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bootstrap.Models
{
    public class MatchResultsModel
    {
        public string MatchId { set; get; }
        public string Map { set; get; }
        public string GameMode { set; get; }
        public string GameVariant { set; get; }
        public DateTime MatchDate { set; get; }
        // public string WinningTeam { set; get; }
        // public int Score { set; get; }

        //construct must be called with parameters for now
        public MatchResultsModel(string mid, string m, string gm, string gv, DateTime md)
        {
            MatchId = mid;
            Map = m;
            GameMode = gm;
            GameVariant = gv;
            MatchDate = md;
        }

    }    

}