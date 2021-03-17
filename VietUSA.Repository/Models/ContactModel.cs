using System.ComponentModel.DataAnnotations.Schema;
using VietUSA.Repository.EFData.Entity;

namespace VietUSA.Repository.Models
{
    [NotMapped]
	public class ContactModel : Contact
	{
        public long? RowNumber { get; set; }
        public bool IsDisableControl { get; set; }

        public bool HasAttachment { get; set; }
        public string SentTime { get; set; }
    }
}
  
