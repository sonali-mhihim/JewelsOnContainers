using Microsoft.EntityFrameworkCore;
using ProductCatalogAPI.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductCatalogAPI.Data
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions Options) : base(Options)
        {

        }
        public DbSet<CatalogType> CatalogTypes { get; set; }
        public DbSet<CatalogBrand> CatalogBrands { get; set; }
        public DbSet<CatalogItem> CatalogItems { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatalogBrand>(e =>
            {
                e.ToTable("CatalogBrands");
                e.Property(b => b.Id)
                .IsRequired()
                .UseHiLo("catalog_brand_hilo");

                e.Property(b => b.Brand)
                .IsRequired()
                .HasMaxLength(100);

            });
            modelBuilder.Entity<CatalogType>(e =>
            {
                e.ToTable("CatalogTypes");
                e.Property(t => t.Id)
                .IsRequired()
                .UseHiLo("catalog_type_hilo");

                e.Property(t => t.Type)
                .IsRequired()
                .HasMaxLength(100);
            });
            modelBuilder.Entity<CatalogItem>(e =>
            {
                e.ToTable("Catalog");
                e.Property(c => c.Id)
                .IsRequired()
                .UseHiLo("catalog_hilo");

                e.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

                e.Property(c => c.Price)
                .IsRequired();

                e.HasOne(c => c.CatalogType)
                .WithMany()
                .HasForeignKey(c => c.CatalogTypeId);

                e.HasOne(c => c.CatalogBrand)
                .WithMany()
                .HasForeignKey(c => c.CatalogBrandId);

            });
        }
    }
}
