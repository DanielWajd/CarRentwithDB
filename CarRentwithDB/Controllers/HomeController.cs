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
        private readonly IRentalService _rentalService;
        private readonly IUserService _userService;

        public HomeController(ILogger<HomeController> logger, ICarService carService, IRentalService rentalService, IUserService userService)
        {
            _logger = logger;
            _carService = carService;
            _rentalService = rentalService;
            _userService = userService;
        }

        public async Task<IActionResult> Index()
        {
            var homeViewModel = new HomeViewModel
            {
                TotalCars = await _carService.GetCarsCountAsync(),
                TotalRentals = await _rentalService.GetRentalCountAsync(),
                TotalUsers = await _userService.GetUsersCountAsync()
            };
            return View(homeViewModel);
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
