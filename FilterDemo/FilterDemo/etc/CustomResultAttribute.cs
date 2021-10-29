using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FilterDemo.etc
{
    public class CustomResultAttribute : FilterAttribute, IResultFilter
    {
        private Stopwatch timer;
        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            timer = Stopwatch.StartNew();
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            timer.Stop(); // 결과 실행후에 Stop
            filterContext.HttpContext.Response.Write(
                string.Format($"<div>액션 결과 실행 시간: {timer.Elapsed.TotalSeconds:F6}</div>"));
        }
    }
}