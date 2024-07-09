
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebCore.Domain.Catalog;
using WebCore.Models;

namespace WebCore.Infrastructures.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategoryMapping> ProductCategoryMappings { get; set; }
        public DbSet<ProductImageMapping> ProductImageMappings { get; set; }
        public DbSet<ProductManufacturerMapping> ProductManufacturerMappings { get; set; }
        public DbSet<ProductSpecificationMapping> ProductSpecificationMappings { get; set; }
        public DbSet<Specification> Specifications { get; set; }

        public DbSet<ArticleCategory> ArticleCategory { get; set; }

        public DbSet<Articles> Articles { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>().ToTable("Category");
            builder.Entity<Image>().ToTable("Image");
            builder.Entity<Manufacturer>().ToTable("Manufacturer");
            builder.Entity<Product>().ToTable("Product");
            builder.Entity<ProductCategoryMapping>().ToTable("ProductCategoryMapping");
            builder.Entity<ProductImageMapping>().ToTable("ProductImageMapping");
            builder.Entity<ProductManufacturerMapping>().ToTable("ProductManufacturerMapping");
            builder.Entity<ProductSpecificationMapping>().ToTable("ProductSpecificationMapping");
            builder.Entity<Articles>().ToTable("Articles");
            builder.Entity<ArticleCategory>().ToTable("ArticleCategory");

            builder.Entity<Specification>().ToTable("Specification");


        }
    }
}
