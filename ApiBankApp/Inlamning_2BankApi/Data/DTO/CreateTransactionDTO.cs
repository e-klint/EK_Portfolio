namespace ApiBankApp.Data.DTO
{
    public class CreateTransactionDTO
    {
        public int fromAccountId { get; set; } //Avsändarkonto

        public decimal Amount { get; set; }

        public string? Symbol { get; set; } //Valfritt meddelande

        public int toAccountId { get; set; } //Mottagarkonto
    }
}
