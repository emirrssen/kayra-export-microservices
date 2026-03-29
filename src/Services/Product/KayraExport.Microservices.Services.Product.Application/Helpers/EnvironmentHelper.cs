namespace KayraExport.Microservices.Services.Product.Application.Helpers
{
    public class EnvironmentHelper
    {
        public static string GetEnvVariable(string name)
                => Environment.GetEnvironmentVariable(name) ?? "";

        /// <summary>
        /// PostgreSql bağlantısının yapılabilmesi için connection string değeridir.
        /// </summary>
        public static string PostgreSqlConnectionString => GetEnvVariable("KAYRA_POSTGRESQL_CNN");

        /// <summary>
        /// JWT tokenlarını oluşturan yetkili sistem veya uygulama adı.
        /// Environment Variable: JWT_ISSUER
        /// </summary>
        public static string JwtIssuer => GetEnvVariable("KAYRA_JWT_ISSUER");

        /// <summary>
        /// JWT tokenlarının hitap ettiği hedef kitle/uygulama.
        /// Environment Variable: JWT_AUDIENCE
        /// </summary>
        public static string JwtAudience => GetEnvVariable("KAYRA_JWT_AUDIENCE");

        /// <summary>
        /// JWT tokenlarının imzalanmasında kullanılacak güvenli gizli anahtar (secret key).
        /// Environment Variable: JWT_SECURITY_KEY
        /// </summary>
        public static string JwtSecurityKey => GetEnvVariable("KAYRA_JWT_SECURITY_KEY");

        /// <summary>
        /// RabbitMq bağlantısının yapılabilmesi için connection string değeridir.
        /// </summary>
        public static string RabbitMqConnectionString => GetEnvVariable("KAYRA_RABBITMQ_CNN");
    }
}
