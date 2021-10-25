using OnlineSchool.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

//데이터베이스 컨텍스트 클래스 : 데이터 모델들(Course, Student, Enrollment) 조정하는 역할을 담당
// Entity Framework에서 제공하는 기능을 이용하여 조정을 하는 주 클래스이다.
// 이 클래스는 System.Data.Entity.DbContext 클래스를 상속 받아서 생성할 수 있다.
// 이 클래스의 코드를 통해 데이터 모델에 포함될 엔티티들을 지정 
namespace OnlineSchool.DAL
{
    public class SchoolContext : DbContext
    {
        public SchoolContext() : base("SchoolContext")
        {

        }

        //엔티티 집합 지정하기(Entity set)
        public DbSet<Student> Students { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Course> Courses { get; set; }
        
        //단수형 테이블 만들기
        //데이터베이스에 테이블을 만들 때 Students가 아닌 Student단수형 테이블로 만든다.
        //Courses, Enrollments ---> Course, Enrollment 테이블로 만들어짐.
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}