//using NetCore.Data.DataModels;
using NetCore.Data.Classes;
using NetCore.Data.ViewModels;
using NetCore.Services.Data;
using NetCore.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace NetCore.Services.Svcs
{
    public class UserService : IUser
    {
        // 의존성 주입
        private DBFirstDbContext _context;

        public UserService(DBFirstDbContext context)
        {
            _context = context;
        }

        #region private methods
        private IEnumerable<User> GetUserInfos()
        {
            return _context.Users.ToList();
            //return new List<User>()
            //{
            //    new User()
            //    {
            //        UserId = "dogeunKim",
            //        UserName = "김도근",
            //        UserEmail = "rlaehrms7@gmail.com",
            //        Password = "123456"
            //    }
            //};
        }

        private User GetUserInfo(string userId, string password)
        {
            User user;

            //Lambda
            //user = _context.Users.Where(u => u.UserId.Equals(userId) && u.Password.Equals(password)).FirstOrDefault();

            //FromSql

            //TABLE
            //user = _context.Users.FromSql("SELECT UserId, UserName, UserEmail, Password, IsMembershipWithdrawn, JoinedUtcDate FROM dbo.[User]")
            //                    .Where(u => u.UserId.Equals(userId) && u.Password.Equals(password))
            //                    .FirstOrDefault();

            //VIEW
            //user = _context.Users.FromSql("SELECT UserId, UserName, UserEmail, Password, IsMembershipWithdrawn, JoinedUtcDate FROM dbo.uvwUser")
            //                    .Where(u => u.UserId.Equals(userId) && u.Password.Equals(password))
            //                    .FirstOrDefault();

            //FUNCTION // 파라미터 설정 가능
            //user = _context.Users.FromSql($"SELECT UserId, UserName, UserEmail, Password, IsMembershipWithdrawn, JoinedUtcDate FROM dbo.ufnUser({userId},{password})")
            //                    .FirstOrDefault();

            //STORED PROCEDURE // 파라미터 설정 가능
            user = _context.Users.FromSql("dbo.uspCheckLoginByUserId @p0, @p1", new[] { userId, password })
                                  .FirstOrDefault();

            if(user == null)
            {
                // 접속 실패횟수에 대한 증가
                int rowAffected;

                // SQL문 직접 작성
                //rowAffected = _context.Database.ExecuteSqlCommand($"Update dbo.[User] SET AccessFailedCount += 1 WHERE UserId={userId}");

                //Stored Procedure
                rowAffected = _context.Database.ExecuteSqlCommand("dbo.FailedLoginBuUserId", parameters: new[] { userId });
            }

            return user;
        }
        private bool checkTheUserInfo(string userid, string password)
        {
            //return GetUserInfos().Where(u => u.UserId.Equals(userid) && u.Password.Equals(password)).Any(); // 리스트 데이터 유무체크
            return GetUserInfo(userid, password) != null ? true : false;
        }
        #endregion

        bool IUser.MatchTheUserInfo(LoginInfo login) // 명시적으로 인터페이스 구현
        {
            return checkTheUserInfo(login.UserId, login.Password);
        }
    }
}