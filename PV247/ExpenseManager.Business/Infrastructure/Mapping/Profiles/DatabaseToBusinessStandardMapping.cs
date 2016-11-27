using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;
using ExpenseManager.Business.DataTransferObjects.Filters.Accounts;
using ExpenseManager.Business.DataTransferObjects.Filters.CostInfos;
using ExpenseManager.Business.DataTransferObjects.Filters.Users;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Filters;
using ExpenseManager.Database.Filters.Accounts;
using ExpenseManager.Database.Filters.CostInfos;
using ExpenseManager.Database.Filters.Users;

namespace ExpenseManager.Business.Infrastructure.Mapping.Profiles
{
    /// <summary>
    /// Standard mapping profile.
    /// </summary>
    public class DatabaseToBusinessStandardMapping : Profile
    {
        /// <summary>
        /// Creates mapping.
        /// </summary>
        public DatabaseToBusinessStandardMapping()
        {
            CreateMap<UserModel, User>()
                .ForMember(dest => dest.AccountId, opts => opts.MapFrom(src => src.Account.Id))
                .ForMember(dest => dest.AccountName, opts => opts.MapFrom(src => src.Account.Name))
                .ReverseMap();

            CreateMap<PlanModel,Plan>()
                .ForMember(dest => dest.AccountId, opts => opts.MapFrom(src => src.Account.Id))
                .ForMember(dest => dest.AccountName, opts => opts.MapFrom(src => src.Account.Name))
                .ForMember(dest => dest.PlannedTypeId, opts => opts.MapFrom(src => src.PlannedType.Id))
                .ForMember(dest => dest.PlannedTypeName, opts => opts.MapFrom(src => src.PlannedType.Name))
                .ReverseMap();

            CreateMap<CostTypeModel, CostType>()
                .ReverseMap();

            CreateMap<CostInfoModel, CostInfo>()
                .ForMember(dest => dest.AccountId, opts => opts.MapFrom(src => src.Account.Id))
                .ForMember(dest => dest.AccountName, opts => opts.MapFrom(src => src.Account.Name))
                .ForMember(dest => dest.TypeId, opts => opts.MapFrom(src => src.Type.Id))
                .ForMember(dest => dest.TypeName, opts => opts.MapFrom(src => src.Type.Name))
                .ReverseMap();

            CreateMap<BadgeModel, Badge>()
                .ReverseMap();

            CreateMap<AccountBadgeModel, AccountBadge>()
                .ForMember(dest => dest.AccountId, opts => opts.MapFrom(src => src.Account.Id))
                .ForMember(dest => dest.AccountName, opts => opts.MapFrom(src => src.Account.Name))
                .ForMember(dest => dest.BadgeId, opts => opts.MapFrom(src => src.Badge.Id))
                .ForMember(dest => dest.BadgeDescription, opts => opts.MapFrom(src => src.Badge.Description))
                .ForMember(dest => dest.BadgeImgUri, opts => opts.MapFrom(src => src.Badge.BadgeImgUri))
                .ReverseMap();

            CreateMap<AccountModel, Account>()
               .ReverseMap();

            CreateMap<CostInfosByAccountId, CostInfoModelsByAccountId>()
             .ReverseMap();

            CreateMap<CostInfosByItsPeriodicity, CostInfoModelsByItsPeriodicity>()
             .ReverseMap();

            CreateMap<AccountsByName, AccountModelsByName>()
             .ReverseMap();

            CreateMap<UsersByAccountId, UserModelsByAccountId>()
             .ReverseMap();

            CreateMap<UsersByAccountName, UserModelsByAccountName>()
             .ReverseMap();

            CreateMap<UsersByEmail, UserModelsByEmail>()
             .ReverseMap();

            CreateMap<UsersByAccountName, UserModelsByAccountName>()
             .ReverseMap();

            CreateMap<UsersByAccessType, UserModelsByAccessType>()
             .ReverseMap();

            CreateMap<CostInfosByTypeId, CostInfoModelsByPlannedTypeId>()
             .ReverseMap();
        }
    }
}
