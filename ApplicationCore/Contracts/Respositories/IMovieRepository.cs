using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Respositories
{
    public interface IMovieRepository: IRepository<Movie>
    {
        Task<IEnumerable<Movie>> Get30HighestGrossingMovies();
        Task<IEnumerable<Movie>> Get30HighestRatedMovies();
        Task<IEnumerable<Movie>> GetMoviesByGenre(int id, int pageSize, int pageNumber);
    }
}
