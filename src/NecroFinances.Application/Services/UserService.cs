using Microsoft.AspNetCore.Identity;
using NecroFinances.Application.Interfaces;
using NecroFinances.Application.Interfaces.Repositories;
using NecroFinances.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecroFinances.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepositorie _repositorie;
        private readonly PasswordHasher<UserModel> _passwordHasher = new();
        public UserService(
            IUserRepositorie repositorie
            )
        {
            _repositorie = repositorie;
        }

        public async Task<UserModel> RegisterAsync(UserDTO userDTO)
        {
            if (await _repositorie.IsRegisteredAsync(userDTO.Username) != null)
            {
                return null;
            }

            var user = new UserModel
            {
                Username = userDTO.Username
            };

            user.Password = _passwordHasher.HashPassword(user, userDTO.Password);
            _repositorie.AddNewUser(user);

            return user;
        }

        public async Task<UserModel> AuthenticateAsync(UserDTO userDTO)
        {
            UserModel user = await _repositorie.IsRegisteredAsync(userDTO.Username);
            if (user == null)
            {
                return null;
            }

            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, userDTO.Password);


            return result == PasswordVerificationResult.Success ? user : null;
        }

        public void Logout(long userID)
        {
            UserModel user = _repositorie.GetUserById(userID);
            if (user == null)
            {
                return;
            }

            user.RefreshTokenExpiryTime = DateTime.UtcNow;
            _repositorie.UpdateUser(user);
        }
    }
}
