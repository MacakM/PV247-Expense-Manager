using APILayer.DTOs;
using AutoMapper;
using DAL.Entities;
using DAL.Infrastructure;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.DataAccess.Repositories
{
    public class UserPasteAccessRepository : ExpenseManagerRepository<UserPasteAccess, UserPasteAccessDTO, int>
    {
        public UserPasteAccessRepository(IUnitOfWorkProvider provider, Mapper mapper) : base(provider, mapper) { }
    }
}
