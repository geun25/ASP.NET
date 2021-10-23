using FilterDemo.etc;
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
    }
}