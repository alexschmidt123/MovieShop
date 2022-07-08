using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Models;
using Infrastructure.Repositories;

namespace Infrastructure.Services
{
    public class GenreService: IGenreService
    {
        private readonly IRepository<Genre> _genreRepository;
        private readonly IGenreRepository _GenreRepository;
        public GenreService(IRepository<Genre> genreRepository, IGenreRepository GenreRepository)
        {
            _genreRepository = genreRepository;
            _GenreRepository = GenreRepository;
        }

        public async Task<IEnumerable<GenreModel>> GetAllGenres()
        {
            var genres = await _genreRepository.GetAll();
            var genresModel = genres.Select(g => new GenreModel { Id = g.Id, Name = g.Name });
            return genresModel;
        }


        public async Task<bool> AddGenre(GenreModel genreModel)
        {
            var genre = new Genre();
            genre.Id = genreModel.Id;
            genre.Name = genreModel.Name;
            var addedGenre = await _genreRepository.Add(genre);
            if (addedGenre.Id > 0)
            {
                return true; 
            }
            return false;   
        }

        public async Task<GenreModel> GetGenreById(int id)
        {
            var genre = await _GenreRepository.GetById(id);
            var genreModel = new GenreModel();
            genreModel.Id = id;
            genreModel.Name = genre.Name;
            return genreModel;
        }
        public async Task<bool> DeleteGenre(int GenreId)
        {
            var deletedGenre = await _GenreRepository.GetById(GenreId);
            var deleted = await _genreRepository.Delete(deletedGenre);
            if (deleted.Id > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<List<MovieCardModel>> GetMoviesByGenre(int genreId)
        {
            var movies = await _GenreRepository.GetMoviesByGenre(genreId);
            var movieCards = new List<MovieCardModel>();
            foreach (var movie in movies)
            {
                movieCards.Add(new MovieCardModel { Id = movie.Id, PosterUrl = movie.PosterUrl, Title = movie.Title });
            }
            return movieCards;
        }
    }
}
