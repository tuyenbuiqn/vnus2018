using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace VietUSA.Repository.EFData.Entity
{
    [Table("Article")]
	public partial class Article
	{
        [Key]   
        public int ArticleId  { get; set; }
        
        
		[StringLength(250)]   
        public string Title  { get; set; }

        [StringLength(250)]
        public string Alias { get; set; }

        public int? ArticleCategoryId  { get; set; }
        
        
		[StringLength(250)]   
        public string ThumbnailUrl  { get; set; }
        
        
		[StringLength(250)]   
        public string ImageUrl  { get; set; }
        
        
		[Column(TypeName = "ntext")]   
        public string Description  { get; set; }
        
        
		[Column(TypeName = "ntext")]   
        [AllowHtml]
        public string ArticleDetail  { get; set; }
        
        
		[Column(TypeName = "ntext")]   
        public string Tags  { get; set; }
        
           
        public bool? IsPublished  { get; set; }
        
           
        public int? OrderSequence  { get; set; }
        
           
        public bool? IsHomePage  { get; set; }
        
           
        public bool? IsMostRead  { get; set; }
        
           
        public bool? IsHighLight  { get; set; }
        
           
        public bool? IsEvent  { get; set; }
        
           
        public bool? IsNew  { get; set; }
        
           
        public bool? IsHot  { get; set; }
        
           
        public int? SlideId  { get; set; }
        
           
        public int? ViewStaticstic  { get; set; }
        
        
		[StringLength(250)]   
        public string Author  { get; set; }
        
           
        public int? DataStatusType  { get; set; }
        
        
		[Column(TypeName = "ntext")]   
        public string Note  { get; set; }
        
           
        public DateTime? CreatedDate  { get; set; }
        
           
        public int? CreatedBy  { get; set; }
        
           
        public DateTime? LastModifiedDate  { get; set; }
        
           
        public int? LastModifiedBy  { get; set; }
        
	}
}
  
