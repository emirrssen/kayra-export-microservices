using KayraExport.Microservices.Services.Auth.Application.Repositories.PostgreSql;
using KayraExport.Microservices.Services.Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace KayraExport.Microservices.Services.Auth.Infrastructure.EntityFrameworkCore.Repositories.PostgreSql
{
    public class UserRepository : IUserRepository
    {
        public DbSet<User> Users { get; set; }
        protected AuthDbContext Context { get; private set; }

        public UserRepository(AuthDbContext context)
        {
            Context = context;
            Users = context.Set<User>();
        }

        public async Task<User?> GetByEmailOrUsernameAsync(string email, string userName)
        {
            var query = Users.AsQueryable();
            query.AsNoTrackingWithIdentityResolution();

            return await query.FirstOrDefaultAsync(x => 
                (x.EmailAddress == email ||
                x.Username == userName) &&
                x.DeletedAt == null
            );
        }

        public async Task InsertAsync(User user) => await Users.AddAsync(user);
        public async Task UpdateAsync(User user) => await Task.FromResult(Users.Update(user));
    }
}
