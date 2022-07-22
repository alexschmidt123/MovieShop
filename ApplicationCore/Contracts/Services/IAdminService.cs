using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
    public interface IAdminService
    {
        //all the business functionality methdos pertaining to Movies
        //Movie
        Task<bool> AddMovie(MovieCardModel movieCardModel);
        //Task<MovieDetailsModel> UpdateMovie(MovieDetailsModel movieDetailsModel, MovieCardModel movieCardModel);
    }
}
