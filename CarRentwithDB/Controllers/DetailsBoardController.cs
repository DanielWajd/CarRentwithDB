using CarRentwithDB.Helpers;
using CarRentwithDB.Interfaces;
using CarRentwithDB.Models;
using CarRentwithDB.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarRentwithDB.Controllers
{
    public class DetailsBoardController : Controller
    {
        private readonly IDetailsBoardRepository _detailsBoardRepository;
        private readonly UserManager<AppUser> _userManager;

        public DetailsBoardController(IDetailsBoardRepository detailsBoardRepository, UserManager<AppUser> userManager)
        {
            _detailsBoardRepository = detailsBoardRepository;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            if (!User.IsInRole("employee"))
            {
                return View("Error");
            }
            string userId = _userManager.GetUserId(User);
            var userCar = await _detailsBoardRepository.GetAllCreatedCars(userId);

            //do zdjec
            foreach (var car in userCar)
            {
                car.Image = ImageHelper.ConvertToImage(car.ImageData);
            }
            //reszta
            var detailsboardViewModel = new DetailsBoardViewModel()
            {
                Cars = userCar
            };
            return View(detailsboardViewModel);
        }
        
        public async Task<IActionResult> UserRentals()
        {
            string userId = _userManager.GetUserId(User);
            var userRentals = await _detailsBoardRepository.GetAllUserRentals(userId);

            //do zdjec
            foreach (var car in userRentals)
            {
                car.Car.Image = ImageHelper.ConvertToImage(car.Car.ImageData);
            }
            //reszta
            var detailsBoardViewModel = new DetailsBoardViewModel()
            {
                Rentals = userRentals
            };
            return View(detailsBoardViewModel);
        }
    }
}
