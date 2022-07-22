using Microsoft.EntityFrameworkCore;
using Smarticube.API.HKJC.Models;

namespace Smarticube.API.HKJC.Data
{
    public class HkjcDbContext : DbContext
    {
        public HkjcDbContext(DbContextOptions<HkjcDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<hkjcDataPool_result>().HasKey(table => new {
                table.weekday,
                table.matchname,
                table.matchdt
            });
        }
        //Dbset (.net framework core used as table in MSSQL Server)
        public DbSet<hkjcDataPool_result> hkjcDataPool_results { get; set; }
        //public DbSet<ProductItem> ProductItems { get; set; }
        //public DbSet<Item> Items { get; set; }
    }
}
