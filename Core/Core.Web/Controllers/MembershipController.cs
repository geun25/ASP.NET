using Core.Data.ViewModels;
using Core.Services.Interfaces;
using Core.Services.Svcs;
using Core.Web.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Web.Controllers
{
    public class MembershipController : Controller
    {
        //private IUser _user = new UserService(); //인터페이스에서 Service를 사용하기 위해 Service Class 인스턴스를 받아온다.

        // 의존성 주입 - 생성자
        private IUser _user;
        public MembershipController(IUser user)
        {
            _user = user;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("/Login")]
        [ValidateAntiForgeryToken] // 위조방지토큰을 통해 View로부터 받은 Post data가 유효한지 검증

        //Data => Services => Web
        //Data => Services
        //Data => Web

        public IActionResult Login(LoginInfo login)
        {
            string message = string.Empty;

            if (ModelState.IsValid)
            {
                // 서비스 개념 : 재사용성, 모듈화를 통한 효율적 관리
                if (_user.MatchTheUserInfo(login))
                {
                    TempData["Message"] = "로그인 성공!!!";
                    return RedirectToAction("Index", "Membership");
                }
                else
                {
                    message = "로그인 실패...";
                }
            }
            else
            {
                message = "로그인 정보를 올바르게 입력하세요.";
            }

            ModelState.AddModelError(string.Empty, message);
            return View(login);
        }
    }
}
