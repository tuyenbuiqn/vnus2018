using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VietUSA.Repository.EFData.Entity
{
    [Table("GroupRole")]
    public partial class GroupRole
    {
        [Key]
        public int GroupRoleId { get; set; }

        public int GroupId { get; set; }

        public int RoleId { get; set; }

        [StringLength(250)]
        public string Note { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedById { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public int? LastModifiedById { get; set; }
    }
}
