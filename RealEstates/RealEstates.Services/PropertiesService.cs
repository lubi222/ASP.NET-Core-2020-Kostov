using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using RealEstates.Data;
using RealEstates.Models;
using RealEstates.Services.Models;

namespace RealEstates.Services
{
    public class PropertiesService : IPropertiesService
    {
        private RealEstateDbContext db;

        public PropertiesService(RealEstateDbContext db)
        {
            this.db = db;
        }
        public void Create(string district, int size, int? year, int price, string propertyType, string buildingType, int? floor,
            int? maxFloors)
        {
            if (district == null)
            {
                throw new ArgumentNullException(nameof(district));
            }

            var property = new RealEstateProperty()
            {
                Size = size,
                Price = price,
                Year = year < 1800 ? null : year,
                Floor = floor,
                TotalNumberOfFloors = maxFloors

            };

            if (property.Floor< 0)
            {
                property.Floor = null;
            }

            if (property.TotalNumberOfFloors <= 0)
            {
                property.TotalNumberOfFloors = null;
            }
            /// District
            var districtEntity = this.db.Districts.FirstOrDefault(d => d.Name.Trim() == district.Trim());
            if (districtEntity == null)
            {
                districtEntity = new District {Name = district,};
            }
            property.District = districtEntity;

            /// Property Type
            var propertyTypeEntity = this.db.PropertyTypes.FirstOrDefault(pt => pt.Name.Trim() == propertyType.Trim());
            if (propertyTypeEntity == null)
            {
                propertyTypeEntity = new PropertyType {Name = propertyType};
            }
            property.PropertyType = propertyTypeEntity;
            
            /// Building Type
            var buildingTypeEntity = this.db.BuildingTypes.FirstOrDefault(bt => bt.Name.Trim() == buildingType.Trim());
            if (buildingTypeEntity == null)
            {
                buildingTypeEntity = new BuildingType {Name = buildingType};
            }

            property.BuildingType = buildingTypeEntity;


            this.db.RealEstateProperties.Add(property);
            this.db.SaveChanges();

            /// After db.SaveChanges is called, the property receives an Id and we can access it
            this.UpdateTags(property.Id);
        }

        public void UpdateTags(int propertyId)
        {
            var property = this.db.RealEstateProperties.FirstOrDefault(p => p.Id == propertyId);
            property.Tags.Clear();
            if (property.Year.HasValue && property.Year < 1990)
            {
                property.Tags.Add(new RealEstatePropertyTag()
                {
                    Tag = this.GetOrCreateTag("OldBuilding")
                });
            }

            if (property.Size > 120)
            {
                property.Tags.Add(new RealEstatePropertyTag()
                {
                    Tag = this.GetOrCreateTag("HugeApartment")
                });
            }
            
            if(((double)property.Price / property.Size )> 2000)
            {
                property.Tags.Add(new RealEstatePropertyTag()
                {
                    Tag = this.GetOrCreateTag("ExpensiveProperty")
                });
            }

            if (((double)property.Price / property.Size) < 800)
            {
                property.Tags.Add(new RealEstatePropertyTag()
                {
                    Tag = this.GetOrCreateTag("ExpensiveProperty")
                });
            }

            this.db.SaveChanges();
        }

        private Tag GetOrCreateTag(string tag)
        {
            var tagEntity = this.db.Tags.FirstOrDefault(t => t.Name.Trim() == tag.Trim());
            if (tagEntity == null)
            {
                tagEntity = new Tag {Name = tag};
            }

            return tagEntity;
        }

        public IEnumerable<PropertyViewModel> Search(int minYear, int maxYear, int minSize, int maxSize)
        {
            return this.db.RealEstateProperties
                .Where(rep => rep.Year >= minYear && rep.Year <= maxYear && rep.Size >= minSize && rep.Size <= maxSize)
                .Select(rep => new PropertyViewModel
                {
                    Price = rep.Price,
                    Floor = (rep.Floor ?? 0) + "/" + (rep.TotalNumberOfFloors ?? 0),
                    Size = rep.Size,
                    BuildingType = rep.BuildingType.Name,
                    District = rep.District.Name,
                    PropertyType = rep.PropertyType.Name,
                })
                .OrderBy(pr => pr.Price)
                .ToList();
        }

        public IEnumerable<PropertyViewModel> SearchByPrice(int minPrice, int maxPrice)
        {
            return this.db.RealEstateProperties
                .Where(rep => rep.Price >= minPrice && rep.Price <= maxPrice)
                .Select(rep => new PropertyViewModel
                {
                    Price = rep.Price,
                    Floor = (rep.Floor ?? 0) + "/" + (rep.TotalNumberOfFloors ?? 0),
                    Size = rep.Size,
                    BuildingType = rep.BuildingType.Name,
                    District = rep.District.Name,
                    PropertyType = rep.PropertyType.Name,
                })
                .OrderBy(pr => pr.Price)
                .ToList();
        }
    }
}
