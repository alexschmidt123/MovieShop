using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;

namespace MovieShopMVC.Controllers
{
    [Authorize]
    //a Microsoft filter to check if user is authorized
    public class UserController: Controller
    {
        // all these action methods should only be execute when user is loged in

        [HttpGet]
        public async Task<IActionResult> Purchases()
        {
            //go to database and get all the movies purchased by user, user id
            //var cookie =this.HttpContext.Request.Cookies["MovieShopAuthCookie"];

            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Favorites()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddReview()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddFavorite()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> BuyMovie()
        {
            return View();
        }
    }
}
