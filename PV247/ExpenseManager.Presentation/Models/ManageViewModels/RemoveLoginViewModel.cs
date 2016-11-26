namespace ExpenseManager.Presentation.Models.ManageViewModels
{
    /// <summary>
    /// view model for removing login
    /// </summary>
    public class RemoveLoginViewModel
    {
        /// <summary>
        /// login provider
        /// </summary>
        public string LoginProvider { get; set; }

        /// <summary>
        /// the key of the provider
        /// </summary>
        public string ProviderKey { get; set; }
    }
}
