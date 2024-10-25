using CarRentwithDB.Interfaces;
using CarRentwithDB.Models;
using CarRentwithDB.Services;
using CarRentwithDB.ViewModels;
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
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateCarViewModel createCarViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createCarViewModel);
            }

            var car = new Car
            {
                Make = createCarViewModel.Make,
                Model = createCarViewModel.Model,
                Year = createCarViewModel.Year,
                City = createCarViewModel.City,
                carType = createCarViewModel.carType,
                fuelType = createCarViewModel.fuelType,
                Mileage = createCarViewModel.Mileage,
                carColor = createCarViewModel.carColor,
                DailyRate = createCarViewModel.DailyRate,
                VIN = createCarViewModel.VIN,
                LicencePlate = createCarViewModel.LicencePlate,
                IsAvailable = createCarViewModel.IsAvailable,
                Description = createCarViewModel.Description,
                Image = createCarViewModel.Image,
                CarDetails = new CarDetails
                {
                    HorsePower = createCarViewModel.CarDetails.HorsePower,
                    Seats = createCarViewModel.CarDetails.Seats,
                    EngineCapacity = createCarViewModel.CarDetails.EngineCapacity,
                    TrunkCapacity = createCarViewModel.CarDetails.TrunkCapacity,
                    TransmissionType = createCarViewModel.CarDetails.TransmissionType
                }
            };

            _carService.Add(car);
            return RedirectToAction("Index");
        }


    }
}
