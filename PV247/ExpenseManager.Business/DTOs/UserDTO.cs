
namespace ExpenseManager.Business.DTOs
{
    public class UserDTO : ExpenseManagerDTO<int>
    {
        /// <summary>
        /// Name of the user.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Email of the user.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Account of the user.
        /// </summary>
        public AccountDTO Account { get; set; }

        /// <summary>
        /// Access type of the user.
        /// </summary>
        public AccountAccessTypeDTO AccessType { get; set; }
    }
}
