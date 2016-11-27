namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filtery by money from
    /// </summary>
    public class CostInfosByMoneyFrom : Filter<CostInfo>
    {
        /// <summary>
        /// Money from
        /// </summary>
        public decimal MoneyFrom { get; set; }

        /// <summary>
        /// Filter constructor
        /// </summary>
        /// <param name="moneyFrom"></param>
        public CostInfosByMoneyFrom(decimal moneyFrom)
        {
            MoneyFrom = moneyFrom;
        }

    }
}