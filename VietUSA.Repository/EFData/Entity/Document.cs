using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VietUSA.Repository.EFData.Entity
{
    [Table("Document")]
	public partial class Document
	{
        [Key]   
        public int DocumentId  { get; set; }
        
           
        public int? ObjectId  { get; set; }
        
           
        public int? ObjectTypeId  { get; set; }
        
        
		[StringLength(250)]   
        public string ObjectTypeName  { get; set; }
        
           
        public int? DocumentTypeId  { get; set; }
        
        
		[StringLength(250)]   
        public string DocumentTypeName  { get; set; }
        
           
        public int? OtherType  { get; set; }
        
        
		[StringLength(250)]   
        public string OtherTypeName  { get; set; }
        
        
		[StringLength(250)]   
        public string DocumentNo  { get; set; }
        
        
		[StringLength(250)]   
        public string DocumentName  { get; set; }
        
        
		[StringLength(250)]   
        public string OrganizationIssue  { get; set; }
        
           
        public DateTime? IssueDate  { get; set; }
        
           
        public DateTime? EffectiveDate  { get; set; }
        
           
        public DateTime? EndDate  { get; set; }
        
        
		[StringLength(250)]   
        public string FileName  { get; set; }
        
        
		[StringLength(250)]   
        public string FilePath  { get; set; }
        
           
        public int? DataStatusType  { get; set; }
        
        
		[Column(TypeName = "ntext")]   
        public string Note  { get; set; }
        
           
        public DateTime? CreatedDate  { get; set; }
        
           
        public int? CreatedBy  { get; set; }
        
           
        public DateTime? LastModifiedDate  { get; set; }
        
           
        public int? LastModifiedBy  { get; set; }
        
	}
}
  
