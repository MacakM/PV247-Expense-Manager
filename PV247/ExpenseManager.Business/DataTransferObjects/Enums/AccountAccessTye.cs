namespace ExpenseManager.Business.DataTransferObjects.Enums
{
    /// <summary>
    /// Types of access to the cost information.
    /// </summary>
    public enum AccountAccessType
    {
        /// <summary>
        /// User can read.
        /// </summary>
        Read = 1,
        /// <summary>
        /// User can read and write.
        /// </summary>
        Full = 2
    }
}
