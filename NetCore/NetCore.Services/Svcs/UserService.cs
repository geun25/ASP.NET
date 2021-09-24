using NetCore.Data.DataModels;
using NetCore.Data.ViewModels;
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
        #region private methods
        private IEnumerable<User> GetUserInfos()
        {
            return new List<User>()
            {
                new User()
                {
                    UserId = "dogeunKim",
                    UserName = "김도근",
                    UserEmail = "rlaehrms7@gmail.com",
                    Password = "123456"
                }
            };
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