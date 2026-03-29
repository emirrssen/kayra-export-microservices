namespace KayraExport.Microservices.Services.Log.Core.Helpers
{
    public class EnvironmentHelper
    {
        public static string GetEnvVariable(string name)
                => Environment.GetEnvironmentVariable(name) ?? "";

        /// <summary>
        /// RabbitMq bağlantısının yapılabilmesi için connection string değeridir.
        /// </summary>
        public static string RabbitMqConnectionString => GetEnvVariable("KAYRA_RABBITMQ_CNN");
    }
}
