using System.Collections.Generic;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;

namespace VietUSA.Repository.Interfaces
{
    public  interface IContactRepository
    {
        SearchResultModel<List<ContactModel>> Search(SearchModel<ContactModel> parameter);
    }
    
}
  
