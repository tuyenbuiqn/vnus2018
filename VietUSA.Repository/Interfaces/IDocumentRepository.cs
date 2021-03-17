using System.Collections.Generic;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;

namespace VietUSA.Repository.Interfaces
{
    public  interface IDocumentRepository
    {
        SearchResultModel<List<DocumentModel>> Search(SearchModel<DocumentModel> parameter);
    }
}
  
