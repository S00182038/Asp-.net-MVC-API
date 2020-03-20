using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class ProductModel
    {
        
        public string Description { get; set; }
        
        public int Quantity { get; set; }
        
        public int ReorderLevel { get; set; }
        
        public float Price { get; set; }
        
        public int SupplierID { get; set; }
    }

    public class SupplierModel
    {
        
        public string Name { get; set; }
        
        public string Address { get; set; }
        public List<ProductModel> ProductsSupplied { get; set; }
    }

    public class ProductsSupplied
    {
        public int ID { get; set; }
        public string Description { get; set; }
        public int Quality { get; set; }
        public int ReorderLevel { get; set; }
        public double Price { get; set; }
        public int SupplierID { get; set; }
    }
}
