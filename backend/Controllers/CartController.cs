using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend.Controllers
{
    [Route("api/cart")]
    public class CartController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<Cart> Get()
        {
            List<Cart> carts = new List<Cart>();
            using (MySqlConnection connect = new MySqlConnection("server=localhost;user=root;password=root1234;port=3306;database=mysql"))
            {
                connect.Open();
                MySqlCommand command = new MySqlCommand("select * from cart inner join product on cart.product_id = product.id join stock on stock.id = cart.product_id", connect);
                MySqlDataReader query = command.ExecuteReader();

                while (query.Read())
                {
                    Cart cart = new Cart();
                    cart.Id = Convert.ToInt32(query["id"]);
                    cart.Name = query["name"].ToString();
                    cart.Price = Convert.ToDouble(query["price"]);
                    cart.Count = Convert.ToInt32(query["count"]);
                    cart.Product_id = query["product_id"].ToString();
                    cart.Stock = Convert.ToInt32(query["stock"]);
                    carts.Add(cart);
                }

                query.Close();
                connect.Close();
            }

            return carts;
        }


        // POST api/values
        [HttpPost]
        public void Post([FromBody] Cart cart)
        {
            using (MySqlConnection connect = new MySqlConnection("server=localhost;user=root;password=root1234;port=3306;database=mysql"))
            {
                connect.Open();
                MySqlCommand command = new MySqlCommand("insert into cart set count = " + cart.Count + ", product_id = " + cart.Id, connect);
                command.ExecuteReader();
                connect.Close();
            }
        }

        //PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] int value)
        {
            using (MySqlConnection connect = new MySqlConnection("server=localhost;user=root;password=root1234;port=3306;database=mysql"))
            {
                connect.Open();
                MySqlCommand command = new MySqlCommand("update cart set count = " + value + " where product_id = " + id, connect);
                command.ExecuteReader();
                connect.Close();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            List<Cart> carts = new List<Cart>();
            using (MySqlConnection connect = new MySqlConnection("server=localhost;user=root;password=root1234;port=3306;database=mysql"))
            {
                connect.Open();
                MySqlCommand command = new MySqlCommand("delete from cart where id = " + id, connect);
                command.ExecuteReader();
                connect.Close();
            }
        }
    }
}
