using ApiBankApp.Core.Interfaces;
using ApiBankApp.Data.Entities;
using ApiBankApp.Data.Interfaces;

namespace ApiBankApp.Core.Services
{
    public class TransactionService: ITransactionService
    {
        private readonly ITransactionRepo _transactionRepo;

        public TransactionService(ITransactionRepo transactionRepo)
        {
            _transactionRepo = transactionRepo;
        }

        public async Task<List<Transaction>> GetTransactionsForAccountAsync(int accountId, int customerId)
        {
            return await _transactionRepo.GetTransactionsForAccountAsync(accountId, customerId);
        }

        public async Task CreateTransactionAsync(Transaction transaction)
        {
            await _transactionRepo.CreateTransactionAsync(transaction);
        }
    }
}
