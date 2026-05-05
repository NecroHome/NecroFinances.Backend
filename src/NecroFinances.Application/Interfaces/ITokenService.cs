using NecroFinances.Application.Models;

namespace NecroFinances.Application.Interfaces
{
    public interface ITokenService
    {
        TokenResponse GenerateToken(UserModel user);
        string GenerateRefreshToken(UserModel user);
        TokenResponse RefreshToken(string refreshToken);
    }
}
