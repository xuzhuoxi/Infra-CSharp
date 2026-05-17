using System.Security.Cryptography;
using System.Text;

namespace JLGames.Infra.Crypto.Key
{
    /// <summary>
    /// 口令/共享材料到对称密钥的派生（SHA-256 与 PBKDF2）。
    /// </summary>
    public static class KeyDerivation
    {
        private static readonly byte[] s_Salt = Encoding.UTF8.GetBytes("JLGames.Infra.Crypto.Key");
        private static readonly int s_Iterations = 100000;
        private static readonly int s_KeyLen = 32; // 32 bytes for AES-256 or HMAC

        /// <summary>
        /// 将 UTF-8 口令经 SHA-256 哈希为 32 字节密钥（无盐，仅适合非生产场景）。
        /// </summary>
        /// <param name="passphrase">口令字符串</param>
        /// <returns>32 字节派生密钥</returns>
        public static byte[] SharedKeySha256Str(string passphrase)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Encoding.UTF8.GetBytes(passphrase));
            }
        }

        /// <summary>
        /// 将口令字节经 SHA-256 哈希为 32 字节密钥（无盐）。
        /// </summary>
        /// <param name="passphrase">口令字节</param>
        /// <returns>32 字节派生密钥</returns>
        public static byte[] SharedKeySha256(byte[] passphrase)
        {
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(passphrase);
            }
        }

        /// <summary>
        /// 使用内置盐、迭代次数（100000）与 32 字节长度，从字符串派生 PBKDF2 密钥（推荐生产使用）。
        /// </summary>
        /// <param name="passphrase">口令字符串</param>
        /// <returns>32 字节派生密钥</returns>
        public static byte[] DeriveKeyPbkdf2StrDefault(string passphrase)
        {
            return DeriveKeyPbkdf2Str(passphrase, s_Salt, s_Iterations, s_KeyLen);
        }

        /// <summary>
        /// 使用内置盐、迭代次数与 32 字节长度，从字节口令派生 PBKDF2 密钥。
        /// </summary>
        /// <param name="passphrase">口令字节</param>
        /// <returns>32 字节派生密钥</returns>
        public static byte[] DeriveKeyPbkdf2Default(byte[] passphrase)
        {
            return DeriveKeyPbkdf2(passphrase, s_Salt, s_Iterations, s_KeyLen);
        }

        /// <summary>
        /// 使用 PBKDF2（HMAC-SHA1）从字符串口令、盐与迭代次数派生密钥。
        /// </summary>
        /// <param name="passphrase">口令字符串（UTF-8 编码）</param>
        /// <param name="salt">盐值</param>
        /// <param name="iterations">迭代次数</param>
        /// <param name="keyLen">输出密钥长度（字节）</param>
        /// <returns>派生密钥</returns>
        public static byte[] DeriveKeyPbkdf2Str(string passphrase, byte[] salt, int iterations, int keyLen)
        {
            var bs = Encoding.UTF8.GetBytes(passphrase);
            return DeriveKeyPbkdf2(bs, salt, iterations, keyLen);
        }

        /// <summary>
        /// 使用 PBKDF2（HMAC-SHA1）从字节口令、盐与迭代次数派生密钥。
        /// </summary>
        /// <param name="passphrase">口令字节</param>
        /// <param name="salt">盐值</param>
        /// <param name="iterations">迭代次数</param>
        /// <param name="keyLen">输出密钥长度（字节）</param>
        /// <returns>派生密钥</returns>
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