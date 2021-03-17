using System.Collections.Generic;
using System.Web.Mvc;
using VietNamUSA.Common;
using VietUSA.Business;
using VietUSA.Business.Interfaces;
using VietUSA.Repository.Common.Constants;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;

namespace VietNamUSA.Areas.XPanel.Controllers
{
    public class OrganisationController : BaseController
    {
        readonly IOrganisationBusiness _organisationBusiness;
        public OrganisationController()
        {
            if (CurrentUser == null) return;
            _organisationBusiness = new OrganisationBusiness(CurrentUser);
        }
        
        [CustomAuthorize(Permission = new[] { Role.R_ORG })]
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult Search(SearchModel<OrganisationModel> parameter)
        {
            if (parameter.Cretia == null)
            {
                parameter.Cretia = new OrganisationModel();
            }
            var data = _organisationBusiness.SearchPaging(parameter);
            return PartialView("_Organisations", data);
        }

        [CustomAuthorize(Permission = new[] { Role.R_ORG })]
        public ActionResult Get(OrganisationModel parameter)
        {
            var data = _organisationBusiness.Get(parameter);
            if (data.IsError || data.Data == null)
            {
                data.Data = new OrganisationModel();
            }
            return PartialView("_AddOrEdit", data.Data);
        }

        [CustomAuthorize(Permission = new[] { Role.C_ORG, Role.U_ORG, Role.D_ORG })]
        public JsonResult Save(OrganisationModel parameter)
        {
            var validateFileUpload = WebCommonFunctions.ValidateFileUpload(Request.Files);
            if (validateFileUpload.IsError)
            {
                return Json(validateFileUpload, JsonRequestBehavior.AllowGet);
            }

            if (Request.Files.Count > 0)
            {
                const string folder = "Organisation";
                var physicalFileResult = new ResultModel<DocumentModel>();
                foreach (var item in Request.Files)
                {
                    var fileItem = Request.Files[item.ToString()];
                    var document = new DocumentModel()
                    {
                        FullFilePath = Server.MapPath("~/Uploads/" + folder),
                        FilePath = "/Uploads/" + folder
                    };
                    if (!physicalFileResult.IsError)
                        physicalFileResult = WebCommonFunctions.CreateFile(document, fileItem);
                }
                parameter.ImageUrl = physicalFileResult.Data.FilePath;
            }
            var data = _organisationBusiness.InsertOrUpdate(parameter);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        
        [CustomAuthorize(Permission = new[] { Role.D_ORG })]
        public JsonResult Remove(OrganisationModel parameter)
        {
            var data = _organisationBusiness.Remove(parameter);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Permission = new[] { Role.U_ORG })]
        public JsonResult RemoveOrganisationImage(OrganisationModel parameter)
        {
            var dataBeforeUpdate = _organisationBusiness.RemoveOrganisationImage(parameter);
            if (!dataBeforeUpdate.IsError)
            {
                System.IO.File.Delete(Server.MapPath("~" + parameter.ImageUrl));
                return Json(dataBeforeUpdate, JsonRequestBehavior.AllowGet);
            }
            return Json(new ResultModel<int>(true, dataBeforeUpdate.Message, -1), JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(Permission = new[] { Role.U_ORG })]
        public JsonResult UpdateOrder(OrganisationOrderModel parameter)
        {
            parameter.UserId = CurrentUser.UserId;
            var data = _organisationBusiness.UpdateOrganisationOrder(parameter);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Permission = new[] { Role.U_ORG })]
        public JsonResult ChangePublishStatus(List<int> parameters)
        {
            var data = _organisationBusiness.ChangePublishStatus(parameters);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
  
