using System;
using System.Security.Cryptography;

namespace MicropostMVC.Framework.Common
{
    public static class Encryptor
    {
        public static string CreateHashedPassword(string password, string salt)
        {
            var sha256 = new SHA256CryptoServiceProvider(); 
            byte[] passwordBytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
            byte[] hashedPasswordBytes = sha256.ComputeHash(passwordBytes);
            return Convert.ToBase64String(hashedPasswordBytes);
        }

        public static string CreateSalt(int size)
        {
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);
            return Convert.ToBase64String(buff);
        }
    }
}

    
