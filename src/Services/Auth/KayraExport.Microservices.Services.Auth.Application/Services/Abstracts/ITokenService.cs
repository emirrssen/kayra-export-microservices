using KayraExport.Microservices.Services.Auth.Domain.DTOs.Token;
using KayraExport.Microservices.Services.Auth.Domain.Entities;

namespace KayraExport.Microservices.Services.Auth.Application.Services.Abstracts
{
    public interface ITokenService
    {
        /// <summary>
        /// Verilen kullanıcı bilgileri doğrultusunda JWT (Access Token) ve Refresh Token çifti üretir.
        /// </summary>
        /// <param name="user">Tokenları üretilecek kullanıcı</param>
        /// <returns>Erişim ve yenileme tokenlarını içeren TokenResponse döner.</returns>
        Task<TokenResponse> CreateTokenAsync(User user);
    }
}
