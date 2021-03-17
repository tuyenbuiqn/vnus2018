using System.Web.Mvc;
using VietNamUSA.Common;
using VietUSA.Business;
using VietUSA.Business.Interfaces;
using VietUSA.Repository.Common.Models;
using VietUSA.Repository.Models;
using VietUSA.Repository.Common.Constants;

namespace VietNamUSA.Areas.XPanel.Controllers
{
    public class CategoryController : BaseController
    {
        readonly ICategoryBusiness _categoryBusiness;
        //readonly ICommonBusiness _commonBusiness;
        public CategoryController()
        {
            if (CurrentUser == null) return;
            _categoryBusiness = new CategoryBusiness(CurrentUser);
            //_commonBusiness = new CommonBusiness(CurrentUser);
        }
        // GET: Category
        [CustomAuthorize(Permission = new[] { Role.R_CATE })]
        public ActionResult Index(CategoryModel parameter)
        {
            if (parameter.GroupCategoryId <= 0)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public ActionResult Search(SearchModel<CategoryModel> parameter)
        {
            if (parameter.Cretia == null)
            {
                parameter.Cretia = new CategoryModel();
            }
            parameter.Cretia.IsSystemCategory = false;
            var data = _categoryBusiness.SearchPagingFilter(parameter);
            return PartialView("_Categories", data);
        }

        //public JsonResult CheckCategoryIsUsed(CategoryModel parameter)
        //{
        //    var data = _categoryBusiness.GetReferenceCountReturnBoolean(parameter);
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}

        public ActionResult GetCategory(CategoryModel parameter)
        {
            var data = _categoryBusiness.GetCategory(parameter);
            return PartialView("_AddOrEdit", data.Data);
        }

        [CustomAuthorize(Permission = new[] { Role.R_CATE, Role.U_CATE })]
        public JsonResult Save(CategoryModel parameter)
        {
            parameter.IsSystemCategory = false;
            var data = _categoryBusiness.UpdateOrInsert(parameter);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [CustomAuthorize(Permission = new[] { Role.D_CATE })]
        public JsonResult Remove(CategoryModel parameter)
        {
            var data = _categoryBusiness.RemoveCategory(parameter);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }
}