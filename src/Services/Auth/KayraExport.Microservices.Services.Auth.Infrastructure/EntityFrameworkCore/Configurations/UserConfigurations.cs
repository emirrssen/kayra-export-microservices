using KayraExport.Microservices.BuildingBlocks.Shared.Domain.Consts;
using KayraExport.Microservices.Services.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KayraExport.Microservices.Services.Auth.Infrastructure.EntityFrameworkCore.Configurations
{
    public class UserConfigurations : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users", SchemasConst.Auth);
        }
    }
}
