using System.Diagnostics;
using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using MovieShopMVC.Models;

namespace MovieShopMVC.Controllers
{
    public class MoviesController: Controller
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        //show the details of movie
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);
            return View(movie);
        }

        public async Task<IActionResult>Genre(int id, int pageSize = 30, int pageNumber = 1)
        {
            // call movie Service and get the data
            var pagedMovies = await _movieService.GetMoviesByGenre(id, pageSize, pageNumber);
            pagedMovies.GenreId = id;  
            return View("PagedMovies", pagedMovies);
        }
    }
}
