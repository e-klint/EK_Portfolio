using ApiBankApp.Data.Entities;
using ApiBankApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiBankApp.Data.Repos
{
    public class TransactionRepo: ITransactionRepo
    {
        private readonly BankAppDataContext _context;

        public TransactionRepo(BankAppDataContext context)
        {
            _context = context;
        }

        public async Task<List<Transaction>> GetTransactionsForAccountAsync(int accountId, int customerId)
        {
            return await _context.Transactions
                .Where(t => t.AccountId == accountId
                            && t.AccountNavigation.Dispositions.Any(d => d.CustomerId == customerId))
                .OrderByDescending(t => t.Date)
                .ToListAsync();
        }

        public async Task CreateTransactionAsync(Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
        }
    }
}
