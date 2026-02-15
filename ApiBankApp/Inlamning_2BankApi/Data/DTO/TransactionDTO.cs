namespace ApiBankApp.Data.DTO
{
    public class TransactionDTO //Output
    {
        public int TransactionId { get; set; }
        public int AccountId { get; set; }

        public DateOnly Date { get; set; }

        public string Type { get; set; } = null!; //Credit, debit

        public string Operation { get; set; } = null!; //Credit in cash

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }

    }
}
