using ApiBankApp.Core.Interfaces;
using ApiBankApp.Data.Entities;
using ApiBankApp.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiBankApp.Core.Services
{
    public class LoanService: ILoanService
    {
        private readonly BankAppDataContext _context;
        private readonly ILoanRepo _loanRepo;
        private readonly IAccountService _accountService;

        public LoanService(BankAppDataContext context, ILoanRepo loanRepo, IAccountService accountService)
        {
            _context = context;
            _loanRepo = loanRepo;
            _accountService = accountService;
        }

        public async Task<Loan> CreateLoanAsync(Loan loan)
        {
            using var dbTransaction = await _context.Database.BeginTransactionAsync();

            try
            {
                //Skapa lån
                await _loanRepo.CreateLoanAsync(loan);

                //Kontrollera att konto existerar 
                var account = await _accountService.GetAccountByIdAsync(loan.AccountId);
                if (account == null)
                    throw new Exception("Account not found");

                //För över pengar till konton + uppdatera konto
                account.Balance += loan.Amount;
                await _accountService.UpdateAccountAsync(account);

                // Save and commit
                await _context.SaveChangesAsync();
                await dbTransaction.CommitAsync();
                return loan;
            }
            catch (Exception ex) 
            {
                await dbTransaction.RollbackAsync();
                Logger.Logger.LogErrorToFile(ex);
                return null;
            }
        }
    }
}
