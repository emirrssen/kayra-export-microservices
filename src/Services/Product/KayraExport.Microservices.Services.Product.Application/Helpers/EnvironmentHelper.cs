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
    }
}
