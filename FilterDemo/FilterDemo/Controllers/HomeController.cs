using FilterDemo.etc;
using System;
using System.Web.Mvc;

namespace FilterDemo.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        //public ActionResult Index()
        //{
        //    return View();
        //}

        //[CustomAuth(false)] // 로컳 요청 거부
        [Authorize(Users ="admin")]
        public string Index()
        {
            return "HomeController의 Index 액션 메소드";
        }

        [NaverAuth]
        [Authorize(Users = "dg@naver.com")]
        public string AccountTest()
        {
            return "홈 컨트롤의 AccountTest 액션 메소드";
        }

        [ExceptionTest]
        public string ExceptionTest(int id)
        {
            if(id > 20)
            {
                return String.Format($"아이디 값 : {id}");
            }
            else
            {
                throw new ArgumentOutOfRangeException("id", id, "");
            }
        }

        public ActionResult About()
        {
            throw new Exception("예외 발생");
        }

        [CustomAction2]
        [CustomResult]
        public string ActionFilterTest()
        {
            return "액션 필터 테스트 입니다";
        }
    }
}