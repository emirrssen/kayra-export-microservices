using KayraExport.Microservices.BuildingBlocks.Shared.Application.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace KayraExport.Microservices.BuildingBlocks.Shared.Application.Services.Concrete
{
    public sealed class TransactionService(DbContext context) : ITransactionService
    {
        public async Task BeginTransactionAsync()
        {
            if (context.Database.CurrentTransaction is null)
                await context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (context.Database.CurrentTransaction is not null)
                await context.Database.CurrentTransaction.CommitAsync();
        }

        public async Task CreateSavepointAsync(string savepointName)
        {
            if (context.Database.CurrentTransaction is not null)
                await context.Database.CurrentTransaction.CreateSavepointAsync(savepointName);
        }

        public async Task RollbackToSavepointAsync(string savepointName)
        {
            if (context.Database.CurrentTransaction is not null)
                await context.Database.CurrentTransaction.RollbackToSavepointAsync(savepointName);
        }

        public async Task<int> SaveChangesAsync() => await context.SaveChangesAsync();
        public async Task<int> SaveChangesAndCommitTransactionAsync()
        {
            await CommitTransactionAsync();
            return await SaveChangesAsync();
        }

        public async Task RollbackTransactionAsync()
        {
            if (context.Database.CurrentTransaction is not null)
                await context.Database.CurrentTransaction.RollbackAsync();
        }
    }
}
