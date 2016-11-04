namespace ExpenseManager.Business.DataTransferObjects
{
    public abstract class ExpenseManager<TKey>
    {
        public TKey Id { get; set; }
    }
}
