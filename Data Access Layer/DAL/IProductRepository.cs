using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DAL
{
    public interface IProductRepository : IRepository<Product>
    {
        List<Product> GetReorderList();

        List<Product> GetProducts();
        Product OrderItem(int ProductID, int Quantity);
    }
}
