using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace KayraExport.Microservices.Services.Auth.Domain.Entities
{
    public class User : BasePostgreSqlEntity
    {
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string EmailAddress { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? RefreshToken { get; set; }
        [Column(TypeName = "timestamp without time zone")] public DateTime? RefreshTokenExpiresAt { get; set; }
    }
}
