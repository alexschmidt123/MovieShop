using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository<Movie> _movieRepository;
        public AdminService(IRepository<Movie> movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public async Task<bool> AddMovie(MovieCardModel movieCardModel)
        {
            var movie = new Movie();
            movie.Id = movieCardModel.Id;
            movie.Title = movieCardModel.Title;
            movie.PosterUrl = movieCardModel.PosterUrl;
            var addedMovie = await _movieRepository.Add(movie);
            if (addedMovie != null)
            {
                return true;
            }
            return false;
        }
    }
}
