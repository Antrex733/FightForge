using FightForge.DTOs;

namespace FightForge.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUserDto, User>();
        }
    }
}
