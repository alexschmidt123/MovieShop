using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
    public interface IAccountService
    {
        //all the business functionality methdos pertaining to Movies
        //Movie
        Task<bool> RegisterUser(UserRegisterModel model);
        Task<UserModel> ValidateUser(string email, string password);
    }
}
