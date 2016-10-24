using AutoMapper;
using BL.DTOs;
using DAL.Entities;

namespace BL.Startup.Mapping.Profiles
{
    public class StandardMappingProfile : Profile
    {
        public StandardMappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Plan, PlanDTO>().ReverseMap();
        }
    }
}
