using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class ProductDbContext : DbContext
    {
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }

        public ProductDbContext() : base("RAD302Week5")
        {
            Configuration.LazyLoadingEnabled = false;
            Database.SetInitializer<ProductDbContext>(new ContextInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<Supplier>()
            .HasMany(s => s.ProductsSupplied)
            .WithOptional(p => p.ProductSupplier)
            .WillCascadeOnDelete(false);
        }
    }
}
