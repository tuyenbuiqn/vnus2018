using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VietUSA.Repository.EFData.Entity
{
    [Table("Contact")]
	public partial class Contact
	{
        [Key]   
        public long ContactId  { get; set; }
        
        
		[StringLength(500)]   
        public string Subject  { get; set; }
        
        
		[StringLength(250)]   
        public string FirstName  { get; set; }
        
        
		[StringLength(250)]   
        public string LastName  { get; set; }
        
        
		[StringLength(500)]   
        public string FullName  { get; set; }
        
        
		[StringLength(250)]   
        public string PhoneNumber  { get; set; }
        
        
		[StringLength(250)]   
        public string Email  { get; set; }
        
        
		[StringLength(250)]   
        public string Website  { get; set; }
        
        
		[StringLength(500)]   
        public string Address  { get; set; }
        
        
		[Column(TypeName = "ntext")]   
        public string Message  { get; set; }
        
        public int? DataStatusType  { get; set; }

        [Column(TypeName = "ntext")]   
        public string Note  { get; set; }
        
           
        public bool? IsProcessed  { get; set; }
        
           
        public int? ProcessedById  { get; set; }
        
           
        public DateTime? ProcessedDate  { get; set; }
        
           
        public DateTime? CreatedDate  { get; set; }
        
           
        public int? CreatedBy  { get; set; }
        
           
        public DateTime? LastModifiedDate  { get; set; }
        
           
        public int? LastModifiedBy { get; set; }
        
	}
}
  
