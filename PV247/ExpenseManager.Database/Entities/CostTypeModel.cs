using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.Entities
{
    /// <summary>
    /// Represents type of the cost information.
    /// </summary>
    public class CostTypeModel : IEntity<Guid>
    {
        /// <summary>
        /// Id of the cost type.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        /// <summary>
        /// Name of this type.
        /// </summary>
        [MaxLength(128)]
        public string Name { get; set; }
        /// <summary>
        /// All costs of this type.
        /// </summary>
        public virtual List<CostInfoModel> CostInfoList { get; set; }
    }
}
