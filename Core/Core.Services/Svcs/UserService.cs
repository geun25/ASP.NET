using Core.Data.DataModels;
using Core.Data.ViewModels;
using Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Svcs
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

        bool IUser.MatchTheUserInfo(LoginInfo login) //IUser 상속받은후 명시적 구현
        {
            return checkTheUserInfo(login.UserId, login.Password);
        }
    }
}
