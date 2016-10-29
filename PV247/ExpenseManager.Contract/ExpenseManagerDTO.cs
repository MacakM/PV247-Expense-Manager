namespace ExpenseManager.Contract
{
    public abstract class ExpenseManagerDTO<TKey>
    {
        public TKey Id { get; set; }
    }
}
