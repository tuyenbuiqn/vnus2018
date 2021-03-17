using System.Collections.Generic;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;

namespace VietUSA.Repository.Interfaces
{
    public interface ICustomizeExcelExportRepository
    {
        ResultModel<bool> UpdateExcelFormat(CUserExcelFormat parameter);
        ResultModel<bool> UpdateExcelFile(CUserExcelFile parameter);
        ResultModel<CUserExcelFile> GetUserExcelFile(CUserExcelFile parameter);
        ResultModel<List<CUserExcelColumn>> GetUserExcelColumn(CUserExcelColumn parameter);
        ResultModel<List<CUserExcelColumn>> GetUserExcelUnselectedColumn(CUserExcelColumn parameter);
        ResultModel<CUserExcelFormat> GetUserExcelFormat(CUserExcelFormat parameter);
        ResultModel<bool> UpdateExcelColumn(CUserExcelColumn parameter);
        ResultModel<bool> UpdateExcelColumnOrder(CExportUpdateExcelColumnParameter parameter);
        //ResultModel<CustomizeExportExcelModel> GetUserCustomizeExcel(CExportExcelInputModel paramter);
    }
}
