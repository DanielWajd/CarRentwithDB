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

        public AccountController(IUserService userService, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            CarRentDBContext context)
        {
            _userService = userService;
            _userManager = userManager;
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
            TempData["Error"] = "NIe ma takiego użytkownika";
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
                return View(registerViewModel);
            }

            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email is already in use";
                return View(registerViewModel);
            }
            var newUser = new AppUser()
            {
                Name = registerViewModel.Name,
                Surname = registerViewModel.Surname,
                Phone = registerViewModel.Phone,
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress,
                UserType = registerViewModel.UserType
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (newUserResponse.Succeeded)
            {
                
                if (registerViewModel.UserType == UserType.Customer)
                {
                    await _userManager.AddToRoleAsync(newUser, UserRoles.Customer);
                }
                else if (registerViewModel.UserType == UserType.Employee)
                {
                    await _userManager.AddToRoleAsync(newUser, UserRoles.Employee);
                }

                return RedirectToAction("Index", "Home");
            }
            else
            {
                // Error in creating account
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
            Console.WriteLine($"Liczba wypożyczeń: {user.Rentals?.Count()}");
            var userDetailViewModel = new UserDetailViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
                Phone = user.Phone,
                UserType = user.UserType,
                Cars = user.Cars ?? new List<Car>(),  
                Rentals = user.Rentals ?? new List<Rental>()
            };
            return View(userDetailViewModel);
        }
    }
}
