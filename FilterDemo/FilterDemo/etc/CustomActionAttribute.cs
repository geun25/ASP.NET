using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilterDemo.etc
{
    public class CustomActionAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if(filterContext.HttpContext.Request.IsLocal)
            {
                filterContext.Result = new HttpNotFoundResult(); // 404 에러
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }
    }
}