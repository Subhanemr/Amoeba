using Amoeba.Areas.Admin.ViewModels;
using Amoeba.Areas.Admin.ViewModels.Account;
using Amoeba.DAL;
using Amoeba.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Plugins;

namespace Amoeba.Areas.Admin.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public AccountController(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginVM login)
        {
            if (!ModelState.IsValid) return View(login);

            AppUser user = await _userManager.FindByNameAsync(login.UserNameOrPassword);
            if(user == null)
            {
                user = await _userManager.FindByEmailAsync(login.UserNameOrPassword);
                if(user == null)
                {
                    ModelState.AddModelError(string.Empty, "Username, Email or Password wrong");
                    return View(user);
                }
            }
            var result = await _signInManager.PasswordSignInAsync(user, login.Password, false,true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "User Loked out");
                return View(login);

            }
            if (result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Username, Email or Password is incorrect");
                return View(login);
            }
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterVM register)
        {
            if (!ModelState.IsValid) return View(register);
            AppUser user = new AppUser
            {
                Name = register.Name,
                Surname = register.Surname,
                UserName = register.UserName,
                Email = register.Email
            };

            var result = await _userManager.CreateAsync(user, register.Password);
            if(result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View(register);
            }

            return RedirectToAction(nameof(Login));
        }
    }
}
