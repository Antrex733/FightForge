using FightForge.DTOs;

namespace FightForge.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUserDto, User>();

            CreateMap<Sport, SportDto>();

            CreateMap<Gym, GymDto>()
                .ForMember(c => c.City, c => c.MapFrom(a => a.Address.City))
                .ForMember(c => c.Street, c => c.MapFrom(a => a.Address.Street))
                .ForMember(c => c.PostalCode, c => c.MapFrom(a => a.Address.PostalCode));
        }
    }
}
