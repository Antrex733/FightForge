

namespace FightForge.Services
{
    public class GymService : IGymService
    {
        private readonly GymDbContext _context;
        private readonly IMapper _mapper;

        public GymService(GymDbContext gymDbContext, IMapper mapper)
        {
            _context = gymDbContext;
            _mapper = mapper;
        }

        public Task Create(CreateGymDto dto)
        {
            var gym = _mapper.Map<Gym>(dto);
            gym.CreatedById = 
        }

        public IEnumerable<GymDto> GetAll()
        {
            var gyms = _context
                .Gyms
                .Include(a => a.Address)
                .Include(b => b.Sports)
                .ToList();

            var gymsDto = _mapper.Map<List<GymDto>>(gyms);

            return gymsDto;
        }

        public GymDto GetById(int id)
        {
            var gym = _context
                .Gyms
                .Include(a => a.Address)
                .Include(b => b.Sports)
                .FirstOrDefault(x => x.Id == id);


            if (gym == null)
            {
                throw new NotFoundException("Gym not found");
            }

            var gymDto = _mapper.Map<GymDto>(gym);

            return gymDto;
        }
    }
}
