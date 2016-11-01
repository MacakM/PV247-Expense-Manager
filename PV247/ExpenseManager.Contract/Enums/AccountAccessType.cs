using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseManager.Contract.Enums
{
    /// <summary>
    /// Types of access to the cost information.
    /// </summary>
    public enum AccountAccessType
    {
        /// <summary>
        /// User can read.
        /// </summary>
        Read,
        /// <summary>
        /// User can read and write.
        /// </summary>
        Full
    }
}
