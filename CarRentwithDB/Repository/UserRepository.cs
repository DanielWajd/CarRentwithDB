using CarRentwithDB.Interfaces;
using CarRentwithDB.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CarRentwithDB.Service
{
    public class UserRepository : IUserRepository
    {
        private readonly CarRentDBContext _context;

        public UserRepository(CarRentDBContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AppUser>> GetAllUsers(){
            return await _context.Users.ToListAsync();
        }

        public async Task<AppUser> GetUserById(string id)
        {
            var user = await _context.Users
                         .Include(u => u.Cars)    
                         .Include(u => u.Rentals) 
                         .ThenInclude(r => r.Car)  
                         .FirstOrDefaultAsync(u => u.Id == id);

            if (user != null)
            {
                user.Rentals = user.Rentals.Where(r => !r.isCanceled).ToList();
            }
            return user;
        }
        public async Task<AppUser> GetIdByNoTracking(string id)
        {
            return await _context.Users.Where(u=>u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }
        public async Task<bool> UpdateUser(AppUser user)
        {
            _context.Users.Update(user);
            return await Save();
        }
        public async Task<bool> Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public async Task<bool> VerifyPasswordAsync(AppUser user, string currentPassword)
        {
            var password = new PasswordHasher<AppUser>();
            var result = password.VerifyHashedPassword(user, user.PasswordHash, currentPassword);

            return result ==PasswordVerificationResult.Success;
        }

        public async Task<bool> UpdatePasswordAsync(AppUser user, string newPassword)
        {
            var password = new PasswordHasher<AppUser>();
            user.PasswordHash = password.HashPassword(user, newPassword);

            try
            {
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<IEnumerable<AppUser>> GetFilteredUsers(string name, string surname, string email, string phone)
        {
            var query = _context.Users.AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                query = query.Where(u => u.Name == name);
            }
            if (!string.IsNullOrEmpty(surname))
            {
                query = query.Where(u => u.Surname == surname);
            }
            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(u => u.Email == email);
            }
            if (!string.IsNullOrEmpty(phone))
            {
                query = query.Where(u => u.Phone == phone);
            }

            return await query.ToListAsync();
        }

        public async Task<int> GetUsersCountAsync()
        {
            return await _context.Users.Where(u => u.UserType == Data.Enum.UserType.Customer).CountAsync();
        }
    } 
}
