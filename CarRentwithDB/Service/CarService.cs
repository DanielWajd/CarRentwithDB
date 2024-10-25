using CarRentwithDB.Interfaces;
using CarRentwithDB.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentwithDB.Services
{
    public class CarService : ICarService
    {
        private readonly CarRentDBContext _context;

        public CarService(CarRentDBContext context)
        {
            _context = context;
        }
        public bool Add(Car car)
        {
            _context.Add(car);
            return Save();
        }

        public bool Delete(Car car)
        {
            _context.Remove(car);
            return Save();
        }

        public async Task<IEnumerable<Car>> GetAll()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car> GetByIdAsync(int id)
        {
            return await _context.Cars.Include(i => i.CarDetails).FirstOrDefaultAsync(i => i.CarId == id);
        }

        public async Task<IEnumerable<Car>> GetCarByCity(string city)
        {
            return await _context.Cars.Where(i => i.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Car car)
        {
            throw new NotImplementedException();
        }
    }
}
