using System.Security.Principal;
using System.Web.Mvc;
using System.Web.Mvc.Filters;
using System.Web.Routing;
using System.Web.Security;

namespace FilterDemo.etc
{
    public class NaverAuthAttribute : FilterAttribute, IAuthenticationFilter
    {
        /// <summary>
        /// 인증검사 수행(다른 종류 필터 실행 전에 호출)
        /// </summary>
        /// <param name="context"></param>
        public void OnAuthentication(AuthenticationContext context)
        {
            IIdentity ident = context.Principal.Identity;
            if(!ident.IsAuthenticated || !ident.Name.EndsWith("@naver.com"))
            {
                context.Result = new HttpUnauthorizedResult(); 
            }
        }

        /// <summary>
        /// 인증 및 권한부여 정책에 맞지 않을때 수행
        /// </summary>
        /// <param name="context"></param>
        
        // MVC 프레임워크에서는 마지막 인증요청을 처리하기 위해
        // 액션 메소드가 실행된 직후, ActionResult가 반환 및 실행되기전에 
        // 다시 한번 OnAuthenticationChallenge 메소드를 호출
        // 인증 필터에 액션이 완료되었거나 결과가 변경되었다는 사실을 알릴 때 사용한다.
        
        // 아래 코드는 SigmOut 메소드를 호출했기 때문에 액션 메소드를 다시 사용하기 위해서는 
        // 인증을 매번 거쳐야 한다.
        public void OnAuthenticationChallenge(AuthenticationChallengeContext context)
        {
            if(context.Result == null || context.Result is HttpUnauthorizedResult)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller", "NaverAccount"},
                    { "action", "Login"},
                    { "returnUrl", context.HttpContext.Request.RawUrl }
                });
            }
            else
            {
                FormsAuthentication.SignOut();
            }
        }
    }
}