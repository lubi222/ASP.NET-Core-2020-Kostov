using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RealEstates.Web.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using RealEstates.Services;

namespace RealEstates.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDistrictsService districtsService;

        public HomeController(IDistrictsService districtsService)
        {
            this.districtsService = districtsService;
        }

        public IActionResult Index()
        {
            var districts = this.districtsService.GetTopDisctrictsByAveragePrice();
            return View(districts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
