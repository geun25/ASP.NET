using Core.Services.Bridges;
using Core.Services.Data;
using Core.Services.Interfaces;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Svcs
{
    public class PasswordHasher : IPasswordHasher
    {
        private DBFirstDbContext _context;

        private PasswordHasher(DBFirstDbContext context)
        {
            _context = context;
        }

        #region private methods
        private string GetGUIDSalt()
        {
            return Guid.NewGuid().ToString();
        }

        private string GetRNGSalt()
        {
            // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }

        private string GetPasswordHash(string userId, string password, string guidSalt, string rngSalt)
        {
            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            //Pbkdf2 : Password-based key derivation function 2
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: userId.ToLower() + password.ToLower() + guidSalt, // 아이디와 비밀번호에 대해서 대소문자 처리
                salt: Encoding.UTF8.GetBytes(rngSalt), // byte배열로 converting
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 45000, // 10000, 25000, 45000
                numBytesRequested: 256 / 8));
        }

        private bool CheckThePasswordInfo(string userId, string password, string guidSalt, string rngSalt, string passwordHash)
        {
            return GetPasswordHash(userId, password, guidSalt, rngSalt).Equals(passwordHash); //일치하면 true, 불일치하면 false반환
        }

        private PasswordHashInfo PasswordInfo(string userId, string password)
        {
            string guidSalt = GetGUIDSalt();
            string rngSalt = GetRNGSalt();

            var passwordInfo = new PasswordHashInfo()
            {
                GUIDSalt = guidSalt,
                RNGSalt = rngSalt,
                PasswordHash = GetPasswordHash(userId, password, guidSalt, rngSalt)
            };

            return passwordInfo;
        }
        #endregion

        string IPasswordHasher.GetGUIDSalt()
        {
            return GetGUIDSalt();
        }

        string IPasswordHasher.GetRNGSalt()
        {
            return GetRNGSalt();
        }

        string IPasswordHasher.GetPasswordHash(string userId, string password, string guidSalt, string rngSalt)
        {
            return GetPasswordHash(userId, password, guidSalt, rngSalt);
        }

        bool IPasswordHasher.CheckThePasswordInfo(string userId, string password, string guidSalt, string rngSalt, string passwordHash)
        {
            return CheckThePasswordInfo(userId, password, guidSalt, rngSalt, passwordHash);
        }

        PasswordHashInfo IPasswordHasher.SetPasswordInfo(string userId, string password)
        {
            return PasswordInfo(userId, password);
        }
    }
}
