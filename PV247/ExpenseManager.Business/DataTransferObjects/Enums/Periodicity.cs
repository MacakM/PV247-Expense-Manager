namespace ExpenseManager.Business.DataTransferObjects.Enums
{
    /// <summary>
    /// Represents periodicity of costs
    /// </summary>
    public enum Periodicity
    {
        /// <summary>
        /// Cost is NOT periodic
        /// </summary>
        None = 1,
        /// <summary>
        /// Day period
        /// </summary>
        Day = 2,
        /// <summary>
        /// Week period
        /// </summary>
        Week = 4,
        /// <summary>
        /// Month period
        /// </summary>
        Month = 8
    }
}
