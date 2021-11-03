using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineSchool.DAL;
using OnlineSchool.Models;
using PagedList;

namespace OnlineSchool.Controllers
{
    public class StudentController : Controller
    {
        private SchoolContext db = new SchoolContext();

        // GET: Student
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder; // 현재 정렬 속성값
            ViewBag.NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : ""; // 초기값 name_desc
            ViewBag.DateSort = sortOrder == "Date" ? "date_desc" : "Date"; // 초기값 Date

            if(searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var students = from s in db.Students
                           select s;
            if(!String.IsNullOrEmpty(searchString))
            {
                students = students.Where(s => s.Name.Contains(searchString));
            }

            //내림차순, 오름차순 토글처리
            switch(sortOrder)
            {
                case "name_desc":
                    students = students.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    students = students.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    students = students.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    students = students.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 4; //한 페이지당 4명의 수강생
            int pageNumber = (page ?? 1);

            //return View(db.Students.ToList());
            //return View(students.ToList());
            return View(students.ToPagedList(pageNumber, pageSize));
        }

        // GET: Student/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student/Create
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하세요. 
        // 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하세요.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,EnrollmentDate")] Student student)
        {
            try
            {
                if (ModelState.IsValid) // 유효성 검사
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch(DataException) // 외부의 원인으로 인해 발생하는 경우가 많음.
            {
                ModelState.AddModelError("", "저장할 수 없습니다. 다시 시도해주세요!");
            }

            return View(student);
        }

        // GET: Student/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        // 초과 게시 공격으로부터 보호하려면 바인딩하려는 특정 속성을 사용하도록 설정하세요. 
        // 자세한 내용은 https://go.microsoft.com/fwlink/?LinkId=317598을(를) 참조하세요.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public ActionResult EditPost(int? id)
        {
            if(id == null)
            {
                // Http 상태 코드를 브라우저에 전송하는 클래스 반환
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                // 404결과 전송시
                // return new HttpNotFoundResult(); HttpStatusCodeResult클래스의 파생클래스
                // return HttpNotFound(); 
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

            //if (ModelState.IsValid)
            //{
            //    db.Entry(student).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            return View(studentToUpdate);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id, bool? saveChangeError = false)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            if(saveChangeError.GetValueOrDefault())
            {
                ViewBag.ErrorMesasge = "삭제 실패하였습니다. 다시 시도해주세요";
            }

            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound(); 
            }
            return View(student);
        }

        // POST: Student/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id) : MVC 스캐폴딩 과정에서 유일한 시그니처를
        //부여하기 위해 DeleteConfirmed메서드의 이름을 사용한다.
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                Student student = db.Students.Find(id);
                db.Students.Remove(student); // 엔티티의 상태를 Deleted 상태로 설정
                db.SaveChanges(); // SQL DELETE명령이 실행된다.
            }
            catch(DataException)
            {
                return RedirectToAction("Delete", new { id = id, saveChangeError = true });
            }
            return RedirectToAction("Index");
        }

        // 데이터베이스 연결끊음.
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
