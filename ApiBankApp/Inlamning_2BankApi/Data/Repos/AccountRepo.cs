using ApiBankApp.Data.Entities;
using ApiBankApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiBankApp.Data.Repos
{
    public class AccountRepo : IAccountRepo
    {
        private readonly BankAppDataContext _context;

        public AccountRepo(BankAppDataContext context)
        {
            _context = context;
        }

        public async Task<Account?> GetAccountByIdAsync(int accountId)
        {
         return await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId);
        }

        public async Task CreateAccountAsync(Account account)
        {
            await _context.Accounts.AddAsync(account);
        }

        public async Task CreateDisposition(Disposition disposition)
        {
            await _context.Dispositions.AddAsync(disposition);
        }

        public async Task<List<Account>> GetAccountsForCustomerAsync(int customerId)
        {
            return await _context.Dispositions
                .Where(d => d.CustomerId == customerId)
                .Include(d => d.Account)
                .Select(d => d.Account)
                .ToListAsync();
        }

        //För att uppdatera saldo vid transaktioner
        public async Task<bool> UpdateAccountAsync(Account account)
        {
           _context.Accounts.Update(account);
           return true;
        }

        //Hämtar CustomerId från Disposition-tabellen baserat på AccountId
        public async Task<Account?> GetAccountWithCustomerAsync(int accountId)
        {
            return await _context.Accounts
          .Include(a => a.Dispositions)
              .ThenInclude(d => d.Customer)
          .FirstOrDefaultAsync(a => a.AccountId == accountId);
        }
    }
}
