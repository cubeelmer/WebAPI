using Microsoft.EntityFrameworkCore;
using Smarticube.API.DemoService.Models;

namespace Smarticube.API.DemoService.Data
{
    public class ProductsDbContext : DbContext
    {
        public ProductsDbContext(DbContextOptions<ProductsDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<ProductItem>().HasKey(table => new {
                table.ProductId,
                table.ItemId
            });
        }
        //Dbset (.net framework core used as table in MSSQL Server)
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<Item> Items { get; set; }
    }
}
