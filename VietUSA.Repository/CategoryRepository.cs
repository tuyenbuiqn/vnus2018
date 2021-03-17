using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using AutoMapper;
using VietUSA.Repository.Base;
using VietUSA.Repository.Common.Functions;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.EFData.Entity;
using VietUSA.Repository.Interfaces;
using VietUSA.Repository.Models;

namespace VietUSA.Repository
{
    public class CategoryRepository : EntityFrameworkRepository, ICategoryRepository
    {
        public CategoryRepository(UserInfoModel userInfoModel) : base(userInfoModel)
        {

        }
        #region Category

        public SearchResultModel<List<CategoryModel>> SearchFilter(SearchModel<CategoryModel> parameter)
        {
            var result = new SearchResultModel<List<CategoryModel>>()
            {
                Data = new List<CategoryModel>()
            };


            var isError = new SqlParameter("pIsError", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            var message = new SqlParameter("pMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };
            var totalRecords = new SqlParameter("pTotalRecords", SqlDbType.Int) { Direction = ParameterDirection.Output };

            var data = Context.Database.SqlQuery<CategoryModel>("dbo.SP_CATEGORY_SEARCH @pCategoryName, @pIsSystemCategory, @pGroupCategoryId, @pDisplayInHomePage, @pDisplayInContentPage, @pIsPublished, @pCategoryId, @pPageIndex, @pPageSize, @pOrderColumn, @pTotalRecords out, @pIsError out, @pMessage out",
                CommonFunctions.GetParameter(parameter.Cretia.CategoryName, "pCategoryName")
                , CommonFunctions.GetParameter(parameter.Cretia.IsSystemCategory, "pIsSystemCategory")
                , CommonFunctions.GetParameter(parameter.Cretia.GroupCategoryId, "pGroupCategoryId")
                , CommonFunctions.GetParameter(parameter.Cretia.DisplayInHomePage, "pDisplayInHomePage")
                , CommonFunctions.GetParameter(parameter.Cretia.DisplayInContentPage, "pDisplayInContentPage")
                , CommonFunctions.GetParameter(parameter.Cretia.IsPublished, "pIsPublished")
                , CommonFunctions.GetParameter(parameter.Cretia.CategoryId, "pCategoryId")
                , CommonFunctions.GetParameter(parameter.PageIndex, "pPageIndex")
                , CommonFunctions.GetParameter(parameter.PageSize, "pPageSize")
                , CommonFunctions.GetParameter(parameter.ColumnOrder, "pOrderColumn")
                , totalRecords
                , isError
                , message
                ).ToList();
            var query = (from p in data
                         from q in CommonFunctions.GroupCategoryEnumToList()
                         where p.GroupCategoryId == q.Id
                         select new CategoryModel()
                         {
                             CategoryId = p.CategoryId,
                             CategoryName = p.CategoryName,
                             Note = p.Note,
                             GroupCategoryId = p.GroupCategoryId,
                             IsSystemCategory = p.IsSystemCategory,
                             RowNumber = p.RowNumber,
                             DisplayInContentPage = p.DisplayInContentPage,
                             DisplayInHomePage = p.DisplayInHomePage,
                             Order = p.Order,
                             IsPublished = p.IsPublished,
                             GroupCategoryName = q.Text,
                             CategoryCode = p.CategoryCode,
                             CategoryType = p.CategoryType,
                             DataStatusType = p.DataStatusType,
                             CreatedBy = p.CreatedBy,
                             CreatedDate = p.CreatedDate
                         });
            result.Data = query.OrderBy(x => x.GroupCategoryId).ThenBy(x => x.Order).ToList();

            result.IsError = RepositoryCommonFunctions.ConvertSqlParameterToBoolean(isError);
            result.Message = RepositoryCommonFunctions.ConvertSqlParameterToString(message);
            result.TotalRecord = RepositoryCommonFunctions.ConvertSqlParameterToInt(totalRecords);
            result.PageIndex = parameter.PageIndex;
            result.PageSize = parameter.PageSize;

            return result;
        }

        public SearchResultModel<List<CategoryModel>> Search(SearchModel<CategoryModel> parameter)
        {
            var result = new SearchResultModel<List<CategoryModel>>()
            {
                Data = new List<CategoryModel>()
            };


            var isError = new SqlParameter("pIsError", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            var message = new SqlParameter("pMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };
            var totalRecords = new SqlParameter("pTotalRecords", SqlDbType.Int) { Direction = ParameterDirection.Output };

            var data = Context.Database.SqlQuery<CategoryModel>("dbo.SP_CATEGORY_SEARCH @pCategoryName, @pIsSystemCategory, @pGroupCategoryId, @pDisplayInHomePage, @pDisplayInContentPage, @pIsPublished, @pCategoryId, @pPageIndex, @pPageSize, @pOrderColumn, @pTotalRecords out, @pIsError out, @pMessage out",
                CommonFunctions.GetParameter(parameter.Cretia.CategoryName, "pCategoryName")
                , CommonFunctions.GetParameter(parameter.Cretia.IsSystemCategory, "pIsSystemCategory")
                , CommonFunctions.GetParameter(parameter.Cretia.GroupCategoryId, "pGroupCategoryId")
                , CommonFunctions.GetParameter(parameter.Cretia.DisplayInHomePage, "pDisplayInHomePage")
                , CommonFunctions.GetParameter(parameter.Cretia.DisplayInContentPage, "pDisplayInContentPage")
                , CommonFunctions.GetParameter(parameter.Cretia.IsPublished, "pIsPublished")
                 , CommonFunctions.GetParameter(parameter.Cretia.CategoryId, "pCategoryId")
                , CommonFunctions.GetParameter(parameter.PageIndex, "pPageIndex")
                , CommonFunctions.GetParameter(parameter.PageSize, "pPageSize")
                , CommonFunctions.GetParameter(parameter.ColumnOrder, "pOrderColumn")
                , totalRecords
                , isError
                , message
                ).ToList();
            
            result.Data = data.ToList();

            result.IsError = RepositoryCommonFunctions.ConvertSqlParameterToBoolean(isError);
            result.Message = RepositoryCommonFunctions.ConvertSqlParameterToString(message);
            result.TotalRecord = RepositoryCommonFunctions.ConvertSqlParameterToInt(totalRecords);
            result.PageIndex = parameter.PageIndex;
            result.PageSize = parameter.PageSize;

            return result;
        }
        public ResultModel<bool> UpdateOrInsert(CategoryModel parameter)
        {
            var result = new ResultModel<bool>
            {
                IsError = false,
                Message = string.Empty
            };

            var updateObject = Mapper.Map<CategoryModel, Category>(parameter);
            if (updateObject.CategoryId > 0)
            {
                InsertOrUpdate<Category>(updateObject, false);
            }
            else
            {
                InsertOrUpdate<Category>(updateObject, true);
            }

            return result;
        }

        public ResultModel<int> GetReferenceCount(CategoryModel parameter)
        {
            var result = new ResultModel<int>()
            {
                IsError = false,
                Message = string.Empty
            };

            var isError = new SqlParameter("pIsError", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            var message = new SqlParameter("pMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };
            var totalRecords = new SqlParameter("pTotalRecords", SqlDbType.Int) { Direction = ParameterDirection.Output };


            var data = Context.Database.SqlQuery<int?>("dbo.SP_CATEGORY_FIND_REFERENCE @pTotalRecords out, @pIsError out, @pMessage out, @pCategoryId, @pCategoryType",
                isError, message, totalRecords, CommonFunctions.GetParameter(parameter.CategoryId, "pCategoryId"), CommonFunctions.GetParameter(parameter.CategoryType, "pCategoryType"));

            result.IsError = RepositoryCommonFunctions.ConvertSqlParameterToBoolean(isError);
            result.Message = RepositoryCommonFunctions.ConvertSqlParameterToString(message); ;
            result.Data = CommonFunctions.ConvertFromObjectToInt(data.FirstOrDefault());

            return result;
        }

        public ResultModel<List<Category>> GetCategoryByGroup(int groupId)
        {
            try
            {

                var result = new ResultModel<List<Category>>()
                {
                    IsError = false,
                    Message = string.Empty,
                    Data = Context.Categories.Where(o => o.GroupCategoryId == groupId).ToList()
                };
                return result;


            }
            catch (Exception ex)
            {
                return new ResultModel<List<Category>>
                {
                    IsError = true,
                    Message = ex.Message
                };
            }
        }
        #endregion End Category
    }
}
