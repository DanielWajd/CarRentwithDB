using CarRentwithDB.Interfaces;
using CarRentwithDB.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentwithDB.Service
{
    public class DetailsBoardService : IDetailsBoardService
    {
        private readonly CarRentDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DetailsBoardService(CarRentDBContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<List<Car>> GetAllCreatedCars()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userCars = _context.Cars.Where(r => r.AppUser.Id == curUser);
            return userCars.ToList();
        }

        public async Task<List<Car>> GetAllRentedCars()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();
            var userCars = await _context.Rental.Where(r =>r.AppUser.Id == curUser).
                Select(r => r.Car).ToListAsync();
            return userCars;
        }
    }
}
