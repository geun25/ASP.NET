//using Core.Data.DataModels;
using Core.Data.Classes;
using Core.Data.ViewModels;
using Core.Services.Data;
using Core.Services.Interfaces;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System;

namespace Core.Services.Svcs
{
    public class UserService : IUser
    {
        // 의존성 주입
        private DBFirstDbContext _context;
        private IPasswordHasher _hasher;

        public UserService(DBFirstDbContext context, IPasswordHasher hasher)
        {
            _context = context;
            _hasher = hasher;
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
            user = _context.Users.FromSqlRaw("dbo.uspCheckLoginByUserId @p0, @p1", new[] { userId, password })
                                  .FirstOrDefault();

            if (user == null)
            {
                // 접속 실패횟수에 대한 증가
                //int rowAffected;

                // SQL문 직접 작성
                //rowAffected = _context.Database.ExecuteSqlCommand($"Update dbo.[User] SET AccessFailedCount += 1 WHERE UserId={userId}");

                //Stored Procedure
                //rowAffected = _context.Database.ExecuteSqlCommand("dbo.FailedLoginBuUserId", parameters: new[] { userId });
            }

            return user;
        }

        private bool checkTheUserInfo(string userid, string password)
        {
            //return GetUserInfos().Where(u => u.UserId.Equals(userid) && u.Password.Equals(password)).Any(); // 리스트 데이터 유무체크
            return GetUserInfo(userid, password) != null ? true : false;
        }

        private User GetUserInfo(string userId)
        {
            return _context.Users.Where(u => u.UserId.Equals(userId)).FirstOrDefault();
        }

        private IEnumerable<UserRolesByUser> GetUserRolesByUserInfos(string userId)
        {
            var userRolesByUserInfos = _context.UserRolesByUsers.Where(uru => uru.UserId.Equals(userId)).ToList();

            foreach (var role in userRolesByUserInfos) // 권한에 대한 이름과 우선순위를 가져오기 위해 foreach 사용
            {
                role.UserRole = GetUserRole(role.RoleId);
            }

            return userRolesByUserInfos.OrderByDescending(uru => uru.UserRole.RolePriority); // 내림차순 정렬
        }

        private UserRole GetUserRole(string roleId)
        {
            return _context.UserRoles.Where(ur => ur.RoleId.Equals(roleId)).FirstOrDefault();
        }

        // 아이디에 대해서 대소문자 처리
        private int RegisterUser(RegisterInfo register)
        {
            var utcNow = DateTime.UtcNow;
            var passwordInfo = _hasher.SetPasswordInfo(register.UserId, register.Password);

            var user = new User()
            {
                UserId = register.UserId.ToLower(),
                UserName = register.UserName,
                UserEmail = register.UserEmail,

                GUIDSalt = passwordInfo.GUIDSalt,
                RNGSalt = passwordInfo.RNGSalt,
                PasswordHash = passwordInfo.PasswordHash,

                AccessFailedCount = 0,
                IsMembershipWithdrawn = false,
                JoinedUtcDate = utcNow
            };

            var userRolesByUser = new UserRolesByUser()
            {
                UserId = register.UserId.ToLower(),
                RoleId = "AssociateUser",
                OwnedUtcDate = utcNow
            };

            _context.Add(user);
            _context.Add(userRolesByUser);

            return _context.SaveChanges();
        }

        /// <summary>
        /// 업데이트를 위한 사용자 정보
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private UserInfo GetUserInfoForUpdate(string userId)
        {
            var user = GetUserInfo(userId);
            var userInfo = new UserInfo()
            {
                UserId = null,
                UserName = user.UserName,
                UserEmail = user.UserEmail,
                ChangeInfo = new ChangeInfo()
                {
                    UserName = user.UserName,
                    UserEmail = user.UserEmail
                }
            };

            return userInfo;
        }

        private int UpdateUser(UserInfo user)
        {
            var userInfo = _context.Users.Where(u => u.UserId.Equals(user.UserId)).FirstOrDefault(); //데이터베이스에서 값들 받아오기

            if (userInfo == null)
                return 0;

            bool check = _hasher.CheckThePasswordInfo(user.UserId, user.Password, userInfo.GUIDSalt, userInfo.RNGSalt, userInfo.PasswordHash);

            int rowAffected = 0;

            if (check)
            {
                _context.Update(userInfo);

                userInfo.UserName = user.UserName;
                userInfo.UserEmail = user.UserEmail;

                rowAffected = _context.SaveChanges();
            }

            return rowAffected;
        }

        private bool MatchTheUserInfo(LoginInfo login)
        {
            var user = _context.Users.Where(u => u.UserId.Equals(login.UserId)).FirstOrDefault(); //데이터베이스에서 값들 받아오기

            if (user == null)
                return false;

            return _hasher.CheckThePasswordInfo(login.UserId, login.Password, user.GUIDSalt, user.RNGSalt, user.PasswordHash);
        }

        private bool CompareInfo(UserInfo user)
        {
            return user.ChangeInfo.Equals(user);
        }

        private int WithdrawnUser(WithdrawnInfo user)
        {
            var userInfo = _context.Users.Where(u => u.UserId.Equals(user.UserId)).FirstOrDefault(); //데이터베이스에서 값들 받아오기

            if (userInfo == null)
                return 0;

            bool check = _hasher.CheckThePasswordInfo(user.UserId, user.Password, userInfo.GUIDSalt, userInfo.RNGSalt, userInfo.PasswordHash);
            int rowAffected = 0;

            if(check)
            {
                _context.Remove(userInfo);
                rowAffected = _context.SaveChanges(); // 데이터베이스에 적용
            }

            return rowAffected;
        }
        #endregion

        bool IUser.MatchTheUserInfo(LoginInfo login) //IUser 상속받은후 명시적 구현
        {
            //return checkTheUserInfo(login.UserId, login.Password);
            return MatchTheUserInfo(login);
        }

        User IUser.GetUserInfo(string userId)
        {
            return GetUserInfo(userId);
        }

        IEnumerable<UserRolesByUser> IUser.GetRolesOwnedByUser(string userId)
        {
            return GetUserRolesByUserInfos(userId);
        }

        int IUser.RegisterUser(RegisterInfo register)
        {
            return RegisterUser(register);
        }

        UserInfo IUser.GetUserInfoForUpdate(string userId)
        {
            return GetUserInfoForUpdate(userId);
        }

        int IUser.UpdateUser(UserInfo user)
        {
            return UpdateUser(user);
        }

        bool IUser.CompareInfo(UserInfo user)
        {
            return CompareInfo(user);
        }

        int IUser.WithdrawnUser(WithdrawnInfo user)
        {
            return WithdrawnUser(user);
        }
    }
}
