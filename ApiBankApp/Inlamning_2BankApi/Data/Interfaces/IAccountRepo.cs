using ApiBankApp.Data.Entities;

namespace ApiBankApp.Data.Interfaces
{
    public interface IAccountRepo
    {
        Task<Account?> GetAccountByIdAsync(int accountId);
        Task CreateAccountAsync(Account account);

        Task CreateDisposition(Disposition disposition);

        Task<List<Account>> GetAccountsForCustomerAsync(int customerId);

        Task<bool> UpdateAccountAsync(Account account);

        Task<Account?> GetAccountWithCustomerAsync(int accountId);
    }
}
