using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
    public class MovieTestService : IMovieService
    {
        public Task<List<MovieCardModel>> Genre(int genreId, int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public Task<List<MovieCardModel>> GetAllMovies()
        {
            throw new NotImplementedException();
        }

        public Task<PagedResultSetModel<MovieCardModel>> GetMovieByGenre(int genreId, int pageSize = 30, int pageNumber = 1)
        {
            throw new NotImplementedException();
        }

        public async Task<MovieDetailsModel> GetMovieDetails(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<PagedResultSetModel<MovieCardModel>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1)
        {
            throw new NotImplementedException();
        }

        public async Task<List<MovieCardModel>> GetTopGrossingMovies()
        {
            // going to no SQL database and getting the data
            var movies = new List<MovieCardModel> {
                new MovieCardModel { Id = 11, PosterUrl = "http://images.tmdb.org/t/p/w342//9gk7adHYeDvHkCSEqAvQNLV5Uge.jpg", Title = "Inception"},
                new MovieCardModel { Id = 12, PosterUrl = "", Title = ""},
                new MovieCardModel { Id = 13, PosterUrl = "", Title = ""},
                new MovieCardModel { Id = 14, PosterUrl = "", Title = ""},
                new MovieCardModel { Id = 15, PosterUrl = "", Title = ""}
            };
            return movies;
        }
    }
  
}
