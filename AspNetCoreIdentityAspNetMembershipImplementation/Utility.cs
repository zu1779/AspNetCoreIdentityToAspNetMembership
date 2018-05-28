namespace AspNetCoreIdentityAspNetMembershipImplementation
{
    using System;
    using System.Security.Cryptography;

    public class Utility : IUtility
    {
        private const int SALT_SIZE = 16;

        public string GenerateSalt()
        {
            byte[] buf = new byte[SALT_SIZE];
            (new RNGCryptoServiceProvider()).GetBytes(buf);
            return Convert.ToBase64String(buf);
        }
    }
}
