using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using VietUSA.Repository.Base;
using VietUSA.Repository.Common.Functions;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Interfaces;
using VietUSA.Repository.Models;

namespace VietUSA.Repository
{
    public class ReportRepository : EntityFrameworkRepository, IReportRepository
    {
        public ReportRepository(UserInfoModel userInfoModel) : base(userInfoModel)
        {

        }

        public SearchResultModel<List<UserModel>> AbtractionSubmissionReport(SearchModel<UserModel> parameter)
        {
            var result = new SearchResultModel<List<UserModel>>()
            {
                Data = new List<UserModel>()
            };

            var isError = new SqlParameter("pIsError", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            var message = new SqlParameter("pMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };
            var totalRecords = new SqlParameter("pTotalRecords", SqlDbType.Int) { Direction = ParameterDirection.Output };

            var data = Context.Database.SqlQuery<UserModel>("dbo.sp_Abtraction_Submission_Report @pPageIndex, @pPageSize, @pOrderColumn, @pTotalRecords out, @pIsError out, @pMessage out, @pUserName, @pFullName,@pUserType,@pEmail,@pOrganization,@pGender,@pIsCertificated,@pIsEmail,@pAddress"
                , CommonFunctions.GetParameter(parameter.PageIndex, "pPageIndex")
                , CommonFunctions.GetParameter(parameter.PageSize, "pPageSize")
                , CommonFunctions.GetParameter(parameter.ColumnOrder, "pOrderColumn")
                , totalRecords, isError, message
                , CommonFunctions.GetParameter(parameter.Cretia.Username, "pUserName")
                , CommonFunctions.GetParameter(parameter.Cretia.FullName, "pFullName")
                , CommonFunctions.GetParameter(parameter.Cretia.UserTypeId, "pUserType")
                , CommonFunctions.GetParameter(parameter.Cretia.Email, "pEmail")
                , CommonFunctions.GetParameter(parameter.Cretia.Organization, "pOrganization")
                , CommonFunctions.GetParameter(parameter.Cretia.Gender, "pGender")
                , CommonFunctions.GetParameter(parameter.Cretia.IsCertificated, "pIsCertificated")
                , CommonFunctions.GetParameter(parameter.Cretia.IsEmailed, "pIsEmail")
                , CommonFunctions.GetParameter(parameter.Cretia.Address, "pAddress")
                ).ToList();

            result.Data = data;
            result.IsError = RepositoryCommonFunctions.ConvertSqlParameterToBoolean(isError);
            result.Message = RepositoryCommonFunctions.ConvertSqlParameterToString(message);
            result.TotalRecord = RepositoryCommonFunctions.ConvertSqlParameterToInt(totalRecords);
            result.PageIndex = parameter.PageIndex;
            result.PageSize = parameter.PageSize;

            return result;
        }
    }
}
