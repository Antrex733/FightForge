using System.ComponentModel.DataAnnotations;

namespace FightForge.Models
{
    public class CreateSportDto
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; } = default!;
        [MaxLength(30)]
        [Required]
        public string Difficulty { get; set; } = default!;

        public int TrainerId { get; set; }
    }
}
