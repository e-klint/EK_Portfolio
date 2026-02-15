using ApiBankApp.Data.Entities;

namespace ApiBankApp.Core.Interfaces
{
    public interface ILoanService
    {
        Task<Loan> CreateLoanAsync(Loan loan);
    }
}
