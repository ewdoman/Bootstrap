using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Bootstrap.Models
{
    public class Clan
    {
        public int ID { get; set; }
        public string Gamertag { get; set; }
        public string Company { get; set; }
    }

    public class ClanDBContext : DbContext
    {
        public DbSet<Clan> Clans { get; set; }
    }

}