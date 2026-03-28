using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Domain.Models
{
    public class ServiceRegistrationConfig
    {
        public Assembly Assembly { get; set; } = null!;
        public Type ContextType { get; set; } = null!;
    }
}
