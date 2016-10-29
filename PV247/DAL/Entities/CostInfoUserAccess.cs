using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using APILayer.Enums;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Entities
{
    /// <summary>
    /// Represents user access to cost information.
    /// </summary>
    public class CostInfoUserAccess : IEntity<int>
    {
        /// <summary>
        /// Id.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// User id.
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// User that has access into cost information.
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// Cost information id.
        /// </summary>
        public int CostId { get; set; }
        /// <summary>
        /// Cost information.
        /// </summary>
        public CostInfo Cost { get; set; }
        /// <summary>
        /// Access type of the user.
        /// </summary>
        public CostAccessType AccessType { get; set; }
    }
}
