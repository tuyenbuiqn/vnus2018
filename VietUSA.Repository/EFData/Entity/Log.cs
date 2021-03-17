using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VietUSA.Repository.EFData.Entity
{
    [Table("Log")]
    public partial class Log
    {
        [Key]
        public long LogId { get; set; }

        public int? ModuleId { get; set; }

        [StringLength(250)]
        public string Action { get; set; }

        [StringLength(50)]
        public string LogType { get; set; }

        [Column(TypeName = "ntext")]
        public string LogContent { get; set; }

        public DateTime? CreatedDate { get; set; }

        public int? CreatedBy { get; set; }
    }
}
