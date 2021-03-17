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
    public class CustomizeExcelExportRepository : EntityFrameworkRepository, ICustomizeExcelExportRepository
    {
        public CustomizeExcelExportRepository(UserInfoModel userInfoModel) : base(userInfoModel)
        {

        }
        public ResultModel<bool> UpdateExcelFormat(CUserExcelFormat parameter)
        {
            var result = new ResultModel<bool>()
            {
                IsError = false,
                Message = string.Empty
            };

            var isError = new SqlParameter("pIsError", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            var message = new SqlParameter("pMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };

            Context.Database.ExecuteSqlCommand("dbo.SP_CUSTOMIZE_EXPORT_EXCEL_UPDATE_EXCEL_FORMAT  @pCUserExcelFormatId, @pCExcelFormatId, @pCExcelFileId, @pFormatStyleId, @pFormatStyleName, @pFontSize,	@pFontBold,	@pFontItalic, @pFontColor, @pVerticalAlignment,	@pHorizontalAlignment, @pBackgroundColor, @pBorderColor, @pBorderStyle,	@pUserId, @pObjectTypeId, @pObjectTypeName, @pIsError out, @pMessage out"
                , CommonFunctions.GetParameter(parameter.CUserExcelFormatId, "pCUserExcelFormatId")
                , CommonFunctions.GetParameter(parameter.CExcelFormatId, "pCExcelFormatId")
                , CommonFunctions.GetParameter(parameter.CExcelFileId, "pCExcelFileId")
                , CommonFunctions.GetParameter(parameter.FormatStyleId, "pFormatStyleId")
                , CommonFunctions.GetParameter(parameter.FormatStyleName, "pFormatStyleName")
                , CommonFunctions.GetParameter(parameter.FontSize, "pFontSize")
                , CommonFunctions.GetParameter(parameter.FontBold, "pFontBold")
                , CommonFunctions.GetParameter(parameter.FontItalic, "pFontItalic")
                , CommonFunctions.GetParameter(parameter.FontColor, "pFontColor")
                , CommonFunctions.GetParameter(parameter.VerticalAlignment, "pVerticalAlignment")
                , CommonFunctions.GetParameter(parameter.HorizontalAlignment, "pHorizontalAlignment")
                , CommonFunctions.GetParameter(parameter.BackgroundColor, "pBackgroundColor")
                , CommonFunctions.GetParameter(parameter.BorderColor, "pBorderColor")
                , CommonFunctions.GetParameter(parameter.BorderStyle, "pBorderStyle")
                , CommonFunctions.GetParameter(parameter.UserId, "pUserId")
                , CommonFunctions.GetParameter(parameter.ObjectTypeId, "pObjectTypeId")
                , CommonFunctions.GetParameter(parameter.ObjectTypeName, "pObjectTypeName")
                , isError, message);

            result.IsError = RepositoryCommonFunctions.ConvertSqlParameterToBoolean(isError);
            result.Message = RepositoryCommonFunctions.ConvertSqlParameterToString(message);
            result.Data = true;

            return result;
        }

        public ResultModel<bool> UpdateExcelFile(CUserExcelFile parameter)
        {
            var result = new ResultModel<bool>()
            {
                IsError = false,
                Message = string.Empty
            };

            var isError = new SqlParameter("pIsError", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            var message = new SqlParameter("pMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };

            Context.Database.ExecuteSqlCommand("dbo.SP_CUSTOMIZE_EXPORT_EXCEL_UPDATE_EXCEL_FILE @pCUserExcelFileId, @pCExcelFileId, @pTemplateFileName, @pStartRow,@pStartColumn,@pFileName,@pSheetIndex,@pSheetName,@pUserId,@pObjectTypeId,@pObjectTypeName, @pIsError out, @pMessage out"
                , CommonFunctions.GetParameter(parameter.CUserExcelFileId, "pCUserExcelFileId")
                , CommonFunctions.GetParameter(parameter.CExcelFileId, "pCExcelFileId")
                , CommonFunctions.GetParameter(parameter.TemplateFileName, "pTemplateFileName")
                , CommonFunctions.GetParameter(parameter.StartRow, "pStartRow")
                , CommonFunctions.GetParameter(parameter.StartColumn, "pStartColumn")
                , CommonFunctions.GetParameter(parameter.FileName, "pFileName")
                , CommonFunctions.GetParameter(parameter.SheetIndex, "pSheetIndex")
                , CommonFunctions.GetParameter(parameter.SheetName, "pSheetName")
                , CommonFunctions.GetParameter(parameter.UserId, "pUserId")
                , CommonFunctions.GetParameter(parameter.ObjectTypeId, "pObjectTypeId")
                , CommonFunctions.GetParameter(parameter.ObjectTypeName, "pObjectTypeName")
                , isError, message);

            result.IsError = RepositoryCommonFunctions.ConvertSqlParameterToBoolean(isError);
            result.Message = RepositoryCommonFunctions.ConvertSqlParameterToString(message);
            result.Data = true;

            return result;
        }
        public ResultModel<CUserExcelFile> GetUserExcelFile(CUserExcelFile parameter)
        {

            var result = new ResultModel<CUserExcelFile>()
            {
                Data = new CUserExcelFile()
            };

            var isError = new SqlParameter("pIsError", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            var message = new SqlParameter("pMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };

            var data = Context.Database.SqlQuery<CUserExcelFile>
                ("dbo.SP_CUSTOMIZE_EXPORT_EXCEL_GET_EXCEL_FILE @pUserId, @pObjectTypeId, @pIsError out, @pMessage out"
                , CommonFunctions.GetParameter(parameter.UserId, "pUserId")
                , CommonFunctions.GetParameter(parameter.ObjectTypeId, "pObjectTypeId"), isError, message
               ).FirstOrDefault();

            result.IsError = RepositoryCommonFunctions.ConvertSqlParameterToBoolean(isError);
            result.Message = RepositoryCommonFunctions.ConvertSqlParameterToString(message);
            result.Data = data;
            return result;
        }

        public ResultModel<List<CUserExcelColumn>> GetUserExcelColumn(CUserExcelColumn parameter)
        {
            var result = new ResultModel<List<CUserExcelColumn>>()
            {
                Data = new List<CUserExcelColumn>()
            };

            var isError = new SqlParameter("pIsError", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            var message = new SqlParameter("pMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };

            var data = Context.Database.SqlQuery<CUserExcelColumn>
                ("dbo.SP_CUSTOMIZE_EXPORT_EXCEL_GET_EXCEL_COLUMN @pExcelFileId, @pUserId, @pObjectTypeId,  @pIsError out, @pMessage out"
                , CommonFunctions.GetParameter(parameter.CExcelFileId, "pExcelFileId")
                , CommonFunctions.GetParameter(parameter.UserId, "pUserId")
                , CommonFunctions.GetParameter(parameter.ObjectTypeId, "pObjectTypeId"), isError, message
               ).ToList();

            result.IsError = RepositoryCommonFunctions.ConvertSqlParameterToBoolean(isError);
            result.Message = RepositoryCommonFunctions.ConvertSqlParameterToString(message);
            result.Data = data;
            return result;
        }

        public ResultModel<bool> UpdateExcelColumn(CUserExcelColumn parameter)
        {
            var result = new ResultModel<bool>()
            {
                IsError = false,
                Message = string.Empty
            };

            var isError = new SqlParameter("pIsError", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            var message = new SqlParameter("pMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };

            Context.Database.ExecuteSqlCommand("dbo.SP_CUSER_EXCEL_COLUMN_ADD_OR_REMOVE_COLUMN @pCUserExcelColumnId, @pCExcelColumnId, @pUserId, @pType, @pIsError out, @pMessage out"
                , CommonFunctions.GetParameter(parameter.CUserExcelColumnId, "pCUserExcelColumnId")
                , CommonFunctions.GetParameter(parameter.CExcelColumnId, "pCExcelColumnId")
                , CommonFunctions.GetParameter(CurrentUser.UserId, "pUserId")
                , CommonFunctions.GetParameter(parameter.Type, "pType")
                , isError, message);

            result.IsError = RepositoryCommonFunctions.ConvertSqlParameterToBoolean(isError);
            result.Message = RepositoryCommonFunctions.ConvertSqlParameterToString(message);
            result.Data = true;

            return result;
        }
        public ResultModel<bool> UpdateExcelColumnOrder(CExportUpdateExcelColumnParameter parameter)
        {

            var isError = new SqlParameter("@pIsError", SqlDbType.Bit)
            {
                Direction = ParameterDirection.Output
            };
            var message = new SqlParameter("@pMessage", SqlDbType.NVarChar, 4000)
            {
                Direction = ParameterDirection.Output
            };
            var userId = new SqlParameter("@pUserId", SqlDbType.Int)
            {
                Value = parameter.UserId
            };
            var excelFileId = new SqlParameter("@pCExcelFileId", SqlDbType.Int)
            {
                Value = parameter.CExcelFileId
            };
            var objectTypeId = new SqlParameter("@pObjectTypeId", SqlDbType.Int)
            {
                Value = parameter.ObjectTypeId
            };

            var selectedData = new SqlParameter("@pSelectedColumns", SqlDbType.Structured)
            {
                Value = parameter.SelectedData,
                TypeName = "[dbo].[Excel_Column_Type]"
            };
            var unSelectedData = new SqlParameter("@pUnSelectedColumn", SqlDbType.Structured)
            {
                Value = parameter.UnSelectedData,
                TypeName = "[dbo].[Excel_Column_Type]"
            };
            Context.Database.CommandTimeout = 15 * 60;
            // Other
            Context.Database.ExecuteSqlCommand("exec " + "dbo.[SP_CUSTOMIZE_EXPORT_EXCEL_ORDER_EXCEL_COLUMN] @pSelectedColumns,@pUnSelectedColumn,@pUserId,@pCExcelFileId,@pObjectTypeId,@pIsError,@pMessage", selectedData, unSelectedData,userId,excelFileId,objectTypeId, isError, message);
            return new ResultModel<bool>(false, CommonConstants.Success, true);
        }
        public ResultModel<List<CUserExcelColumn>> GetUserExcelUnselectedColumn(CUserExcelColumn parameter)
        {
            var result = new ResultModel<List<CUserExcelColumn>>()
            {
                Data = new List<CUserExcelColumn>()
            };

            var isError = new SqlParameter("pIsError", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            var message = new SqlParameter("pMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };

            var data = Context.Database.SqlQuery<CUserExcelColumn>
                ("dbo.SP_CUSTOMIZE_EXPORT_EXCEL_GET_UNSELECTED_EXCEL_COLUMN @pExcelFileId, @pUserId, @pObjectTypeId,  @pIsError out, @pMessage out"
                , CommonFunctions.GetParameter(parameter.CExcelFileId, "pExcelFileId")
                , CommonFunctions.GetParameter(parameter.UserId, "pUserId")
                , CommonFunctions.GetParameter(parameter.ObjectTypeId, "pObjectTypeId"), isError, message
               ).ToList();

            result.IsError = RepositoryCommonFunctions.ConvertSqlParameterToBoolean(isError);
            result.Message = RepositoryCommonFunctions.ConvertSqlParameterToString(message);
            result.Data = data;
            return result;
        }

        public ResultModel<CUserExcelFormat> GetUserExcelFormat(CUserExcelFormat parameter)
        {
            var result = new ResultModel<CUserExcelFormat>()
            {
                Data = new CUserExcelFormat()
            };

            var isError = new SqlParameter("pIsError", SqlDbType.Bit) { Direction = ParameterDirection.Output };
            var message = new SqlParameter("pMessage", SqlDbType.NVarChar, 500) { Direction = ParameterDirection.Output };

            var data = Context.Database.SqlQuery<CUserExcelFormat>
                ("dbo.SP_CUSTOMIZE_EXPORT_EXCEL_GET_EXCEL_FORMAT @pExcelFileId,@pFormatStyleId, @pUserId, @pObjectTypeId,  @pIsError out, @pMessage out"
                , CommonFunctions.GetParameter(parameter.CExcelFileId, "pExcelFileId")
                , CommonFunctions.GetParameter(parameter.FormatStyleId, "pFormatStyleId")
                , CommonFunctions.GetParameter(parameter.UserId, "pUserId")
                , CommonFunctions.GetParameter(parameter.ObjectTypeId, "pObjectTypeId"), isError, message
               ).FirstOrDefault();

            result.IsError = RepositoryCommonFunctions.ConvertSqlParameterToBoolean(isError);
            result.Message = RepositoryCommonFunctions.ConvertSqlParameterToString(message);
            result.Data = data;
            return result;
        }
    }
}
