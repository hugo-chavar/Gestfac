using Gestfac.DTOs;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Gestfac.DbContexts
{
    public class GestfacDbContext : DbContext
    {
        public GestfacDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ProductDTO> Products { get; set; }
        public DbSet<PriceUpdateDTO> PriceUpdates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductDTO>().ToTable("Products");
            modelBuilder.Entity<PriceUpdateDTO>().ToTable("PriceUpdates");
            modelBuilder.Entity<ProductDTO>()
                        .HasMany(p => p.PriceUpdates)
                        .WithOne(pu => pu.Product)
                        .HasForeignKey(pu => pu.ProductId)
                        .HasConstraintName("FK_PriceUpdates_Products_ProductId");

            modelBuilder.Entity<ProductDTO>()
                        .HasOne(p => p.CurrentPriceUpdate)
                        .WithMany()
                        .HasForeignKey(p => p.CurrentPriceUpdateId)
                        .HasConstraintName("FK_Products_PriceUpdates_CurrentPriceUpdateId");

            modelBuilder.Entity<ProductDTO>()
                        .HasIndex(u => u.ExternalId)
                        .IsUnique();


            base.OnModelCreating(modelBuilder);
        }
    }
}
