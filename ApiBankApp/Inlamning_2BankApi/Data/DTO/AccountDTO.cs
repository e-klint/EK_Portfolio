using ApiBankApp.Data.Entities;

namespace ApiBankApp.Data.DTO
{
    public class AccountDTO 
    {
        public int AccountId { get; set; }

        public string Frequency { get; set; } = null!;

        public DateOnly Created { get; set; }

        public decimal Balance { get; set; }

        public int? AccountTypesId { get; set; }
    }
}
