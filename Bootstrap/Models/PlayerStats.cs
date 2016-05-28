using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bootstrap.Models
{
    public class PlayerStats
    { 
        public string playerName { set; get; }
        public int TeamID { set; get; }
        public int playerRank { set; get; }
        public int playerKills { set; get; }
        public int playerAssists { set; get; }
        public int playerDeaths { set; get; }

        //construct must be called with parameters for now
        public PlayerStats(string pN, int tID, int pR, int pK, int pA, int pD)
        {
            playerName = pN;
            TeamID = tID;
            playerRank = pR;
            playerKills = pK;
            playerAssists = pA;
            playerDeaths = pD;
        }
    }
}