using System;
using System.Text;

namespace OngProject.Core.Helper
{
    public static class AuthHelper
    {
        public static  string EncryptPassword(string password)
        {
            string salt = "OTSampleSalt300";
            password += salt;
            byte[] encoded = Encoding.UTF8.GetBytes(password);
            string encrypted = Convert.ToBase64String(encoded);
            return encrypted;
        }
    }
}
