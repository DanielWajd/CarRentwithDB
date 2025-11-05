using CarRentwithDB.Interfaces;
using CarRentwithDB.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentwithDB.Repository
{
    public class RentalRepository : IRentalRepository
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
        public async Task<IEnumerable<Rental>> GetFilteredRentals(string plate, string name, DateTime? startDate, DateTime? endDate)
        {
            var query = _context.Rental.Where(r => !r.isCanceled).Include(r => r.Car).Include(r => r.AppUser).AsQueryable();

            if (!string.IsNullOrEmpty(plate))
            {
                query = query.Where(r => r.Car.LicencePlate.Contains(plate));
            }
            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(r => r.AppUser.Surname.Contains(name));
            }
            if (startDate.HasValue)
            {
                query = query.Where(r => r.StartDate >= startDate.Value);
            }
            if (endDate.HasValue)
            {
                query = query.Where(r => r.EndDate <= endDate.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Rental>> GetCurrentRentals()
        {
            var time = DateTime.Now;
            return await _context.Rental.Where(r => !r.isCanceled).Where(r => r.StartDate <= time && r.EndDate >= time).Include(r => r.Car).Include(r => r.AppUser).ToListAsync();
        }

        public async Task<int> GetRentalCountAsync()
        {
            return await _context.Rental.Where(r => !r.isCanceled).CountAsync();
        }

        public async Task<Rental> GetByIdAsync(int id)
        {
            return await _context.Rental.FirstOrDefaultAsync(i => i.RentalId == id);
        }

        public async Task<bool> Update(Rental rental)
        {
            _context.Update(rental);
            return await Save();
        }
    }
}
