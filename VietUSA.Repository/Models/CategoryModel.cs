using System.ComponentModel.DataAnnotations.Schema;
using VietUSA.Repository.EFData.Entity;

namespace VietUSA.Repository.Models

{
    [NotMapped]
    public class CategoryModel:Category
    {
        public long? RowNumber { get; set; }
        public int ReferenceCount { get; set; }
        public string CategoryType { get; set; }
    }
}
