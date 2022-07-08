using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IMovieRepository: IRepository<Movie>
    {
        Task<IEnumerable<Movie>> Get30HighestGrossingMovies(int pageNumber, int pageSize = 30);
        Task<IEnumerable<Movie>> Get30HighestRatedMovies(int pageNumber,int pageSize = 30);
        Task<PagedResultSetModel<Movie>> GetMoviesByGenre(int genreId, int pageSize = 30, int pageNumber = 1);

        Task<IEnumerable<Movie>> GetAllMovies();
        Task<IEnumerable<Review>> GetMovieReviewsById(int id);


    }
}
