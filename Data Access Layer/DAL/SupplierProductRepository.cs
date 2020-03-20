using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DAL
{
    public class SupplierProductRepository : IProductRepository, ISupplier, IDisposable
    {
        ProductDbContext context;

        public SupplierProductRepository()
        {
            this.context = new ProductDbContext();
        }

        public void Dispose()
        {
            context.Dispose();
        }
        
        public List<Product> GetReorderList()
        {
            return context.Products.Where(p => p.Quantity <= p.ReorderLevel).ToList();
        }

        public Product UpdateEntity(Product entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
            return entity;
        }
        public Supplier UpdateEntity(Supplier entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
            return entity;
        }
        public Product InsertEntity(Product entity)
        {
            context.Products.Add(entity);
            context.SaveChanges();
            return entity;
        }
        public Supplier InsertEntity(Supplier entity)
        {
            context.Suppliers.Add(entity);
            context.SaveChanges();
            return entity;
        }

        public List<ProductsSupplied> SupplierProducts(string supplierName)
        {
            Supplier supplier = context.Suppliers.Include( s=>s.ProductsSupplied).FirstOrDefault(s => s.Name == supplierName);

            List<ProductsSupplied> productsSupplied = new List<ProductsSupplied>();
            // Populate DTO Object with detached references
            foreach (var item in supplier.ProductsSupplied)
            {
                productsSupplied.Add(new ProductsSupplied
                {
                    ID = item.ID,
                    Description = item.Description,
                    Quality = item.Quantity,
                    ReorderLevel = item.ReorderLevel,
                    Price = item.Price,
                    SupplierID = supplier.ID
                });
            }
            return productsSupplied;
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public Product DeleteEntity(Product entity)
        {
            Product p = context.Products.Remove(entity);
            context.SaveChanges();
            return p;

        }

        public Supplier DeleteEntity(Supplier entity)
        {
            Supplier s = context.Suppliers.Include(p => p.ProductsSupplied).FirstOrDefault(sup => sup.ID == entity.ID);
            context.Suppliers.Remove(s);
            context.SaveChanges();
            return s;
        }

        public void getEntity(out Product entity, int id)
        {
            entity = context.Products.FirstOrDefault(pr => pr.ID == id);
        }

        public void getEntity(out Supplier entity, int id)
        {
            entity = context.Suppliers.FirstOrDefault(pr => pr.ID == id);
        }

        public List<Product> GetProducts()
        {
            return context.Products.ToList();
        }

        public Product OrderItem(int ProductID, int Quantity)
        {
            Product p = context.Products.Find(ProductID);
            if (p != null && p.Quantity - Quantity > 0)
                p.Quantity -= Quantity;
            context.Entry(p).State = EntityState.Modified;
            context.SaveChanges();
            return p;

        }
        public List<Supplier> getSupplierList()
        {
            return context.Suppliers.ToList();
        }
    }
}
