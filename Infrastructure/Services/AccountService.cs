using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Entities;
using ApplicationCore.Exceptions;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;
        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> RegisterUser(UserRegisterModel model)
        {
            // check if the user already exists in the database, check email 
            // UserRepository
            var user = await _userRepository.GetUserByEmail(model.Email);
            if (user != null)
            {
                throw new ConflictException("Email already exists, please try to login");
            }
            var salt = GetRandomSalt();
            var hashedPassword = GetHashedPassword(model.Password,salt);
            var newUser = new User
            {
                Email = model.Email,
                HashedPassword = hashedPassword,
                Salt = salt,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth
            };

            // save the user to User table using UserRepository

            var savedUser =  await _userRepository.Add(newUser);
            if (savedUser.Id > 1)
            {
                return true;
            }
            return false;
        }

        public async Task<UserModel> ValidateUser(string email, string password)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                throw new Exception("Email does not exist!");
            }

            var hashedPassword = GetHashedPassword(password, user.Salt);

            if (hashedPassword == user.HashedPassword)
            {
                // good password
                var userModel = new UserModel
                {
                    Id = user.Id,
                    DateOfBirth = user.DateOfBirth.GetValueOrDefault(),
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
                return userModel;
            }
            return null;

        }

        private string GetRandomSalt()
        {
            var randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }

            return Convert.ToBase64String(randomBytes);
        }

        private string GetHashedPassword(string password, string salt)
        {
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(password,
           Convert.FromBase64String(salt),
           KeyDerivationPrf.HMACSHA512,
           10000,
           256 / 8));
            return hashed;
        }

        public async Task<bool> CheckEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null)
            {
                return false;
            } else
            {
                return true;
            }
        }
    }
}
