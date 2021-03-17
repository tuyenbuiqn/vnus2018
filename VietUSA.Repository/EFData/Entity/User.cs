using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VietUSA.Repository.EFData.Entity
{
    [Table("User")]
	public partial class User
	{
        [Key]   
        public int UserId  { get; set; }
        
        
		[StringLength(250)]   
        public string Username  { get; set; }
        
        
		[StringLength(250)]   
        public string Password  { get; set; }
        
        
		[StringLength(250)]   
        public string FullName  { get; set; }
        
           
        public DateTime? Birthday  { get; set; }
        
           
        public bool? Gender  { get; set; }
        
        
		[StringLength(250)]   
        public string Nation  { get; set; }
        
        
		[StringLength(250)]   
        public string PhoneNumber  { get; set; }
        
        
		[StringLength(250)]   
        public string Email  { get; set; }
        
        
		[StringLength(250)]   
        public string Organization  { get; set; }
        
        
		[StringLength(250)]   
        public string Address  { get; set; }
        
        
		[Required]   
        public int UserStatusType  { get; set; }
        
           
        public int? UserTypeId  { get; set; }
        
        
		[StringLength(250)]   
        public string UserType  { get; set; }
        
           
        public int? DepartmentId  { get; set; }
        
        
		[StringLength(250)]   
        public string DepartmentName  { get; set; }
        
        
		[Required]   
        public bool IsSuperAdministrator  { get; set; }
        
           
        public bool? IsFirstLoginPasswordChanged  { get; set; }
        
           
        public int? DataStatusType  { get; set; }
        
        
		[StringLength(250)]   
        public string Ip  { get; set; }
        
        
		[StringLength(4000)]   
        public string UserSecretInformation  { get; set; }
        
        
		[Column(TypeName = "ntext")]   
        public string Note  { get; set; }
        
           
        public bool? IsCertificated  { get; set; }
        
           
        public bool? IsEmailed  { get; set; }
        
           
        public int? CertificatedById  { get; set; }
        
           
        public DateTime? CertificatedDate  { get; set; }
        
           
        public DateTime? CreatedDate  { get; set; }
        
           
        public int? CreatedById  { get; set; }
        
           
        public DateTime? LastModifiedDate  { get; set; }
        
           
        public int? LastModifiedById  { get; set; }
        
	}
}
  
