using System.ComponentModel.DataAnnotations;
using TheBookParlour.Data.Entities;

namespace TheBookParlour.Data.DTO
{
    public class AddGenreRequest
    {
        [MaxLength(50)]
        [Required(AllowEmptyStrings = false)] //Förhindrar att tom stäng skickas in.
        public required string Name { get; set; }

        public string? Image { get; set; }

    }
}
