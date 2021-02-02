using System;
using System.Collections.Generic;
using System.Text;
using RealEstates.Services.Models;

namespace RealEstates.Services
{
    public interface IDistrictsService
    {
        IEnumerable<DistrictViewModel> GetTopDisctrictsByAveragePrice(int count = 10);
        IEnumerable<DistrictViewModel> GetTopDistrictsByNumberOfProperties(int count = 10);

    }
}
