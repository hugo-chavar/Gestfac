using Gestfac.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Gestfac.DbContexts
{
    public class GestfacDbContext : DbContext
    {
        public GestfacDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ProductDTO> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductDTO>().ToTable("Products");
            modelBuilder.Entity<PriceUpdateDTO>().ToTable("PriceUpdates");
            modelBuilder.Entity<ProductDTO>()
                        .HasMany(p => p.PriceUpdates)
                        .WithOne(pu => pu.ProductDTO)
                        .HasConstraintName("FK_PriceUpdates_Products_ProductId");

            modelBuilder.Entity<ProductDTO>()
                        .HasIndex(u => u.ExternalId)
                        .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
