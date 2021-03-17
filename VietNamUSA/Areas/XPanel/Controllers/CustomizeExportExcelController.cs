using System.Web.Mvc;
using VietNamUSA.Common;
using VietUSA.Business;
using VietUSA.Business.Interfaces;
using VietUSA.Repository.Models;

namespace VietNamUSA.Areas.XPanel.Controllers
{
    [CustomAuthorize]
    public class CustomizeExportExcelController : BaseController
    {
        private readonly ICustomizeExcelExportBusiness _customizeExcelExportBusiness;
        public CustomizeExportExcelController()
        {
            if (CurrentUser == null) return;
            _customizeExcelExportBusiness = new CustomizeExcelExportBusiness(CurrentUser);
        }
        // GET: CustomizeExportExcel
        public ActionResult GetCustomizeExportExcel(CExportExcelInputModel parameter)
        {
            if(parameter == null)
                return PartialView("_CustomizeExportExcel", new CustomizeExportExcelModel());

            parameter.UserId = CurrentUser.UserId;
            if (string.IsNullOrEmpty(parameter.StringParameter))
                parameter.StringParameter = "{}";
            var data = _customizeExcelExportBusiness.GetUserExportExcel(parameter);
            return PartialView("_CustomizeExportExcel", data.Data);
        }
        public JsonResult UpdateExcelFormat(CUserExcelFormat parameter)
        {
            parameter.UserId = CurrentUser.UserId;
            var data = _customizeExcelExportBusiness.UpdateExcelFormat(parameter);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateExcelFile(CUserExcelFile parameter)
        {
            parameter.UserId = CurrentUser.UserId;
            var data = _customizeExcelExportBusiness.UpdateExcelFile(parameter);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateExcelColumn(CUserExcelColumn parameter)
        {
            parameter.UserId = CurrentUser.UserId;
            var data = _customizeExcelExportBusiness.UpdateExcelColumn(parameter);
            return PartialView("_UserExcelColumn", data.Data);
        }
        public ActionResult UpdateExcelColumnOrder(CExportUpdateExcelColumnParameter parameter)
        {
            parameter.UserId = CurrentUser.UserId;
            var data = _customizeExcelExportBusiness.UpdateExcelColumnOrder(parameter);
            return PartialView("_UserExcelColumn", data.Data);
        }
    }
}