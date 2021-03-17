using System.Collections.Generic;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;

namespace VietUSA.Business.Interfaces
{
    public  interface IOrganisationBusiness
    {
        ListPagedResultModel<OrganisationModel> SearchPaging(SearchModel<OrganisationModel> parameter);
        SearchResultModel<List<OrganisationModel>> Search(SearchModel<OrganisationModel> parameter);
        ResultModel<int> RemoveOrganisationImage(OrganisationModel parameter);
        ResultModel<int> InsertOrUpdate(OrganisationModel parameter);
        ResultModel<OrganisationModel> GetById(OrganisationModel parameter);
        ResultModel<OrganisationModel> Get(OrganisationModel parameter);
        ResultModel<bool> Remove(OrganisationModel parameter);
        ResultModel<bool> UpdateOrganisationOrder(OrganisationOrderModel parameter);
        ResultModel<List<int>> ChangePublishStatus(List<int> parameters);
        ResultModel<List<OrganisationViewModel>> GetOrganisation(OrganisationModel parameter);
    }
}
  
