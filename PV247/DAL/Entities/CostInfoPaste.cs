using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Riganti.Utils.Infrastructure.Core;

namespace DAL.Entities
{
    public class CostInfoPaste : IEntity<int>
    {
        /// <summary>
        /// Id.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Cost information Id.
        /// </summary>
        public int CostInfoId { get; set; }
        /// <summary>
        /// Cost information.
        /// </summary>
        [Required]
        [ForeignKey("CostInfoId")]
        public CostInfo CostInfo { get; set; }
        /// <summary>
        /// Paste id.
        /// </summary>
        public int PasteId { get; set; }
        /// <summary>
        /// Paste that is accessible to user.
        /// </summary>
        [Required]
        [ForeignKey("PasteId")]
        public Paste Paste { get; set; }
    }
}
