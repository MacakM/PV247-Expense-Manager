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

            CreateMap<CostInfo, Models.Expense.CreateViewModel>()
                .ReverseMap();

            CreateMap<CostType, Models.CostType.IndexViewModel>()
                .ReverseMap();
        }
    }
}