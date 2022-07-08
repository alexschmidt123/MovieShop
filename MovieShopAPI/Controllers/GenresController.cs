using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MovieShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenresController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _genreService.GetAllGenres();
            if (genres == null || !genres.Any())
            {
                // 404
                return NotFound(new { errorMessage = "No Genres Found" });
            }

            return Ok(genres);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetGenreById(int id)
        {
            var genre = await _genreService.GetGenreById(id);
            if (genre == null)
            {
                // 404
                return NotFound(new { errorMessage = "No Genre Found" });
            }

            return Ok(genre);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddGenre([FromBody] GenreModel genreModel)
        {
            var addedGenre = await _genreService.AddGenre(genreModel);
            return Ok(addedGenre);
        }

        [HttpDelete]
        [Route("Delete/{genreId:int}")]
        public async Task<IActionResult> DeleteGenre(int genreId)
        {
            var deletedGenre = await _genreService.DeleteGenre(genreId);
            return Ok(deletedGenre);
        }

        [HttpGet]
        [Route("{id:int}/Movies")]
        public async Task<IActionResult> GetMoviesByGenre(int id)
        {
            var movies = await _genreService.GetMoviesByGenre(id);
            if (movies == null)
            {
                // 404
                return NotFound(new { errorMessage = "No Movies Found in this Genre" });
            }

            return Ok(movies);
        }


    }
}
