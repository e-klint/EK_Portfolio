using System.ComponentModel.DataAnnotations;

namespace TheBookParlour.Data.Entities
{
    public class Author
    {
        [Key]
        public int AuthorId { get; set; }
        
        [MaxLength(50)]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Surname { get; set; }
    }
}
