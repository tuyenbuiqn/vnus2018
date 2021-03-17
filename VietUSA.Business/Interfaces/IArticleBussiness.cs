using System.Collections.Generic;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;

namespace VietUSA.Business.Interfaces
{
    public  interface IArticleBusiness
    {
        ListPagedResultModel<ArticleModel> SearchPaging(SearchModel<ArticleModel> parameter);
        SearchResultModel<List<ArticleModel>> Search(SearchModel<ArticleModel> parameter);
        ResultModel<int> InsertOrUpdate(ArticleModel parameter);
        ResultModel<int> RemoveArticleImage(ArticleModel parameter);
        ResultModel<ArticleModel> GetById(ArticleModel parameter);
        ResultModel<ArticleModel> Get(ArticleModel parameter);
        ResultModel<bool> Remove(ArticleModel parameter);
    }
}
  
