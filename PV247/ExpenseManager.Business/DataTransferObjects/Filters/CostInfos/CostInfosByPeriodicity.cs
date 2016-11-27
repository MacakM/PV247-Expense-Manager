using ExpenseManager.Business.DataTransferObjects.Enums;

namespace ExpenseManager.Business.DataTransferObjects.Filters.CostInfos
{
    /// <summary>
    /// Filters cost infos by periodicity
    /// </summary>
    public class CostInfosByItsPeriodicity : IFilter<CostInfo>
    {
        /// <summary>
        /// Periodicity of cost 
        /// </summary>
        public Periodicity Periodicity { get; set; }

        /// <summary>
        /// Filter construcor
        /// </summary>
        /// <param name="periodicity"></param>
        public CostInfosByItsPeriodicity(Periodicity periodicity)
        {
            Periodicity = periodicity;
        }
    }
}
