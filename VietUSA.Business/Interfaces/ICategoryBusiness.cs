using System.Collections.Generic;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.EFData.Entity;
using VietUSA.Repository.Models;

namespace VietUSA.Business.Interfaces
{
    public interface ICategoryBusiness
    {
        #region Category
        ListPagedResultModel<CategoryModel> SearchPagingFilter(SearchModel<CategoryModel> parameter);
        ListPagedResultModel<CategoryModel> SearchPaging(SearchModel<CategoryModel> parameter);
        SearchResultModel<List<CategoryModel>> SearchFilter(SearchModel<CategoryModel> parameter);
        SearchResultModel<List<CategoryModel>> Search(SearchModel<CategoryModel> parameter);
        ResultModel<bool> UpdateOrInsert(CategoryModel parameter);
        ResultModel<bool> RemoveCategory(CategoryModel parameter);
        ResultModel<CategoryModel> GetById(CategoryModel parameter);
        ResultModel<CategoryModel> GetCategory(CategoryModel parameter);
        ResultModel<SelectModel> GroupCategoryToSelect2(SelectModel parameter);
        ResultModel<SelectModel> CategorySearchToSelect2(SelectModel parameter);
        ResultModel<SelectModel> CategorySearchToSelect2ByParentOrGroupCategories(SelectModel parameter);
        ResultModel<List<Category>> GetCategoryByGroup(int groupId);
        #endregion End Category
    }
}
