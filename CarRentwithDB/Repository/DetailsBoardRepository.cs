using CarRentwithDB.Interfaces;
using CarRentwithDB.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentwithDB.Service
{
    public class DetailsBoardRepository : IDetailsBoardService
    {
        private readonly CarRentDBContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DetailsBoardRepository(CarRentDBContext context, IHttpContextAccessor httpContextAccessor)
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

        public async Task<List<Rental>> GetAllRentals()
        {
            var curUser = _httpContextAccessor.HttpContext?.User.GetUserId();

            var userRentals = await _context.Rental
                .Include(r => r.Car) 
                .Where(r => r.AppUserId == curUser)
                .ToListAsync();

            return userRentals;
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
