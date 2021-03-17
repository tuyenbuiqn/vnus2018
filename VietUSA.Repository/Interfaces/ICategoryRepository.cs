using System.Collections.Generic;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.EFData.Entity;
using VietUSA.Repository.Models;

namespace VietUSA.Repository.Interfaces
{
    public interface ICategoryRepository
    {
        #region Category
        SearchResultModel<List<CategoryModel>> SearchFilter(SearchModel<CategoryModel> parameter);
        SearchResultModel<List<CategoryModel>> Search(SearchModel<CategoryModel> parameter);
        ResultModel<bool> UpdateOrInsert(CategoryModel parameter);
        ResultModel<int> GetReferenceCount(CategoryModel parameter);
        ResultModel<List<Category>> GetCategoryByGroup(int groupId);
        #endregion End Category

    }
}
