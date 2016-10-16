using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL
{
    public class ExpenseDbContext : DbContext
    {
        public DbSet<CostInfo> CostInfo { get; set; }
        public DbSet<CostType> CostTypes { get; set; }
        public DbSet<Paste> Pastes { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
