using System.ComponentModel.DataAnnotations;

namespace TheBookParlour.Data.Entities
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [MaxLength(20)]
        public required string UserName { get; set; }

        public required string PasswordHash { get; set; } //Obs! Kör migration + uppdatera databas

        public required string Role { get; set; }
    }
}
