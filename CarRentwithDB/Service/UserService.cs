using CarRentwithDB.Interfaces;
using CarRentwithDB.Models;
using Microsoft.EntityFrameworkCore;

namespace CarRentwithDB.Service
{
    public class UserService : IUserService
    {
        private readonly CarRentDBContext _context;

        public UserService(CarRentDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AppUser>> GetAllUsers(){
            return await _context.Users.ToListAsync();
        }

        public async Task<AppUser> GetUserById(string id)
        {
            return await _context.Users
                         .Include(u => u.Cars)    
                         .Include(u => u.Rentals) 
                         .ThenInclude(r => r.Car)  
                         .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
