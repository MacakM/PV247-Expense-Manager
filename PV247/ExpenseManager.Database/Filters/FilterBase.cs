using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Database.Filters
{
    public abstract class FilterBase
    {
        private int _pageSize = 10;
        public int PageSize
        {
            get { return PageNumber == null ? int.MaxValue : _pageSize; }
            set { _pageSize = value; }
        }

        public int? PageNumber { get; set; }
        public bool? OrderByDesc { get; set; }
        public string OrderByPropertyName { get; set; }
    }
}
