using Microsoft.EntityFrameworkCore;
using NecroFinances.Application.Interfaces.Repositories;
using NecroFinances.Application.Models;
using NecroFinances.Infrastructure.Persistence;

namespace NecroFinances.Infrastructure.Repositories
{
    public class UserRepositorie : IUserRepositorie
    {
        private readonly AppDbContext _context;
        public UserRepositorie(
            AppDbContext context
            )
        {
            _context = context;
        }
        public async Task<UserModel> AddNewUser(UserModel user)
        {
            _context.Usuarios.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<UserModel> IsRegisteredAsync(string username)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(f => f.Username == username);
        }

        public void UpdateRefreshToken(long userID, string refreshToken)
        {
            UserModel user = _context.Usuarios.FirstOrDefault(f => f.ID == userID);

            if (user == null)
            {
                throw new Exception("Usuario não existe");
            }

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddMonths(1);

            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public UserModel GetByUserIdAndRefreshToken(string refreshToken)
        {
            UserModel user = _context.Usuarios.Where(w => w.RefreshToken == refreshToken).FirstOrDefault();
            if (user == null)
            {
                throw new Exception("Não foi encontrado o Usuario para o Token");
            }

            if (user.RefreshTokenExpiryTime <= DateTime.UtcNow)
            {
                throw new Exception("Sessão expirada, por favor, faça o Login novamente.");
            }

            return user;
        }

        public UserModel GetUserById(long id)
        {
            return _context.Usuarios.FirstOrDefault(f => f.ID == id);
        }

        public UserModel UpdateUser(UserModel user)
        {
            _context.Entry(user).State = EntityState.Modified;
            return user;
        }
    }
}
