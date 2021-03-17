using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VietNamUSA.Common;

namespace VietNamUSA.Areas.XPanel.Controllers
{
    [CustomAuthorize]
    public class HomeController : Controller
    {
        // GET: XPanel/Home

        public ActionResult Index()
        {
            return View();
        }
    }
}