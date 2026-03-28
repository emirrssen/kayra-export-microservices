namespace KayraExport.Microservices.BuildingBlocks.Shared.Application.Services.Abstract
{
    public interface ITransactionService
    {
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task CreateSavepointAsync(string savepointName);
        Task RollbackToSavepointAsync(string savepointName);
        Task<int> SaveChangesAsync();
        Task<int> SaveChangesAndCommitTransactionAsync();
    }
}
