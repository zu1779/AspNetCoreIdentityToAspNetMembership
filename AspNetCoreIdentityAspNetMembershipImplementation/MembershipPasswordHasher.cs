namespace AspNetCoreIdentityAspNetMembershipImplementation
{
    using System;
    using System.Security.Cryptography;
    using System.Text;

    using Microsoft.AspNetCore.Identity;

    public class AspNetMembershipPasswordHasher : IPasswordHasher<ApplicationUser>
    {
        public virtual string HashPassword(ApplicationUser user, string password)
        {
            switch (user.PasswordFormat)
            {
                case MembershipPasswordFormat.Clear: return password;
                case MembershipPasswordFormat.Encrypted: return EncryptPassword(password, user.PasswordSalt);
                case MembershipPasswordFormat.Hashed: return HashPassword(password, user.PasswordSalt);
                default: throw new ApplicationException($"Unknown password format: {user.PasswordFormat}");
            }
        }

        public virtual PasswordVerificationResult VerifyHashedPassword(ApplicationUser user, string hashedPassword, string providedPassword)
        {
            string providedPasswordHashed = HashPassword(user, providedPassword);
            //TODO: decide when to return PasswordVerificationResult.SuccessRehashNeeded
            return providedPasswordHashed == hashedPassword ? PasswordVerificationResult.Success : PasswordVerificationResult.Failed;
        }

        protected virtual string EncryptPassword(string password, string salt)
        {
            byte[] bIn = Encoding.Unicode.GetBytes(password);
            byte[] bSalt = Convert.FromBase64String(salt);
            //byte[] bRet = null;
            byte[] bAll = new byte[bSalt.Length + bIn.Length];
            Buffer.BlockCopy(bSalt, 0, bAll, 0, bSalt.Length);
            Buffer.BlockCopy(bIn, 0, bAll, bSalt.Length, bIn.Length);
            //bRet = EncryptPassword(bAll, _LegacyPasswordCompatibilityMode);

            throw new NotImplementedException();

            //return Convert.ToBase64String(bRet);
        }

        protected virtual string HashPassword(string password, string salt)
        {
            byte[] bIn = Encoding.Unicode.GetBytes(password);
            byte[] bSalt = Convert.FromBase64String(salt);
            byte[] bRet = null;

            HashAlgorithm hm = GetHashAlgorithm();
            if (hm is KeyedHashAlgorithm kha)
            {
                if (kha.Key.Length == bSalt.Length)
                {
                    kha.Key = bSalt;
                }
                else if (kha.Key.Length < bSalt.Length)
                {
                    byte[] bKey = new byte[kha.Key.Length];
                    Buffer.BlockCopy(bSalt, 0, bKey, 0, bKey.Length);
                    kha.Key = bKey;
                }
                else
                {
                    byte[] bKey = new byte[kha.Key.Length];
                    for (int iter = 0; iter < bKey.Length;)
                    {
                        int len = Math.Min(bSalt.Length, bKey.Length - iter);
                        Buffer.BlockCopy(bSalt, 0, bKey, iter, len);
                        iter += len;
                    }
                    kha.Key = bKey;
                }
                bRet = kha.ComputeHash(bIn);
            }
            else
            {
                byte[] bAll = new byte[bSalt.Length + bIn.Length];
                Buffer.BlockCopy(bSalt, 0, bAll, 0, bSalt.Length);
                Buffer.BlockCopy(bIn, 0, bAll, bSalt.Length, bIn.Length);
                bRet = hm.ComputeHash(bAll);
            }

            return Convert.ToBase64String(bRet);
        }

        private string s_HashAlgorithm;
        private HashAlgorithm GetHashAlgorithm()
        {
            if (s_HashAlgorithm != null) return HashAlgorithm.Create(s_HashAlgorithm);

            string temp = "SHA1";
            //TODO: search for hash algorithm selection
            //string temp = Membership.HashAlgorithmType;
            //if (_LegacyPasswordCompatibilityMode == MembershipPasswordCompatibilityMode.Framework20 &&
            //    !Membership.IsHashAlgorithmFromMembershipConfig &&
            //    temp != "MD5")
            //{
            //    temp = "SHA1";
            //}
            HashAlgorithm hashAlgo = HashAlgorithm.Create(temp);
            if (hashAlgo == null) throw new ApplicationException($"Unable to create HashAlgoritm '{temp}'");
            //    RuntimeConfig.GetAppConfig().Membership.ThrowHashAlgorithmException();
            s_HashAlgorithm = temp;
            return hashAlgo;
        }
    }
}