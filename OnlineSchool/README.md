# 온라인 스쿨 

## MODELS
- 학생 모델 생성
```swift
public class Student
{
  public int ID { get; set; }

  [Display(Name="이름")]
  public string Name { get; set; }

  [Display(Name = "수강신청일")]
  public DateTime EnrollmentDate { get; set; }

  [Display(Name="수강 상세")]
  public virtual ICollection<Enrollment> Enrollments { get; set; } 
}
```
한 학생이 여러 과목을 수강할 수 있고(1:N 관계), 학생의 ID는 기본키, 수강 테이블에서 외래키로 동작한다.
<br><br>
- 수강 테이블 생성
```swift
public enum Score
{
  A, B, C, D, F
}

public class Enrollment
{
  public int EnrollmentID { get; set; }
  public int CourseID { get; set; }
  public int StudentID { get; set; }
  public Score? Score { get; set; }

  //탐색 속성
  public virtual Course Course { get; set; }
  public virtual Student Student { get; set; }
}
```
Entity Framework는 특정 속성의 이름이 <탐색 속성 이름><기본키 속성 이름> 패턴으로 구성되어 있으면 해당 속성을 외래키로 해석한다.

<기본키 속성 이름>을 그대로 외래키 속성 이름으로 사용가능하다.
<br><br>
- 과목 모델 생성
```swift
public class Course
{
  [DatabaseGenerated(DatabaseGeneratedOption.None)] // EntityFramework에서 기본키 값을 자동으로 생성하지 않고, 직접 적용
  public int CourseID { get; set; } 
  public string Title { get; set; }
  public virtual ICollection<Enrollment> Enrollments { get; set; }
  }
```
과목은 수강 테이블에서 외래키로 동작하고, 학생 모델과 마찬가지로 탐색 속성 Enrollments를 추가한다.

---

## DAL(Data Access Layer)

### DataBaseContext : 데이터 모델들(Course, Student, Enrollment) 조정하는 역할을 담당.

Entity Framework에서 제공하는 기능을 이용하여 조정을 하는 주 클래스이다.

이 클래스는 System.Data.Entity.DbContext 클래스를 상속 받아서 생성할 수 있다.

* 엔티티 집합 지정하기(Entity set)
```swift
public DbSet<Student> Students { get; set; }
public DbSet<Enrollment> Enrollments { get; set; }
public DbSet<Course> Courses { get; set; }
```

* 단수형 테이블 만들기
```swift
protected override void OnModelCreating(DbModelBuilder modelBuilder)
{
  modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
}
```
데이터베이스에 테이블을 만들 때 Students가 아닌 Student단수형 테이블로 만든다.
<br><br>
### DataInitializer

Entity Framework는 어플리케이션이 실행되는 시점에 자동으로 데이터베이스를 생성할 수 있는 기능을 제공한다.

이때, 데이터베이스를 Drop후 다시 생성할 수 있다.

매번 어플리케이션이 실행될 때마다 이 작업이 수행되도록 할지, 데이터베이스에 변경된 내용이 있을 때만 수행할지 설정할 수 있다.
<br><br>
```swift
public class DataInitializer : DropCreateDatabaseIfModelChanges<SchoolContext>
```
데이터베이스에 변경내용이 있을 때마다 지우고 다시 생성하는 개체를 상속받아 사용한다.
<br><br>
* Seed 메소드 : 테스트 데이터 입력
```swift
var students = new List<Student>
{
  new Student{Name="홍길동", EnrollmentDate=DateTime.Parse("2017-10-10")},
  new Student{Name="임꺽정", EnrollmentDate=DateTime.Parse("2016-4-25")},
  new Student{Name="장길산", EnrollmentDate=DateTime.Parse("2017-1-2")},
  new Student{Name="고길동", EnrollmentDate=DateTime.Parse("2014-3-3")},
  new Student{Name="이길동", EnrollmentDate=DateTime.Parse("2015-9-9")},
  new Student{Name="이만수", EnrollmentDate=DateTime.Parse("2011-5-5")},
  new Student{Name="강길동", EnrollmentDate=DateTime.Parse("2014-5-23")},
  new Student{Name="일지매", EnrollmentDate=DateTime.Parse("2016-6-6")},
};

students.ForEach(s => context.Students.Add(s));
context.SaveChanges(); 
```
DbSet에 람다식으로 데이터를 추가한다. SaveChanges는 사용하지 않아도 무방(오류 발생시 유용)

Course와 Enrollment 데이터도 마찬가지로 입력해준다.
<br><br>
* Web.config 설정
```swift
<entityFramework>
    <contexts>
      <context type="OnlineSchool.DAL.SchoolContext, OnlineSchool">
        <databaseInitializer type="OnlineSchool.DAL.DataInitializer, OnlineSchool" />
      </context>
    <contexts>
```
DataInitializer를 이용하기 위해서 Web.config의 entityFramework태그에 contexts태그를 추가해준다.
<br><br>
```swift
<connectionStrings>
    <add name="SchoolContext" connectionString="Data Source=(LocalDb)\MSSQLLocalDB;
    Initial Catalog=OnlineSchool;Integrated Security=SSPI;" providerName="System.Data.SqlClient" />
  </connectionStrings>
```
SchoolContext의 테스트 데이터가 입력될 LocalDB 설정

---

## VIEWS

### Student의 Details
```swift
<dt>
    @Html.DisplayNameFor(model => model.Enrollments)
</dt>
<dd>
    <table class="table">
        <tr>
            <th>강좌명</th>
            <th>성적</th>
        </tr>
        @foreach(var item in Model.Enrollments)
        {
            <tr>
                <td>
                    @Html.DisplayFor(modelItem => item.Course.Title)
                </td>
                <td>
                    @Html.DisplayFor(modelItem => item.Score)
                </td>
            </tr>
        }
    </table>
</dd>
```

![image](https://user-images.githubusercontent.com/78133537/141381007-a69d236d-c366-46c6-a79f-ff93f496296e.png)

---

## Controller

### StudentController

- Create 액션 메소드(POST)

```swift
[HttpPost]
[ValidateAntiForgeryToken]
public ActionResult Create([Bind(Include = "Name,EnrollmentDate")] Student student)
{
  try
  {
    if (ModelState.IsValid) 
    {
      db.Students.Add(student);
      db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
  catch(DataException) 
  {
    ModelState.AddModelError("", "저장할 수 없습니다. 다시 시도해주세요!");
  }

  return View(student);
}
```
if문의 ModelState.IsValid는 유효성 검사를 하고, DataException의 경우 외부의 원인으로 인해 발생하는 경우가 많음.

try-catch문을 이용해 예외처리
<br><br>
- SaveChanges메소드 호출 과정

새로운 엔티티를 Add메소드에 전달하면 이때 이 엔티티의 상태가 Added로 설정된다.

그 다음에 SaveChanges메소드를 호출하면 데이터베이스 컨텍스트가 SQL INSERT명령을 실행하게 된다.

*** 엔티티의 상태들

-Added : 아직 데이터베이스에 엔티티가 존재하지 않는 상태(SaveChanges메소드가 INSERT구문을 실행한다.)

-Unchanged : 데이터베이스에 읽어온 직후의 엔티티 상태(SaveChanges메소드에 의해 어떠한 작업도 수행되지 않는다.)

-Modified : 엔티티의 속성값들 중에서 일부 또는 전체가 변경된 상태(SaveChanges메소드가 UPDATE구문을 실행한다.)

-Deleted : 삭제해야 할 엔티티로 표시된 상태(SaveChanges메소드가 DELETE구문을 실행한다.)

-Detached : 이 상태의 엔티티는 더이상 데이터베이스 컨텍스트에 의해서 추적되지 않는다.

<br><br>

- Edit 엑션 메소드(GET)
```swift
public ActionResult Edit(int? id)
{
  if(id == null)
  {
    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
  }
  Student student = db.Students.Find(id);
  if(student == null)
  {
    return HttpNotFound();
  }
  return View(student);
}
```
id값으로 Student개체를 참조하고, 해당 개체를 반환한다.
<br><br>
- Edit 엑션 메소드(POST)
```swift
[HttpPost, ActionName("Edit")]
[ValidateAntiForgeryToken]
public ActionResult EditPost(int? id)
{
  if(id == null)
  {    
    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
  }
  
  var studentToUpdate = db.Students.Find(id);
  if(TryUpdateModel(studentToUpdate, "", new string[] { "Name", "EnrollmentDate" }))
  {
    try
    {
      db.SaveChanges();
    }
    catch(DataException)
    {
      ModelState.AddModelError("", "저장할 수 없습니다. 다시 시도하세요!");
    }
  }
  
  return View(studentToUpdate);
}
```
HttpStatusCodeResult : Http 상태 코드를 브라우저에 전송하는 클래스

TryUpdateModel메소드 : 개체의 인스턴스를 업데이트

try-catch문을 이용해 예외처리
<br><br>
*** 404결과 전송시...
```swift
return new HttpNotFoundResult(); //HttpStatusCodeResult클래스의 파생클래스
return HttpNotFound(); 
```
<br><br>
-DELETE 액션 메소드(GET)
```swift
if(saveChangeError.GetValueOrDefault())
{
  ViewBag.ErrorMesasge = "삭제 실패하였습니다. 다시 시도해주세요";
}
```
삭제 처리 여부 메시지 추가
<br><br>
-DELETE 액션 메소드(POST)
```swift
public ActionResult Delete(int id)
{
  try
  {
    Student student = db.Students.Find(id);
    db.Students.Remove(student); 
    db.SaveChanges(); 
  }
  catch(DataException)
  {
    return RedirectToAction("Delete", new { id = id, saveChangeError = true });
  }
  return RedirectToAction("Index");
}
```
Remove메소드 : 엔티티의 상태를 Deleted 상태로 설정
saveChanges메소드 : SQL DELETE명령이 실행된다.
