using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace backend.Controllers
{
    [Route("api/stock")]
    public class StockController : Controller
    {
        // GET: api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Stock stock)
        {
            //List<Stock> stocks = new List<Stock>();
            using (MySqlConnection connect = new MySqlConnection("server=localhost;user=root;password=root1234;port=3306;database=mysql"))
            {
                connect.Open();
                MySqlCommand command = new MySqlCommand("update stock set stock =" + stock.Count + " where product_id = " + id, connect);

                command.ExecuteReader();
                connect.Close();
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
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
