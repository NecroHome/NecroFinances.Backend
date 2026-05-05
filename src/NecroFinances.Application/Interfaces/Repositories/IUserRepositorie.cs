using NecroFinances.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecroFinances.Application.Interfaces.Repositories
{
    public interface IUserRepositorie
    {
        Task<UserModel> IsRegisteredAsync(string username);
        Task<UserModel> AddNewUser(UserModel user);
        void UpdateRefreshToken(long userID, string refreshToken);
        UserModel GetByUserIdAndRefreshToken(string refreshToken);
        UserModel GetUserById(long userID);
        UserModel UpdateUser(UserModel user);
    }
}
