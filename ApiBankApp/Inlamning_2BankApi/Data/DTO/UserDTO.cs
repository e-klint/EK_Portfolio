namespace ApiBankApp.Data.DTO
{
    public class UserDTO //Output
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Role { get; set; } = null!;

        public int? CustomerId { get; set; }
    }
}
