using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        private readonly IMovieService _movieService;

        public AdminController(IAdminService adminService, IMovieService movieService)
        {
            _adminService = adminService;
           _movieService = movieService;
        }

        [HttpPost]
        [Route("AddMovie")]
        public async Task<IActionResult> AddMovie([FromBody] MovieCardModel movieCardModel)
        {
            var addedMovie = await _adminService.AddMovie(movieCardModel);
            return Ok(addedMovie);
        }

        //[HttpPut]
        //[Route("UpdateMovie/{id:int}")]
        //public async Task<IActionResult> UpdateMovie(int id, MovieCardModel movieCardModel)
        //{
        //    var movie = await _movieService.GetMovieDetails(id);
        //    var updatedMovie = await _adminService.UpdateMovie(movie, movieCardModel);
        //    return Ok(updatedMovie);
        //}
    }
}
