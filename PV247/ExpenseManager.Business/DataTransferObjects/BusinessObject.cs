namespace ExpenseManager.Business.DataTransferObjects
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BusinessObject<TKey>
    {
        /// <summary>
        /// 
        /// </summary>
        public TKey Id { get; set; }
    }
}
