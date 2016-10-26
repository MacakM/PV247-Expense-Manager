namespace DAL.DTOs
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
    }
}
