using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace COMP2084___A1.Models
{
    public partial class UserSelection
    {
        [Key]
        [Column("userID")]
        public int UserId { get; set; }
        [Required]
        [Column("full_name")]
        [StringLength(255)]
        public string FullName { get; set; }
        [Required]
        [Column("objectName")]
        [StringLength(255)]
        public string ObjectName { get; set; }
        [Required]
        [Column("description")]
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        [Column("reuse")]
        [StringLength(255)]
        public string Reuse { get; set; }
        [Required]
        [Column("reduce")]
        [StringLength(255)]
        public string Reduce { get; set; }
        [Required]
        [Column("recycle")]
        [StringLength(255)]
        public string Recycle { get; set; }
        [Required]
        [Column("photo")]
        [StringLength(255)]
        public string Photo { get; set; }
        [Column("ecoscore")]
        public int Ecoscore { get; set; }
        [Column("ecoscore_ID")]
        public int EcoscoreId { get; set; }

        [ForeignKey(nameof(EcoscoreId))]
        [InverseProperty(nameof(EcoScore.UserSelection))]
        public virtual EcoScore EcoscoreNavigation { get; set; }
    }
}
