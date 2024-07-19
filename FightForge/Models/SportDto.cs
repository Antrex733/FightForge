using System.ComponentModel.DataAnnotations;

namespace FightForge.Models
{
    public class SportDto
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; } = default!;
        [MaxLength(30)]
        [Required]
        public string Difficulty { get; set; } = default!;
    }
}
