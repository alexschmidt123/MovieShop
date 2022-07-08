using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
    public interface IMovieService
    {
        Task<List<MovieCardModel>> GetAllMovies();
        Task<List<MovieCardModel>> GetTopGrossingMovies(int pageNumber, int pageSize = 30);

        Task<List<MovieCardModel>> GetTopRatedMovies(int pageNumber, int pageSize = 30);

        // get movie details
        Task<MovieDetailsModel> GetMovieDetails(int id);
        Task<PagedResultSetModel<MovieCardModel>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1);

        Task<List<ReviewModel>> GetMovieReviewsById(int id);
    }
}
