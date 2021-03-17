using System.Collections.Generic;
using System.Web.Mvc;
using VietNamUSA.Common;
using VietUSA.Business;
using VietUSA.Business.Interfaces;
using VietUSA.Repository.Common.Constants;
using VietUSA.Repository.Common.Enums;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;

namespace VietNamUSA.Areas.XPanel.Controllers
{
    public class MemberController : BaseController
    {
        readonly IMemberBusiness _memberBusiness;
        public MemberController()
        {
            if (CurrentUser == null) return;
            _memberBusiness = new MemberBusiness(CurrentUser);
        }
        
        [CustomAuthorize(Permission = new[] { Role.R_MEM })]
        public ActionResult Index(int memberTypeId)
        {
            ViewBag.MemberTypeId = memberTypeId;
            return View();
        }
        
        public ActionResult Search(SearchModel<MemberModel> parameter)
        {
            if (parameter.Cretia == null)
            {
                parameter.Cretia = new MemberModel();
            }
            var data = _memberBusiness.SearchPaging(parameter);
            return PartialView("_Members", data);
        }

        [CustomAuthorize(Permission = new[] { Role.R_MEM })]
        public ActionResult Get(MemberModel parameter)
        {
            var data = _memberBusiness.Get(parameter);
            if (data.IsError || data.Data == null)
            {
                data.Data = new MemberModel();
            }
            return PartialView("_AddOrEdit", data.Data);
        }

        [CustomAuthorize(Permission = new[] { Role.C_MEM, Role.U_MEM, Role.D_MEM })]
        public JsonResult Save(MemberModel parameter)
        {
            var validateFileUpload = WebCommonFunctions.ValidateFileUpload(Request.Files);
            if (validateFileUpload.IsError)
            {
                return Json(validateFileUpload, JsonRequestBehavior.AllowGet);
            }

            if (Request.Files.Count > 0)
            {
                var folder = parameter.MemberTypeId == (int) MemberTypeEnum.InvitedAddresses
                    ? "InvitedAddresses"
                    : "ProgramCommittee";
                ResultModel <DocumentModel> physicalFileResult = new ResultModel<DocumentModel>();
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
            var data = _memberBusiness.InsertOrUpdate(parameter);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Permission = new[] { Role.U_MEM })]
        public JsonResult RemoveMemberImage(MemberModel parameter)
        {
            var dataBeforeUpdate = _memberBusiness.RemoveMemberImage(parameter);
            if (!dataBeforeUpdate.IsError)
            {
                System.IO.File.Delete(Server.MapPath("~" + parameter.ImageUrl));
                return Json(dataBeforeUpdate, JsonRequestBehavior.AllowGet);
            }
            return Json(new ResultModel<int>(true, dataBeforeUpdate.Message, -1), JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Permission = new[] { Role.D_MEM })]
        public JsonResult Remove(MemberModel parameter)
        {
            var data = _memberBusiness.Remove(parameter);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Permission = new[] { Role.U_MEM })]
        public JsonResult UpdateOrder(MemberOrderModel parameter)
        {
            parameter.UserId = CurrentUser.UserId;
            var data = _memberBusiness.UpdateMemberOrder(parameter);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ChangePublishStatus(List<int> parameters)
        {
            var data = _memberBusiness.ChangePublishStatus(parameters);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
  
