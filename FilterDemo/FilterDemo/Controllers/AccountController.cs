using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace FilterDemo.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
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
            bool result = FormsAuthentication.Authenticate(userName, password);
            if(result)
            {
                FormsAuthentication.SetAuthCookie(userName, false);
                return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
            }
            else
            {
                ModelState.AddModelError("", "인증 실패");
                return View();
            }
        }
    }
}