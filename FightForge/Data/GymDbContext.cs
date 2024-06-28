namespace FightForge.Data
{
    public class GymDbContext : DbContext
    {
        public GymDbContext( DbContextOptions<GymDbContext> options): base(options)
        {
        }
        public DbSet<Gym> Gyms { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
