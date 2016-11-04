using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
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
            CreateMap<UserModel, User>().ReverseMap();
            CreateMap<PlanModel, Plan>().ReverseMap();
        }
    }
}
