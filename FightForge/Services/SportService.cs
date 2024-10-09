namespace FightForge.Services
{
    public class SportService : ISportService
    {
        private readonly GymDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserContextService _contextService;
        private readonly IAuthorizationService _authorizationService;

        public SportService(GymDbContext context, IMapper mapper, IUserContextService contextService,
            IAuthorizationService authorizationService)
        {
            _context = context;
            _mapper = mapper;
            _contextService = contextService;
            _authorizationService = authorizationService;
        }
        public async Task<int> Create(int gymId, CreateSportDto dto)
        {
            var gym = GetGymById(gymId);
            var sport = _mapper.Map<Sport>(dto);

            sport.GymId = gymId;
            sport.TrainerId = dto.TrainerId;

            var authorizationResult = _authorizationService.AuthorizeAsync(_contextService.User, gym,
                new ResourceOperationRequirement(OperationType.Create)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            await _context.AddAsync(sport);
            await _context.SaveChangesAsync();

            return sport.Id;
        }

        public async Task DeleteAll(int gymId)
        {
            var gym = GetGymById(gymId);
            var sports = gym.Sports.Where(x => x.GymId == gymId);

            var authorizationResult = _authorizationService.AuthorizeAsync(_contextService.User, gym,
                new ResourceOperationRequirement(OperationType.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            _context.RemoveRange(sports);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(int gymId, int sportId)
        {
            var gym = GetGymById(gymId);
            var sport = gym.Sports.FirstOrDefault(x => x.Id == sportId);

            if (sport == null || sport.GymId != gymId)
            {
                throw new NotFoundException("Sport not found");
            }

            var authorizationResult = _authorizationService.AuthorizeAsync(_contextService.User, gym,
                new ResourceOperationRequirement(OperationType.Delete)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            _context.Remove(sport);
            await _context.SaveChangesAsync();
        }

        public PagedResult<SportDto> GetAll(int gymId, SportQuery query)
        {
            var baseQuery = _context
                .Sports
                .Include(x => x.Trainer)
                .Where(x => x.GymId == gymId)
                .Where(x => query.SearchPhrase == null ||
                        x.Name.ToLower() == query.SearchPhrase.ToLower() ||
                        x.Difficulty.ToLower() == query.SearchPhrase.ToLower());

            if (baseQuery == null)
            {
                throw new NotFoundException("Sports not found");
            }
        
            if (!string.IsNullOrEmpty(query.SortBy) && query.SortDirection != null)
            {
                var propSelectors = new Dictionary<string, Expression<Func<Sport, object>>>
                {
                    { nameof(Sport.Name), x => x.Name },
                    { nameof(Sport.Difficulty), x => x.Difficulty }
                };

                var selectedProp = propSelectors[query.SortBy];

                baseQuery = query.SortDirection == SortDirection.ASC
                    ? baseQuery.OrderBy(selectedProp)
                    : baseQuery.OrderByDescending(selectedProp);
            }

            var sports = baseQuery
                .Skip(query.PageSize * (query.PageNumber - 1))
                .Take(query.PageSize);

            var totalItemsCount = baseQuery.Count();

            var spoprtsDto = _mapper.Map<List<SportDto>>(sports);

            var pageResult = new PagedResult<SportDto>(spoprtsDto, totalItemsCount, 
                                                        query.PageSize, query.PageNumber);

            return pageResult;
        }
       
        public SportDto GetById(int gymId, int sportId)
        {
            var gym = GetGymById(gymId);
            var sport = gym
                .Sports
                .FirstOrDefault(x => x.Id == sportId);

            if (sport == null || sport.GymId != gymId)
            {
                throw new NotFoundException("Sport not found");
            }

            var sportDto = _mapper.Map<SportDto>(sport);

            return sportDto;
        }

        public async Task Update(int gymId, int sportId, UpdateSportDto sportDto)
        {
            var gym = GetGymById(gymId);

            var sport = gym.Sports.FirstOrDefault(x => x.Id == sportId);

            if (sport == null || sport.GymId != gymId)
            {
                throw new NotFoundException("Sport not found");
            }

            var authorizationResult = _authorizationService.AuthorizeAsync(_contextService.User, gym,
                new ResourceOperationRequirement(OperationType.Update)).Result;

            if (!authorizationResult.Succeeded)
            {
                throw new ForbidException();
            }

            _mapper.Map(sportDto, sport);

            await _context.SaveChangesAsync();
        }

        private Gym GetGymById(int gymId) 
        {
            var gym = _context
                .Gyms
                .Include(x => x.Sports)
                    .ThenInclude(x => x.Trainer)
                .FirstOrDefault(x => x.Id == gymId);
                

            if (gym == null)
            {
                throw new NotFoundException("Gym not found");
            }

            return gym;
        }
    }
}
