using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.Infrastructure.Mapping.Profiles
{
    /// <summary>
    /// Standard mapping profile.
    /// </summary>
    public class DatabaseToBusinessMapping : Profile
    {
        /// <summary>
        /// Creates mapping.
        /// </summary>
        public DatabaseToBusinessMapping()
        {
            CreateMap<UserModel, User>().ReverseMap();
            CreateMap<PlanModel, Plan>().ReverseMap();
        }
    }
}
