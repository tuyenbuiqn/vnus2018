using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VietUSA.Repository.EFData.Entity
{
    [Table("Organisation")]
	public partial class Organisation
	{
        [Key]   
        public int OrganisationId  { get; set; }
        
        
		[StringLength(250)]   
        public string Name  { get; set; }
        
        
		[StringLength(250)]   
        public string Website  { get; set; }
        
        
		[StringLength(250)]   
        public string ImageUrl  { get; set; }
        
        
		[Column(TypeName = "ntext")]   
        public string Description  { get; set; }
        
           
        public int? OrganisationTypeId  { get; set; }
        
        
		[StringLength(250)]   
        public string OrganisationTypeName  { get; set; }
        
           
        public bool? DisplayInHomePage  { get; set; }
        
           
        public bool? DisplayInContentPage  { get; set; }

        [StringLength(500)]
        public string Style { get; set; }

        public int? Order  { get; set; }
        
           
        public bool? IsPublished  { get; set; }
        
           
        public bool? DisplayImage  { get; set; }
        
           
        public int? DataStatusType  { get; set; }
        
        
		[Column(TypeName = "ntext")]   
        public string Note  { get; set; }
        
           
        public DateTime? CreatedDate  { get; set; }
        
           
        public int? CreatedBy  { get; set; }
        
           
        public DateTime? LastModifiedDate  { get; set; }
        
           
        public int? LastModifiedBy  { get; set; }
        
	}
}
  
