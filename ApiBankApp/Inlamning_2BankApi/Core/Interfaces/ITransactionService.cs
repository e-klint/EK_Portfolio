
using ApiBankApp.Data.Entities;
using System.Threading.Tasks;

namespace ApiBankApp.Core.Interfaces
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetTransactionsForAccountAsync(int accountId, int customerId);
        Task CreateTransactionAsync(Transaction transaction);
    }
}
