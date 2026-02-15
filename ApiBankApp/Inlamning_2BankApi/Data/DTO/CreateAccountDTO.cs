namespace ApiBankApp.Data.DTO
{
    public class CreateAccountDTO //Används när kund skapar konto själv
    {
 
        public int AccountTypesId { get; set; } // "Sparkonto", "Personkonto" 
        public decimal Balance { get; set; }
        public string Frequency { get; set; }
    }
}
