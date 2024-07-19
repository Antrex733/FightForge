using System.ComponentModel.DataAnnotations;

namespace FightForge.Models
{
    public class CreateGymDto
    {
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

        [MaxLength(50)]
        [Required]
        public string? City { get; set; }
        [MaxLength(50)]
        [Required]
        public string? Street { get; set; }
        public string? PostalCode { get; set; }
    }
}
