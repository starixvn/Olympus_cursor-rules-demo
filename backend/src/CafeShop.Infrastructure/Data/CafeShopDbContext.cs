using CafeShop.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace CafeShop.Infrastructure.Data
{
    public class CafeShopDbContext : DbContext
    {
        public CafeShopDbContext(DbContextOptions<CafeShopDbContext> options)
            : base(options)
        {
        }

        public DbSet<MenuItem> MenuItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MenuItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
                entity.Property(e => e.Price).HasPrecision(10, 2);
                entity.Property(e => e.Category).IsRequired().HasMaxLength(50);
                entity.Property(e => e.CreatedAt).IsRequired();
            });
        }
    }
} 