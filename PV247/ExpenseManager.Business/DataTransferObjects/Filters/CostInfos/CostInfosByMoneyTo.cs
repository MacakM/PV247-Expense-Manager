namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filtery by money to
    /// </summary>
    public class CostInfosByMoneyTo : IFilter<CostInfo>
    {
        /// <summary>
        /// Money to
        /// </summary>
        public decimal MoneyTo { get; set; }

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="moneyTo"></param>
        public CostInfosByMoneyTo(decimal moneyTo)
        {
            MoneyTo = moneyTo;
        }
    }
}
