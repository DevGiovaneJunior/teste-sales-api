using Microsoft.EntityFrameworkCore;
using Teste.Sale.Ambev.Domain.Entities;

namespace Teste.Sale.Ambev.Infrastructure.Data
{
    public class SalesDbContext : DbContext
    {
        public SalesDbContext(DbContextOptions<SalesDbContext> options) : base(options) { }

        public DbSet<SaleEntity> Sales => Set<SaleEntity>();
        public DbSet<SaleItemEntity> Items => Set<SaleItemEntity>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SaleEntity>(entity =>
            {
                entity.HasKey(x => x.SaleId);
                entity.Property(x => x.CustomerName).IsRequired();
                entity.Property(x => x.BranchName).IsRequired();
            });

            modelBuilder.Entity<SaleItemEntity>(entity =>
            {
                entity.HasKey(x => new { x.SaleItemId, x.ProductId });

                entity.Property(x => x.ProductName).IsRequired();
                entity.Property(x => x.UnitPrice);
                entity.Property(x => x.Discount);
                entity.Property(x => x.Cancelled).IsRequired();
            });

        }
    }
}
