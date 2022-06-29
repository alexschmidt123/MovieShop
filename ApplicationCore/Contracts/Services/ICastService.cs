using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
    public interface ICastService
    {
        //all the business functionality methdos pertaining to Movies
        //Movie
        Task<CastDetailsModel> GetCastDetails(int id);
    }
}
