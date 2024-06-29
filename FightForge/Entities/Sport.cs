namespace FightForge.Entities
{
    public class Sport
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Difficulty { get; set; } = default!;

        //public User Trainer { get; set; }

        public int GymId { get; set; }
        public Gym Gym { get; set; } = default!;
    }
}
