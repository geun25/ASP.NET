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

        private bool checkTheUserInfo(string userid, string password)
        {
            return GetUserInfos().Where(u => u.UserId.Equals(userid) && u.Password.Equals(password)).Any(); // 리스트 데이터 유무체크
        }
        #endregion

        bool IUser.MatchTheUserInfo(LoginInfo login) // 명시적으로 인터페이스 구현
        {
            return checkTheUserInfo(login.UserId, login.Password);
        }
    }
}