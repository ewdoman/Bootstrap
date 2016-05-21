namespace Bootstrap.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BattleModel : DbContext
    {
        public BattleModel()
            : base("name=BattleModel")
        {
        }

        public virtual DbSet<v_Battle> v_Battle { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<v_Battle>()
                .Property(e => e.matchID)
                .IsUnicode(false);

            modelBuilder.Entity<v_Battle>()
                .Property(e => e.gamevariant)
                .IsUnicode(false);

            modelBuilder.Entity<v_Battle>()
                .Property(e => e.mapname)
                .IsUnicode(false);

            modelBuilder.Entity<v_Battle>()
                .Property(e => e.mapvariant)
                .IsUnicode(false);

            modelBuilder.Entity<v_Battle>()
                .Property(e => e.b_company)
                .IsUnicode(false);

            modelBuilder.Entity<v_Battle>()
                .Property(e => e.a_company)
                .IsUnicode(false);

            modelBuilder.Entity<v_Battle>()
                .Property(e => e.a_mvp)
                .IsUnicode(false);

            modelBuilder.Entity<v_Battle>()
                .Property(e => e.b_mvp)
                .IsUnicode(false);

            modelBuilder.Entity<v_Battle>()
                .Property(e => e.a_win)
                .IsFixedLength();

            modelBuilder.Entity<v_Battle>()
                .Property(e => e.b_win)
                .IsFixedLength();
        }
    }
}
