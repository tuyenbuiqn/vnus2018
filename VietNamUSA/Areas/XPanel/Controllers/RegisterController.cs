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
    public class RegisterController : BaseController
    {
        readonly IUserBusiness _userBusiness;
        public RegisterController()
        {
            if (CurrentUser != null)
            {
                _userBusiness = new UserBusiness(CurrentUser);
            }
        }

        [CustomAuthorize(Permission = new[] {Role.R_A_S })]
        public ActionResult Index()
        {
            return View("Index");
        }

        public ActionResult Search(SearchModel<UserModel> parameter)
        {
            if (parameter.Cretia == null)
            {
                parameter.Cretia = new UserModel();
            }
            parameter.Cretia.UserTypeId = (int) UserTypeEnum.AnonymousUser;
            var data = _userBusiness.SearchPagingWithDocument(parameter);
            return PartialView("_Users", data);
        }

        public JsonResult ProcessCertificated(List<int> parameters)
        {
            var data = _userBusiness.ProcessCertificated(parameters);
            return Json(data,JsonRequestBehavior.AllowGet);
        }

        public JsonResult Remove(List<int> parameters)
        {
            var data = _userBusiness.RemoveRegister(parameters);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}
