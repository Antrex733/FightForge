using FightForge.DTOs;

namespace FightForge.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterUserDto, User>();

            CreateMap<Sport, SportDto>()
                .ForMember(c => c.TrainersFirstName, c => c.MapFrom(a => a.Trainer.FirstName))
                .ForMember(c => c.TrainersLastName, c => c.MapFrom(a => a.Trainer.LastName));

            CreateMap<Gym, GymDto>()
                .ForMember(c => c.City, c => c.MapFrom(a => a.Address.City))
                .ForMember(c => c.Street, c => c.MapFrom(a => a.Address.Street))
                .ForMember(c => c.PostalCode, c => c.MapFrom(a => a.Address.PostalCode));

            CreateMap<CreateGymDto, Gym>()
                .ForMember(c => c.Address, c => c.MapFrom(dto => new Address()
                { City = dto.City, Street = dto.Street, PostalCode = dto.PostalCode }));
                

            CreateMap<UpdateGymDto, Gym>()
                .ForMember(n => n.Name, m => m.MapFrom(n => n.Name))
                .ForMember(d => d.Description, m => m.MapFrom(d => d.Description))
                .ForMember(c => c.ContactNumber, m => m.MapFrom(c => c.ContactNumber))
                .ForMember(c => c.ContactEmail, m => m.MapFrom(c => c.ContactEmail));

            CreateMap<CreateSportDto, Sport>();

            CreateMap<UpdateSportDto, Sport>();
                //.ForMember(x => x.);
        }
    }
}
