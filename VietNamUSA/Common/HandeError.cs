using System.Web.Mvc;

namespace VietNamUSA.Common
{
    public class HandeError : HandleErrorAttribute
    {
        public override void OnException(ExceptionContext filterContext)
        {
            //Exception ex = filterContext.Exception;
            //filterContext.ExceptionHandled = true;
            //var model = new HandleErrorInfo(filterContext.Exception, "Home", "Error");

            //filterContext.Result = new ViewResult()
            //{
            //    ViewName = "Error1",
            //    ViewData = new ViewDataDictionary(model)
            //};
        }
    }
}