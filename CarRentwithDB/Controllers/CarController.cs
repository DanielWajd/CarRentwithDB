using CarRentwithDB.Data.Enum;
using CarRentwithDB.Interfaces;
using CarRentwithDB.Models;
using CarRentwithDB.Services;
using CarRentwithDB.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


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
        public async Task<IActionResult> Index(int? page, string city, string type, string makeModel, string sortOrder)
        {
            //IEnumerable<Car> cars = await _carService.GetAll();
            var cars = await _carService.GetFilteredCars(city, type, makeModel, sortOrder);
            if (User.IsInRole("customer") || !User.Identity.IsAuthenticated)
            {
                cars = cars.Where(car => car.IsAvailable);
            }

            //Paginacja
            int pageSize = 6;
            int pageIndex;
            if (page.HasValue)
            {
                pageIndex = page.Value;
            }
            else
            {
                pageIndex = 1; 
            }
            int count = cars.Count();
            int totalPages = (int)Math.Ceiling(count/(double)pageSize);
            var items = cars.Skip((pageIndex-1) * pageSize).Take(pageSize).ToList();


            var carIndexViewModel = new CarIndexViewModel
            {
                Cars = items,
                City = city,
                Type = type,
                MakeModel = makeModel,
                SortOrder = sortOrder,
                //Paginacja
                CurrentPage = pageIndex,
                TotalPages = totalPages,
            };
            //return View(cars.ToPagedList(pageNumber, pageSize));
            return View(carIndexViewModel);
        }
        public async Task<IActionResult> Details(int id)
        {
            var car = await _carService.GetByIdAsync(id);
            var carDetailsViewModel = new CarDetailsViewModel
            {
                CarId = car.CarId,
                Make = car.Make,
                Model = car.Model,
                Year = car.Year,
                City = car.City,
                carType = car.carType,
                Mileage = car.Mileage,
                carColor = car.carColor,
                fuelType = car.fuelType,
                DailyRate = car.DailyRate,
                Description = car.Description,
                IsAvailable = car.IsAvailable,
                Image = car.Image,
                steeringSide = car.steeringSide,
                TechCarDetails = new TechCarDetailsViewModel
                {
                    HorsePower = car.CarDetails.HorsePower,
                    EngineCapacity = car.CarDetails.EngineCapacity,
                    Seats = car.CarDetails.Seats,
                    TrunkCapacity = car.CarDetails.TrunkCapacity,
                    TransmissionType = car.CarDetails.TransmissionType
                }
            };


            return View(carDetailsViewModel);
        }

        public async Task<IActionResult> Create()
        {
            if (!User.IsInRole("employee"))
            {
                return View("Error");
            }
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
                steeringSide = createCarViewModel.steeringSide,
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

            await _carService.Add(car);
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var car = await _carService.GetByIdAsyncNoTracking(id);
            if (car == null || !User.IsInRole("employee"))
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
                steeringSide = car.steeringSide,
                LicencePlate = car.LicencePlate,
                IsAvailable = car.IsAvailable,
                Description = car.Description,
                Image = car.Image,
                CarDetails = new TechCarDetailsViewModel
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
            car.steeringSide = editCarViewModel.steeringSide;
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

            await _carService.Update(car);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var carDetails = await _carService.GetByIdAsync(id);
            if (carDetails == null || !User.IsInRole("employee"))
            {
                return View("Error");
            }
            return View(carDetails);
        }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var carDetails = await _carService.GetByIdAsync(id);

            if (carDetails == null || !User.IsInRole("employee"))
            {
                return View("Error");
            }
            //To do usuwania całkowitego z bazy danych
            //_carService.Delete(carDetails);

            carDetails.IsActive = false;
            await _carService.Update(carDetails);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> GetUnAvailableCarsToUpdate()
        {
            var unavailableCarsToUpade = await _carService.GetUnAvailableCarsAsyncToUpdate();
            if (!User.IsInRole("employee"))
            {
                return View("Error");
            }
            var model = new CarIndexViewModel
            {
                Cars = unavailableCarsToUpade,
                City = null,
                Type = null,
                MakeModel = null,
                SortOrder = null
            };

            return View("Index", model);
            //return View("Index", unavailableCarsToUpade);
        }
        [HttpGet]
        public async Task<IActionResult> GetUnAvailableCars()
        {
            var unavailableCars = await _carService.GetUnAvailableCarsAsync();
            if (!User.IsInRole("employee"))
            {
                return View("Error");
            }
            var model = new CarIndexViewModel
            {
                Cars = unavailableCars,
                City = null,
                Type = null,
                MakeModel = null,
                SortOrder = null
            };

            return View("Index", model);
            //return View("Index", unavailableCars);
        }
        //[HttpGet]
        //public async Task<IActionResult> SortCars(string? sort)
        //{
        //    var cars = await _carService.GetSortedCars(sort);
        //    return View("Index", cars);
        //}
    }
}
