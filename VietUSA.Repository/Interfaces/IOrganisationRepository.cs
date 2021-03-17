using System.Collections.Generic;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;

namespace VietUSA.Repository.Interfaces
{
    public  interface IOrganisationRepository
    {
        SearchResultModel<List<OrganisationModel>> Search(SearchModel<OrganisationModel> parameter);
        ResultModel<bool> UpdateOrganisationOrder(OrganisationOrderModel parameter);
    }
}
  
