namespace FightForge.Entities
{
    public class Sport
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Difficulty { get; set; }

        //public User Trainer { get; set; }

        public int GymId { get; set; }
        public Gym Gym { get; set; } = default!;
    }
}
