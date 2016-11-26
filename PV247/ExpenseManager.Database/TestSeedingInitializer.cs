using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Enums;

namespace ExpenseManager.Database
{
    /// <summary>
    /// Initializer
    /// </summary>
    internal class TestSeedingInitializer : IDatabaseInitializer<ExpenseDbContext>
    {
        /// <summary>
        /// Initialize database
        /// </summary>
        /// <param name="context">context</param>
        public void InitializeDatabase(ExpenseDbContext context)
        {
            Seed(context);
        }

        /// <summary>
        /// Seed
        /// </summary>
        /// <param name="context">context</param>
        protected void Seed(ExpenseDbContext context)
        {
            TruncateDB(context);
            
            var badge2 = new BadgeModel()
            {
                Name = "Officer",
                BadgeImgUri = "mmm",
                Description = "Buy donuts"
            };

            context.Badges.Add(badge2);

            var badge = new BadgeModel()
            {
                Name = "Survivor",
                BadgeImgUri = "hmm",
                Description = "I will survive"
            };

            context.Badges.Add(badge);

            Random random = new Random();

            var account = new AccountModel()
            {
                Name = "testerAccount"
            };

            context.Accounts.Add(account);

            var user = new UserModel()
            {
                AccessType = AccountAccessTypeModel.Full,
                Name = "tester",
                Email = "tester@email.com",
                Account = account
            };

            context.Users.Add(user);

            var user2 = new UserModel()
            {
                Name = "tester2",
                Email = "tester2@email.com",
            };

            context.Users.Add(user2);

            var costType1 = new CostTypeModel()
            {
                Name = "Strava"
            };

            var costType2 = new CostTypeModel()
            {
                Name = "Zábava"
            };

            context.CostTypes.Add(costType1);
            context.CostTypes.Add(costType2);

            var cost1 = new CostInfoModel()
            {
                Account = account,
                Created = DateTime.Now,
                Description = "Fajný rohlík",
                IsIncome = false,
                Periodicity = PeriodicityModel.None,
                Money = 20,
                Type = costType1
            };

            var cost2 = new CostInfoModel()
            {
                Account = account,
                Created = DateTime.Now,
                Description = "Chlebík",
                IsIncome = false,
                Periodicity = PeriodicityModel.None,
                Money = 50,
                Type = costType1
            };

            var cost3 = new CostInfoModel()
            {
                Account = account,
                Created = DateTime.Now,
                Description = "Futsal",
                IsIncome = false,
                Periodicity = PeriodicityModel.None,
                Money = 100,
                Type = costType2
            };

            var cost4 = new CostInfoModel()
            {
                Account = account,
                Created = DateTime.Now,
                Description = "Byt",
                IsIncome = false,
                Periodicity = PeriodicityModel.Month,
                Money = 200,
                Type = costType1
            };

            var cost5 = new CostInfoModel()
            {
                Account = account,
                Created = DateTime.Now,
                Description = "Výplata",
                IsIncome = true,
                Periodicity = PeriodicityModel.Day,
                Money = 1000,
                Type = costType1
            };

            var cost6 = new CostInfoModel()
            {
                Account = account,
                Created = DateTime.Now,
                Description = "Príjem",
                IsIncome = true,
                Periodicity = PeriodicityModel.None,
                Money = 2700,
                Type = costType1
            };

            context.CostInfos.Add(cost1);
            context.CostInfos.Add(cost2);
            context.CostInfos.Add(cost3);
            context.CostInfos.Add(cost4);
            context.CostInfos.Add(cost5);
            context.CostInfos.Add(cost6);

            for (int i = 0; i < 30; i++)
            {
                var cost = new CostInfoModel()
                {
                    Account = account,
                    Created = DateTime.Now,
                    Description = "Seeded expense",
                    IsIncome = false,
                    Periodicity = PeriodicityModel.None,
                    Money = (decimal) (random.NextDouble() * 150),
                    Type = costType1
                };
                context.CostInfos.Add(cost);
            }

            var plan1 = new PlanModel()
            {
                Account = account,
                Start = Convert.ToDateTime("22.11.2016"),
                Deadline = Convert.ToDateTime("24.12.2016"),
                Description = "Ušetriť na rohlík",
                IsCompleted = false,
                PlannedMoney = 100,
                PlannedType = costType1,
                PlanType = PlanTypeModel.Save
            };

            var plan2 = new PlanModel()
            {
                Account = account,
                Start = Convert.ToDateTime("15.11.2016"),
                Deadline = Convert.ToDateTime("20.11.2016"),
                Description = "Ušetriť na Škodovku",
                IsCompleted = false,
                PlannedMoney = 5200,
                PlannedType = costType2,
                PlanType = PlanTypeModel.Save
            };

            var plan3 = new PlanModel()
            {
                Account = account,
                Start = Convert.ToDateTime("22.11.2016"),
                Deadline = Convert.ToDateTime("24.12.2016"),
                Description = "Neprežierať sa",
                IsCompleted = false,
                PlannedMoney = 2000,
                PlannedType = costType1,
                PlanType = PlanTypeModel.MaxSpend
            };

            var plan4 = new PlanModel()
            {
                Account = account,
                Start = Convert.ToDateTime("11.10.2016"),
                Deadline = Convert.ToDateTime("15.10.2016"),
                Description = "Ušetrené na niečo",
                IsCompleted = true,
                PlannedMoney = 2000,
                PlannedType = costType1,
                PlanType = PlanTypeModel.MaxSpend
            };

            var plan5 = new PlanModel()
            {
                Account = account,
                Start = Convert.ToDateTime("15.11.2016"),
                Deadline = Convert.ToDateTime("24.12.2016"),
                Description = "Ušetriť na Škodovku",
                IsCompleted = false,
                PlannedMoney = 3200,
                PlannedType = costType2,
                PlanType = PlanTypeModel.Save
            };

            context.Plans.Add(plan1);
            context.Plans.Add(plan2);
            context.Plans.Add(plan3);
            context.Plans.Add(plan4);
            context.Plans.Add(plan5);

            context.SaveChanges();
        }

        private void TruncateDB(ExpenseDbContext context)
        {
            DeleteAll<PlanModel>(context);
            DeleteAll<CostInfoModel>(context);
            DeleteAll<CostTypeModel>(context);
            DeleteAll<UserModel>(context);
            DeleteAll<AccountModel>(context);
            DeleteAll<BadgeModel>(context);
        }

        public static void DeleteAll<T>(DbContext context) where T : class
        {
            foreach (var p in context.Set<T>())
            {
                context.Entry(p).State = EntityState.Deleted;
            }

            context.SaveChanges();
        }
    }
}
