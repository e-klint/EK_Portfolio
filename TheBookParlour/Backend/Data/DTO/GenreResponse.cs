using System.ComponentModel.DataAnnotations;
using TheBookParlour.Data.Entities;

namespace TheBookParlour.Data.DTO
{
    public record GenreResponse
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public required string Name { get; set; }

        public string? Image { get; set; }
    }
}
