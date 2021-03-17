using System.ComponentModel.DataAnnotations.Schema;
using VietUSA.Repository.EFData.Entity;

namespace VietUSA.Repository.Models
{
    [NotMapped]
	public class ArticleModel : Article
	{
        public long? RowNumber { get; set; }
        public bool IsDisableControl { get; set; }
	}
}
  
