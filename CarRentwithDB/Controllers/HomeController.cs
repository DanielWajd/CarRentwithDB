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
        private readonly ICarRepository _carRepository;
        private readonly IRentalRepository _rentalRepository;
        private readonly IUserRepository _userRepository;

        public HomeController(ILogger<HomeController> logger, ICarRepository carRepository, IRentalRepository rentalRepository, IUserRepository userRepository)
        {
            _logger = logger;
            _carRepository = carRepository;
            _rentalRepository = rentalRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var homeViewModel = new HomeViewModel
            {
                TotalCars = await _carRepository.GetCarsCountAsync(),
                TotalRentals = await _rentalRepository.GetRentalCountAsync(),
                TotalUsers = await _userRepository.GetUsersCountAsync()
            };
            return View(homeViewModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> Map()
        {
            var cities = await _carRepository.GetCarsCities();
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
