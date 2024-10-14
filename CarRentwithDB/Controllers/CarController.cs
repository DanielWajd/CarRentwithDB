using CarRentwithDB.Models;
using Microsoft.AspNetCore.Mvc;

namespace CarRentwithDB.Controllers
{
    
    public class CarController : Controller
    {
        private readonly CarRentDBContext _context;

        public CarController(CarRentDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            List<Car> cars = _context.Cars.ToList();
            return View(cars);
        }
        public IActionResult Details(int id)
        {
            Car car = _context.Cars.FirstOrDefault(c => c.CarId == id);
            return View(car);
        }
    }
}
