namespace KayraExport.Microservices.BuildingBlocks.Shared.Domain.Models
{
    public class JwtConfig
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string SecurityKey { get; set; } = string.Empty;
    }
}
