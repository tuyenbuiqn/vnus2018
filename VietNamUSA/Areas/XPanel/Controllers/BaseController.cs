using System.Data.SqlClient;
using System.Globalization;
using System.Threading;
using System.Web.Mvc;
using System.Web.Routing;
using VietNamUSA.Common;
using VietUSA.Repository.Common.Constants;
using VietUSA.Repository.Models;

namespace VietNamUSA.Areas.XPanel.Controllers
{
    [HandeError]
    public abstract class BaseController : Controller
    {
        protected UserInfoModel CurrentUser => System.Web.HttpContext.Current.Session[SessionKeys.UserInfo] as UserInfoModel;
        protected UserInfoModel FrontEndUser = new UserInfoModel()
        {
            UserId = 2,
            Username = "FrontEndAccount",
            FullName = "Frontend Account",
            ConnectionInfo =
                {
                    SchemaName = "dbo",
                    SqlConnection = new SqlConnection(System.Configuration.ConfigurationManager
                        .ConnectionStrings["WebsiteVietUsaDatabase"]
                        .ConnectionString)
                }
        };
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);

            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-GB");
            Thread.CurrentThread.CurrentUICulture = CultureInfo.GetCultureInfo("en-GB");
        }

    }
}