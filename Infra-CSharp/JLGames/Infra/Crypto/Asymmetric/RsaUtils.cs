using System.IO;
using System.Security.Cryptography;

namespace JLGames.Infra.Crypto.Asymmetric
{
    public static class RsaUtils
    {
        /// <summary>
        /// 加载 PKCS#1 v1.5 RSA私钥
        /// </summary>
        public static IRsaPrivateCipher LoadPrivateCipherPkcs1V15(string keyPath)
        {
            var key = LoadPkcs1V15Private(keyPath);
            return new RsaPrivateCipher(key);
        }

        /// <summary>
        /// 加载 PKCS#1 v1.5 RSA公钥
        /// </summary>
        public static IRsaPublicCipher LoadPublicCipherPkcs1V15(string keyPath)
        {
            var key = LoadPkcs1V15Public(keyPath);
            return new RsaPublicCipher(key);
        }

        /// <summary>
        /// 加载 PKCS#1 v1.5 RSA私钥
        /// </summary>
        public static RSA LoadPkcs1V15Private(string keyPath)
        {
            var keyContent = File.ReadAllText(keyPath);
            var derData = RsaParamUtils.ExtractDerData(keyContent, PemTypes.RsaPrivateKey);
            var @params = RsaParamUtils.DecodePkcs1V15Private(derData);
            var rsa = RSA.Create();
            rsa.ImportParameters(@params);
            return rsa;
        }

        /// <summary>
        /// 加载 PKCS#1 v1.5 RSA公钥
        /// </summary>
        public static RSA LoadPkcs1V15Public(string keyPath)
        {
            var keyContent = File.ReadAllText(keyPath);
            var derData = RsaParamUtils.ExtractDerData(keyContent, PemTypes.RsaPublicKey);
            var @params = RsaParamUtils.DecodePkcs1V15Public(derData);
            var rsa = RSA.Create();
            rsa.ImportParameters(@params);
            return rsa;
        }
    }
}