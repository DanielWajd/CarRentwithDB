using CarRentwithDB.Data;
using CarRentwithDB.Data.Enum;
using CarRentwithDB.Interfaces;
using CarRentwithDB.Models;
using CarRentwithDB.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CarRentwithDB.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;

        public AccountController(IUserService userService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IHttpContextAccessor httpContextAccessor,
            CarRentDBContext context)
        {
            _userService = userService;
            _userManager = userManager;
            _contextAccessor = httpContextAccessor;
            _signInManager = signInManager;
        }
        //Get
        public IActionResult Login()
        {
            var response = new LoginViewModel();
            return View(response);
        }
        //Post
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View(loginViewModel);

            var user = await _userManager.FindByNameAsync(loginViewModel.EmailAddress);

            if (user != null)
            {
                //User is found, check password
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    //Password correct, sign in
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Index", "Car");
                    }
                }
                //When Password is incorrect
                TempData["Error"] = "Złe hasło";
                return View(loginViewModel);
            }
            //When User not found
            TempData["Error"] = "Nie ma takiego użytkownika";
            return View(loginViewModel);
        }
        public IActionResult Register()
        {
            var response = new RegisterViewModel();
            return View(response);
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return View(registerViewModel);
            }

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "Ten emial jest już zajęty";
                return View(registerViewModel);
            }
            IdentityResult newUserResponse;

            if (registerViewModel.UserType == UserType.Customer)
            {
                // Customer

                var newAddress = new Address
                {
                    Street = registerViewModel.registerCustomer.Street,
                    City = registerViewModel.registerCustomer.City,
                    HouseNumber = registerViewModel.registerCustomer.HouseNumber,
                    Voivodeship = registerViewModel.registerCustomer.Voivodeship,
                    Zip = registerViewModel.registerCustomer.Zip
                };
                var newCustomer = new Customer()
                {
                    Name = registerViewModel.Name,
                    Surname = registerViewModel.Surname,
                    Phone = registerViewModel.Phone,
                    Email = registerViewModel.EmailAddress,
                    UserName = registerViewModel.EmailAddress,
                    UserType = UserType.Customer,
                    DrivingLicence = registerViewModel.registerCustomer.DrivingLicence,
                    Image = registerViewModel.Image,
                    Address = newAddress
                };

                newUserResponse = await _userManager.CreateAsync(newCustomer, registerViewModel.Password);
                if (newUserResponse.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newCustomer, UserRoles.Customer);
                }
            }
            else
            {
                // Employee
                var newEmployee = new Employee()
                {
                    Name = registerViewModel.Name,
                    Surname = registerViewModel.Surname,
                    Phone = registerViewModel.Phone,
                    Email = registerViewModel.EmailAddress,
                    UserName = registerViewModel.EmailAddress,
                    UserType = UserType.Employee,
                    EmployeeType = registerViewModel.registerEmployee.EmployeeType,
                    Image = registerViewModel.Image
                };
                

                newUserResponse = await _userManager.CreateAsync(newEmployee, registerViewModel.Password);
                if (newUserResponse.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newEmployee, UserRoles.Employee);
                }
            }

            if (newUserResponse.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Error"] = "Error while creating the user";
                return View(registerViewModel);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        
        public async Task<IActionResult> Index()
        {
            IEnumerable<AppUser> users = await _userService.GetAllUsers();
            return View(users);
        }
        public async Task<IActionResult> Details(string id)
        {
            var user = await _userService.GetUserById(id);
            if (user == null)
            {
                return NotFound(); 
            }
            if (string.IsNullOrEmpty(user.Image))
            {
                user.Image = "/img/user.png";
            }
            var userDetailViewModel = new UserDetailViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Phone = user.Phone,
                Image = user.Image,
                UserType = user.UserType,
                Cars = user.Cars ?? new List<Car>(),  
                Rentals = user.Rentals ?? new List<Rental>()
            };
            
            return View(userDetailViewModel);
        }
        public async Task<IActionResult> EditUserProfile()
        {
            var curUserId = _contextAccessor.HttpContext.User.GetUserId();
            var user = await _userService.GetUserById(curUserId);
            if (user == null) {
                return View("Error");
            }
            var editUserViewModel = new EditUserProfileViewModel
            {
                Id = curUserId,
                Name = user.Name,
                Surname = user.Surname,
                EmailAddress = user.Email,
                Phone = user.Phone,
                Image = user.Image

            };
            return View(editUserViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserProfileViewModel editUserProfile)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Niepowodzenie w edycji profilu");
                return View("EditUserProfile", editUserProfile);
            }
            var user = await _userService.GetIdByNoTracking(editUserProfile.Id);

            if (user == null)
            {
                TempData["Error"] = "Nie znaleziono użytkownika.";
                return RedirectToAction("Index", "Home");
            }


            user.Name = editUserProfile.Name;
            user.Surname = editUserProfile.Surname;
            user.Email = editUserProfile.EmailAddress;
            user.Phone = editUserProfile.Phone;
            user.Image = editUserProfile.Image;

            if (!string.IsNullOrEmpty(editUserProfile.CurrentPassword) && !string.IsNullOrEmpty(editUserProfile.NewPassword) && !string.IsNullOrEmpty(editUserProfile.ConfirmNewPassword))
            {
                var passwordCheck = await _userService.VerifyPasswordAsync(user, editUserProfile.CurrentPassword);
                if (!passwordCheck)
                {
                    ModelState.AddModelError("", "Aktualne hasło jest nieprawidłowe.");
                    return View("EditUserProfile", editUserProfile);
                }

                if (editUserProfile.NewPassword != editUserProfile.ConfirmNewPassword)
                {
                    ModelState.AddModelError("", "Nowe hasło i potwierdzenie nowego hasła nie są zgodne.");
                    return View("EditUserProfile", editUserProfile);
                }

                var passwordUpdateResult = await _userService.UpdatePasswordAsync(user, editUserProfile.NewPassword);
                if (!passwordUpdateResult)
                {
                    ModelState.AddModelError("", "Nie udało się zaktualizować hasła.");
                    return View("EditUserProfile", editUserProfile);
                }
            }

            _userService.UpdateUser(user);

            TempData["Success"] = "Profil został zaktualizowany.";
            return RedirectToAction("Index", "Car");
        }
    }
}
