using ApiBankApp.Data.Entities;

namespace ApiBankApp.Core.Interfaces
{
    public interface IAccountService
    {
        Task<Account?> GetAccountByIdAsync(int accountId);
        Task<bool> CreateAccountAsync(Account account);
        Task<bool> UpdateAccountAsync(Account account);
        Task<List<Account>> GetAccountsForCustomerAsync(int customerId);
        Task<bool> CreateDispositionAsync(Disposition disposition);
        Task<Account?> GetAccountIfOwnedByCustomerAsync(int accountId, int customerId);
    }
}
