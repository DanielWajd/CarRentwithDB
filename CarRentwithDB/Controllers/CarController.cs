using CarRentwithDB.Interfaces;
using CarRentwithDB.Models;
using CarRentwithDB.Services;
using Microsoft.AspNetCore.Mvc;

namespace CarRentwithDB.Controllers
{
    
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Car> cars = await _carService.GetAll();
            return View(cars);
        }
        public async Task<IActionResult> Details(int id)
        {
            Car car = await _carService.GetByIdAsync(id);
            return View(car);
        }
    }
}
