using System.Collections.Generic;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;

namespace VietUSA.Business.Interfaces
{
    public  interface IContactBusiness
    {
        ListPagedResultModel<ContactModel> SearchPaging(SearchModel<ContactModel> parameter);
        SearchResultModel<List<ContactModel>> Search(SearchModel<ContactModel> parameter);
        ResultModel<long> InsertOrUpdate(ContactModel parameter);
        ResultModel<ContactModel> GetById(ContactModel parameter);
        ResultModel<ContactModel> Get(ContactModel parameter);
        ResultModel<bool> Remove(ContactModel parameter);
    }
}
  
