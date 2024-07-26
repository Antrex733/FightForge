namespace FightForge.Entities
{
    public class Sport
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Difficulty { get; set; } = default!;

        public virtual User? Trainer { get; set; } = default!;
        public int? TrainerId { get; set; }

        public int GymId { get; set; }
        public virtual Gym Gym { get; set; } = default!;
    }
}
