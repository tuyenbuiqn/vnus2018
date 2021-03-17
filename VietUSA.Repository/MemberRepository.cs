using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using VietUSA.Repository.Base;
using VietUSA.Repository.Common.Constants;
using VietUSA.Repository.Common.Functions;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Interfaces;
using VietUSA.Repository.Models;

namespace VietUSA.Repository
{
	public class MemberRepository : EntityFrameworkRepository, IMemberRepository
	{
        public MemberRepository(UserInfoModel userInfoModel) : base(userInfoModel)
        {

        }
        public SearchResultModel<List<MemberModel>> Search(SearchModel<MemberModel> parameter)
        {

            var result = new SearchResultModel<List<MemberModel>>()
            {
                Data = new List<MemberModel>()
            };

            var isError = new SqlParameter("pIsError", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            var message = new SqlParameter("pMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };
            var totalRecords = new SqlParameter("pTotalRecords", SqlDbType.Int) { Direction = ParameterDirection.Output };

            var data = Context.Database.SqlQuery<MemberModel>
                ("SP_MEMBER_SEARCH @pPageIndex, @pPageSize, @pOrderColumn, @pTotalRecords out, @pIsError out, @pMessage out, @pName, @pOfficeName, @pWebsite, @pTitle, @pMemberTypeId, @pIsPublised"
                , CommonFunctions.GetParameter(parameter.PageIndex, "pPageIndex")
                , CommonFunctions.GetParameter(parameter.PageSize, "pPageSize")
                , CommonFunctions.GetParameter(parameter.ColumnOrder, "pOrderColumn"), totalRecords, isError, message
                , CommonFunctions.GetParameter(parameter.Cretia.Name, "pName")
                , CommonFunctions.GetParameter(parameter.Cretia.OfficeName, "pOfficeName")
                , CommonFunctions.GetParameter(parameter.Cretia.Website, "pWebsite")
                , CommonFunctions.GetParameter(parameter.Cretia.Title, "pTitle")
                , CommonFunctions.GetParameter(parameter.Cretia.MemberTypeId, "pMemberTypeId")
                , CommonFunctions.GetParameter(parameter.Cretia.IsPublished, "pIsPublised")
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

        public ResultModel<bool> UpdateMemberOrder(MemberOrderModel parameter)
        {
            var result = new ResultModel<bool>()
            {
                IsError = false,
                Message = string.Empty
            };

            var isError = new SqlParameter("pIsError", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            var message = new SqlParameter("pMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };

            Context.Database.ExecuteSqlCommand("dbo.SP_UPDATE_MEMBER_ORDER @pMemberTypeId, @pUserId, @pData, @pIsError out, @pMessage out"
                , CommonFunctions.GetParameter(parameter.MemberTypeId, "pMemberTypeId")
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
  
