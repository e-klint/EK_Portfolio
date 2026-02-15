using ApiBankApp.Core.Interfaces;
using ApiBankApp.Data.DTO;
using ApiBankApp.Data.Entities;
using ApiBankApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiBankApp.Core.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        private readonly IAccountService _accountService;
        private readonly ITransactionService _transactionService;
        private readonly BankAppDataContext _context;

        public UserService(IUserRepo userRepo, IAccountService accountService, BankAppDataContext context, ITransactionService transactionService)
        {
            _userRepo = userRepo;
            _accountService = accountService;
            _context = context;
            _transactionService = transactionService;
        }

        //Använde en Tuple för att kunna returnera både true och user, detta löser felhanteringen i controllern.
        public async Task<(bool IsValid, User User)> ValidateUserAsync(string username, string password)
        {
            var user = await _userRepo.GetByUsernameAsync(username);
            if (user == null || user.Password != password)
                return (false, null);

            return (true, user);
        }

        public string GenerateToken(User user)
        {
            //Sätta upp kryptering. Samma säkerhetsnyckel som när vi satte upp tjänsten
            //Denna förvaras på ett säkert ställe tex Azure Keyvault eller liknande och hårdkodas
            //inte in på detta sätt
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsAVerySecretJwtKey_ForBankApp123!"));

            //Lista med vilka behörigheter som en användare har
            var claims = new List<Claim>
            {
              new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
              new Claim(ClaimTypes.Name, user.Username),
              new Claim(ClaimTypes.Role, user.Role)
             };

            var signinCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            //Skapa options för att sätta upp en token
            var tokenOptions = new JwtSecurityToken(
                    issuer: "http://BankApp",
                    audience: "http://BankApp",
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(60),
                    signingCredentials: signinCredentials);

            //Generar en ny token som skall skickas tillbaka 
            var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);

            return tokenString;
        }

        public async Task<User?> GetUserByIdAsync(int userId) 
        { 
            return await _userRepo.GetUserByIdAsync(userId);
        }

        //Adminfunktion, skapar användarkonto + bankkonto
        public async Task<User?> CreateUserAccountAsync(User user, Account account)
        {
            //Kontrollerar att kunden inte redan har ett användarkonto.
            bool exists = await _context.Users.AnyAsync(u => u.CustomerId == user.CustomerId);

            if (exists)
                return null;

            using var dbTransaction = await _context.Database.BeginTransactionAsync();


            try
            {

                // Skapa användare
                await _userRepo.CreateUserAsync(user);

                //Skapa konto
                await _accountService.CreateAccountAsync(account);

                // Säkerställ att account.AccountId har värde innan disposition skapas
                await _context.SaveChangesAsync();

                //Skapa disposition
                var disposition = new Disposition
                {
                    CustomerId = (int)user.CustomerId!,
                    AccountId = account.AccountId, //Användaren som admin skapar
                    Type = "OWNER" //Användaren är ägare
                };

                await _accountService.CreateDispositionAsync(disposition);

                // Save and commit
                await _context.SaveChangesAsync();
                await dbTransaction.CommitAsync();
                return user;

            }
            catch (Exception ex)
            {
                await dbTransaction.RollbackAsync();
                Logger.Logger.LogErrorToFile(ex);
                return null;
            }
        }

        //Kundfunktion, skapa bankkono
        public async Task<Account> CreateCustomerAccountAsync(Account account, Customer customer)
        {
            using var dbTransaction = await _context.Database.BeginTransactionAsync();

            try
            {

                //Skapa konto
                await _accountService.CreateAccountAsync(account);
                // Säkerställ att account.AccountId har värde innan disposition skapas
                await _context.SaveChangesAsync();

                //Skapa disposition
                var disposition = new Disposition
                {
                    CustomerId = customer.CustomerId,
                    AccountId = account.AccountId, //Användaren som admin skapar
                    Type = "OWNER" //Användaren är ägare
                };

                await _accountService.CreateDispositionAsync(disposition);

                // Save and commit
                await _context.SaveChangesAsync();
                await dbTransaction.CommitAsync();
                return account;
            }
            catch (Exception ex)
            {
                await dbTransaction.RollbackAsync();
                Logger.Logger.LogErrorToFile(ex);
                return null;
            }
        }

        //Kundfunktion, skapa transaktion
        public async Task<Transaction> CreateCustomerTransactionAsync(CreateTransactionDTO createTransaction, Customer customer)
        {

            using var dbTransaction = await _context.Database.BeginTransactionAsync();
            try
            {   
                //Kontrollerar att konto finns och kund är ägare
                var fromAccount = await _accountService.GetAccountIfOwnedByCustomerAsync(createTransaction.fromAccountId, customer.CustomerId);
                if (fromAccount == null)
                    throw new Exception("User doesn't own account or account doesn't exist.");

                //Kontrollera att bankkonto finns
                var toAccount = await _accountService.GetAccountByIdAsync(createTransaction.toAccountId);
                if (toAccount == null)
                    throw new Exception("Account doesn't exist");

                if (fromAccount.Balance < createTransaction.Amount)
                    throw new Exception("Insufficient funds");

                fromAccount.Balance -= createTransaction.Amount;
                toAccount.Balance += createTransaction.Amount;

                var transactionFromAccount = new Transaction
                {
                    AccountId = createTransaction.fromAccountId,
                    Date = DateOnly.FromDateTime(DateTime.UtcNow),
                    Type = "Credit",
                    Operation = "Transfer",
                    Amount = -createTransaction.Amount,
                    Balance = fromAccount.Balance, 
                    Symbol = createTransaction.Symbol,
                    Bank = "BankApp",
                    Account = toAccount.AccountId.ToString()
                };

                var transactionToAccount = new Transaction
                {
                    AccountId = createTransaction.toAccountId,
                    Date = DateOnly.FromDateTime(DateTime.UtcNow),
                    Type = "Debit",
                    Operation = "Transfer",
                    Amount = createTransaction.Amount,
                    Balance = toAccount.Balance,
                    Symbol = createTransaction.Symbol,
                    Bank = "BankApp",
                    Account = fromAccount.AccountId.ToString()
                };

                //Skapa transaktioner (från konto + till konto
                await _transactionService.CreateTransactionAsync(transactionFromAccount);
                await _transactionService.CreateTransactionAsync(transactionToAccount);

                // Uppdatera konton
                await _accountService.UpdateAccountAsync(fromAccount);
                await _accountService.UpdateAccountAsync(toAccount);

                // Save and commit
                await _context.SaveChangesAsync();
                await dbTransaction.CommitAsync();
                return transactionFromAccount; 
            }
            catch(Exception ex)
            {
                await dbTransaction.RollbackAsync();
                Logger.Logger.LogErrorToFile(ex);
                return null;
            }
        }

    }
}
