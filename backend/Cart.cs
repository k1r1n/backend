using System;
namespace backend
{
    public class Cart
    {
        public int Id { get; set; }
        public string Product_id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
    }
}
