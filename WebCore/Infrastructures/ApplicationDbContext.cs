
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
        

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Data Source=14.162.144.237;Database=hoangminh_db;User Id=sa;Password=aDBtbjR5dHIwaWQzcA");
        //    base.OnConfiguring(optionsBuilder);
        //}
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);


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
