namespace FightForge.Services.Interfaces
{
    public interface IGymService
    {
        public IEnumerable<GymDto> GetAll();
        public GymDto GetById(int id);
        public Task<int> Create(CreateGymDto dto);
        public Task Patch(int gymId, UpdateGymDto dto);
        public Task Delete(int gymId);
    }
}
