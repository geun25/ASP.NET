using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Core.Test.PasswordHasher
{
    class Program
    {
        static void Main(string[] args)
        {
            //Password => GUIDSalt, RNGSalt, PasswordHash
            Console.Write("아이디를 입력하세요: ");
            string userId = Console.ReadLine();

            Console.Write("비밀번호를 입력하세요: ");
            string password = Console.ReadLine();

            string guidSalt = Guid.NewGuid().ToString();

            string rngSalt = GetRNGSalt();

            string passwordHash = GetPasswordHash(userId, password, guidSalt, rngSalt);

            // 데이터베이스의 비밀번호정보와 지금 입력한 비밀번호정보를 비교해서 같은 해시값이 나오면 로그인 성공
            bool check = CheckThePasswordInfo(userId, password, guidSalt, rngSalt, passwordHash);

            Console.WriteLine($"UserId:{userId}");
            Console.WriteLine($"Password:{password}");
            Console.WriteLine($"GUIDSalt:{guidSalt}");
            Console.WriteLine($"RNGSalt: {rngSalt}");
            Console.WriteLine($"PasswordHash: {passwordHash}");
            Console.WriteLine($"Check:{(check ? "비밀번호 정보가 일치" : "불일치")}");

            Console.ReadLine();
        }

        private static string GetRNGSalt()
        {
            // generate a 128-bit salt using a cryptographically strong random sequence of nonzero values
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

           return Convert.ToBase64String(salt);
        }

        private static string GetPasswordHash(string userId, string password, string guidSalt, string rngSalt)
        {
            // derive a 256-bit subkey (use HMACSHA256 with 100,000 iterations)
            //Pbkdf2 : Password-based key derivation function 2
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: userId + password + guidSalt,
                salt: Encoding.UTF8.GetBytes(rngSalt), // byte배열로 converting
                prf: KeyDerivationPrf.HMACSHA512,
                iterationCount: 45000, // 10000, 25000, 45000
                numBytesRequested: 256 / 8));
        }

        private static bool CheckThePasswordInfo(string userId, string password, string guidSalt, string rngSalt, string passwordHash)
        {
            return GetPasswordHash(userId, password, guidSalt, rngSalt).Equals(passwordHash); //일치하면 true, 불일치하면 false반환
        }
    } 
}
