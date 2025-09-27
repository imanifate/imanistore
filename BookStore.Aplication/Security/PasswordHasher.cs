using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Aplication.Security
{
    public static class PasswordHasher
    {
        //Encrypt using MD5   
        public static string EncodePasswordMd5(this string password)
        {
            var x = new MD5CryptoServiceProvider();
            byte[] data = Encoding.UTF8.GetBytes(password);
            data = x.ComputeHash(data);
            var ret = new StringBuilder(32);
            for (int i = 0; i < data.Length; i++)
                ret.AppendFormat("{0:x2}", data[i]);
            return ret.ToString();
        }
    }
}
