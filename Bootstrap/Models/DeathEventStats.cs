using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Bootstrap.Models
{
    public class DeathEventStats
    {
        public TimeSpan TimeSinceStart { set; get; }
        public string Victim { set; get; }
        public string Killer { set; get; }

        public DeathEventStats(TimeSpan TSS, string V, string K)
        {
            TimeSinceStart = TSS;
            Victim = V;
            Killer = K;
        }
    }
}