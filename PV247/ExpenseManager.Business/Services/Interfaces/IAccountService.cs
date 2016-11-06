using System.Collections.Generic;
using ExpenseManager.Business.DataTransferObjects;
using ExpenseManager.Business.DataTransferObjects.Filters;

namespace ExpenseManager.Business.Services.Interfaces
{
    public interface IAccountService
    {
        void CreateAccount(Account account);
        void UpdateAccount(Account updatedAccount);
        void DeleteAccount(int accountId);
        Account GetAccount(int accountId);
        List<Account> ListAccounts(AccountFilter filter);
    }
}
