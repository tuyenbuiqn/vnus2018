using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VietUSA.Repository.EFData.Entity
{
    [Table("Role")]
    public partial class Role
    {
        [Key]
        public int RoleId { get; set; }

        [StringLength(50)]
        public string RoleCode { get; set; }

        [StringLength(250)]
        public string RoleName { get; set; }

        public int? DataStatusType { get; set; }

        [Column(TypeName = "ntext")]
        public string Note { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedById { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public int? LastModifiedBy { get; set; }
    }
}
