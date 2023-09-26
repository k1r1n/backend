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
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Stock stock)
        {
            using (MySqlConnection connect = new MySqlConnection("server=localhost;user=root;password=root1234;port=3306;database=mysql"))
            {
                connect.Open();
                MySqlCommand command = new MySqlCommand("update stock set stock = " + stock.Count + " where product_id = " + id, connect);
                command.ExecuteReader();
                connect.Close();
            }
        }
    }
}
