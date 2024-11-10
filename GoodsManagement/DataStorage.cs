using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace GoodsManagement
{
    public class DataStorage
    {
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Product> Products { get; set; } = new List<Product>();
        public List<Supplier> Suppliers { get; set; } = new List<Supplier>();

        public void SaveData()
        {
            try
            {
                string json = JsonConvert.SerializeObject(this);
                File.WriteAllText("data.json", json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка збереження даних: {ex.Message}");
            }
        }

        public void LoadData()
        {
            try
            {
                if (File.Exists("data.json"))
                {
                    string json = File.ReadAllText("data.json");
                    DataStorage data = JsonConvert.DeserializeObject<DataStorage>(json);

                    Categories = data.Categories;
                    Products = data.Products;
                    Suppliers = data.Suppliers;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Помилка завантаження даних: {ex.Message}");
            }
        }
    }
}