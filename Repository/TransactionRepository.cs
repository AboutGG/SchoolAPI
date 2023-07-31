using Microsoft.EntityFrameworkCore.Storage;
using SchoolAPI.Interfaces;
using SchoolAPI.Models;

namespace SchoolAPI.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly SchoolContext _context;
        
        public TransactionRepository(SchoolContext context)
        {
            this._context = context;
        }

        public IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
        }

        /// <summary>
        /// Commit transaction
        /// </summary>
        /// <param name="transaction"></param>
        public void CommitTransaction(IDbContextTransaction transaction)
        {
            transaction.Commit();
        }

        /// <summary>
        /// Rollback transaction
        /// </summary>
        /// <param name="transaction"></param>
        public void RollbackTransaction(IDbContextTransaction transaction)
        {
            transaction.Rollback();
        }

    }
}
