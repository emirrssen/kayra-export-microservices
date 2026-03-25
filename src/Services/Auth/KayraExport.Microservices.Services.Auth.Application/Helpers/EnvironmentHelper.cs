namespace KayraExport.Microservices.Services.Auth.Application.Helpers
{
    public class EnvironmentHelper
    {
        public static string GetEnvVariable(string name)
                => Environment.GetEnvironmentVariable(name) ?? "";

        // JWT Configurations

        /// <summary>
        /// JWT tokenlarını oluşturan yetkili sistem veya uygulama adı.
        /// Environment Variable: JWT_ISSUER
        /// </summary>
        public static string JwtIssuer => GetEnvVariable("JWT_ISSUER");

        /// <summary>
        /// JWT tokenlarının hitap ettiği hedef kitle/uygulama.
        /// Environment Variable: JWT_AUDIENCE
        /// </summary>
        public static string JwtAudience => GetEnvVariable("JWT_AUDIENCE");

        /// <summary>
        /// JWT tokenlarının imzalanmasında kullanılacak güvenli gizli anahtar (secret key).
        /// Environment Variable: JWT_SECURITY_KEY
        /// </summary>
        public static string JwtSecurityKey => GetEnvVariable("JWT_SECURITY_KEY");

        /// <summary>
        /// Access Token'ın geçerlilik süresi (dakika/saat cinsinden değeri parse etmeniz gerekebilir).
        /// Environment Variable: JWT_ACCESS_TOKEN_EXPIRATION
        /// </summary>
        public static string JwtAccessTokenExpiration => GetEnvVariable("JWT_ACCESS_TOKEN_EXPIRATION");

        /// <summary>
        /// Refresh Token'ın geçerlilik süresi (gün/saat cinsinden değeri parse etmeniz gerekebilir).
        /// Environment Variable: JWT_REFRESH_TOKEN_EXPIRATION
        /// </summary>
        public static string JwtRefreshTokenExpiration => GetEnvVariable("JWT_REFRESH_TOKEN_EXPIRATION");
    }
}
