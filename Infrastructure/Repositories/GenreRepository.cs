using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(MovieShopDbContext dbContext) : base(dbContext)
        {
        }
        public async override Task<Genre> GetById(int id)
        {
            var genreDetails = await _dbContext.Genres
               .Include(m => m.MoviesOfGenre).ThenInclude(m => m.Movie)
               .FirstOrDefaultAsync(m => m.Id == id);
            return genreDetails;
        }

        public async Task<List<Movie>> GetMoviesByGenre(int genreId)
        {
            var movies = await _dbContext.MovieGenres.Where(g => g.GenreId == genreId)
                .Include(g => g.Movie)
                .OrderByDescending(mg => mg.Movie.Revenue)
                .Select(mg => new Movie { Id = mg.MovieId, PosterUrl = mg.Movie.PosterUrl, Title = mg.Movie.Title })
                .ToListAsync();
            return movies;
        }
    }
}
