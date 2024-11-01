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
        private readonly IHttpContextAccessor _contextAccessor;
        public CarController(ICarService carService, IHttpContextAccessor contextAccessor)
        {
            _carService = carService;
            _contextAccessor = contextAccessor;
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
            var curUserId = _contextAccessor.HttpContext.User.GetUserId();
            var createCarViewModel = new CreateCarViewModel { AppUserId = curUserId };
            return View(createCarViewModel);

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
                AppUserId = createCarViewModel.AppUserId,
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
        public async Task<IActionResult> Edit(int id)
        {
            var car = await _carService.GetByIdAsyncNoTracking(id);
            if (car == null)
            {
                return View("Error");
            }
            var carViewModel = new EditCarViewModel
            {
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                City = car.City,
                carType = car.carType,
                fuelType = car.fuelType,
                Mileage = car.Mileage,
                carColor = car.carColor,
                DailyRate = car.DailyRate,
                VIN = car.VIN,
                LicencePlate = car.LicencePlate,
                IsAvailable = car.IsAvailable,
                Description = car.Description,
                Image = car.Image,
                CarDetails = new CarDetailsViewModel
                {
                    HorsePower = car.CarDetails.HorsePower,
                    Seats = car.CarDetails.Seats,
                    EngineCapacity = car.CarDetails.EngineCapacity,
                    TrunkCapacity = car.CarDetails.TrunkCapacity,
                    TransmissionType = car.CarDetails.TransmissionType
                }
            };
            return View(carViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditCarViewModel editCarViewModel)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Nie można dokonać edycji");
                return View("Edit", editCarViewModel);
            }
            var car = await _carService.GetByIdAsyncNoTracking(id);
            if (car == null)
            {
                return View("Error");
            }

            car.Make = editCarViewModel.Make;
            car.Model = editCarViewModel.Model;
            car.Year = editCarViewModel.Year;
            car.City = editCarViewModel.City;
            car.carType = editCarViewModel.carType;
            car.fuelType = editCarViewModel.fuelType;
            car.Mileage = editCarViewModel.Mileage;
            car.carColor = editCarViewModel.carColor;
            car.DailyRate = editCarViewModel.DailyRate;
            car.VIN = editCarViewModel.VIN;
            car.LicencePlate = editCarViewModel.LicencePlate;
            car.IsAvailable = editCarViewModel.IsAvailable;
            car.Description = editCarViewModel.Description;
            car.Image = editCarViewModel.Image;

            if (car.CarDetails != null)
            {
                car.CarDetails.HorsePower = editCarViewModel.CarDetails.HorsePower;
                car.CarDetails.Seats = editCarViewModel.CarDetails.Seats;
                car.CarDetails.EngineCapacity = editCarViewModel.CarDetails.EngineCapacity;
                car.CarDetails.TrunkCapacity = editCarViewModel.CarDetails.TrunkCapacity;
                car.CarDetails.TransmissionType = editCarViewModel.CarDetails.TransmissionType;
            }

            _carService.Update(car);
            return RedirectToAction("Index");
        }

    }
}
