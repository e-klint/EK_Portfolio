using ApiBankApp.Core.Interfaces;
using ApiBankApp.Data.Entities;
using ApiBankApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiBankApp.Core.Services
{
    public class AccountService: IAccountService
    {
        private readonly IAccountRepo _repo;
        private readonly BankAppDataContext _context;
        public AccountService(IAccountRepo repo, BankAppDataContext context)
        {
            _repo = repo;
            _context = context;
        }

        public async Task<Account?> GetAccountByIdAsync(int accountId)
        { 
            return await _repo.GetAccountByIdAsync(accountId);
        }

        public async Task<bool> CreateAccountAsync(Account account)
        {
            //Validera typ av konto
            bool exists = await _context.AccountTypes
              .AnyAsync(at => at.AccountTypeId == account.AccountTypesId); 

            if (!exists)
                return false;

            account.Created = DateOnly.FromDateTime(DateTime.UtcNow);
            await _repo.CreateAccountAsync(account);

            return true;
        }

        public async Task<List<Account>> GetAccountsForCustomerAsync(int customerId) 
        {
            return await _repo.GetAccountsForCustomerAsync(customerId);
        }

        public async Task<bool> CreateDispositionAsync(Disposition disposition)
        {
            await _repo.CreateDisposition(disposition);
            return true;
        }

        public async Task<bool> UpdateAccountAsync(Account account)
        {
            await _repo.UpdateAccountAsync(account);
            return true;
        }

        //Kontrollerar att en specifik kund äger ett specifikt konto
        public async Task<Account?> GetAccountIfOwnedByCustomerAsync(int accountId, int customerId) { 
          
            //Hämtar kontot
           var account = await _repo.GetAccountWithCustomerAsync(accountId);
              if (account is null)
                 return null;

            //Kontrollera att kunden äger kontot 
            bool isOwner = await _context.Dispositions
                .AnyAsync(d => d.AccountId == accountId && d.CustomerId == customerId);
            if (!isOwner)
                return null;

          return account;
        }
    }

    
}
