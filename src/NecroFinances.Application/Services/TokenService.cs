using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using NecroFinances.Application.Interfaces;
using NecroFinances.Application.Interfaces.Repositories;
using NecroFinances.Application.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;


namespace NecroFinances.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwt;
        private readonly IUserRepositorie _userRepositorie;
        public TokenService(
            IOptions<JwtSettings> jwt,
            IUserRepositorie userRepositorie
            )
        {
            _jwt= jwt.Value;
            _userRepositorie = userRepositorie;
        }

        public TokenResponse GenerateToken(UserModel user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.ID.ToString()),
                new Claim(ClaimTypes.Name, user.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.TokenKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.AccessTokenExpirationTimeMinutes),
                signingCredentials: creds);

            return new TokenResponse
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = GenerateRefreshToken(user)
            };
        }

        public string GenerateRefreshToken(UserModel user)
        {
            var randomNumber = new byte[64];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            var refreshToken = Convert.ToBase64String(randomNumber);
            _userRepositorie.UpdateRefreshToken(user.ID, refreshToken);

            return refreshToken;
        }

        public TokenResponse RefreshToken(string refreshToken)
        {
            UserModel user = _userRepositorie.GetByUserIdAndRefreshToken(refreshToken);
            return GenerateToken(user);
        }
    }
}
