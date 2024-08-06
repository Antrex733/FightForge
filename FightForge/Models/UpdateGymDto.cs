namespace FightForge.Models
{
    public class UpdateGymDto
    {
        [MaxLength(50)]
        public string Name { get; set; } = default!;
        [MaxLength(400)]
        public string Description { get; set; } = default!;
        [Phone]
        public string ContactNumber { get; set; } = default!;
        [EmailAddress]
        public string ContactEmail { get; set; } = default!;
    }
}
