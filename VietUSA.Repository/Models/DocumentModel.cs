using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using VietUSA.Repository.EFData.Entity;

namespace VietUSA.Repository.Models
{
    [NotMapped]
	public class DocumentModel : Document
	{
        public long? RowNumber { get; set; }
        public string FullFilePath { get; set; }
        public bool IsDisableControl { get; set; }
                /// <summary>
        /// Khai báo id nào sẽ chứa module này
        /// Default: divPaperLicenseData
        /// </summary>
        public string ContainerId { get; set; } = "divDocumentWrapper";

	}
     public class DocumentSearchResult
    {
        public DocumentModel Document { get; set; }
        public List<DocumentModel> Documents { get; set; }
    }

    [NotMapped]
    public class DocumentUpdate : Document
    {
        public string ModuleName { get; set; }
    }
}
  
