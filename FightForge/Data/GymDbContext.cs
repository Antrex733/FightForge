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
        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
