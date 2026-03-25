using KayraExport.Microservices.Services.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace KayraExport.Microservices.Services.Auth.Infrastructure.EntityFrameworkCore
{
    public class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext(options)
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
            => modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        public DbSet<User> Users { get; set; }
    }
}
