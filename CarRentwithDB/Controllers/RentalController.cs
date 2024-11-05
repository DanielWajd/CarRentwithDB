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
        public RentalController(IRentalService rentalService, ICarService carService)
        {
            _rentalService = rentalService;
            _carService = carService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public async Task<IActionResult> Create(int carId)
        {
            var rentalViewModel = new RentalViewModel
            {
                CarId = carId
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
            var rent = new Rental
            {
                StartDate = rentalViewModel.StartDate,
                EndDate = rentalViewModel.EndDate,
                CarId = rentalViewModel.CarId
            };
            await _rentalService.CreateRental(rent);
            await _carService.UpdateCarAvailability(rentalViewModel.CarId, false);
            return RedirectToAction("Index");
        }

    }
}
