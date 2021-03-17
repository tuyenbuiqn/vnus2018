using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using VietUSA.Repository.Base;
using VietUSA.Repository.Common.Functions;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Interfaces;
using VietUSA.Repository.Models;

namespace VietUSA.Repository
{
	public class ArticleRepository : EntityFrameworkRepository, IArticleRepository
	{
        public ArticleRepository(UserInfoModel userInfoModel) : base(userInfoModel)
        {

        }
        public SearchResultModel<List<ArticleModel>> Search(SearchModel<ArticleModel> parameter)
        {

            var result = new SearchResultModel<List<ArticleModel>>()
            {
                Data = new List<ArticleModel>()
            };

            var isError = new SqlParameter("pIsError", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            var message = new SqlParameter("pMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };
            var totalRecords = new SqlParameter("pTotalRecords", SqlDbType.Int) { Direction = ParameterDirection.Output };

            var data = Context.Database.SqlQuery<ArticleModel>
                ("SP_ARTICLE_SEARCH @pPageIndex, @pPageSize, @pOrderColumn, @pTotalRecords out, @pIsError out, @pMessage out"
                , CommonFunctions.GetParameter(parameter.PageIndex, "pPageIndex")
                , CommonFunctions.GetParameter(parameter.PageSize, "pPageSize")
                , CommonFunctions.GetParameter(parameter.ColumnOrder, "pOrderColumn"), totalRecords, isError, message).ToList();
            var listData = new List<ArticleModel>();

            foreach (var item in data.ToList())
            {
                var itemData = new ArticleModel
                {
                        ArticleId = item.ArticleId,
                        Title = item.Title,
                        ArticleCategoryId = item.ArticleCategoryId,
                        ThumbnailUrl = item.ThumbnailUrl,
                        ImageUrl = item.ImageUrl,
                        Description = item.Description,
                        ArticleDetail = item.ArticleDetail,
                        Tags = item.Tags,
                        IsPublished = item.IsPublished,
                        OrderSequence = item.OrderSequence,
                        IsHomePage = item.IsHomePage,
                        IsMostRead = item.IsMostRead,
                        IsHighLight = item.IsHighLight,
                        IsEvent = item.IsEvent,
                        IsNew = item.IsNew,
                        IsHot = item.IsHot,
                        SlideId = item.SlideId,
                        ViewStaticstic = item.ViewStaticstic,
                        Author = item.Author,
                        DataStatusType = item.DataStatusType,
                        Note = item.Note,
                        CreatedDate = item.CreatedDate,
                        CreatedBy = item.CreatedBy,
                        LastModifiedDate = item.LastModifiedDate,
                        LastModifiedBy = item.LastModifiedBy,
                };
                listData.Add(itemData);
            }

            result.IsError = RepositoryCommonFunctions.ConvertSqlParameterToBoolean(isError);
            result.Message = RepositoryCommonFunctions.ConvertSqlParameterToString(message);
            result.TotalRecord = RepositoryCommonFunctions.ConvertSqlParameterToInt(totalRecords);
            result.PageIndex = parameter.PageIndex;
            result.PageSize = parameter.PageSize;
            result.Data = data;
            //result.Data = listData;
            return result;
        }
	}
}
  
