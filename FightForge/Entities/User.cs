namespace FightForge.Entities
{
    public class User
    {
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PasswordHash { get; set; }

        //public Role Role { get; set; }
    }
}
