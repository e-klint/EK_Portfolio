namespace ApiBankApp.Data.DTO
{
    public class LoanResponseDTO
    {
        public int AccountId { get; set; }

        public DateOnly Date { get; set; }

        public decimal Amount { get; set; }

        public int Duration { get; set; }

        public decimal Payments { get; set; }

        public string Status { get; set; } = null!;
    }
}
