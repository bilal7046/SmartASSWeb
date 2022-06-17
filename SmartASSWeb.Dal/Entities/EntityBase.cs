using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SmartASSWeb.Dal.Entities
{
    public abstract class EntityBase
    {
        [Required]
        [MaxLength(200)]
        public string CreatedBy { get; set; }
        [Required]
        public DateTime DateCreated { get; set; }
        [MaxLength(200)]
        public string UpdatedBy { get; set; }
        public DateTime? DateUpdated { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [ConcurrencyCheck]
        [Column("rowversion", TypeName = "bigint")]
        public virtual long RowVersion { get; set; }

    }
}
