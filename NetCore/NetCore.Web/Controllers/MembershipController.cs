using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetCore.Data.ViewModels;
using NetCore.Services.Interfaces;
using NetCore.Services.Svcs;
using NetCore.Web.Models;
using System.Security.Claims;
using System.Threading.Tasks;

namespace NetCore.Web.Controllers
{
    public class MembershipController : Controller
    {
        //private IUser _user = new UserService(); //인터페이스에서 Service를 사용하기 위해 Service Class 인스턴스를 받아온다.

        // 의존성 주입 - 생성자
        private IUser _user;
        private HttpContext _context;

        public MembershipController(IHttpContextAccessor accessor, IUser user)
        {
            _user = user;
            _context = accessor.HttpContext;
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

        public async Task<IActionResult> LoginAsync(LoginInfo login)
        {
            string message = string.Empty;

            if (ModelState.IsValid)
            {
                // 데이터베이스를 통해서 데이터모델이 연동되어 값을 가지고 있어서
                // 그것과 입력받은 값들과 비교를 해야함.
                // 서비스의 개념...
                if (_user.MatchTheUserInfo(login))
                {
                    // 신원보증과 승인권한
                    var userInfo = _user.GetUserInfo(login.UserId);
                    var roles = _user.GetRolesOwnedByUser(login.UserId);
                    
                    var identity = new ClaimsIdentity(claims: new[]
                    {
                        new Claim(type:ClaimTypes.Name,
                                  value:userInfo.UserName),
                        new Claim(type:ClaimTypes.Role,
                                  value:)
                    }, authenticationType:);
                    await _context.SignInAsync(scheme:CookieAuthenticationDefaults.AuthenticationScheme,
                                               principal:new ClaimsPrincipal(identity:identity),
                                               properties:);

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
