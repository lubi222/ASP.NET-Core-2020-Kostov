using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using RealEstates.Data;
using RealEstates.Services.Models;

namespace RealEstates.Services
{
    public class DistrictsService : IDistrictsService
    {
        private RealEstateDbContext db;

        public DistrictsService(RealEstateDbContext db)
        {
            this.db = db;
        }
        public IEnumerable<DistrictViewModel> GetTopDisctrictsByAveragePrice(int count = 10)
        {
            return this.db.Districts
                .OrderBy(d => d.Properties.Average(p => p.Price))
                .Select(d => new DistrictViewModel()
                {
                    AveragePrice = d.Properties.Average(p => p.Price),
                    MinPrice = d.Properties.Min(p => p.Price),
                    MaxPrice = d.Properties.Max(p => p.Price),
                    PropertiesCount = d.Properties.Count()
                })
                .Take(count)
                .ToList();
        }

        public IEnumerable<DistrictViewModel> GetTopDistrictsByNumberOfProperties(int count = 10)
        {
            return this.db.Districts
                .OrderByDescending(d => d.Properties.Count())
                .Select(d => new DistrictViewModel()
                {
                    AveragePrice = d.Properties.Average(p => p.Price),
                    MinPrice = d.Properties.Min(p => p.Price),
                    MaxPrice = d.Properties.Max(p => p.Price),
                    PropertiesCount = d.Properties.Count()
                })
                .Take(count)
                .ToList();
        }
    }
}
