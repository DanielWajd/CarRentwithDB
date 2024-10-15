using Microsoft.EntityFrameworkCore;

namespace CarRentwithDB.Models
{
    public class CarRentDBContext : DbContext
    {
        public CarRentDBContext(DbContextOptions<CarRentDBContext> options) :base(options) { 


        }
        public DbSet<Address> Address { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Rental> Rental { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }
}
