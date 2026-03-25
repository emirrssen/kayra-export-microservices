using KayraExport.Microservices.Services.Auth.Application.Helpers;
using KayraExport.Microservices.Services.Auth.Application.Services.Abstracts;
using KayraExport.Microservices.Services.Auth.Domain.DTOs.Token;
using KayraExport.Microservices.Services.Auth.Domain.Entities;
using Microsoft.IdentityModel.Tokens;
using NETCore.Encrypt;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KayraExport.Microservices.Services.Auth.Infrastructure.Services.Concretes
{
    public class TokenService : ITokenService
    {
        /// <summary>
        /// Kullanıcı bilgilerini ve ortam değişkenlerindeki kimlik doğrulama ayarlarını kullanarak
        /// yeni bir Access Token ve Refresh Token çifti üretir. Token içerisindeki hassas alanlar şifrelenir.
        /// </summary>
        /// <param name="user">Token'ı üretilecek olan kullanıcı nesnesi.</param>
        /// <returns>Üretilen token bilgilerini içeren TokenResponse nesnesi döner.</returns>
        public Task<TokenResponse> CreateTokenAsync(User user)
        {
            var securityKeyStr = EnvironmentHelper.JwtSecurityKey;
            var issuer = EnvironmentHelper.JwtIssuer;
            var audience = EnvironmentHelper.JwtAudience;

            double accessTokenExpirationMinutes = double.TryParse(EnvironmentHelper.JwtAccessTokenExpiration, out var aExp) ? aExp : 60;
            double refreshTokenExpirationDays = double.TryParse(EnvironmentHelper.JwtRefreshTokenExpiration, out var rExp) ? rExp : 7;

            var accessTokenExpiration = DateTimeHelper.GetNowByTurkiyeTimeZone().AddMinutes(accessTokenExpirationMinutes);
            var refreshTokenExpiration = DateTimeHelper.GetNowByTurkiyeTimeZone().AddDays(refreshTokenExpirationDays);

            var aesKey = EncryptHelper.FormatEncryptionKey(securityKeyStr);
            var encryptedUserId = EncryptProvider.AESEncrypt(user.Id.ToString(), aesKey);

            var claims = new List<Claim> { new("UserId", encryptedUserId) };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKeyStr));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = accessTokenExpiration,
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var accessTokenStr = tokenHandler.WriteToken(token);

            var refreshTokenStr = Guid.NewGuid().ToString("N");

            return Task.FromResult(new TokenResponse(
                accessTokenStr,
                refreshTokenStr,
                accessTokenExpiration,
                refreshTokenExpiration
            ));
        }
    }
}
