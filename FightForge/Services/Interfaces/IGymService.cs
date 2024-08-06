namespace FightForge.Services.Interfaces
{
    public interface IGymService
    {
        public PagedResult<GymDto> GetAll(GymQuery query);
        public GymDto GetById(int id);
        public Task<int> Create(CreateGymDto dto);
        public Task Patch(int gymId, UpdateGymDto dto);
        public Task Delete(int gymId);
    }
}
