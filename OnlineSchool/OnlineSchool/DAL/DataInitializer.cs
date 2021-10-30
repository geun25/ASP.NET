using OnlineSchool.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

// Entity Framework는 어플리케이션이 실행되는 시점에 자동으로
// 데이터베이스를 생성할 수 있는 기능을 제공한다.
// 이때, 데이터베이스를 Drop후 다시 생성할 수 있다.
// 매번 어플리케이션이 실행될 때마다 이 작업이 수행되도록 할지,
// 데이터베이스에 변경된 내용이 있을 때만 수행할지 설정할 수 있다.

// Entity Framework는 데이터베이스 생성 후 테스트 데이터를 넣기 위해서
// Seed 메소드를 호출한다.

namespace OnlineSchool.DAL
{
    public class DataInitializer :
        DropCreateDatabaseIfModelChanges<SchoolContext> // 변경 내용 자동 생성
    {
        protected override void Seed(SchoolContext context)
        {
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
            context.SaveChanges(); // 사용 안해도 무방 - 데이터 오류시 유용

            var courses = new List<Course>
            {
                new Course{ CourseID=1000, Title="C언어"},
                new Course{ CourseID=2000, Title="자바"},
                new Course{ CourseID=3000, Title="ASP.NET"},
                new Course{ CourseID=1010, Title="C++"},
                new Course{ CourseID=1020, Title="C#"},
                new Course{ CourseID=2050, Title="JSP"},
                new Course{ CourseID=3010, Title="ASP.NET MVC"},
            };

            courses.ForEach(s => context.Courses.Add(s));
            context.SaveChanges();

            var enrollments = new List<Enrollment>
            {
                new Enrollment{StudentID=1, CourseID=1000, Score=Score.B},
                new Enrollment{StudentID=1, CourseID=2000, Score=Score.C},
                new Enrollment{StudentID=1, CourseID=1010, Score=Score.B},
                new Enrollment{StudentID=2, CourseID=1020, Score=Score.A},
                new Enrollment{StudentID=2, CourseID=2050, Score=Score.C},
                new Enrollment{StudentID=2, CourseID=2000, Score=Score.F},
                new Enrollment{StudentID=3, CourseID=3010},
                new Enrollment{StudentID=4, CourseID=3000},
                new Enrollment{StudentID=4, CourseID=1020, Score=Score.F},
                new Enrollment{StudentID=5, CourseID=1000, Score=Score.C},
                new Enrollment{StudentID=6, CourseID=2050, Score=Score.C},
                new Enrollment{StudentID=7, CourseID=3010},
                new Enrollment{StudentID=8, CourseID=2000, Score=Score.A},
            };

            enrollments.ForEach(s => context.Enrollments.Add(s));
            context.SaveChanges();
        }
    }
}