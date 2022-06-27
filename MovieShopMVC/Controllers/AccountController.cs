using System.Diagnostics;
using System.Security.Claims;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;

namespace MovieShopMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel model)
        {
            var user = await _accountService.ValidateUser(model.Email, model.Password);
            if (user != null)
            {
                // redirect to home page
                // create a cookie, cookies are always sent from browser automatically to server
                // Inside the cookie we store encrypted information (User claims) that Server can recognize and tell whether user is loged in or not
                // Cookie should have an expration time
                // 2 hours
                var claims = new List<Claim>
                {
                    new Claim( ClaimTypes.Email, model.Email ),
                    new Claim(ClaimTypes.Surname, user.FirstName),
                    new Claim(ClaimTypes.GivenName,user.LastName),
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                    new Claim(ClaimTypes.DateOfBirth, user.DateOfBirth.ToShortDateString()),
                    new Claim(ClaimTypes.Country, "USA"),
                    new Claim("Language", "English"),
                };

                var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                return LocalRedirect("~/");


            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Register()
        {
            // show the empty register Page when we make a GET Request
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterModel model)
        {
            // Model Binding 
            var user = await _accountService.RegisterUser(model);
            // redirect to logn page
            return RedirectToAction("Login");
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Login");
        }

    }
}
