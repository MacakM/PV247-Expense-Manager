namespace ExpenseManager.Business.Services.Interfaces
{
    /// <summary>
    /// Service with methods that resolve if accounts deserve any badges
    /// </summary>
    public interface IBadgeManagerService
    {
        /// <summary>
        /// Check accounts if any deserves some badges
        /// </summary>
        void CheckBadgesRequirements();
    }
}
