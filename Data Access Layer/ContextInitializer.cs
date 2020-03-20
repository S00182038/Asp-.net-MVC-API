using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer
{
    public class ContextInitializer : DropCreateDatabaseIfModelChanges<ProductDbContext>
    {
        protected override void Seed(ProductDbContext context)
        {
           // Craete two suppliers with associated Products
            context.Suppliers.AddOrUpdate(new Supplier[] 
            { new Supplier {
                Name = "Bob",
                Address = "Anderson",
                 ProductsSupplied = new Product[] {
                     new Product { Description = "Bob's Bolts", Price = 0.25f, Quantity = 100, ReorderLevel = 50 },
                     new Product { Description = "Bob's Nuts", Price = 0.15f, Quantity = 200, ReorderLevel = 100 },
                 } },

                new Supplier
                {
                    Name = "Bill",
                    Address = "Bloggs",
                    ProductsSupplied = new Product[] {
                     new Product { Description = "Bill's Bolts", Price = 0.20f, Quantity = 200, ReorderLevel = 50 },
                     new Product { Description = "Bill's Nuts", Price = 0.10f, Quantity = 100, ReorderLevel = 50 },
                     new Product { Description = "Bill's bit", Price = 0.20f, Quantity = 20, ReorderLevel = 50 },
                 },
                }
            });

            context.SaveChanges();
            

            base.Seed(context);
        }
    }
}
