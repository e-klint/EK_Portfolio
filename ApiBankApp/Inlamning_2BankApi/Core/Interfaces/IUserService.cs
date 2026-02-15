using ApiBankApp.Data.DTO;
using ApiBankApp.Data.Entities;
using System.Threading.Tasks;

namespace ApiBankApp.Core.Interfaces
{
    public interface IUserService //Funktioner för anvädare (kund och admin + login). (Om projektet växer, kan man göra en CustomerUser och AdminUser)
    {
        Task<(bool IsValid, User User)> ValidateUserAsync(string username, string password);

        public string GenerateToken(User user);

        Task<User?> CreateUserAccountAsync(User user, Account account);

        Task<Account> CreateCustomerAccountAsync(Account account, Customer customer);

        Task<Transaction> CreateCustomerTransactionAsync(CreateTransactionDTO createTransaction, Customer customer);

        Task<User?> GetUserByIdAsync(int userId);
    }
}
