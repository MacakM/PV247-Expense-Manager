using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using ExpenseManager.Database;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Enums;

namespace ExpenseManager.DataSeeding
{
    /// <summary>
    /// Demo data for ExpenseDbContext
    /// </summary>
    public class ExpenseDbInitializer : IDatabaseInitializer<ExpenseDbContext>
    {
        /// <summary>
        /// Performs ExpenseDB initialization
        /// </summary>
        /// <param name="context">ExpenseDbContext to initialize the db</param>
        public void InitializeDatabase(ExpenseDbContext context)
        {
            TruncateDB(context);

            context.Users.AddOrUpdate(new UserModel { Name = "Demo user", Email = "demo@demo.com" });

            context.Badges.AddOrUpdate(new BadgeModel
            {
                Accounts = new List<AccountBadgeModel>(),
                BadgeImgUri = "badge.png",
                Name = "PassionatePennyPincher",
                Description = "Save >=20k CZK within all completed plans"
            });

            context.Badges.AddOrUpdate(new BadgeModel
            {
                Accounts = new List<AccountBadgeModel>(),
                BadgeImgUri = "badge.png",
                Name = "PlanCompleter",
                Description = "Complete at least 5 plans"
            });

            Random random = new Random();

            var account = new AccountModel()
            {
                Name = "testerAccount"
            };

            context.Accounts.AddOrUpdate(account);

            var user = new UserModel()
            {
                AccessType = AccountAccessTypeModel.Full,
                Name = "tester",
                Email = "tester@email.com",
                Account = account
            };

            context.Users.AddOrUpdate(user);

            var user2 = new UserModel()
            {
                Name = "tester2",
                Email = "tester2@email.com",
            };

            context.Users.AddOrUpdate(user2);

            var costType1 = new CostTypeModel()
            {
                Name = "Strava",
                Account = account
            };

            var costType2 = new CostTypeModel()
            {
                Name = "Zábava",
                Account = account
            };

            context.CostTypes.AddOrUpdate(costType1);
            context.CostTypes.AddOrUpdate(costType2);

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

            context.CostInfos.AddOrUpdate(cost1);
            context.CostInfos.AddOrUpdate(cost2);
            context.CostInfos.AddOrUpdate(cost3);
            context.CostInfos.AddOrUpdate(cost4);
            context.CostInfos.AddOrUpdate(cost5);
            context.CostInfos.AddOrUpdate(cost6);

            for (int i = 0; i < 30; i++)
            {
                var cost = new CostInfoModel()
                {
                    Account = account,
                    Created = DateTime.UtcNow.AddDays(- random.Next(0,14)),
                    Description = "Seeded expense",
                    IsIncome = false,
                    Periodicity = PeriodicityModel.None,
                    Money = (decimal)(random.NextDouble() * 150),
                    Type = costType1
                };
                context.CostInfos.Add(cost);
            }

            var plan1 = new PlanModel()
            {
                Account = account,
                Start = DateTime.ParseExact("22/11/2016", "dd/MM/yyyy", null),
                Deadline = DateTime.ParseExact("24/12/2016", "dd/MM/yyyy", null),
                Description = "Ušetriť na rohlík",
                IsCompleted = false,
                PlannedMoney = 100,
                PlannedType = costType1,
                PlanType = PlanTypeModel.Save
            };

            var plan2 = new PlanModel()
            {
                Account = account,
                Start = DateTime.ParseExact("15/11/2016", "dd/MM/yyyy", null),
                Deadline = DateTime.ParseExact("20/11/2016", "dd/MM/yyyy", null),
                Description = "Ušetriť na Škodovku",
                IsCompleted = false,
                PlannedMoney = 5200,
                PlannedType = costType2,
                PlanType = PlanTypeModel.Save
            };

            var plan3 = new PlanModel()
            {
                Account = account,
                Start = DateTime.ParseExact("22/11/2016", "dd/MM/yyyy", null),
                Deadline = DateTime.ParseExact("24/12/2016", "dd/MM/yyyy", null),
                Description = "Nemíňať na jedlo",
                IsCompleted = false,
                PlannedMoney = 2000,
                PlannedType = costType1,
                PlanType = PlanTypeModel.MaxSpend
            };

            var plan4 = new PlanModel()
            {
                Account = account,
                Start = DateTime.ParseExact("11/10/2016", "dd/MM/yyyy", null),
                Deadline = DateTime.ParseExact("15/10/2016", "dd/MM/yyyy", null),
                Description = "Ušetrené na niečo",
                IsCompleted = true,
                PlannedMoney = 2000,
                PlannedType = costType1,
                PlanType = PlanTypeModel.MaxSpend
            };

            var plan5 = new PlanModel()
            {
                Account = account,
                Start = DateTime.ParseExact("15/11/2016", "dd/MM/yyyy", null),
                Deadline = DateTime.ParseExact("24/12/2016", "dd/MM/yyyy", null),
                Description = "Ušetriť na Škodovku",
                IsCompleted = false,
                PlannedMoney = 3200,
                PlannedType = costType2,
                PlanType = PlanTypeModel.Save
            };

            context.Plans.AddOrUpdate(plan1);
            context.Plans.AddOrUpdate(plan2);
            context.Plans.AddOrUpdate(plan3);
            context.Plans.AddOrUpdate(plan4);
            context.Plans.AddOrUpdate(plan5);

            context.CostTypes.AddOrUpdate(new CostTypeModel {CostInfoList = new List<CostInfoModel>(), Name = "Food", Account = account});

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


        private static void DeleteAll<T>(DbContext context) where T : class
        {
            foreach (var p in context.Set<T>())
            {
                context.Entry(p).State = EntityState.Deleted;
            }

            context.SaveChanges();
        }
    }
}
