using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Infrastructure
{
    /// <summary>
    /// Class is used to deliver application-level configuration to EF contexts.
    /// </summary>
    public class ConnectionOptions
    {
        public string ConnectionString { get; set; }
    }
}
