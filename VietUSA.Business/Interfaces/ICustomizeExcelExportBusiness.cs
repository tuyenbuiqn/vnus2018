using System.Collections.Generic;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;

namespace VietUSA.Business.Interfaces
{
    public interface ICustomizeExcelExportBusiness
    {
        ResultModel<byte[]> ExportCustomizeExcelData<T>(List<T> data, CExportExcelInputModel excelParameter);
        ResultModel<byte[]> ExportSimpleExcelTemplate<T>(List<T> data, CustomizeExportExcelModel excelParameter);
        ResultModel<bool> UpdateExcelFormat(CUserExcelFormat parameter);
        ResultModel<bool> UpdateExcelFile(CUserExcelFile parameter);
        ResultModel<CustomizeExportExcelModel> GetUserExportExcel(CExportExcelInputModel parameter);
        ResultModel<CustomizeExportExcelModel> UpdateExcelColumn(CUserExcelColumn parameter);
        ResultModel<CustomizeExportExcelModel> UpdateExcelColumnOrder(CExportUpdateExcelColumnParameter parameter);
        //ResultModel<CustomizeExportExcelModel> GetColumnAndExcelFileToExoport(CExportExcelInputModel parameter);
    }
}
