﻿using DAL.Entities;
using Riganti.Utils.Infrastructure.Core;
using Riganti.Utils.Infrastructure.EntityFramework;

namespace BL.Repositories
{
    public class CostInfoPasteRepository : EntityFrameworkRepository<CostInfoPaste, int>
    {
        public CostInfoPasteRepository(IUnitOfWorkProvider provider) : base(provider) { }
    }
}
