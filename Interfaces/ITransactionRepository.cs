using Microsoft.EntityFrameworkCore.Storage;

namespace SchoolAPI.Interfaces
{
    public interface ITransactionRepository
    {
        public IDbContextTransaction BeginTransaction();

        public void CommitTransaction(IDbContextTransaction transaction);

        public void RollbackTransaction(IDbContextTransaction transaction);
    }
}
