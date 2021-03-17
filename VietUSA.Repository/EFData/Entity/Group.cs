using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VietUSA.Repository.EFData.Entity
{
    [Table("Group")]
	public partial class Group
	{
		[Key]
		public int GroupId { get; set; }

		[StringLength(50)]
		public string GroupCode { get; set; }

		[StringLength(250)]
		public string GroupName { get; set; }

		public bool IsAdministrator { get; set; }

		public int? GroupType { get; set; }

		public int? DataStatusType { get; set; }

		[Column(TypeName = "ntext")]
		public string Note { get; set; }

		public DateTime? CreatedDate { get; set; }

		public int? CreatedById { get; set; }

		public DateTime? LastModifiedDate { get; set; }

		public int? LastModifiedById { get; set; }
	}
}
