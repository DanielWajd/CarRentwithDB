using CarRentwithDB.Helpers;
using CarRentwithDB.Interfaces;
using CarRentwithDB.Models;
using CarRentwithDB.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CarRentwithDB.Controllers
{
    public class DetailsBoardController : Controller
    {
        private readonly IDetailsBoardRepository _detailsBoardRepository;

        public DetailsBoardController(IDetailsBoardRepository detailsBoardRepository)
        {
            _detailsBoardRepository = detailsBoardRepository;
        }
        public async Task<IActionResult> Index()
        {
            if (!User.IsInRole("employee"))
            {
                return View("Error");
            }
            var userCar = await _detailsBoardRepository.GetAllCreatedCars();

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

            var userRentals = await _detailsBoardRepository.GetAllUserRentals();

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
