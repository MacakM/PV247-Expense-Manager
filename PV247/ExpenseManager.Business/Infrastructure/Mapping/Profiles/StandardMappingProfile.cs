using AutoMapper;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Database.Infrastructure.Mapping.Profiles
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
