using CarRentwithDB.Interfaces;
using CarRentwithDB.Models;
using CarRentwithDB.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarRentwithDB.Controllers
{
    public class RentalController : Controller
    {
        private readonly IRentalService _rentalService;
        private readonly ICarService _carService;
        public readonly IHttpContextAccessor _contextAccessor;
        public RentalController(IRentalService rentalService, ICarService carService, IHttpContextAccessor contextAccessor)
        {
            _rentalService = rentalService;
            _carService = carService;
            _contextAccessor = contextAccessor;
        }
        public async Task<IActionResult> Index(string plate, string name, DateTime? startDate, DateTime? endDate)
        {

            //var rentals = await _rentalService.GetAllRentals();
            var currentRentals = await _rentalService.GetCurrentRentals();
            var rentalsFilter = await _rentalService.GetFilteredRentals(plate, name, startDate, endDate);

            var currentRentalsViewModel = currentRentals.Select(rental => new AllRentalsViewModel
            {
                RentalId = rental.RentalId,
                StartDate = rental.StartDate,
                EndDate = rental.EndDate,
                Price = rental.Price,
                Make = rental.Car.Make,
                Model = rental.Car.Model,
                Image = rental.Car.Image,
                UserName = rental.AppUser.Name,
                UserSurname = rental.AppUser.Surname
            }).ToList();
            var filteredRentalsViewModel = rentalsFilter.Select(rental => new AllRentalsViewModel
            {
                RentalId = rental.RentalId,
                StartDate = rental.StartDate,
                EndDate = rental.EndDate,
                Price = rental.Price,
                Make = rental.Car.Make,
                Model = rental.Car.Model,
                Image = rental.Car.Image,
                UserName = rental.AppUser.Name,
                UserSurname = rental.AppUser.Surname
            }).ToList();

            var rentalPageViewModel = new RentalPageViewModel
            {
                CurrentRentals = currentRentalsViewModel,
                FilteredRentals = filteredRentalsViewModel,
            };

            return View("Index", rentalPageViewModel);
        }
        public async Task<IActionResult> Create(int carId)
        {
            var curUserId = _contextAccessor.HttpContext.User.GetUserId();
            var car = await _carService.GetByIdAsync(carId);
            if (car == null)
            {
                return NotFound();
            }
            var rentalViewModel = new RentalViewModel
            {
                CarId = carId,
                AppUserId = curUserId,
                DailyRate = car.DailyRate,
                Image = car.Image,
                Make = car.Make,
                Model = car.Model
                
            };
            return View(rentalViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(RentalViewModel rentalViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(rentalViewModel);
            }

            var car = await _carService.GetByIdAsync(rentalViewModel.CarId);
            if (car == null)
            {
                return View(rentalViewModel);
            }
            int rentalDays = (rentalViewModel.EndDate - rentalViewModel.StartDate).Days;

            if (rentalDays <= 0)
                rentalDays = 1;

            rentalViewModel.Price = rentalDays * car.DailyRate;

            decimal price;
            if (!decimal.TryParse(rentalViewModel.Price.ToString(), out price))
            {
                ModelState.AddModelError("Price", "Cena jest niepoprawna.");
                return View(rentalViewModel);
            }

            rentalViewModel.Price = price;

            var rent = new Rental
            {
                StartDate = rentalViewModel.StartDate,
                EndDate = rentalViewModel.EndDate,
                CarId = rentalViewModel.CarId,
                AppUserId = rentalViewModel.AppUserId,
                Price = rentalViewModel.Price
            };
            await _rentalService.CreateRental(rent);
            await _carService.UpdateCarAvailability(rentalViewModel.CarId, false);
            return RedirectToAction("UserRentals","DetailsBoard");
        }

    }
}
