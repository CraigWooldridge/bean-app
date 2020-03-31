using System.Threading.Tasks;
using BeanApp.Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BeanApp.API.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager; 
            _signInManager = signInManager;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await _signInManager.SignOutAsync();
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("HttpStatusCodeHandler, Error");
            }
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUser login)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var result = await _signInManager.PasswordSignInAsync(login.UserName, login.Password, login.RememberMe, false);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("index", "beans");
                    }

                    ModelState.AddModelError(string.Empty, "Invalid login attempt, please try again");
                }
                return View(login);
            }
            catch
            {
                return RedirectToAction("HttpStatusCodeHandler, Error");
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUser register)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    var user = new IdentityUser
                    {
                        UserName = register.UserName
                    };

                    var result = await _userManager.CreateAsync(user, register.Password);

                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return RedirectToAction("index", "beans");
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

                return View(register);
            }
            catch
            {
                return RedirectToAction("HttpStatusCodeHandler, Error");
            }
        }
    }
}
    