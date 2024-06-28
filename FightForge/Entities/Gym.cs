namespace FightForge.Entities
{
    public class Gym
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string Description { get; set; } = default!;
        public string ContactNumber { get; set; } = default!;
        public string ContactEmail { get; set; } = default!;

        public int AddressId { get; set; }
        public Address Address { get; set; } = default!;
        public List<Sport> Sports { get; set; } = new();
    }
}
