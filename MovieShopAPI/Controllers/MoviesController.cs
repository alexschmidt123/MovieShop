using ApplicationCore.Contracts.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllMovies()
        {
            var movies = await _movieService.GetAllMovies();
            if (movies == null || !movies.Any())
            {
                // 404
                return NotFound(new { errorMessage = "No Movies Found" });
            }

            return Ok(movies);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await _movieService.GetMovieDetails(id);
            if (movie == null)
            {
                return NotFound(new { errorMessage = $"No Movie Found for id: {id}" });
            }

            return Ok(movie);
        }

        [HttpGet]
        [Route("top-rated/Page/{pageNum:int}")]
        public async Task<IActionResult> GetTopRatedMovies(int pageNum, int pageSize = 30)
        {
            var movies = await _movieService.GetTopRatedMovies(pageNum,pageSize);
            if (movies == null || !movies.Any())
            {
                return NotFound(new { errorMessage = "No Movies Found" });
            }
            return Ok(movies);
        }

        [HttpGet]
        [Route("top-grossing/Page/{pageNum:int}")]
        // Attribute Routing
        // MVC http://localhost/movies/GetTopGrossingMovies => Traditional/Convention based routing
        // http://localhost/api/movies/top-grossing
        public async Task<IActionResult> GetTopGrossingMovies(int pageNum, int pageSize = 30)
        {
            // call my service
            var movies = await _movieService.GetTopGrossingMovies(pageNum, pageSize);
            // return the movies information in JSON Format
            // ASP.NET Core automatically serializes C# objects to JSOn objects
            // System.Text.Json .NET 3
            // older version of .NET to work with JSON Newtonsoft.JSON
            // return data(json format) along with it return the HTTP status code

            if (movies == null || !movies.Any())
            {
                // 404
                return NotFound(new { errorMessage = "No Movies Found" });
            }

            return Ok(movies);

        }

        [HttpGet]
        [Route("{id:int}/reviews")]
        public async Task<IActionResult> GetMovieReviewsById(int id)
        {
            var reviews = await _movieService.GetMovieDetails(id);
            if (reviews == null)
            {
                return NotFound(new { errorMessage = $"No Movie Reviews Found for id: {id}" });
            }
            return Ok(reviews.Reviews);
            //var reviews = await _movieService.GetMovieReviewsById(id);
            //if (reviews == null || !reviews.Any())
            //{
            //    return NotFound(new { errorMessage = "No Movie Reviews Found" });
            //}
            //return Ok(reviews);
        }
    }
}
