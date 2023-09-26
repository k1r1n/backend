using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend.Controllers
{
    [Route("api/product")]
    public class ProductController : Controller
    {
        // GET: api/values
        [HttpGet] 
        public IEnumerable<Product> Get()
        {
            List<Product> products = new List<Product>();
            using(MySqlConnection connect = new MySqlConnection("server=localhost;user=root;password=root1234;port=3306;database=mysql"))
            {
                connect.Open();
                MySqlCommand command = new MySqlCommand("select * from product inner join stock on product.id = stock.product_id", connect);
                MySqlDataReader query = command.ExecuteReader();

                while(query.Read())
                {
                    Product product = new Product();
                    product.Id = Convert.ToInt32(query["id"]);
                    product.Name = query["name"].ToString();
                    product.Price = Convert.ToDouble(query["price"]);
                    product.Stock = query["stock"].ToString();
                    products.Add(product);
                }

                query.Close();
                connect.Close();
            }

            return products;
        }

    }
}
