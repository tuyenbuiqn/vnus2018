using System.Collections.Generic;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;

namespace VietUSA.Business.Interfaces
{
    public  interface IDocumentBusiness
    {
        SearchResultModel<List<DocumentModel>> Search(SearchModel<DocumentModel> parameter);
        ResultModel<DocumentSearchResult> Search(DocumentModel parameter);
        ResultModel<int> UpdateOrInsert(DocumentModel parameter);
        ResultModel<int> UpdateOrInsert(DocumentUpdate parameter);
        ResultModel<DocumentModel> Get(DocumentModel parameter);
        ResultModel<DocumentModel> GetById(DocumentModel parameter);
        ResultModel<bool> Remove(DocumentModel parameter);
    }
}
  
