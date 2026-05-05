using NecroFinances.Application.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace NecroFinances.Application.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> RegisterAsync(UserDTO userDTO);
        Task<UserModel> AuthenticateAsync(UserDTO userDTO);
        void Logout(long userID);
    }
}
