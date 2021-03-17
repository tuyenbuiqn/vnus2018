using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VietUSA.Repository.EFData.Entity
{
    [Table("LogDbError")]
    public partial class LogDbError
    {
        [Key]
        public long LogDbErrorId { get; set; }

        [StringLength(250)]
        public string ModuleName { get; set; }

        [Column(TypeName = "ntext")]
        public string LogContent { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
