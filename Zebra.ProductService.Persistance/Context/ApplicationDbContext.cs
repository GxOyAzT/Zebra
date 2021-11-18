using Microsoft.EntityFrameworkCore;
using Zebra.ProductService.Domain.Entities;
using Zebra.ProductService.Domain.Views;
using Zebra.ProductService.Persistance.Context.EntityTypeConfiguration;

namespace Zebra.ProductService.Persistance.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<ProductModel> Products { get; set; }
        public DbSet<PriceModel> Prices { get; set; }
        public DbSet<RatingModel> Ratings { get; set; }
        public DbSet<ProductPriceModel> ProductPriceView { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ProductPriceViewConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
