using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DAL
{
    public interface ISupplier : IRepository<Supplier>
    {
        List<ProductsSupplied> SupplierProducts(string supplierName);

        List<Supplier> getSupplierList();
    }
}
