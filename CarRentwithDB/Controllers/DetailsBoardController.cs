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
            var userCar = await _detailsBoardService.GetAllCars();
            var detailsboardViewModel = new DetailsBoardViewModel()
            {
                Cars = userCar
            };
            return View(detailsboardViewModel);
        }
    }
}
