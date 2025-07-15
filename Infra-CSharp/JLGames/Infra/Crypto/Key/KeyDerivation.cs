using System.Security.Cryptography;
using System.Text;

namespace JLGames.Infra.Crypto.Key
{
    public static class KeyDerivation
    {
        private static readonly byte[] s_Salt = Encoding.UTF8.GetBytes("JLGames.Infra.Crypto.Key");
        private static readonly int s_Iterations = 100000;
        private static readonly int s_KeyLen = 32; // 32 bytes for AES-256 or HMAC

        /// <summary>
        /// SharedKeySha256Str - Converts a passphrase string to a 32-byte key using SHA256
        /// </summary>
        /// <param name="passphrase"></param>
        /// <returns></returns>
        public static byte[] SharedKeySha256Str(string passphrase)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(passphrase));
            }
        }

        /// <summary>
        /// SharedKeySha256 - Converts a passphrase byte array to a 32-byte key using SHA256
        /// </summary>
        /// <param name="passphrase"></param>
        /// <returns></returns>
        public static byte[] SharedKeySha256(byte[] passphrase)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(passphrase);
            }
        }

        /// <summary>
        /// DeriveKeyPbkdf2StrDefault - Derives a strong key using PBKDF2 from a string (recommended for production)
        /// </summary>
        /// <param name="passphrase"></param>
        /// <returns></returns>
        public static byte[] DeriveKeyPbkdf2StrDefault(string passphrase)
        {
            return DeriveKeyPbkdf2Str(passphrase, s_Salt, s_Iterations, s_KeyLen);
        }

        /// <summary>
        /// DeriveKeyPbkdf2Default - Derives a strong key using PBKDF2 from a byte array (recommended for production)
        /// </summary>
        /// <param name="passphrase"></param>
        /// <returns></returns>
        public static byte[] DeriveKeyPbkdf2Default(byte[] passphrase)
        {
            return DeriveKeyPbkdf2(passphrase, s_Salt, s_Iterations, s_KeyLen);
        }

        /// <summary>
        /// DeriveKeyPbkdf2Str - Derives a strong key using PBKDF2 from password + salt
        /// </summary>
        /// <param name="passphrase"></param>
        /// <param name="salt"></param>
        /// <param name="iterations"></param>
        /// <param name="keyLen"></param>
        /// <returns></returns>
        public static byte[] DeriveKeyPbkdf2Str(string passphrase, byte[] salt, int iterations, int keyLen)
        {
            var bs = Encoding.UTF8.GetBytes(passphrase);
            return DeriveKeyPbkdf2(bs, salt, iterations, keyLen);
        }

        /// <summary>
        /// DeriveKeyPbkdf2 - Derives a strong key using PBKDF2 from password + salt
        /// </summary>
        /// <param name="passphrase"></param>
        /// <param name="salt"></param>
        /// <param name="iterations"></param>
        /// <param name="keyLen"></param>
        /// <returns></returns>
        public static byte[] DeriveKeyPbkdf2(byte[] passphrase, byte[] salt, int iterations, int keyLen)
        {
            // 使用 HMAC-SHA1 作为哈希算法进行 PBKDF2 密钥派生
            // .net standard 2.0 不支持指定哈希算法
            using (var pbkdf2 = new Rfc2898DeriveBytes(passphrase, salt, iterations))
            {
                return pbkdf2.GetBytes(keyLen);
            }
        }
    }
}