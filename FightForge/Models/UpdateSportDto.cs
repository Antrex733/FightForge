namespace FightForge.Models
{
    public class UpdateSportDto
    {
        public string Name { get; set; } = default!;
        [MaxLength(30)]
        [Required]
        public string Difficulty { get; set; } = default!;
    }
}
