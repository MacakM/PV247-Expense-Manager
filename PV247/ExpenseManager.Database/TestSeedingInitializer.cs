﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseManager.Database.Entities;
using ExpenseManager.Database.Enums;

namespace ExpenseManager.Database
{
    class TestSeedingInitializer : IDatabaseInitializer<ExpenseDbContext>
    {

        public void InitializeDatabase(ExpenseDbContext context)
        {
            Seed(context);
        }

        protected void Seed(ExpenseDbContext context)
        {
            TruncateDB(context);

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
                Periodicity = null,
                Money = 20,
                Type = costType1
            };

            var cost2 = new CostInfoModel()
            {
                Account = account,
                Created = DateTime.Now,
                Description = "Chlebík",
                IsIncome = false,
                Periodicity = null,
                Money = 50,
                Type = costType1
            };

            var cost3 = new CostInfoModel()
            {
                Account = account,
                Created = DateTime.Now,
                Description = "Chľast",
                IsIncome = false,
                Periodicity = null,
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
                Periodicity = PeriodicityModel.Month,
                Money = 1000,
                Type = costType1
            };

            context.CostInfos.Add(cost1);
            context.CostInfos.Add(cost2);
            context.CostInfos.Add(cost3);
            context.CostInfos.Add(cost4);
            context.CostInfos.Add(cost5);

            
            
            context.SaveChanges();
        }

        private void TruncateDB(ExpenseDbContext context)
        {
            DeleteAll<CostInfoModel>(context);
            DeleteAll<CostTypeModel>(context);
            DeleteAll<UserModel>(context);
            DeleteAll<AccountModel>(context);
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