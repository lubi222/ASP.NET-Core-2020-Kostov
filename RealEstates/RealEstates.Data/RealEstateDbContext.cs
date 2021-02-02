using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using RealEstates.Models;

namespace RealEstates.Data
{
    public class RealEstateDbContext : DbContext
    {
        public RealEstateDbContext()
        {
            
        }

        public RealEstateDbContext(DbContextOptions options)
            : base(options)
        {
            
        }
        public DbSet<RealEstateProperty> RealEstateProperties { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<BuildingType> BuildingTypes { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Tag> Tags { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=.;Database=RealEstates;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<District>()
                .HasMany(d => d.Properties)
                .WithOne(p => p.District)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<RealEstatePropertyTag>()
                .HasKey(x => new {x.PropertyId, x.TagId});
        }
    }
}
