using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using VietUSA.Repository.Base;
using VietUSA.Repository.Common.Functions;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Interfaces;
using VietUSA.Repository.Models;

//using VNPT.HN.DOT.Repository.EFData.Entity;

namespace VietUSA.Repository
{
	public class DocumentRepository : EntityFrameworkRepository, IDocumentRepository
	{
        public DocumentRepository(UserInfoModel userInfoModel) : base(userInfoModel)
        {

        }
        public SearchResultModel<List<DocumentModel>> Search(SearchModel<DocumentModel> parameter)
        {

            var result = new SearchResultModel<List<DocumentModel>>()
            {
                Data = new List<DocumentModel>()
            };

            var isError = new SqlParameter("pIsError", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            var message = new SqlParameter("pMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };
            var totalRecords = new SqlParameter("pTotalRecords", SqlDbType.Int) { Direction = ParameterDirection.Output };

            var data = Context.Database.SqlQuery<DocumentModel>
                ("dbo.SP_DOCUMENT_SEARCH @pPageIndex, @pPageSize, @pOrderColumn, @pTotalRecords out, @pIsError out, @pMessage out, @pObjectId, @pObjectTypeId,@pDocumentTypeId,@pOtherType"
                , CommonFunctions.GetParameter(parameter.PageIndex, "pPageIndex")
                , CommonFunctions.GetParameter(parameter.PageSize, "pPageSize")
                , CommonFunctions.GetParameter(parameter.ColumnOrder, "pOrderColumn"), totalRecords, isError, message
                , CommonFunctions.GetParameter(parameter.Cretia.ObjectId, "pObjectId")
                , CommonFunctions.GetParameter(parameter.Cretia.ObjectTypeId, "pObjectTypeId")
                , CommonFunctions.GetParameter(parameter.Cretia.DocumentTypeId, "pDocumentTypeId")
                , CommonFunctions.GetParameter(parameter.Cretia.OtherType, "pOtherType")
                ).ToList();
            result.IsError = RepositoryCommonFunctions.ConvertSqlParameterToBoolean(isError);
            result.Message = RepositoryCommonFunctions.ConvertSqlParameterToString(message);
            result.TotalRecord = RepositoryCommonFunctions.ConvertSqlParameterToInt(totalRecords);
            result.PageIndex = parameter.PageIndex;
            result.PageSize = parameter.PageSize;
            result.Data = data;
            return result;
        }
	}
}
  
