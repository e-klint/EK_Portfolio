using ApiBankApp.Data.Entities;

namespace ApiBankApp.Data.Interfaces
{
    public interface ILoanRepo
    {
        Task CreateLoanAsync(Loan loan);

        //Hämta lån med id?

        //Hämta alla lån för en kund
    }
}
