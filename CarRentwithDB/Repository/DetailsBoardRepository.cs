using CarRentwithDB.Interfaces;
using CarRentwithDB.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentwithDB.Repository
{
    public class DetailsBoardRepository : IDetailsBoardRepository
    {
        private readonly CarRentDBContext _context;

        public DetailsBoardRepository(CarRentDBContext context)
        {
            _context = context;
        }
        //Created cars by employee
        public async Task<List<Car>> GetAllCreatedCars(string userId)
        {
            var userCars = _context.Cars.Where(c => c.AppUser.Id == userId);
            return await userCars.ToListAsync();
        }
        //Customer rentals
        public async Task<List<Rental>> GetAllUserRentals(string userId)
        {

            var userRentals = await _context.Rental.Where(r=>!r.isCanceled)
                .Include(r => r.Car) 
                .Where(r => r.AppUserId == userId)
                .ToListAsync();

            return userRentals;
        }
        
    }
}
