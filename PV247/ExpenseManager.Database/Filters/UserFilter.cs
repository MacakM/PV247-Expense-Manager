using ExpenseManager.Database.Enums;

namespace ExpenseManager.Database.Filters
{
    public class UserFilter : FilterBase
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int? AccountId { get; set; }
        public string AccountName { get; set; }
        public bool DoExactMatch { get; set; }
        public AccountAccessType? AccessType { get; set; }
    }
}
