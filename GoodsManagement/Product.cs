using System;

namespace GoodsManagement
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public Category Category { get; set; }

        public Product(int id, string name, string brand, decimal price, int quantity, Category category)
        {
            Id = id;
            Name = name;
            Brand = brand;
            Price = price;
            Quantity = quantity;
            Category = category;
        }
    }
}