using System.Linq.Expressions;

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

        public async Task<int> Create(CreateGymDto dto)
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

            return gym.Id;
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

        public PagedResult<GymDto> GetAll(GymQuery query)
        {
            var baseQuery = _context
                .Gyms
                .Include(a => a.Address)
                .Include(b => b.Sports)
                    .ThenInclude(u => u.Trainer)
                .Where(x => query.SearchPhrase == null ||
                        x.Name.ToLower() == query.SearchPhrase.ToLower() ||
                        x.Address.City.ToLower() == query.SearchPhrase.ToLower() ||
                        x.Address.Street.ToLower() == query.SearchPhrase.ToLower() ||
                        x.Sports.Any(n => n.Name.ToLower() == query.SearchPhrase.ToLower()));

            if (!string.IsNullOrEmpty(query.SortBy) && query.SortDirection != null)
            {
                var propSelectors = new Dictionary<string, Expression<Func<Gym, object>>>
                {
                    { nameof(Gym.Name), x => x.Name },
                    { nameof(Gym.Description), x => x.Description }
                };

                var selectedProp = propSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedProp) 
                    : baseQuery.OrderByDescending(selectedProp);
            }

            var gyms = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize);

            var totalItemsCount = baseQuery.Count();

            var gymsDto = _mapper.Map<List<GymDto>>(gyms);

            var pageResult = new PagedResult<GymDto>(gymsDto, totalItemsCount, query.PageSize, query.PageNumber);

            return pageResult;
        }

        public GymDto GetById(int id)
        {
            var gym = _context
                .Gyms
                .Include(a => a.Address)
                .Include(b => b.Sports)
                    .ThenInclude(c => c.Trainer)
                .FirstOrDefault(x => x.Id == id);

            if (gym == null)
            {
                throw new NotFoundException("Gym not found");
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
