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

    }
}
