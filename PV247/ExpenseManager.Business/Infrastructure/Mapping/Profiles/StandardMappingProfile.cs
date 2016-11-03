using AutoMapper;
using ExpenseManager.Business.DTOs;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.Infrastructure.Mapping.Profiles
{
    /// <summary>
    /// Standard mapping profile.
    /// </summary>
    public class StandardMappingProfile : Profile
    {
        /// <summary>
        /// Creates mapping.
        /// </summary>
        public StandardMappingProfile()
        {
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<Plan, PlanDTO>().ReverseMap();
        }
    }
}
