using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using VietUSA.Repository.Base;
using VietUSA.Repository.Common.Functions;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;
using VietUSA.Repository.EFData.Entity;

namespace VietUSA.Repository
{
    public class GroupRepository : EntityFrameworkRepository
    {
        public GroupRepository(UserInfoModel userInfoModel):base(userInfoModel)
        {

        }
        public string UpdateOrInsert(Group objGroup)
        {
            try
            {
                if (objGroup.GroupId > 0)
                {
                    InsertOrUpdate<Group>(objGroup, false);
                }
                else
                {
                    InsertOrUpdate<Group>(objGroup, true);
                }
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }

        public List<Group> GetbyAll(Group inputObj)
        {
                var linqSql = from data in Context.Groups
                              select data;

                if (inputObj.GroupId > 0)
                    linqSql = linqSql.Where(o => o.GroupId == inputObj.GroupId);
                if (!string.IsNullOrEmpty(inputObj.GroupName))
                    linqSql = linqSql.Where(o => o.GroupName.Contains(inputObj.GroupName));

                return linqSql.ToList();
        }

        public SearchResultModel<List<GroupModel>> Search(SearchModel<GroupModel> parameter)
        {
            var result = new SearchResultModel<List<GroupModel>>()
            {
                Data = new List<GroupModel>()
            };

                var isError = new SqlParameter("pIsError", SqlDbType.Bit) { Direction = ParameterDirection.Output };
                var message = new SqlParameter("pMessage", SqlDbType.NVarChar,500) { Direction = ParameterDirection.Output };
                var totalRecords = new SqlParameter("pTotalRecords", SqlDbType.Int) { Direction = ParameterDirection.Output };

                var data = Context.Database.SqlQuery<GroupModel>("dbo.sp_Group_Search @pPageIndex, @pPageSize, @pOrderColumn, @pTotalRecords out, @pIsError out, @pMessage out, @pGroupName"
                    , CommonFunctions.GetParameter(parameter.PageIndex, "pPageIndex"), CommonFunctions.GetParameter(parameter.PageSize, "pPageSize"), CommonFunctions.GetParameter(parameter.ColumnOrder, "pOrderColumn"), totalRecords, isError, message
                    , CommonFunctions.GetParameter(parameter.Cretia.GroupName, "pGroupName")).ToList();

            result.Data = data;
                result.IsError = (bool)isError.Value;
                result.Message = message.Value.ToString();
                result.TotalRecord = RepositoryCommonFunctions.ConvertSqlParameterToInt(totalRecords);
                result.PageIndex = parameter.PageIndex;
                result.PageSize = parameter.PageSize;
            return result;
        }
    }
}
