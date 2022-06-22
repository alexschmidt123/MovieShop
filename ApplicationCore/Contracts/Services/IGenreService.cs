using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
    public interface IGenreService
    {
        //all the business functionality methdos pertaining to Movies
        //Movie
        Task<IEnumerable<GenreModel>> GetAllGenres();
    }
}
