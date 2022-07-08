using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
    public interface IGenreService
    {
        //all the business functionality methdos pertaining to Movies
        //Movie
        Task<IEnumerable<GenreModel>> GetAllGenres();
        Task<bool> AddGenre(GenreModel genreModel);
        Task<GenreModel> GetGenreById(int id);
        Task<bool> DeleteGenre(int id);
        Task<List<MovieCardModel>> GetMoviesByGenre(int genreId);
    }
}
