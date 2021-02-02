using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using RealEstates.Data;
using RealEstates.Services;
using JsonProperty = RealEstates.Services.JsonProperty;

namespace RealEstates.ConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            var db = new RealEstateDbContext();
            //db.Database.Migrate();

            IPropertiesService propertiesService = new PropertiesService(db);
            //propertiesService.Create("Dianabad", 100, 2019, 322000000, "4-Staen", "Panelka", 17, 21);

            IDistrictsService districtsService = new DistrictsService(db);
            //var districts = districtsService.GetTopDisctrictsByAveragePrice();
            //foreach (var district in districts)
            //{
            //    Console.WriteLine($"{district.Name} => Price: {district.AveragePrice} ({district.MinPrice} - {district.MaxPrice}) => properties: {district.PropertiesCount}");
            //}
            Console.Write("Min price: ");
            int minPrice = int.Parse(Console.ReadLine());
            Console.Write("Max price: ");
            int maxPrice = int.Parse(Console.ReadLine());
            var properties = propertiesService.SearchByPrice(minPrice, maxPrice);
            foreach (var property in properties)
            {
                Console.WriteLine($"{property.Price}");
            }

        }
    }
}
