namespace FightForge.Data
{
    public class GymDbContext : DbContext
    {
        public GymDbContext( DbContextOptions<GymDbContext> options): base(options)
        {
        }
        DbSet<Gym> Gyms { get; set; }
        DbSet<Sport> Sports { get; set; }
        DbSet<Address> Addresses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
