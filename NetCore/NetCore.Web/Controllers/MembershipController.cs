﻿using Microsoft.AspNetCore.Mvc;
using NetCore.Data.ViewModels;
using NetCore.Services.Interfaces;
using NetCore.Services.Svcs;
using NetCore.Web.Models;

namespace NetCore.Web.Controllers
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

        [HttpPost]
        [ValidateAntiForgeryToken] // 위조방지토큰을 통해 View로부터 받은 Post data가 유효한지 검증

        //Data => Services => Web
        //Data => Services
        //Data => Web

        public IActionResult Login(LoginInfo login)
        {
            string message = string.Empty;

            if (ModelState.IsValid)
            {
                // 데이터베이스를 통해서 데이터모델이 연동되어 값을 가지고 있어서
                // 그것과 입력받은 값들과 비교를 해야함.
                // 서비스의 개념...
                if (_user.MatchTheUserInfo(login))
                {
                    TempData["Message"] = "로그인 성공!!!";
                    return RedirectToAction("index", "Membership");
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
