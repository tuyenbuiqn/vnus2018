using System.Web.Mvc;
using VietNamUSA.Areas.XPanel.Controllers;
using VietUSA.Business;
using VietUSA.Business.Interfaces;
using VietUSA.Repository.Common.Constants;
using VietUSA.Repository.Models;

namespace VietNamUSA.Controllers
{
    public class ArticleController : BaseController
    {
        // GET: Article
        public ActionResult Index(int id)
        {
            IArticleBusiness articleBusiness = new ArticleBusiness(FrontEndUser);
            var data = articleBusiness.GetById(new ArticleModel()
            {
                ArticleId = id
            });
            if(data.IsError || data.Data == null)
            {
                data.Data = new ArticleModel()
                {
                    ArticleDetail = CommonConstants.DataNull
                };
            }
            return View(data.Data);
        }
    }
}