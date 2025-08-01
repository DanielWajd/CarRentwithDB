﻿
using CarRentwithDB.Data.Enum;
using CarRentwithDB.Models;
using Microsoft.AspNetCore.Identity;

namespace CarRentwithDB.Data
{
    public class Seed
    {
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Employee))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Employee));
                if (!await roleManager.RoleExistsAsync(UserRoles.Customer))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Customer));
                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                //Users

                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                string employeeUserEmail = "pierwszyPracownik@gmail.com";

                var employeeUser = await userManager.FindByEmailAsync(employeeUserEmail);
                if (employeeUser == null)
                {
                    var newEmployeeUser = new Employee()
                    {
                        UserName = employeeUserEmail,
                        Email = employeeUserEmail,
                        EmailConfirmed = true,
                        Name = "Jan",
                        Surname = "Pierwszy",
                        Phone = "123456789",
                        UserType = UserType.Employee,
                        EmployeeType = EmployeeType.Manager
                    };
                    await userManager.CreateAsync(newEmployeeUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newEmployeeUser, UserRoles.Employee);
                }

                string customerUserEmail = "pierwszyKlient@gmail.com";

                var customerUser = await userManager.FindByEmailAsync(customerUserEmail);
                if (customerUser == null)
                {
                    var newCustomerUser = new Customer()
                    {
                        UserName = customerUserEmail,
                        Email = customerUserEmail,
                        EmailConfirmed = true,
                        Name = "Adam",
                        Surname = "Pierwszy",
                        Phone = "987654321",
                        DrivingLicence = "A1B2C3D4",
                        UserType = UserType.Customer,
                        Address = new Address()
                        {
                            Street = "Warszawska",
                            HouseNumber = "21",
                            City = "Rzeszow",
                            Voivodeship = "Podkrapackie",
                            Zip = "32-210"
                        }

                    };
                    await userManager.CreateAsync(newCustomerUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newCustomerUser, UserRoles.Customer);
                }

                string adminUserEmail = "admin@gmail.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new AppUser()
                    {
                        UserName = adminUserEmail,
                        Email = adminUserEmail,
                        EmailConfirmed = true,
                        Name = "Grzegorz",
                        Surname = "Pierwszy",
                        Phone = "123789456",
                        UserType = UserType.Admin,
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }
            }
        }

    }
}


