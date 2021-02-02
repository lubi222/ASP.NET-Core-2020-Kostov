using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace RealEstates.Models
{
    public class District
    {
        public District()
        {
            this.Properties = new HashSet<RealEstateProperty>();
        }

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<RealEstateProperty> Properties { get; set; }
    }
}
