namespace TheBookParlour.Data.DTO
{
    public class TokenDTO
    {
        public string JwtToken { get; set; }

        //Behöver detta läggas till enligt uppgiften?
        //public string TokenType { get; set; } = "Bearer";
        //public int ExpiresIn { get; set; } = 3600;
    }
}
