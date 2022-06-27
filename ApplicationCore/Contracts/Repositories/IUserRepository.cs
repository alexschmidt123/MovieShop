using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Entities;

namespace ApplicationCore.Contracts.Repositories
{
    public interface IUserRepository:IRepository<User>
    {
        Task<User> GetUserByEmail(string email);
        Task<bool> CheckIfMoviePurchasedByUser(int userId, int movieId);
    }
}
