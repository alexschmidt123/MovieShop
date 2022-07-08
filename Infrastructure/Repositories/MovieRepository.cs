using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext): base(dbContext)
        {

        }
        public async Task<IEnumerable<Movie>> Get30HighestGrossingMovies(int pageNumber, int pageSize = 30)
        {
            var movieNumber = await _dbContext.Movies.CountAsync();
            var maxPageNumber = (int)Math.Ceiling(movieNumber / (double)pageSize);
            if (pageNumber > maxPageNumber)
            {
                pageNumber = 1;
            }
            else if (pageNumber <= 0)
            {
                pageNumber = maxPageNumber;
            }
            // LINQ code to get top 30 grossing movies 
            // select top 30 * from Movie order by Revenue
            // I/O bound operation
            //await
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Movie>> Get30HighestRatedMovies(int pageNumber, int pageSize=30)
        {
            var movieNumber = await _dbContext.Movies.CountAsync();
            var maxPageNumber= (int)Math.Ceiling(movieNumber / (double)pageSize);
            if (pageNumber > maxPageNumber)
            {
                pageNumber = 1;
            }else if(pageNumber <= 0)
            {
                pageNumber = maxPageNumber;
            }
            var movies = await _dbContext.Movies
                .Include(m => m.Reviews)
                .OrderByDescending(m => m.Reviews.Average(re=>re.Rating))
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();
            return movies;
        }


        public async Task<IEnumerable<Movie>> GetAllMovies()
        {
            var movies = await _dbContext.Movies.ToListAsync();
            return movies;
        }
        

        public async override Task<Movie> GetById(int id)
        {
            //select * from Movie
            //join cast and Moviecast
            //join Trailer
            //join Genre and MovieGenre
            //where id = id
            var movieDetails = await _dbContext.Movies
                .Include(m => m.GenresOfMovie).ThenInclude(m => m.Genre)
                .Include(m => m.MovieCasts).ThenInclude(m => m.Cast)
                .Include(m => m.Trailers)
                .Include(m => m.Reviews)
                .FirstOrDefaultAsync(m => m.Id == id);
            //FirstOrDefault
            //First = ex when there are no matching records

            //SingleOrDefault ok for 0 or 1, ex when more than one matching record
            //Single ex when more than one matching record
            return movieDetails;
        }

        public async Task<IEnumerable<Review>> GetMovieReviewsById(int id)
        {
            var reviews = await _dbContext.Movies
               .Include(m => m.Reviews)
               .FirstOrDefaultAsync(m => m.Id == id);
            return reviews.Reviews;
        }

        public async Task<PagedResultSetModel<Movie>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1)
        {
            // get total count movies for the genre
            var totalMoviesForGenre = await _dbContext.MovieGenres.Where(g => g.GenreId == genreId).CountAsync();

            var movies = await _dbContext.MovieGenres
                .Where(g => g.GenreId == genreId)
                .Include(g => g.Movie)
                .OrderByDescending(mg => mg.Movie.Revenue)
                .Select(mg => new Movie { Id = mg.MovieId, PosterUrl = mg.Movie.PosterUrl, Title = mg.Movie.Title })
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize).ToListAsync();

            var pagedMovies = new PagedResultSetModel<Movie>(pageNumber, totalMoviesForGenre, pageSize, movies);
            return pagedMovies;

        }
    }
}
