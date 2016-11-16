using System;
using System.Collections.Generic;
using System.Linq;
using ExpenseManager.Business.Services.Interfaces;
using ExpenseManager.Database.DataAccess.Queries;
using ExpenseManager.Database.DataAccess.Repositories;
using ExpenseManager.Database.Entities;

namespace ExpenseManager.Business.Services.Implementations
{
    /// <summary>
    /// Service with methods that resolve if accounts deserve any badges
    /// </summary>
    public class BadgeManagerService : IBadgeManagerService
    {

        private readonly AccountBadgeRepository _accountBadgeRepository;
        private readonly ListBadgesQuery _badgesQuery;
        private readonly ListAccountsQuery _accountsQuery;
       
        public BadgeManagerService(ListAccountsQuery accountsQuery, AccountBadgeRepository accountBadgeRepository, ListBadgesQuery badgesQuery)
        {
            _accountsQuery = accountsQuery;
            _accountBadgeRepository = accountBadgeRepository;
            _badgesQuery = badgesQuery;
        }

        /// <summary>
        /// Check account if any deserves some badges
        /// </summary>
        public void CheckBadgesRequirements()
        {
            _accountsQuery.Filter = null; 
            foreach (var account in _accountsQuery.Execute())
            {
                var badges = GetBadgesForAccount(account);
                foreach (var badge in badges)
                {

                    if(account.Badges.All(x => x.BadgeId != badge.Id))
                    {
                        var accountBadgeModel = new AccountBadgeModel
                        {
                            BadgeId = badge.Id,
                            Account = account,
                            AccountId = account.Id,
                            Badge = badge,
                            Achieved = DateTime.Now
                        };

                        _accountBadgeRepository.Insert(accountBadgeModel);
                    }
                }
            }
        }
        private List<BadgeModel> GetBadgesForAccount(AccountModel account)
        {
            List<BadgeModel> badges = new List<BadgeModel>();
            _badgesQuery.Filter = null;
            foreach (var badge in _badgesQuery.Execute())
            {
                if (AccountDeservesBadge(account, badge))
                {
                    badges.Add(badge);
                }
            }
            return badges;
        }
        /// <summary>
        ///  We hardcode if account deserves badge. We switch badge.Name and we make new check method for every badge we have.
        ///  WE WILL HAVE ONLY SMALL NUMBER OF BADGES, so we have decided to do it this way.
        /// </summary>
        /// <param name="account"></param>
        /// <param name="badge"></param>
        /// <returns></returns>
        private bool AccountDeservesBadge(AccountModel account, BadgeModel badge)
        {

            switch (badge.Name)
            {
                case "Foo":
                    return CheckAccountForFooBadge(account);
            
                default:
                    return false;
            }
        }

        private bool CheckAccountForFooBadge(AccountModel account)
        {
            return false;
        }
    }
}
