using Microsoft.EntityFrameworkCore.Storage;
using RabbitPublish.Repositories;

namespace RabbitPublish.Unit
{
    public class UnitOfWork<T> : AbstractUnitOfWork<T> where T : class
    {
        public override T Repositorie { get; }
        private IDbContextTransaction _transaction;
        private readonly UserDbContext _dbContext;
        public UnitOfWork(T repositorie, UserDbContext dbContext)
        {
            Repositorie = repositorie;
            _dbContext = dbContext;
        }
        public async override Task BegginTransaction()
        {
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        }
        public async override Task CommitTransaction()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch
            {
                await _transaction.RollbackAsync();
            }
        }
        public async override Task<int> CompleteTask()
        => await _dbContext.SaveChangesAsync();
        
    }
}

