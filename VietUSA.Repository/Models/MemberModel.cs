using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using VietUSA.Repository.EFData.Entity;

namespace VietUSA.Repository.Models
{
    [NotMapped]
	public class MemberModel : Member
	{
        public long? RowNumber { get; set; }
        public bool IsDisableControl { get; set; }

        public List<Select2Data> MemberTypes { get; set; }
	}

    [NotMapped]
    public class MemberOrderModel
    {
        public int UserId { get; set; }
        public int MemberTypeId { get; set; }
        public List<MemberModel> OrderItems { get; set; }
        public string Data { get; set; }
    }
}
  
