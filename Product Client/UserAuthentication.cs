using Data_Access_Layer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Product_Client
{
    public enum AUTHSTATUS { NONE, OK, INVALID, FAILED }
    public static class ProductWebAPIClient
    {
        static public string baseWebAddress = "https://localhost:44344/";
        static public string ProductManagerToken = "";
        static public AUTHSTATUS ProductManagerStatus = AUTHSTATUS.NONE;
        //static public string IgdbUserToken = "PLACE YOUR PRODUCT MANAGER TOKEN HERE";


        // Login and return token
        static public bool login(string username, string password)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var content = new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("grant_type", "password"),
                        new KeyValuePair<string, string>("username", username),
                        new KeyValuePair<string, string>("password", password),
                    });
                var result = client.PostAsync(baseWebAddress + "Token", content).Result;
                try
                {
                    var resultContent = result.Content.ReadAsAsync<Token>(
                        new[] { new JsonMediaTypeFormatter() }
                        ).Result;
                    string ServerError = string.Empty;
                    if (!(String.IsNullOrEmpty(resultContent.AccessToken)))
                    {
                        Console.WriteLine(resultContent.AccessToken);
                        ProductManagerToken = resultContent.AccessToken;
                        ProductManagerStatus = AUTHSTATUS.OK;
                        return true;
                    }
                    else
                    {
                        ProductManagerToken = "Invalid Login";
                        ProductManagerStatus = AUTHSTATUS.INVALID;
                        Console.WriteLine("Invalid credentials");
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    ProductManagerStatus = AUTHSTATUS.FAILED;
                    ProductManagerToken = "Server Error -> " + ex.Message;
                    Console.WriteLine(ex.Message);
                    return false;
                }

            }
        }


        // Post a new Supplier and associated Product 

        static public T Post<T>(string EndPoint, T entity)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ProductManagerToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.PostAsJsonAsync(baseWebAddress + EndPoint, entity).Result;
                if (response.IsSuccessStatusCode)
                {
                    // Return the newly inserted Supplier or Product
                    return response.Content.ReadAsAsync<T>(
                    new[] { new JsonMediaTypeFormatter() }).Result;
                }
                
            }
            // return the original entity
            return entity;
        }

        static public bool DeleteSupplier(int id)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ProductManagerToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.DeleteAsync(baseWebAddress + "api/purchases/supplier/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    // Could return actual object instead of true or false
                    var resultContent = response.Content.ReadAsAsync<Supplier>(
                    new[] { new JsonMediaTypeFormatter() }).Result;
                    return true;
                }

                return false;
            }

        }

        static public List<ProductsSupplied> GetAllProductsForSupplierName(string supplierName)
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ProductManagerToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.GetAsync(baseWebAddress + "api/purchases/supplier/SupplierProducts/" + supplierName).Result;
                var resultContent = response.Content.ReadAsAsync<List<ProductsSupplied>>(
                    new[] { new JsonMediaTypeFormatter() }).Result;
                return resultContent;
            }

        }


        // List all products that need reordering
        static public List<Product> GetProductsForReorder()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ProductManagerToken);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync(baseWebAddress + "api/purchases/reorderlist").Result;
                var resultContent = response.Content.ReadAsAsync<List<Product>>(
                    new[] { new JsonMediaTypeFormatter() }).Result;
                return resultContent;
            }
        }


    }
}
