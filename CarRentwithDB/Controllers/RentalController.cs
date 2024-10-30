using CarRentwithDB.Interfaces;
using CarRentwithDB.Models;
using CarRentwithDB.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarRentwithDB.Controllers
{
    public class RentalController : Controller
    {
        private readonly IRentalService _rentalService;
        public RentalController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(RentalViewModel rentalViewModel) {
            if (!ModelState.IsValid)
            {
                return View(rentalViewModel);
            }
            var rent = new Rental
            {
                StartDate = rentalViewModel.StartDate,
                EndDate = rentalViewModel.EndDate
            };
            await _rentalService.CreateRental(rent);
            return RedirectToAction("Index");
        }

    }
}
