using CarRentwithDB.Interfaces;
using CarRentwithDB.Models;
using CarRentwithDB.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CarRentwithDB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICarService _carService;

        public HomeController(ILogger<HomeController> logger, ICarService carService)
        {
            _logger = logger;
            _carService = carService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Map()
        {
            var cities = await _carService.GetCarsCities();
            var mapViewModel = new MapViewModel
            {
                Cities = cities,
            };
            return View(mapViewModel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
