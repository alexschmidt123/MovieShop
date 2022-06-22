using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;

namespace MovieShopMVC.Controllers
{
    public class AccountController: Controller
    {
        [HttpGet]
        public async Task<IActionResult> Register()
        {
            return View();
        }
    }
}
