using Microsoft.EntityFrameworkCore;
using NetCore.Data.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCore.Services.Data
{
    public class CodeFirstDbContext : DbContext
    {
        //2. Fluent API
        //생성자 상속
        public CodeFirstDbContext(DbContextOptions<CodeFirstDbContext> options) : base(options)
        {

        }

        //DB 테이블 리스트 지정
        public DbSet<User> Users { get; set; }

        //메서드 상속, 부모클래스에서 OnModelCreating 메서드가 virtual
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //4가지 작업
            //DB 테이블이름 지정
            modelBuilder.Entity<User>().ToTable(name: "User");

            //복합키 지정
            modelBuilder.Entity<UserRoleByUser>().HasKey(c => new { c.UserId, c.RoleId });

            //컬럼 기본값 지정
            modelBuilder.Entity<User>(e =>
            {
                e.Property(c => c.IsMembershipWithdrawn).HasDefaultValue(value: false);
            });

            //인덱스 지정
            modelBuilder.Entity<User>().HasIndex(c => new { c.UserEmail });
        }
    }
}
