using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Text;

namespace ASP.NetMVCExample.SecurityHelpers
{
    public class SecurityReturn
    {
        public string Salt { get; internal set; }
        public string SaltedHashedPassword { get; internal set; }
    }

    public static class SecurityHelper
    {
        const int _SaltSize = 32;
        const int _CodeSize = 70;

        private static string SaltGen(int size)
        {
            byte[] Buf = new byte[size];
            using (RNGCryptoServiceProvider RandomSaltGen = new RNGCryptoServiceProvider())
            {
                RandomSaltGen.GetNonZeroBytes(Buf);
            }

            return Convert.ToBase64String(Buf);
        }

        private static string HashString(string String)
        {
            byte[] In = Encoding.UTF8.GetBytes(String);
            byte[] hashed;
            SHA256Managed Hasher = new SHA256Managed();

            hashed = Hasher.ComputeHash(In);

            return Convert.ToBase64String(hashed);
        }

        public static string PasswordToSaltedHash(string Password, string Salt)
        {
            return HashString(Password + Salt);
        }

        public static SecurityReturn PasswordToSaltedHash(string Password, int SaltSize = _SaltSize)
        {
            string salt = SaltGen(SaltSize);
            return new SecurityReturn() { Salt = salt, SaltedHashedPassword = HashString(Password + salt) };
        }

        public static string GetCode(int Size = _CodeSize)
        {
            return SaltGen(Size);
        }

    }
}