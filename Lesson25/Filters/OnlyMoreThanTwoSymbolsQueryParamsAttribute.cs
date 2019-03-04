using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Lesson25.Filters
{
    public class OnlyMoreThanTwoSymbolsQueryParamsAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Log("OnActionExecuted", filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Log("OnActionExecuting", filterContext);

            foreach (string key in HttpContext.Current.Request.QueryString.AllKeys)
            {
                string value = HttpContext.Current.Request.QueryString[key];
                if (value == null || value.Length <= 2)
                {
                    filterContext.Result = new HttpStatusCodeResult(HttpStatusCode.NotAcceptable);
                    return;
                }
            }
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Log("OnResultExecuted", filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            Log("OnResultExecuting ", filterContext);
        }

        private void Log(string methodName, ControllerContext filterContext)
        {
            string controllerName = filterContext.RouteData.Values["controller"].ToString();
            string actionName = filterContext.RouteData.Values["action"].ToString();
            string isAjaxRequest = filterContext.HttpContext.Request.IsAjaxRequest()
                ? "AJAX: "
                : string.Empty;

            var message = String.Format("{0}{1}- controller:{2} action:{3}",
                isAjaxRequest, methodName, controllerName, actionName);
            Debug.WriteLine(message);
        }
    }
}