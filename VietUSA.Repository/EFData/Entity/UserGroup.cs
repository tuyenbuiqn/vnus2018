using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VietUSA.Repository.EFData.Entity
{
    [Table("UserGroup")]
    public partial class UserGroup
    {
        [Key]
        public int UserGroupId { get; set; }

        public int? UserId { get; set; }

        public int? GroupId { get; set; }

        public int? DataStatusType { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedById { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        public int? LastModifiedById { get; set; }
    }
}
