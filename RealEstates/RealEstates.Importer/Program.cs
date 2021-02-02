using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using RealEstates.Data;
using RealEstates.Services;
using JsonProperty = RealEstates.Services.JsonProperty;

namespace RealEstates.Importer
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new RealEstateDbContext();
            var propertiesService = new PropertiesService(db);

            File.Create("imot-raw-data.json");
            var json = File.ReadAllText("imot-raw-data.json");
            var properties = JsonSerializer.Deserialize<IEnumerable<JsonProperty>>(json);

            foreach (var property in properties.Where(x => x.Price > 500))
            {
                propertiesService
                    .Create(property.District,
                        property.Size,
                        property.Year,
                        property.Price,
                        property.Type,
                        property.BuildingType,
                        property.Floor,
                        property.TotalFloors);
            }


        }
    }
}
