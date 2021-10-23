using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FilterDemo.Controllers
{
    public class NaverAccountController : Controller
    {
        // GET: NaverAccount
        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string password, string returnUrl)
        {
            if(userName.EndsWith("@naver.com") && password == "test1234")
            {
                FormsAuthentication.SetAuthCookie(userName, false);
                return Redirect(returnUrl ?? Url.Action("Index", "Home"));
            }
            else
            {
                ModelState.AddModelError("", "인증 실패");
                return View();
            }
        }
    }
}