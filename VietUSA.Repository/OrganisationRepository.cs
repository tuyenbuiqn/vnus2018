using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using VietUSA.Repository.Base;
using VietUSA.Repository.Common.Functions;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.EFData.Entity;
using VietUSA.Repository.Interfaces;
using VietUSA.Repository.Models;

namespace VietUSA.Repository
{
	public class OrganisationRepository : EntityFrameworkRepository, IOrganisationRepository
	{
        public OrganisationRepository(UserInfoModel userInfoModel) : base(userInfoModel)
        {

        }
        public SearchResultModel<List<OrganisationModel>> Search(SearchModel<OrganisationModel> parameter)
        {

            var result = new SearchResultModel<List<OrganisationModel>>()
            {
                Data = new List<OrganisationModel>()
            };

            var isError = new SqlParameter("pIsError", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            var message = new SqlParameter("pMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };
            var totalRecords = new SqlParameter("pTotalRecords", SqlDbType.Int) { Direction = ParameterDirection.Output };

            var data = Context.Database.SqlQuery<OrganisationModel>
                ("SP_ORGANISATION_SEARCH @pPageIndex, @pPageSize, @pOrderColumn, @pTotalRecords out, @pIsError out, @pMessage out, @pOrganisationTypeId, @pDisplayInHomePage, @pDisplayInContentPage, @pDisplayImage, @pIsPublished"
                , CommonFunctions.GetParameter(parameter.PageIndex, "pPageIndex")
                , CommonFunctions.GetParameter(parameter.PageSize, "pPageSize")
                , CommonFunctions.GetParameter(parameter.ColumnOrder, "pOrderColumn"), totalRecords, isError, message
                , CommonFunctions.GetParameter(parameter.Cretia.OrganisationTypeId, "pOrganisationTypeId")
                , CommonFunctions.GetParameter(parameter.Cretia.DisplayInHomePage, "pDisplayInHomePage")
                , CommonFunctions.GetParameter(parameter.Cretia.DisplayInContentPage, "pDisplayInContentPage")
                , CommonFunctions.GetParameter(parameter.Cretia.DisplayImage, "pDisplayImage")
                , CommonFunctions.GetParameter(parameter.Cretia.IsPublished, "pIsPublished")
                
                ).ToList();

            result.IsError = RepositoryCommonFunctions.ConvertSqlParameterToBoolean(isError);
            result.Message = RepositoryCommonFunctions.ConvertSqlParameterToString(message);
            result.TotalRecord = RepositoryCommonFunctions.ConvertSqlParameterToInt(totalRecords);
            result.PageIndex = parameter.PageIndex;
            result.PageSize = parameter.PageSize;
            result.Data = data;
            //result.Data = listData;
            return result;
        }
        
         public ResultModel<bool> UpdateOrganisationOrder(OrganisationOrderModel parameter)
        {
            var result = new ResultModel<bool>()
            {
                IsError = false,
                Message = string.Empty
            };

            var isError = new SqlParameter("pIsError", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            var message = new SqlParameter("pMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };

            Context.Database.ExecuteSqlCommand("dbo.SP_UPDATE_Organisation_ORDER @pUserId, @pData, @pIsError out, @pMessage out"
                , CommonFunctions.GetParameter(CurrentUser.UserId, "pUserId")
                , CommonFunctions.GetParameter(parameter.Data, "pData")
                , isError, message);

            result.IsError = RepositoryCommonFunctions.ConvertSqlParameterToBoolean(isError);
            result.Message = RepositoryCommonFunctions.ConvertSqlParameterToString(message);
            result.Data = true;

            return result;
        }
	}
}
  
