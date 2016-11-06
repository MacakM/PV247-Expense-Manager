namespace ExpenseManager.Business.DataTransferObjects
{
    /// <summary>
    /// Base class of business layer data objects, every object has its key
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    public abstract class BusinessObject<TKey>
    {
        /// <summary>
        /// Represents unique id of object used in database
        /// </summary>
        public TKey Id { get; set; }
    }
}
