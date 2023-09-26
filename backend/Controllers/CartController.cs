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
                MySqlCommand command = new MySqlCommand("select * from cart inner join product on cart.product_id = product.id", connect);
                MySqlDataReader query = command.ExecuteReader();

                while (query.Read())
                {
                    Cart cart = new Cart();
                    cart.Id = Convert.ToInt32(query["id"]);
                    cart.Name = query["name"].ToString();
                    cart.Price = Convert.ToDouble(query["price"]);
                    cart.Count = Convert.ToInt32(query["count"]);
                    carts.Add(cart);
                }

                query.Close();
                connect.Close();
            }

            return carts;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        //PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] int value)
        {
            using (MySqlConnection connect = new MySqlConnection("server=localhost;user=root;password=root1234;port=3306;database=mysql"))
            {
                connect.Open();
                MySqlCommand command = new MySqlCommand("update cart set count = " + value + " where product_id = 3", connect);
                command.ExecuteReader();
                connect.Close();
            }
        }

        //[HttpPut("{id}")]
        //public void Put(int id)
        //{
        //    using (MySqlConnection connect = new MySqlConnection("server=localhost;user=root;password=root1234;port=3306;database=mysql"))
        //    {
        //        connect.Open();
        //        MySqlCommand command = new MySqlCommand("delete from cart", connect);

        //        command.ExecuteReader();
        //        connect.Close();
        //    }
        //}

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            List<Cart> carts = new List<Cart>();
            using (MySqlConnection connect = new MySqlConnection("server=localhost;user=root;password=root1234;port=3306;database=mysql"))
            {
                connect.Open();
                MySqlCommand command = new MySqlCommand("delete from cart", connect);
                command.ExecuteReader();
                connect.Close();
            }
        }
    }
}
