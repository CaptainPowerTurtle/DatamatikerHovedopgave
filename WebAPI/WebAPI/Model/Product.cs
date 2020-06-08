using System;

namespace WebAPI.Model
{
    public class Product
    {
        public Product(Guid id, string name, int price)
        {
            Id = id;
            Name = name;
            Price = price;
        }
        public Guid Id { get; set; }
        public String Name { get; set; }
        public int Price { get; set; }
    }
}