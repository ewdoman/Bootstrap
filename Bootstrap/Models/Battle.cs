//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bootstrap.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Battle
    {
        public int ID { get; set; }
        public string matchID { get; set; }
        public int gamemode { get; set; }
        public string gamevariant { get; set; }
        public string mapname { get; set; }
        public string mapvariant { get; set; }
        public System.DateTime matchdate { get; set; }
        public System.TimeSpan matchduration { get; set; }
        public string a_company { get; set; }
        public string b_company { get; set; }
        public int a_score { get; set; }
        public int b_score { get; set; }
        public string a_mvp { get; set; }
        public string b_mvp { get; set; }
        public byte[] a_win { get; set; }
        public byte[] b_win { get; set; }
    }
}