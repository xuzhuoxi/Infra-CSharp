using System;
using System.Text;
using System.Security.Cryptography;

namespace JLGames.Infra.Utils
{
    /// <summary>
    /// RSA util.
    /// not very efficient
    /// 效率不是很高
    /// </summary>
    public static class RSAUtil
    {
        /// <summary>
        /// The name of the key container, keep the encryption and decryption consistent to decrypt successfully
        /// 密匙容器的名称，保持加密解密一致才能解密成功
        /// </summary>
        private static string KeyContainerName = "oa_erp_dowork";

        // encryption(加密)
        public static string Encrypted(string express)
        {
            var param = new CspParameters();
            param.KeyContainerName = KeyContainerName;
            using (var rsa = new RSACryptoServiceProvider(param))
            {
                var plaindata = Encoding.Default.GetBytes(express); //将要加密的字符串转换为字节数组
                var encryptdata = rsa.Encrypt(plaindata, false); //将加密后的字节数据转换为新的加密字节数组
                return Convert.ToBase64String(encryptdata); //将加密后的字节数组转换为字符串
            }
        }

        // decrypt(解密)
        public static string Decrypted(string ciphertext)
        {
            var param = new CspParameters();
            param.KeyContainerName = KeyContainerName;
            using (var rsa = new RSACryptoServiceProvider(param))
            {
                var encryptdata = Convert.FromBase64String(ciphertext);
                var decryptdata = rsa.Decrypt(encryptdata, false);
                return Encoding.Default.GetString(decryptdata);
            }
        }
    }
}