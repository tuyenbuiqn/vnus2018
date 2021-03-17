using System.Web.Mvc;
using VietNamUSA.Common;
using VietUSA.Business;
using VietUSA.Business.Interfaces;
using VietUSA.Repository.Common.Constants;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;

namespace VietNamUSA.Areas.XPanel.Controllers
{
    public class ArticleController : BaseController
    {
        readonly IArticleBusiness _articleBusiness;
        public ArticleController()
        {
            if (CurrentUser == null) return;
            _articleBusiness = new ArticleBusiness(CurrentUser);
        }

        [CustomAuthorize(Permission = new[] { Role.R_ART })]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Search(SearchModel<ArticleModel> parameter)
        {
            if (parameter.Cretia == null)
            {
                parameter.Cretia = new ArticleModel();
            }
            var data = _articleBusiness.SearchPaging(parameter);
            return PartialView("_Articles", data);
        }

        [CustomAuthorize(Permission = new[] { Role.R_ART })]
        public ActionResult Get(ArticleModel parameter)
        {
            var data = _articleBusiness.Get(parameter);
            if (data.IsError || data.Data == null)
            {
                data.Data = new ArticleModel();
            }
            return PartialView("_AddOrEdit", data.Data);
        }

        [CustomAuthorize(Permission = new[] { Role.C_ART, Role.U_ART, Role.D_ART })]
        public JsonResult Save(ArticleModel parameter)
        {
            var validateFileUpload = WebCommonFunctions.ValidateFileUpload(Request.Files);
            if (validateFileUpload.IsError)
            {
                return Json(validateFileUpload, JsonRequestBehavior.AllowGet);
            }

            if (Request.Files.Count > 0)
            {
                ResultModel<DocumentModel> physicalFileResult = new ResultModel<DocumentModel>();
                foreach (var item in Request.Files)
                {
                    var fileItem = Request.Files[item.ToString()];
                    var document = new DocumentModel()
                    {
                        FullFilePath = Server.MapPath("~/Uploads/Articles"),
                        FilePath = "/Uploads/Articles"
                    };
                    if (!physicalFileResult.IsError)
                        physicalFileResult = WebCommonFunctions.CreateFile(document, fileItem);
                }
                parameter.ImageUrl = physicalFileResult.Data.FilePath;
            }
            var data = _articleBusiness.InsertOrUpdate(parameter);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Permission = new[] { Role.U_ART })]
        public JsonResult RemoveArticleImage(ArticleModel parameter)
        {
            var dataBeforeUpdate = _articleBusiness.RemoveArticleImage(parameter);
            if (!dataBeforeUpdate.IsError)
            {
                System.IO.File.Delete(Server.MapPath("~" + parameter.ImageUrl));
                return Json(dataBeforeUpdate, JsonRequestBehavior.AllowGet);
            }
            return Json(new ResultModel<int>(true, dataBeforeUpdate.Message, -1), JsonRequestBehavior.AllowGet);
        }

        [CustomAuthorize(Permission = new[] { Role.D_ART })]
        public JsonResult Remove(ArticleModel parameter)
        {
            var data = _articleBusiness.Remove(parameter);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}

