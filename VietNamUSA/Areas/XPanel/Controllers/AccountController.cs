using System;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using VietNamUSA.Common;
using VietUSA.Business;
using VietUSA.Business.Interfaces;
using VietUSA.Repository.Common.Constants;
using VietUSA.Repository.Common.Functions;
using VietUSA.Repository.Models;
using VietUSA.Repository.Parameters;

namespace VietNamUSA.Areas.XPanel.Controllers
{
    public class AccountController : BaseController
    {
        //private readonly IUserBusiness _userBusiness;

        public AccountController()
        {
            //_userBusiness = new UserBusiness(CurrentUser);
        }
        // GET: Account
        public ActionResult UnAuthorized()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [CustomAuthorize]
        public ActionResult ChangePassword()
        {
            if (CurrentUser.IsFirstLoginPasswordChanged)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        #region Action
        //[CustomAuthorize]
        //public JsonResult ChangeFirstLoginPassword(LoginParameter parameter)
        //{
        //    IUserBusiness _userBusiness = new UserBusiness(CurrentUser);
        //    var data = _userBusiness.ChangeFirstLoginPassword(parameter);
        //    if (!data.IsError && data.Data)
        //    {
        //        CurrentUser.IsFirstLoginPasswordChanged = true;
        //        Session[SessionKeys.UserInfo] = CurrentUser;
        //    }
        //    return Json(data, JsonRequestBehavior.AllowGet);
        //}
        [HttpPost]
        public JsonResult Login(LoginParameter parameter)
        {
            /*
             INPUT 
                -   Username 
                -   Password
                -   ReturnUrl

            OUTPUT
                -   IsError
                -   Message: SUCCESS/NOT_EXISTED/NOT_AUTHORIZED
                -   Data: Content message.
            - IsError = 0 => Redirect to ReturnUrl
            - IsError = 1 => 
                - NOT_EXISTED: Alert
                - NOT_AUTHORIZED: Redirect to Url
             */
            var userInfo = new UserInfoModel
            {
                ConnectionInfo =
                {
                    SchemaName = "dbo",
                    SqlConnection = new SqlConnection(System.Configuration.ConfigurationManager
                        .ConnectionStrings["WebsiteVietUsaDatabase"]
                        .ConnectionString)
                }
            };




            IUserBusiness userBusiness = new UserBusiness(userInfo);
            var data = userBusiness.LoginAndProcess(parameter);

            var loginData = new
            {
                data.IsError,
                data.Message,
                Data = CommonFunctions.ConvertMessageCodeToMessage(data.Message),
                IsFirstLoginPasswordChanged = data.Data.UserInfoModel.IsFirstLoginPasswordChanged
            };

            // Register to Session
            if (!loginData.IsError)
            {
                userInfo = data.Data.UserInfoModel;

                userInfo.ConnectionInfo.SchemaName = "dbo";
                userInfo.ConnectionInfo.SqlConnection = new SqlConnection(System.Configuration.ConfigurationManager
                    .ConnectionStrings["WebsiteVietUsaDatabase"]
                    .ConnectionString);
                Session[SessionKeys.UserInfo] = data.Data.UserInfoModel;
            }
            return Json(loginData, JsonRequestBehavior.AllowGet);
        }

        public ActionResult LogOff(string returnurl)
        {
            if (Request.Url != null)
            {
                var baseurl = Request.Url.GetLeftPart(UriPartial.Authority) + Url.Content("~");
                if (string.IsNullOrWhiteSpace(returnurl) || returnurl.Length < baseurl.Length || returnurl.Substring(baseurl.Length).Count(f => f == '/') > 0)
                {
                    returnurl = baseurl;
                }
            }
            Session.Clear();
            Session.Abandon();
            return Redirect("/XPanel/Account/Login?returnUrl=" + returnurl);
        }

        #endregion Action
    }
}