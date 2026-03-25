namespace KayraExport.Microservices.Services.Auth.Domain.DTOs.Token
{
    public record TokenResponse(
        string AccessToken,
        string RefreshToken,
        DateTime AccessTokenExpiration,
        DateTime RefreshTokenExpiration
    );

}
