using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilterDemo.etc
{
    public class CustomAction2Attribute : FilterAttribute, IActionFilter
    {
        private Stopwatch timer;

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            timer = Stopwatch.StartNew(); // 액션 메소드가 실행되기 전에 타이머 동작
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            timer.Stop();
            if(filterContext.Exception == null)
            {
                filterContext.HttpContext.Response.Write(
                    string.Format($"<div>액션 메소드의 실행 시간: {timer.Elapsed.TotalSeconds:F6}</div>"));
            } 
        }
    }
}