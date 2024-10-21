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
        private readonly CarRentDBContext _context;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
            CarRentDBContext context)
        {
            _context = context;
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
                //Password is incorrect
                TempData["Error"] = "Złe hasło";
                return View(loginViewModel);
            }
            //User not found
            TempData["Error"] = "NIe ma takiego użytkownika";
            return View(loginViewModel);
        }
    }
}
