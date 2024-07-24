

using FightForge.Authorization;
using FightForge.Entities;
using Microsoft.AspNetCore.Authorization;

namespace FightForge.Services
{
    public class GymService : IGymService
    {
        private readonly GymDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _contextService;
        private readonly IAuthorizationService _authorizationService;

        public GymService(GymDbContext gymDbContext, IMapper mapper, IUserContextService contextService, 
            IAuthorizationService authorizationService)
        {
            _context = gymDbContext;
            _mapper = mapper;
            _contextService = contextService;
            _authorizationService = authorizationService;
        }

        public async Task Create(CreateGymDto dto)
        {
            var gym = _mapper.Map<Gym>(dto);

            gym.CreatedById = _contextService.GetUserId;

            var authorizationResult = _authorizationService.AuthorizeAsync(_contextService.User, gym,
                new ResourceOperationRequirement(OperationType.Create)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            await _context.AddAsync(gym);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int gymId)
        {
            var gym = _context
                .Gyms
                .Include(a => a.Address)
                .Include(b => b.Sports)
                .FirstOrDefault(x => x.Id == gymId);


            if (gym == null)
            {
                throw new NotFoundException("Gym not found");
            }

            var authorizationResult = _authorizationService.AuthorizeAsync(_contextService.User, gym,
                new ResourceOperationRequirement(OperationType.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            _context.Remove(gym);  
            await _context.SaveChangesAsync();
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

            var authorizationResult = _authorizationService.AuthorizeAsync(_contextService.User, gym,
                new ResourceOperationRequirement(OperationType.Read)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            var gymDto = _mapper.Map<GymDto>(gym);

            return gymDto;
        }

        public async Task Patch(int gymId, UpdateGymDto dto)
        {          
            var gym = _context
                .Gyms
                .FirstOrDefault(x => x.Id == gymId);
            if (gym == null)
            {
                throw new NotFoundException("Gym not found");
            }

            var authorizationResult = _authorizationService.AuthorizeAsync(_contextService.User, gym,
                new ResourceOperationRequirement(OperationType.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            gym = _mapper.Map(dto, gym);

            await _context.SaveChangesAsync();
        }

        //wyszukiwanie gym po adres, sport, nazwa gym
        //paginacja
        //zmiana roli
    }
}
