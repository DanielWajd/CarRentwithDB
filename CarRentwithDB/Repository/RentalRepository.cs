using CarRentwithDB.Interfaces;
using CarRentwithDB.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentwithDB.Service
{
    public class RentalRepository : IRentalService
    {
        private readonly CarRentDBContext _context;
        public RentalRepository(CarRentDBContext context)
        {
            _context = context;
        }
        public Task<bool> CreateRental(Rental rental)
        {
            _context.Add(rental);
            return Save();
        }

        private async Task<bool> Save()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0 ? true : false;
        }
        //All Rentals
        public async Task<List<Rental>> GetAllRentals()
        {
            var rentals = await _context.Rental
        .FromSqlRaw("EXEC GetAllRentals")
        .ToListAsync();

            
            foreach (var rental in rentals)
            {
                _context.Entry(rental).Reference(r => r.Car).Load();  
                _context.Entry(rental).Reference(r => r.AppUser).Load();  
            }

            return rentals;
        }
    }
}
