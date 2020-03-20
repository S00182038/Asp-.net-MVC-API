using Data_Access_Layer.Models;
using Product_Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // Part 1. Login and retrieve token
            ProductWebAPIClient.baseWebAddress = "https://localhost:44344/";
            bool logged = ProductWebAPIClient.login("fflynstone", "Flint$12345");
            if (logged)
            {
                // Part 2. Add a new supplier and associated product

                Console.WriteLine("Add a new supplier and associated product");
                Supplier newSupplier = new Supplier {
                    Name = "Fred",
                    Address = "Malblanc",
                    ProductsSupplied = new List<Product> { new Product {
                    Description = "Fred's flyers",
                     Quantity= 200,
                    ReorderLevel = 60,
                    Price = 0.10f
                } }
                };

                Supplier s = ProductWebAPIClient.Post("api/purchases/supplier/create", newSupplier);
                if (s.ID > 0)
                {
                    Console.WriteLine("New Supplier details added {0} : {1}", s.ID,s.Name);
                }
                else
                {
                    Console.WriteLine("Error adding new supplier details");
                }

                //Part 3.Delete a supplier but not associated account

                Console.WriteLine("Delete a supplier but not associated product");

                

                bool deleteResult = ProductWebAPIClient.DeleteSupplier(s.ID);

                if (deleteResult)
                {
                    Console.WriteLine("Supplier with id {0} deleted!", s.ID);
                }
                else
                {
                    Console.WriteLine("Error! Supplier not deleted!", s.ID);
                }


                // 4. Given a supplier name, return all products for that supplier

                Console.WriteLine("All Products for a named supplier ");
                List<ProductsSupplied> productsFromSupplier = ProductWebAPIClient.GetAllProductsForSupplierName("Bob");
                foreach (var item in productsFromSupplier)
                {
                    Console.WriteLine("\nDescription: {0} Price: {1} Quantity: {2} Reorder: Limit {3} \n", item.Description, item.Price, item.Quality, item.ReorderLevel);
                }

                // 5. List all products that need reordering

                Console.WriteLine("\nAll Products that need reordering");
                List<Product> products = ProductWebAPIClient.GetProductsForReorder();
                foreach (var item in products)
                {
                    Console.WriteLine("\nDescription: {0} Price: {1} Quantity: {2} Reorder: Limit {3} \n", item.Description, item.Price, item.Quantity, item.ReorderLevel);
                }
            }
            else
            {
                Console.WriteLine("Failed to acquire Token  ");
            }
            Console.ReadKey();
        }
    }
}
