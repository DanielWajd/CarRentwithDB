using CarRentwithDB.Interfaces;
using CarRentwithDB.Models;

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
    }
}
