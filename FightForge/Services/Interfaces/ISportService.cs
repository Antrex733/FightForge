namespace FightForge.Services.Interfaces
{
    public interface ISportService
    {
        public Task<int> Create(int gymId, CreateSportDto dto);
        public IEnumerable<SportDto> GetAll(int gymId);
        public SportDto GetById(int gymId, int sportId);
        public Task Update(int gymId, int sportId, UpdateSportDto sportDto);
        public Task DeleteById(int gymId, int sportId);
        public Task DeleteAll(int gymId);
    }
}
