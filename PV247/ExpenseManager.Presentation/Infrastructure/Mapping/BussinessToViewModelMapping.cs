using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;

namespace ExpenseManager.Presentation.Infrastructure.Mapping
{
    /// <summary>
    /// Class for managing mapping
    /// </summary>
    public class BussinessToViewModelMapping : Profile
    {
        /// <summary>
        /// Constructor where mapping is done
        /// </summary>
        public BussinessToViewModelMapping()
        {
            CreateMap<CostInfo, Models.Expense.IndexViewModel>()
                .ReverseMap();

            CreateMap<CostInfo, Models.Expense.IndexPermanentExpenseViewModel>()
                .ReverseMap();

            CreateMap<CostInfo, Models.Expense.CreateViewModel>()
                .ReverseMap();

            CreateMap<CostInfo, Models.Expense.CreatePermanentExpenseViewModel>()
                .ReverseMap();

            CreateMap<CostType, Models.CostType.IndexViewModel>()
                .ReverseMap();

            CreateMap<User, Models.User.IndexViewModel>()
                .ReverseMap();

            CreateMap<Plan, Models.Plan.PlanViewModel>()
                .ReverseMap();

            CreateMap<Plan, Models.Plan.CreateViewModel>()
                .ReverseMap();
        }
    }
}