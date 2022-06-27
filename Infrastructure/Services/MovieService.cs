using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;

        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }

        public async Task<MovieDetailsModel> GetMovieDetails(int id)
        {
            var movieDetails = await _movieRepository.GetById(id);
            if (movieDetails == null)
            {
                return null;
            }
            var movie = new MovieDetailsModel
            {
                Id = movieDetails.Id,
                Tagline = movieDetails.Tagline,
                Title = movieDetails.Title,
                Overview = movieDetails.Overview,
                PosterUrl = movieDetails.PosterUrl,
                BackdropUrl = movieDetails.BackdropUrl,
                ImdbUrl = movieDetails.ImdbUrl,
                RunTime = movieDetails.RunTime,
                TmdbUrl = movieDetails.TmdbUrl,
                Revenue = movieDetails.Revenue,
                Budget = movieDetails.Budget,
                ReleaseDate = movieDetails.ReleaseDate,
                Price = movieDetails.Price,
            };
            foreach (var genre in movieDetails.GenresOfMovie)
            {
                movie.Genres.Add(new GenreModel { Id = genre.GenreId, Name = genre.Genre.Name });
            }

            foreach (var trailer in movieDetails.Trailers)
            {
                movie.Trailers.Add(new TrailerModel { Id = trailer.Id, Name = trailer.Name, TrailerUrl = trailer.TrailerUrl });
            }

            foreach (var moviecast in movieDetails.MovieCasts)
            {
                movie.Casts.Add(new CastModel { Id = moviecast.CastId, Name = moviecast.Cast.Name, Gender = moviecast.Cast.Gender, ProfilePath = moviecast.Cast.ProfilePath, TmdbUrl = moviecast.Cast.TmdbUrl, Character = moviecast.Character });
            }

            return movie;
        }

        //method that returns top movies to the caller
        //movie list 

        public async Task<PagedResultSetModel<MovieCardModel>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1)
        {
            var movies = await _movieRepository.GetMoviesByGenre(genreId, pageSize, pageNumber);

            var movieCards = new List<MovieCardModel>();

            foreach (var movie in movies.PagedData)
            {
                movieCards.Add(new MovieCardModel { Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title });
            }

            return new PagedResultSetModel<MovieCardModel>(pageNumber, movies.TotalRecords, pageSize, movieCards);
        }

        // method that return top movies to the caller
        // list of movies

        public async Task<List<MovieCardModel>> GetTopGrossingMovies()
        {
            var movies = await _movieRepository.Get30HighestGrossingMovies();

            var movieCards = new List<MovieCardModel>();

            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardModel { Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title });
            }

            return movieCards;
        }
    }
}
