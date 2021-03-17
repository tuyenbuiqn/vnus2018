using System;
using System.Web.Mvc;
using Newtonsoft.Json;
using VietUSA.Business;
using VietUSA.Business.Interfaces;
using VietUSA.Repository.Common.Constants;
using VietUSA.Repository.Common.Enums;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;

namespace VietNamUSA.Areas.XPanel.Controllers
{
    public class ReportController : BaseController
    {
        private readonly ICustomizeExcelExportBusiness _customizeExcelExportBusiness;
        private readonly IReportBusiness _reportBusiness;

        public ReportController()
        {
            if (CurrentUser == null) return;
            _customizeExcelExportBusiness = new CustomizeExcelExportBusiness(CurrentUser);
            _reportBusiness = new ReportBusiness(CurrentUser);

        }

        // GET: XPanel/Report
        #region Customize Excel Export

        private JsonResult HandleCustomizeExportData(string message)
        {
            return Json(
                       new ResultModel<DownloadModel>()
                       {
                           Data = new DownloadModel(),
                           IsError = true,
                           Message = message,
                       }, JsonRequestBehavior.AllowGet);
        }
        private JsonResult HandleReturnData(ResultModel<byte[]> parameter, string fileName)
        {
            if (parameter.IsError)
            {
                return Json(
                    new ResultModel<DownloadModel>()
                    {
                        Data = new DownloadModel(),
                        IsError = parameter.IsError,
                        Message = parameter.Message
                    }, JsonRequestBehavior.AllowGet);
            }

            var handle = Guid.NewGuid().ToString();
            TempData[handle] = parameter.Data;
            return Json(
                   new ResultModel<DownloadModel>()
                   {
                       Data = new DownloadModel()
                       {
                           FileGuid = handle,
                           FileName = fileName
                       },
                       IsError = parameter.IsError,
                       Message = parameter.Message
                   }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CustomizeExportData(CExportExcelInputModel parameter)
        {
            try
            {
                if (parameter == null || (parameter.ObjectTypeId <= 0))
                {
                    return HandleCustomizeExportData(CommonConstants.InvalidInput + "(UserId/ObjectTypeId)!");
                }
                parameter.UserId = CurrentUser.UserId;
                parameter.TemplateFileName = Request.PhysicalApplicationPath + @"ExcelTemplate\EXCEL_EMPTY_TEMPLATE.xlsx";
                var dataBytes = new ResultModel<byte[]>();
                switch (parameter.ObjectTypeId)
                {

                    case (int)CExcelObjectTypeEnum.AbtractionSubmission:
                        var reportTravelCompanyParameter = JsonConvert.DeserializeObject<SearchModel<UserModel>>(parameter.StringParameter);
                        var reportTravelCompanyData = _reportBusiness.AbtractionSubmissionReport(reportTravelCompanyParameter);
                        dataBytes = _customizeExcelExportBusiness.ExportCustomizeExcelData(reportTravelCompanyData.Data, parameter);
                        break;
                        // Viet tiep vao day neu la Loai CExcelObjectTypeEnum #
                }

                return HandleReturnData(dataBytes, parameter.FileName);
            }
            catch (Exception ex)
            {
                return HandleCustomizeExportData(ex.Message);
            }
        }
        #endregion End Customize Excel Export
    }
}