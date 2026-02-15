using ApiBankApp.Data.Entities;

namespace ApiBankApp.Data.Interfaces
{
    public interface ITransactionRepo
    {
        Task<List<Transaction>> GetTransactionsForAccountAsync(int accountId, int customerId);
        Task CreateTransactionAsync(Transaction transaction);
    }
}
