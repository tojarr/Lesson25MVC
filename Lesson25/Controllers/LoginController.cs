using Lesson25.Filters;
using Lesson25.Helpers;
using Lesson25.Repositories;
using Lesson25.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace Lesson25.Controllers
{
    [OnlyMoreThanTwoSymbolsQueryParams]
    public class LoginController : Controller
    {
        [HttpGet] //Показываем страницу с формой для логина
        public ActionResult SignIn(string returnUrl)
        {
            var loginVm = new LoginVm
            {
                ReturnUrl = returnUrl
            };

            return View(loginVm);
        }

        [HttpPost] //Логиним пользователя на сайт
        public ActionResult SignIn(LoginVm loginVm)
        {
            if (ModelState.IsValid)
            {
                var accountRepository = new AccountRepository();
                var account = accountRepository.GetAccount(loginVm);

                if (account != null)
                {
                    FormsAuthentication.SignOut();
                    FormsAuthentication.SetAuthCookie(account.Login, true);
                    Session["UserId"] = account.UserName;
                    SetRefreshTokenCookie(loginVm);
                    return RedirectToLocal(loginVm.ReturnUrl);//loginVM.ReturnUrl
                }
                else
                {
                    ModelState.AddModelError("SigninError", "There is no user with such login and password");
                }
            }
            // If we got this far, something failed, redisplay form
            return View(loginVm);
        }

        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();

            HttpContext.User = new GenericPrincipal(
                new GenericIdentity(string.Empty), null);

            Session.Clear();
            System.Web.HttpContext.Current.Session.RemoveAll();

            return RedirectToLocal();
        }

        private ActionResult RedirectToLocal(string returnUrl = "")
        {
            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        //Возврашает true/false при проверке логина для закрашивания инпута зеленым
        public JsonResult IsAccountExists(string login)
        {
            var accountRepository = new AccountRepository();
            return Json(accountRepository.IsAccountExists(login),
                JsonRequestBehavior.AllowGet);
        }

        private void SetRefreshTokenCookie(LoginVm loginVm)
        {
            TokenHelper tokenHelper = new TokenHelper();
            var token = tokenHelper.GetRefreshToken(loginVm.Login, loginVm.Password);
            HttpCookie tokenCookie = new HttpCookie("PhoneBookCookie");
            tokenCookie.Value = token.RefreshToken;
            tokenCookie.Expires = DateTime.MaxValue;
            Response.Cookies.Add(tokenCookie);
        }
    }
}