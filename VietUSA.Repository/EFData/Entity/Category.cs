using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VietUSA.Repository.EFData.Entity
{
    [Table("Category")]
    public partial class Category
    {
        [Key]
        public int CategoryId { get; set; }


        public int? GroupCategoryId { get; set; }


        [StringLength(50)]
        public string CategoryCode { get; set; }


        [Required]
        [StringLength(250)]
        public string CategoryName { get; set; }


        public int? Order { get; set; }


        public bool? IsSystemCategory { get; set; }


        [Column(TypeName = "ntext")]
        public string Note { get; set; }


        public bool? DisplayInHomePage { get; set; }


        public bool? DisplayInContentPage { get; set; }


        [StringLength(250)]
        public string GroupCategoryName { get; set; }


        public bool? IsPublished { get; set; }


        public int? DataStatusType { get; set; }


        public DateTime? CreatedDate { get; set; }


        public int? CreatedBy { get; set; }


        public DateTime? LastModifiedDate { get; set; }


        public int? LastModifiedBy { get; set; }

    }
}
