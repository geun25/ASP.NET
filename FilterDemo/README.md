## 자격 증명 - Web.config에 설정

```swift
<authentication mode="Forms">
  <forms loginUrl="~/Account/Login" timeout="2880">
    <credentials passwordFormat="Clear">
      <user name="user" password="test1234"/>
      <user name="admin" password="test1234"/>
    </credentials>
```
두 명의 사용자에 대해 폼인증을 거치는데, 인증처리가 되지 않을경우 loginUrl로 설정된 곳으로 재전송이 이루어진다.

폼 인증을 사용하기 위해서는 System.Web.Security.FormsAuthentication 클래스를 이용한다.

이 클래스의 Authenticate, SetAuthCookie 메소드를 호출해야 한다.
* Authenticate : 사용자가 입력한 자격 증명의 유효성을 검증한다.
* SetAuthCookie : 브라우저에 보내는 Response에 쿠키를 추가해서 사용자가 매번 Request를 할 때마다 다시 인증을 받을 필요가 없도록 해준다.

```swift
[HttpPost]
public ActionResult Login(string userName, string password, string returnUrl)
{
  bool result = FormsAuthentication.Authenticate(userName, password);
  if(result)
  {
    FormsAuthentication.SetAuthCookie(userName, false);
    return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
  }
  else
  {
    ModelState.AddModelError("", "인증 실패");
    return View();
  }
}
```

---
## 사용자 정의 인증 필터
* 액션 메소드 앞에 붙여 사용한다.
```swift
[CustomAuth(false)]
```
* AuthorizeAttribute 클래스를 상속하고, AuthorizeCore 메소드를 override하여 사용한다.

다음 사용자 정의 인증 필터 클래스는 로컬 요청 거부를 수행한다.
```swift
public class CustomAuthAttribute : AuthorizeAttribute
{
  private bool localAllowed;
  public CustomAuthAttribute(bool allowedParam)
  {
    localAllowed = allowedParam;
  }

  protected override bool AuthorizeCore(HttpContextBase httpContext)
  {
    if(httpContext.Request.IsLocal)
    {
      return localAllowed;
    }
    else
    {
      return true;
    }
  }
}
```
* public 속성

-Users(string형식) : 사용자들의 이름 목록

-Roles(string형식) : 역할의 이름 목록
```swift
[Authorize(Users ="admin")]
```

---

## [MVC 프레임워크 필터 형식]
### 1. 인증 필터(Authentication) : 가장 먼저 실행되는 필터(다른 필터나 액션 메소드가 실행되기 전에 실행)
```swift
namespace System.Web.Mvc {
  public interface IAuthenticationFilter {
		void OnAuthentication(AuthenticationContext context); 
		void OnAuthenticationChallenge(AuthenticationChallengeContext context);
  }
}
```
* OnAuthentication() : 인증검사 수행(다른 종류 필터 실행 전에 호출)

* AuthenticationContext에 정의된 속성

-ActionDescriptor : 필터가 적용된 액션 메소드를 설명하는 ActionDescriptor를 반환

-Result : 인증 결과를 나타내는 ActionResult를 설정

-Principal : 사용자가 이미 인증되었다면, 현재 사용자를 식별하는 IPrincipal 인터페이스의 구현을 반환
<br><br>
* OnAuthenticationChallenge() : 요청이 액션 메소드의 인증 및 권한부여 정책에 맞지 않을때 호출한다.

MVC 프레임워크에서는 마지막 인증요청을 처리하기 위해 액션 메소드가 실행된 직후, ActionResult가 반환 및 실행되기전에 다시 한번 OnAuthenticationChallenge 메소드를 호출한다.

인증 필터에 액션이 완료되었거나 결과가 변경되었다는 사실을 알릴 때 사용한다.
<br><br>
아래 코드는 SignOut 메소드를 호출했기 때문에 액션 메소드를 다시 사용하기 위해서는 인증을 매번 거쳐야 한다.
```swift
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
```

* AuthenticationChallengeContext에 정의된 속성

-ActionDescriptor : 필터가 적용된 액션 메소드를 설명하는 ActionDescriptor를 반환 

-Result : 인증 시도의 결과를 나타내는 ActionResult를 설정, 인증 필터가 MVC프레임워크에게 ActionResult를 전달 가능

### 2. 권한 부여 필터(Authorization) : 인증 필터가 실행된 후에, 다른 필터들이나 액션 메서드가 실행 되기 전에 실행(오직 인증된 사용자에 의해서만 호출될 수 있도록 권한 부여를 제어하는 필터)
```swift
namespace System.Web.Mvc{
	public interface IAuthorizationFilter{
		void OnAuthorization(AuthorizationContext filterContext);
	}
}
```

### 3. 액션 필터(Action) : 액션 메소드가 실행 되기 전이나 실행 후에 실행
namespace System.Web.Mvc{
	public interface IActionFilter{
		void OnActionExecuting(ActionExecutingContext filterContext);
		void OnActionExecuted(ActionExecutedContext filterContext);
	}
}

* ActionExecutingContext 클래스의 속성

-ActionDescriptor(ActionDescriptor형식) 

-Result(ActionResult형식)

* 필터 테스트
```swift
private Stopwatch timer;

public void OnActionExecuting(ActionExecutingContext filterContext)
{
	timer = Stopwatch.StartNew(); 
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
```
<image src = "https://user-images.githubusercontent.com/78133537/140911990-19002af6-ca90-490b-8782-bdb6514dc798.png" width = "900">
실행시간은 먼저 브라우저 상에 보여지는것 뿐, 액션 메소드가 호출된 후 실행된 결과이다. 


### 4. 결과 필터(Result) : 액션 결과가 실행 되기 전이나 후에 실행
```swift
namespace System.Web.Mvc{
	public interface IResultFilter{
		void OnResultExecuting(ResultExecutingContext filterContext); 
		void OnResultExecuted(ResultExecutedContext filterContext); 
	}
}
```
* OnResultExecuting 메소드 : 액션 결과가 실행되기 전(액션 메소드가 액션 결과를 반환하는 시점) 호출
* OnResultExecuted 메소드 : 액션 결과가 실행된 후에 호출
	
Action메소드는 ActionResult의 파생클래스인 액션결과를 반환시켜준다.
* 필터 테스트
<image src = "https://user-images.githubusercontent.com/78133537/140911743-75e0d9c7-914f-4a65-8ff3-313a61d369af.png" width = "900"> 

### 5. 예외 필터(Exception) : 다른 필터나 액션 메소드, 액션 결과가 예외를 던지는 경우에만 실행
```swift
namespace System.Web.Mvc{
	public interface IExceptionFilter{
    void OnException(ExceptionContext filterContext);
	}
}
```
* OnException 메소드 : 처리되지 않은 예외가 발생하면 호출.

OnException 메소드에서 사용되는 매개변수 ExceptionContext 개체는 인증필터, 권한부여 필터 구현에 사용되던 컨텍스트 개체와 마찬가지로 ControllerContext 클래스에서 파생된 개체이다.
<br><br>
* ControllerContext의 유용한 속성들

-Controller(ControllerBase타입) : 현재 요청에 대한 컨트롤러 개체 반환

-HttpContext(HttpContextBase타입) : 요청에 대한 여러 상세정보를 얻을 수 있음.

-IsChildAction(bool타입) : 필터가 적용된 액션이 자식 액션인지 여부를 판단

-RequestContext(RequestContext타입) : HttpContext와 라우트 데이터 정보를 얻을 수 있다.

-RouteData(RouteData) : 현재 요청에 대한 라우트 정보를 반환
<br><br>
* ExceptionContext에 정의된 속성

-ActionDescriptor(ActionDescriptor타입) : 액션 메소드에 대한 상세 정보 제공

-Result(ActionResult타입) : 액션 메소드에 대한 결과

-Exception(Exception타입) : 처리되지 않은 예외

-ExceptionHandled(bool타입) : 다른 종류의 필터가 예외를 처리했는지 여부를 파악
<br><br>
* 사용자 정의 예외 필터 : ViewResult 개체를 새로 생성한다.
```swift
if(!filterContext.ExceptionHandled && filterContext.Exception is ArgumentOutOfRangeException)
{
	int val = (int)(((ArgumentOutOfRangeException)filterContext.Exception).ActualValue);

	filterContext.Result = new ViewResult
	{
    		ViewName = "ErrorView",
		ViewData = new ViewDataDictionary<int>(val)
	};
	
	filterContext.ExceptionHandled = true;
}
```
* [내장 예외 필터 클래스(HandleErrorAttribute)의 속성]

-ExceptionType(Type형식) : 필터에 의해 처리되는 예외 형식

-View(string형식) : 필터가 렌더링할 뷰템플릿 이름, 이름을 지정하지 않으면 기본값 Error를 사용한다.
기본값 적용예 > Views/컨트롤러명/Error.cshtml 또는 Views/Shared/Error.cshtml

-Master(string형식) : 필터의 뷰를 렌더링하는 경우 사용되는 레이아웃 템플릿 이름. 이름을 지정하지 않으면 기본 레이아웃 페이지를 이용한다.
