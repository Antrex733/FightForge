namespace FightForge.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string Email { get; set; } = default!;
        public DateTime? DateOfBirth { get; set; }
        public string PasswordHash { get; set; } = default!;

        public virtual Role? Role { get; set; }
        public int RoleId { get; set; }

        public List<Sport> Sports { get; set; } = new();
    }
}
