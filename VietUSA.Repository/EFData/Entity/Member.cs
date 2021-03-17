using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace VietUSA.Repository.EFData.Entity
{
    [Table("Member")]
	public partial class Member
	{
        [Key]   
        public int MemberId  { get; set; }
        
        
		[StringLength(250)]   
        public string Name  { get; set; }
        
        
		[StringLength(250)]   
        public string Title  { get; set; }
        
        
		[StringLength(250)]   
        public string ImageUrl  { get; set; }
        
           
        public bool? Gender  { get; set; }
        
           
        public DateTime? Birthday  { get; set; }
        
           
        public int? MemberTypeId  { get; set; }
        
        
		[StringLength(250)]   
        public string MemberTypeName  { get; set; }
        
        
		[StringLength(250)]   
        public string OfficeName  { get; set; }
        
        
		[StringLength(250)]   
        public string OfficeAddress  { get; set; }
        
        
		[StringLength(250)]   
        public string Address  { get; set; }
        
        
		[Column(TypeName = "ntext")]   
        [AllowHtml]
        public string ShortDescription  { get; set; }
        
        
		[Column(TypeName = "ntext")]   
        [AllowHtml]
        public string Description  { get; set; }
        
        
		[StringLength(250)]   
        public string Telephone  { get; set; }
        
        
		[StringLength(250)]   
        public string Mobile  { get; set; }
        
        
		[StringLength(250)]   
        public string Email  { get; set; }
        
        
		[StringLength(250)]   
        public string Website  { get; set; }
        
           
        public int? Order  { get; set; }

        public bool? IsPublished  { get; set; }


        public int? DataStatusType  { get; set; }
        
        
		[Column(TypeName = "ntext")]   
        public string Note  { get; set; }
        
           
        public DateTime? CreatedDate  { get; set; }
        
           
        public int? CreatedBy  { get; set; }
        
           
        public DateTime? LastModifiedDate  { get; set; }
        
           
        public int? LastModifiedBy  { get; set; }
        
	}
}
  
