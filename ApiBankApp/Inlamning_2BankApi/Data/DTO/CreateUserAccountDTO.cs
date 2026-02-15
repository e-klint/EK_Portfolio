namespace ApiBankApp.Data.DTO
{
    public class CreateUserAccountDTO
    {   //Mappas till User
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }


        //Mappas till Account
        public int AccountTypesId { get; set; } // "Sparkonto", "Personkonto" 
        public decimal Balance { get; set; } 
        public string Frequency { get; set; } 
        public int CustomerId { get; set; } 
    }
}
