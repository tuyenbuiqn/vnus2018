using System.Web.Mvc;
using VietUSA.Business;
using VietUSA.Business.Interfaces;
using VietUSA.Repository.Models;

namespace VietNamUSA.Areas.XPanel.Controllers
{
    public class CommonController : BaseController
    {
        readonly ICategoryBusiness _categoryBusiness;
        public CommonController()
        {
            if (CurrentUser == null) return;
            _categoryBusiness = new CategoryBusiness(CurrentUser);
        }

        [HttpGet]
        public virtual ActionResult Download(string fileGuid, string fileName)
        {
            if (TempData[fileGuid] == null) return new EmptyResult();
            var data = TempData[fileGuid] as byte[];
            TempData[fileGuid] = null;
            return File(data, "application/vnd.ms-excel", fileName);
        }
        public ActionResult GetCategory(SelectModel parameter)
        {
            var dataCate = _categoryBusiness.CategorySearchToSelect2(parameter);
            return PartialView("_DoTSelect", dataCate.Data);
        }
        public ActionResult GetGroupCategory(SelectModel parameter)
        {
            var dataCate = _categoryBusiness.GroupCategoryToSelect2(parameter);
            return PartialView("_DoTSelect", dataCate.Data);
        }
    }
}