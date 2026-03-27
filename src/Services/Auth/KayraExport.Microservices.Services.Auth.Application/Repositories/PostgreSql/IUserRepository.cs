using KayraExport.Microservices.Services.Auth.Domain.Entities;

namespace KayraExport.Microservices.Services.Auth.Application.Repositories.PostgreSql
{
    public interface IUserRepository : IRepository
    {
        Task<User?> GetByEmailOrUsernameAsync(string email, string userName);
        Task InsertAsync(User user);
        Task UpdateAsync(User user);
    }
}
