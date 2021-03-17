using System.Linq;
using System.Web;
using System.Web.Mvc;
using VietUSA.Repository.Common.Constants;
using VietUSA.Repository.Models;

namespace VietNamUSA.Common
{
    /// <summary>
    /// Lớp thuộc tính check ủy quyền của người dùng. Kế thừa từ lớp AuthorizeAttribute
    /// </summary>
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        //public bool IsMenu;
        /// <summary>
        /// Thông tin Action cần check phân quyền   
        /// </summary>
        public int[] Permission { get; set; }
        private bool _hasPermission;
        private bool _isLogged;

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var user = httpContext.Session?[SessionKeys.UserInfo] as UserInfoModel;
            if (user == null)
            {
                _isLogged = false;
                return false;
            }
            _isLogged = true;
            if (user.IsSuperAdministrator || Permission == null)
            {
                _hasPermission = true;
            }
            else
            {
                _hasPermission = user.Permissions != null && Permission.Any(t => user.Permissions.Contains(t));
            }
            return _isLogged && _hasPermission;
        }
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (_isLogged == false)
            {
                if (filterContext.HttpContext.Request.Url != null)
                {
                    var url = filterContext.HttpContext.Request.Url.AbsoluteUri;
                    if (url.ToLower().Contains("/account/changepassword"))
                    {
                        filterContext.Result = new RedirectResult("~/Xpanel/Account/Login");
                    }
                    else
                    {
                        filterContext.Result = new RedirectResult("~/Xpanel/Account/Login?returnUrl=" + url);
                    }
                }
                else
                {
                    filterContext.Result = new RedirectResult("~/Xpanel/Account/Login");
                }
            }
            else
            {
                if (_hasPermission == false)
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        //UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
                        //filterContext.HttpContext.Response.StatusCode = 401;
                        //filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                        //filterContext.HttpContext.Response.ContentType = "application/json";
                        filterContext.Result = new RedirectResult("~/Xpanel/Account/UnAuthorized");
                    }
                    else
                    {
                        //filterContext.Result = new RedirectToRouteResult(
                        //    new RouteValueDictionary
                        //    {
                        //        {"action", "UnAuthorized"},
                        //        {"controller", "Account"}
                        //    });
                        filterContext.Result = new RedirectResult("~/Xpanel/Account/UnAuthorized");
                    }
                }
                else
                {
                    if (filterContext.HttpContext.Request.IsAjaxRequest())
                    {
                        filterContext.HttpContext.Response.StatusCode = 401;
                        filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;
                        UrlHelper urlHelper = new UrlHelper(filterContext.RequestContext);
                        filterContext.HttpContext.Response.ContentType = "application/json";
                        filterContext.Result = new JsonResult
                        {
                            Data = new
                            {
                                ErrorCode = "-1",
                                ErrorMessage = "NotAuthorized",
                                Url = urlHelper.Action("LogOff", "Account")
                            },
                            JsonRequestBehavior = JsonRequestBehavior.AllowGet
                        };
                    }
                    //else
                    //{
                    //    //filterContext.Result = new RedirectResult(ConfigurationManager.AppSettings[AppSettingKeys.SsoLogOffUrl]);
                    //}
                }
            }
            // base.HandleUnauthorizedRequest(filterContext);
        }
    }
}