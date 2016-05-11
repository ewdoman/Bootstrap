using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bootstrap.Models
{
    public class MatchResultsModel
    {
        public Func<string> toString1;
        public Func<string> toString2;
        public Func<string> toString3;
        public DateTime iSO8601Date;

        public string Map { set; get; }
        public string GameMode { set; get; }
        public string GameVariant { set; get; }
        public DateTime MatchDate { set; get; }
        // public string WinningTeam { set; get; }
        // public int Score { set; get; }

        //construct must be called with parameters for now
        public MatchResultsModel(string m, string gm, string gv, DateTime md)
        {
            Map = m;
            GameMode = gm;
            GameVariant = gv;
            MatchDate = md;
        }

        //public MatchResultsModel(Func<string> toString1, Func<string> toString2, Func<string> toString3, DateTime iSO8601Date)
        //{
        //    this.toString1 = toString1;
        //    this.toString2 = toString2;
        //    this.toString3 = toString3;
        //    this.iSO8601Date = iSO8601Date;
        //}
    }    

}