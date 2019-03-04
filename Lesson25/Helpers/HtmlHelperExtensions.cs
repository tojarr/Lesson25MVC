using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lesson25.Helpers
{
    public static class HtmlHelperExtensions
    {
        public static MvcHtmlString LoginLink(this HtmlHelper htmlHelper,
                                                string signinLinkText,
                                                string signoutLinkText,
                                                string signinAction,
                                                string signoutAction,
                                                string controller)
        {
            string linkText;
            string action;

            if (!HttpContext.Current.Request.IsAuthenticated)
            {
                linkText = signinLinkText;
                action = signinAction;
            }
            else
            {
                linkText = string.Format(signoutLinkText, HttpContext.Current.Session["UserID"]);
                action = signoutAction;
            }

            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            var anchor = new TagBuilder("a");
            anchor.InnerHtml = linkText;
            anchor.Attributes["href"] = urlHelper.Action(action, controller);

            return MvcHtmlString.Create(anchor.ToString());
        }
    }
}