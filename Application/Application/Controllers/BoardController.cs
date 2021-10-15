using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Application.Controllers
{
    public class BoardController : Controller
    {
        public ActionResult List(int? Id)
        {
            if (Id == null)
                return Content("Error Message #1");

            return View();
        }
    }
}