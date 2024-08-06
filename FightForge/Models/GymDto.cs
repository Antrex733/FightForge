namespace FightForge.Models
{
    public class GymDto
    {
        public int Id { get; set; }
        [MaxLength(50)]
        [Required]
        public string Name { get; set; } = default!;
        [MaxLength(400)]
        public string Description { get; set; } = default!;
        [Phone]
        public string ContactNumber { get; set; } = default!;
        [EmailAddress]
        [Required]
        public string ContactEmail { get; set; } = default!;

        public string? City { get; set; }
        public string? Street { get; set; }
        public string? PostalCode { get; set; }

        public List<SportDto> Sports { get; set; } = [];
    }
}
