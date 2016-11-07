using AutoMapper;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Presentation.Models.Expense;

namespace ExpenseManager.Presentation.Infrastructure.Mapping
{
    class BussinessToViewModelMapping : Profile
    {
        public BussinessToViewModelMapping()
        {
            CreateMap<CostInfo, IndexViewModel>()
                .ReverseMap();
        }
    }
}