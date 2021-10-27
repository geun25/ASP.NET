using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilterDemo.etc
{
    public class ExceptionTestAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if(!filterContext.ExceptionHandled && filterContext.Exception is ArgumentOutOfRangeException)
            {
                //filterContext.Result = new RedirectResult("~/Content/RangeErrorPage.html");
                //filterContext.ExceptionHandled = true;
                int val = (int)(((ArgumentOutOfRangeException)filterContext.Exception).ActualValue); //id값

                filterContext.Result = new ViewResult
                {
                    ViewName = "ErrorView",
                    ViewData = new ViewDataDictionary<int>(val)
                };

                filterContext.ExceptionHandled = true;
            }
        }
    }
}