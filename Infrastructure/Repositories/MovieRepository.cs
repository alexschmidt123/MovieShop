using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Respositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieShopDbContext dbContext): base(dbContext)
        {

        }
        public async Task<IEnumerable<Movie>> Get30HighestGrossingMovies()
        {

            // LINQ code to get top 30 grossing movies
            // select top 30 * from Movie order by Revenue
            // I/O bound operation
            //await
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
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
                .FirstOrDefaultAsync(m => m.Id == id);
            //FirstOrDefault
            //First = ex when there are no matching records

            //SingleOrDefault ok for 0 or 1, ex when more than one matching record
            //Single ex when more than one matching record
            return movieDetails;
        }

        public async Task<IEnumerable<Movie>> GetMoviesByGenre(int id, int pageSize, int pageNumber)
        {
            //ICollection < MovieGenre > MoviesOfGenre = 
            //// LINQ code to get all movies whose genre list contain the genre whiose Id is id 
            ////await
            //int numberOfMovieByGenre= await _dbContext.Movies
            //    .Include(m => m.GenresOfMovie).ThenInclude(m => m.Genre)
            //    .FirstOrDefaultAsync(m => m.GenresOfMovie.ToList<Genre>
            //    .Count();
            //int maxPageNumber = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(numberOfMovieByGenre / pageSize)));
            //if (pageNumber > maxPageNumber)
            //{
            //    pageNumber = maxPageNumber;
            //}

            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(30).ToListAsync();
            return movies;
        }

        Task<IEnumerable<Movie>> IMovieRepository.Get30HighestRatedMovies()
        {
            throw new NotImplementedException();
        }
    }
}
