using Data_Access_Layer;
using Data_Access_Layer.DAL;
using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Product_Server.Controllers
{
    [Authorize(Roles = "Purchases Manager")]
    [RoutePrefix("api/purchases")]
    public class PurchasesController : ApiController
    {
        SupplierProductRepository repository;
        public PurchasesController()
        {
            this.repository = new SupplierProductRepository();
        }

        // Get api/purchases/products
        [HttpGet]
        [Route("products")]
        public List<Product> GetProducts()
        {
            return repository.GetProducts();
            
        }

        [HttpGet]
        [Route("suppliers")]
        public List<Supplier> GetSuppliers()
        {

            return repository.getSupplierList();
        }

        [HttpGet]
        [Route("product/{id:int}")]
        public Product GetProduct(int id)
        {
            Product p;
            repository.getEntity(out p,id);
            return p;
        }

        [HttpGet]
        [Route("supplier/{id:int}")]
        public Supplier GetSupplier(int id)
        {
            Supplier s;
            repository.getEntity(out s, id);
            return s;
        }

        [HttpGet]
        [Route("product/update")]
        public Product  UpdateProduct(Product p)
        {
            return repository.UpdateEntity(p);
        }

        [HttpGet]
        [Route("supplier/edit/{id:int}")]
        public Supplier UpdateSupplier(int id)
        {

            Supplier supplier;
            repository.getEntity(out supplier,id);
            return repository.UpdateEntity(supplier);
        }

        //[HttpPost]
        //[Route("product/create/{id:int}")]
        //public bool InsertProduct(int id)
        //{
        //    IRepository<Supplier> getSupplier = repository;
        //    Supplier supplier = getSupplier.getEntity(id);
        //    return getSupplier.UpdateEntity(supplier);
        //}

        [HttpPost]
        [Route("supplier/create")]
        public Supplier InsertSupplier(Supplier model)
        {
            //Supplier supplier = new Supplier {Name = model.Name, Address = model.Address };
            
            //repository.Save();
            //if (model.ProductsSupplied.Count > 0)
            //{
            //    foreach (var item in model.ProductsSupplied)
            //    {
            //        repository.InsertEntity(new Product
            //        {
            //            Description = item.Description,
            //            Quantity = item.Quantity,
            //            ReorderLevel = item.ReorderLevel,
            //            Price = item.Price,
                        
            //        });
            //    }
            //    repository.Save();
            //}
            return repository.InsertEntity(model);
        }

        [HttpDelete]
        [Route("supplier/{id:int}")]
        public Supplier DeleteSupplier(int id)
        {
            Supplier supplier = null;
            repository.getEntity(out supplier, id);
            return repository.DeleteEntity(supplier);
        }


        [HttpGet]
        [Route("reorderlist")]
        public List<Product> ReorderList()
        {
            return repository.GetReorderList();
        }

        [HttpGet]
        [Route("supplier/SupplierProducts/{supplierName}")]
        public List<ProductsSupplied> SupplierProducts(string supplierName)
        {
            return repository.SupplierProducts(supplierName);
        }

        [HttpPut]
        [Route("order/ProductId/{id:int}/QuanityOrdered/{qty:int}")]
        public Product ReorderQuantity(int id, int qty)
        {
            return repository.OrderItem(id, qty);
        }


    }
}
