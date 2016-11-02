namespace ExpenseManager.Business.DTOs
{
    public abstract class ExpenseManagerDTO<TKey>
    {
        public TKey Id { get; set; }
    }
}
