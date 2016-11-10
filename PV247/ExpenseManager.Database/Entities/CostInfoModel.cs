using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ExpenseManager.Database.Enums;
using Riganti.Utils.Infrastructure.Core;

namespace ExpenseManager.Database.Entities
{
    /// <summary>
    /// Represents information about user's costs.
    /// </summary>
    public class CostInfoModel : IEntity<int>
    {
        /// <summary>
        /// Id of the cost info.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// State whether set money is income or outcome.
        /// </summary>
        public bool IsIncome { get; set; }
        /// <summary>
        /// How much money has changed.
        /// </summary>
        public decimal Money { get; set; }
        /// <summary>
        /// More concrete description of the cost
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Account id.
        /// </summary>
        public int AccountId { get; set; }
        /// <summary>
        /// Date when the cost info was created.
        /// </summary>
        [DataType(DataType.Date)]
        [Required]
        public DateTime? Created { get; set; } = DateTime.Now;
        /// <summary>
        /// Account whom this cost belongs.
        /// </summary>
        [Required]
        [ForeignKey("AccountId")]
        public AccountModel Account { get; set; }
        /// <summary>
        /// Type id.
        /// </summary>
        public int TypeId { get; set; }
        /// <summary>
        /// Type of the cost.
        /// </summary>
        [Required]
        [ForeignKey("TypeId")]
        public CostTypeModel Type { get; set; }
        /// <summary>
        /// Periodicity of cost
        /// </summary>
        public PeriodicityModel? Periodicity { get; set; }
        /// <summary>
        /// Mulptiplies periodicity
        /// </summary>
        public int? PeriodicMultiplicity { get; set; }
    }
}
