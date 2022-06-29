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
    public class CastService: ICastService
    {
        private readonly ICastRepository _castRepository;

        public CastService(ICastRepository castRepository)
        {
            _castRepository = castRepository;
        }

        public async Task<CastDetailsModel> GetCastDetails(int id)
        {
            var castDetails = await _castRepository.GetById(id);
            if (castDetails == null)
            {
                return null;
            }
            var cast = new CastDetailsModel
            {
                Id = castDetails.Id,
                Name= castDetails.Name,
                ProfilePath = castDetails.ProfilePath,
                TmdbUrl = castDetails.TmdbUrl,
                Gender=castDetails.Gender,
            };

            foreach (var castmovie in castDetails.CastMovies)
            {
                cast.Movies.Add(new MovieDetailsModel { Id = castmovie.MovieId, Title = castmovie.Movie.Title, PosterUrl = castmovie.Movie.PosterUrl});
            }

            return cast;
        }
    }
}
