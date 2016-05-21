namespace Bootstrap.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class v_Battle
    {
        [Key]
        [Column(Order = 0)]
        public int ID { get; set; }

        [Key]
        [Column(Order = 1)]
        [StringLength(36)]
        public string matchID { get; set; }

        [Key]
        [Column(Order = 2)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int gamemode { get; set; }

        [Key]
        [Column(Order = 3)]
        [StringLength(32)]
        public string gamevariant { get; set; }

        [Key]
        [Column(Order = 4)]
        [StringLength(32)]
        public string mapname { get; set; }

        [Key]
        [Column(Order = 5)]
        [StringLength(32)]
        public string mapvariant { get; set; }

        [Key]
        [Column(Order = 6)]
        public DateTime matchdate { get; set; }

        [Key]
        [Column(Order = 7)]
        public TimeSpan matchduration { get; set; }

        [Key]
        [Column(Order = 8)]
        [StringLength(32)]
        public string b_company { get; set; }

        [Key]
        [Column(Order = 9)]
        [StringLength(32)]
        public string a_company { get; set; }

        [Key]
        [Column(Order = 10)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int a_score { get; set; }

        [Key]
        [Column(Order = 11)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int b_score { get; set; }

        [Key]
        [Column(Order = 12)]
        [StringLength(16)]
        public string a_mvp { get; set; }

        [Key]
        [Column(Order = 13)]
        [StringLength(16)]
        public string b_mvp { get; set; }

        [Key]
        [Column(Order = 14)]
        [MaxLength(50)]
        public byte[] a_win { get; set; }

        [Key]
        [Column(Order = 15)]
        [MaxLength(50)]
        public byte[] b_win { get; set; }
    }
}
