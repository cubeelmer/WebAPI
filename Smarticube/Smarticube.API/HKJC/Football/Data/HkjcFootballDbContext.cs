using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Smarticube.API.HKJC.Football.Models;

namespace Smarticube.API.HKJC.Football.Data
{
    public partial class HkjcFootballDbContext : DbContext
    {
        public HkjcFootballDbContext()
        {
        }

        public HkjcFootballDbContext(DbContextOptions<HkjcFootballDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<HkjcDataPool> HkjcDataPools { get; set; } = null!;
        public virtual DbSet<HkjcDataPoolResult> HkjcDataPoolResults { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=sql7001.site4now.net;Initial Catalog=DB_A2AE4C_hkjc;User ID=DB_A2AE4C_hkjc_admin;Password=hkjc.2017");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HkjcDataPool>(entity =>
            {
                entity.HasKey(e => new { e.Weekday, e.Matchtype, e.Matchdate, e.Matchname });

                entity.Property(e => e.Awayteam).HasComputedColumnSql("(right([matchname],(len([matchname])-charindex(N' 對 ',[matchname]))-(2)))", false);

                entity.Property(e => e.Hometeam).HasComputedColumnSql("(left([matchname],charindex(N' 對 ',[matchname])-(1)))", false);

                entity.Property(e => e.Timestamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.WeekdaySeq).HasComputedColumnSql("(CONVERT([int],substring([weekday],charindex(' ',[weekday])+(1),len([weekday])-charindex(' ',[weekday]))))", false);
            });

            modelBuilder.Entity<HkjcDataPoolResult>(entity =>
            {
                entity.HasKey(e => new { e.Weekday, e.Matchname, e.Matchdt });

                entity.Property(e => e.Timestamp)
                    .IsRowVersion()
                    .IsConcurrencyToken();

                entity.Property(e => e.WeekdaySeq).HasComputedColumnSql("(CONVERT([int],substring([weekday],charindex(' ',[weekday])+(1),len([weekday])-charindex(' ',[weekday]))))", false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
