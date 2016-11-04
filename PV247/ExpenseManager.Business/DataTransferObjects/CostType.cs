namespace ExpenseManager.Business.DataTransferObjects
{
    public class CostType : ExpenseManager<int>
    {
        /// <summary>
        /// Name of this type.
        /// </summary>
        public string Name { get; set; }
    }
}
