namespace ExpenseManager.Database.DataAccess.FilterInterfaces
{
    /// <summary>
    /// Interface for filter values
    /// </summary>
    public interface IFilterValue<in TV>
    {
        /// <summary>
        /// Filter value
        /// </summary>
        TV Value { set; }
    }
}
