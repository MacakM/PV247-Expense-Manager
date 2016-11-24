using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;

namespace ExpenseManager.Presentation.Infrastructure.Mapping
{
    class BussinessToViewModelMapping : Profile
    {
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
        }
    }
}