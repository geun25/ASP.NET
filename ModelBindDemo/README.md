## 기본 Model Binder 사용
### 1. PersonData 생성

```swift
private Person[] personData =
{
	new Person {PersonId = 1, Name = "김길동", Role = Role.Admin},
    	new Person {PersonId = 2, Name = "홍길동", Role = Role.User},
	new Person {PersonId = 3, Name = "안길동", Role = Role.User},
	new Person {PersonId = 4, Name = "오길동", Role = Role.Guest},
};
```
Models에 Person클래스를 만들고, HomeController에 Person개체를 배열로서 생성한다.

---

### 2. Index 메서드 Binding 처리

```swift
public ActionResult Index(int id=1) 
{
	Person person = personData.Where(p => p.PersonId == id).First();
    	return View(person);
}
```

URL에서 받은 id값이 Index메서드의 매개변수로 들어오면서 Binding된다.
매개변수가 int형이 아닐때 에러가 발생하는데, Default값을 지정하여 해결한다.

---

#### [Model Binding]
URL의 특정 세그먼트가 액션 메소드의 특정 타입 매개변수 타입으로 변환되는 과정
모델 바인딩은 요청을 수신하는 시점부터 시작되며, 라우팅 엔진에 의해서 바인딩 작업이 이루어짐.

#### [Model Binding 과정]
URL 요청이 들어오면 액션호출자(ControllerActionInvoker 클래스)가 라우팅 정보를 확인을 해서
Index 액션메소드를 사용해야 하는지 확인 작업을 한 후에 이 액션 메소드를 호출하기 위해 
매개변수를 생성해야 한다.

매개변수를 생성하기 위해서는 모델 바인더가 필요하다.
따라서, 액션호출자는 int 형식의 값을 담당하는 모델 바인더를 선택하여 BindModel 메소드를 호출한다.

그러면 해당 모델 바인더는 Index 메소드 호출에 사용되는 매개변수 int형식의 값을 제공한다.

여기서, 모델 바인더는 IModelBinder 인터페이스를 구현하여 정의된다.

```swift
namespace System.Web.Mvc 
{
	public interface IModelBinder 
	{
		object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext);
	}
}
```

대부분 응용프로그램에서는 기본 모델바인더(내장된 바인더 : DefaultModelBinder 클래스)를 그대로 사용한다.

이 DefaultModelBinder 클래스는 매개변수의 데이터를 아래와 같은 순서로 찾게된다.

* Request.Form : 사용자가 HTML form 요소를 통해서 제공한 값들
* RouteData.Values : 라우트 정보를 확인하는 과정에서 얻어진 값들
* Request.QueryString : 요청 URL의 문자열 부분에 포함된 데이터
* Request.Files : 요청의 일부로 업로드된 파일들

모델 바인더는 값을 찾는 즉시 검색을 하지 않는다.

#### [선택적 속성 Binding 처리]
* Exclude : 특정 속성을 바인딩에서 제외
* Include : 특정 속성만 포함시키고, 나머지는 바인딩에서 제외

---

### 3. 다른 개체에 대한 Binding
* Prefix를 이용해서 다른 개체 Bind처리
```swift
public ActionResult AddrSimple([Bind(Prefix="Addr")]AddressSimple addrSimple)
{
	return View(addrSimple);
}
```

* Html.BeginForm("AddrSimple", "Home") : Action 메서드와 Controller 지정
```swift
<h2>Person 새로 추가하기</h2>
@using (Html.BeginForm("AddrSimple", "Home"))
{
	<div>@Html.LabelFor(m => m.PersonId)@Html.EditorFor(m => m.PersonId)</div>
	<div>@Html.LabelFor(m => m.Name)@Html.EditorFor(m => m.Name)</div>
	<div>@Html.LabelFor(m => m.Role)@Html.EditorFor(m => m.Role)</div>

	<div>
	@Html.LabelFor(m => m.Addr.City)
	@Html.EditorFor(m => m.Addr.City)
	</div>
	<div>
	@Html.LabelFor(m => m.Addr.Country)
	@Html.EditorFor(m => m.Addr.Country)
	</div>

	<button type="submit">추가하기</button>
}
```

<img src = "https://user-images.githubusercontent.com/78133537/140453038-f27a6964-6a57-4b74-86f5-bf35db3f99d3.png" height="400"> <img src = "https://user-images.githubusercontent.com/78133537/140453239-f05ab3a3-5177-400b-aafe-ad1a922349c5.png" height="400">


