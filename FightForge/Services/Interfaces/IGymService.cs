namespace FightForge.Services.Interfaces
{
    public interface IGymService
    {
        public IEnumerable<GymDto> GetAll();
        public GymDto GetById(int id);
        public Task Create(CreateGymDto dto);
    }
}
