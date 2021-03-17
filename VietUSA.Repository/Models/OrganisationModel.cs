using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using VietUSA.Repository.EFData.Entity;

namespace VietUSA.Repository.Models
{
    [NotMapped]
    public class OrganisationModel : Organisation
    {
        public long? RowNumber { get; set; }
        public bool IsDisableControl { get; set; }
        public int CategoryId { get; set; }
        public List<CategoryModel> OrganisationTypes { get; set; }
    }

    [NotMapped]
    public class OrganisationOrderModel
    {
        public int UserId { get; set; }
        public List<OrganisationModel> OrderItems { get; set; }
        public string Data { get; set; }
    }

    [NotMapped]
    public class OrganisationViewModel : Category
    {
        public List<OrganisationModel> Organisations { get; set; }

        public OrganisationViewModel()
        {
            Organisations = new List<OrganisationModel>();
        }
    }
}

