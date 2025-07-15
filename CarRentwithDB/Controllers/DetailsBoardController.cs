using CarRentwithDB.Interfaces;
using CarRentwithDB.Models;
using CarRentwithDB.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarRentwithDB.Controllers
{
    public class DetailsBoardController : Controller
    {
        private readonly IDetailsBoardService _detailsBoardService;

        public DetailsBoardController(IDetailsBoardService detailsBoardService)
        {
            _detailsBoardService = detailsBoardService;
        }
        public async Task<IActionResult> Index()
        {
            if (!User.IsInRole("employee"))
            {
                return View("Error");
            }
            var userCar = await _detailsBoardService.GetAllCreatedCars();
            var detailsboardViewModel = new DetailsBoardViewModel()
            {
                Cars = userCar
            };
            return View(detailsboardViewModel);
        }
        
        public async Task<IActionResult> UserRentals()
        {

            var userRentals = await _detailsBoardService.GetAllUserRentals();

            var detailsBoardViewModel = new DetailsBoardViewModel()
            {
                Rentals = userRentals
            };
            return View(detailsBoardViewModel);
        }
    }
}
