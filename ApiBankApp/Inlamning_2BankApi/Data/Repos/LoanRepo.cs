using ApiBankApp.Data.Entities;
using ApiBankApp.Data.Interfaces;

namespace ApiBankApp.Data.Repos
{
    public class LoanRepo: ILoanRepo
    {
        private readonly BankAppDataContext _context;

        public LoanRepo(BankAppDataContext context)
        {
            _context = context;
        }

        public async Task CreateLoanAsync(Loan loan)
        {
            await _context.Loans.AddAsync(loan);
        }
        
    }
}
