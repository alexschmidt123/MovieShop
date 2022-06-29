using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;

namespace Infrastructure.Services
{
    public class UserService : IUserService
    {
        public Task AddFavorite(FavoriteRequestModel favoriteRequest)
        {
            throw new NotImplementedException();
        }

        public Task AddMovieReview(ReviewRequestModel reviewRequest)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMovieReview(int userId, int movieId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> FavoriteExists(int id, int movieId)
        {
            throw new NotImplementedException();
        }

        public Task<List<MovieCardModel>> GetAllPurchasesForUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ReviewModel>> GetAllReviewsByUser(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<MovieDetailsModel>> GetPurchasesDetails(int userId, int movieId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> IsMoviePurchased(PurchaseRequestModel purchaseRequest, int userId)
        {
            throw new NotImplementedException();
        }

        public Task PurchaseMovie(PurchaseRequestModel purchaseRequest, int userId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveFavorite(FavoriteRequestModel favoriteRequest)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMovieReview(ReviewRequestModel reviewRequest)
        {
            throw new NotImplementedException();
        }
    }
}
