namespace ExpenseManager.Business.DataTransferObjects.Filters
{
    public class FilterBase
    {
        private int _pageSize = 10;
        /// <summary>
        /// 
        /// </summary>
        public int PageSize
        {
            get { return PageNumber == null ? int.MaxValue : _pageSize; }
            set { _pageSize = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public int? PageNumber { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool? OrderByDesc { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string OrderByPropertyName { get; set; }
    }
}
