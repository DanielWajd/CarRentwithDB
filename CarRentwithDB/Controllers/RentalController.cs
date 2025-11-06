using CarRentwithDB.Helpers;
using CarRentwithDB.Interfaces;
using CarRentwithDB.Models;
using CarRentwithDB.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarRentwithDB.Controllers
{
    public class RentalController : Controller
    {
        private readonly IRentalRepository _rentalRepository;
        private readonly ICarRepository _carRepository;
        private readonly UserManager<AppUser> _userManager;
        public RentalController(IRentalRepository rentalRepository, ICarRepository carRepository, UserManager<AppUser> userManager)
        {
            _rentalRepository = rentalRepository;
            _carRepository = carRepository;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(string plate, string name, DateTime? startDate, DateTime? endDate)
        {
            if (!User.IsInRole("employee") && !User.IsInRole("admin"))
            {
                return View("Error");
            }
            var currentRentals = await _rentalRepository.GetCurrentRentals();
            var rentalsFilter = await _rentalRepository.GetFilteredRentals(plate, name, startDate, endDate);

            var currentRentalsViewModel = currentRentals.Select(rental => new AllRentalsViewModel
            {
                RentalId = rental.RentalId,
                StartDate = rental.StartDate,
                EndDate = rental.EndDate,
                Price = rental.Price,
                Make = rental.Car.Make,
                Model = rental.Car.Model,
                Image = ImageHelper.ConvertToImage(rental.Car.ImageData),
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
                Image = ImageHelper.ConvertToImage(rental.Car.ImageData),
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
            if(!User.Identity.IsAuthenticated)
            {
                return View("Error");
            }
            string curUserId = _userManager.GetUserId(User);
            var car = await _carRepository.GetByIdAsync(carId);
            if (car == null)
            {
                return NotFound();
            }
            var rentalViewModel = new RentalViewModel
            {
                CarId = carId,
                AppUserId = curUserId,
                DailyRate = car.DailyRate,
                Image = ImageHelper.ConvertToImage(car.ImageData),
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

            var car = await _carRepository.GetByIdAsync(rentalViewModel.CarId);
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
            await _rentalRepository.CreateRental(rent);
            await _carRepository.UpdateCarAvailability(rentalViewModel.CarId, false);
            return RedirectToAction("UserRentals","DetailsBoard");
        }

        [HttpPost]
        public async Task<IActionResult> Cancel(int id)
        {
            var rental = await _rentalRepository.GetByIdAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            rental.isCanceled = true;
            await _rentalRepository.Update(rental);

            //Zmianiam odrazu status auta na dostepny
            var car = await _carRepository.GetByIdAsync(rental.CarId);
            car.IsAvailable = true;
            await _carRepository.Update(car);
            return RedirectToAction("Index", "Rental");
        }
    }
}
