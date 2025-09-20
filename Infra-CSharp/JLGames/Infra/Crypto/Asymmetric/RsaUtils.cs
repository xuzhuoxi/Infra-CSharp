using System;
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
            return null == key ? null : new RsaPrivateCipher(key);
        }

        /// <summary>
        /// 加载 PKCS#1 v1.5 RSA私钥
        /// </summary>
        public static IRsaPrivateCipher LoadPrivateCipherPkcs1V15Content(string keyContent)
        {
            var key = LoadPkcs1V15PrivateContent(keyContent);
            return null == key ? null : new RsaPrivateCipher(key);
        }

        /// <summary>
        /// 加载 PKCS#1 v1.5 RSA公钥
        /// </summary>
        public static IRsaPublicCipher LoadPublicCipherPkcs1V15(string keyPath)
        {
            var key = LoadPkcs1V15Public(keyPath);
            return null == key ? null : new RsaPublicCipher(key);
        }

        /// <summary>
        /// 加载 PKCS#1 v1.5 RSA公钥
        /// </summary>
        public static IRsaPublicCipher LoadPublicCipherPkcs1V15Content(string keyContent)
        {
            var key = LoadPkcs1V15PublicContent(keyContent);
            return null == key ? null : new RsaPublicCipher(key);
        }

        /// <summary>
        /// 加载 PKCS#8 RSA私钥
        /// </summary>
        public static IRsaPrivateCipher LoadPrivateCipherPkcs8(string keyPath)
        {
            var key = LoadPkcs8Private(keyPath);
            return null == key ? null : new RsaPrivateCipher(key);
        }

        /// <summary>
        /// 加载 PKCS#8 RSA私钥
        /// </summary>
        public static IRsaPrivateCipher LoadPrivateCipherPkcs8Content(string keyContent)
        {
            var key = LoadPkcs8PrivateContent(keyContent);
            return null == key ? null : new RsaPrivateCipher(key);
        }

        /// <summary>
        /// 加载 X.509 RSA公钥
        /// </summary>
        public static IRsaPublicCipher LoadPublicCipherX509(string keyPath)
        {
            var key = LoadX509Public(keyPath);
            return null == key ? null : new RsaPublicCipher(key);
        }

        /// <summary>
        /// 加载 X.509 RSA公钥
        /// </summary>
        public static IRsaPublicCipher LoadPublicCipherX509Content(string keyContent)
        {
            var key = LoadX509PublicContent(keyContent);
            return null == key ? null : new RsaPublicCipher(key);
        }

        /// <summary>
        /// 加载 PKCS#1 v1.5 RSA私钥
        /// </summary>
        public static RSA LoadPkcs1V15Private(string keyPath)
        {
            try
            {
                var keyContent = File.ReadAllText(keyPath);
                return LoadPkcs1V15PrivateContent(keyContent);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 加载 PKCS#1 v1.5 RSA私钥
        /// </summary>
        public static RSA LoadPkcs1V15PrivateContent(string keyContent)
        {
            if (string.IsNullOrEmpty(keyContent)) return null;
            try
            {
                var derData = RsaParamUtils.ExtractDerData(keyContent, PemTypes.RsaPrivateKey);
                var @params = RsaParamUtils.DecodePkcs1V15Private(derData);
                var rsa = RSA.Create();
                rsa.ImportParameters(@params);
                return rsa;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 加载 PKCS#8 RSA私钥
        /// </summary>
        public static RSA LoadPkcs8Private(string keyPath)
        {
            try
            {
                var keyContent = File.ReadAllText(keyPath);
                return LoadPkcs8PrivateContent(keyContent);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 加载 PKCS#8 RSA私钥
        /// </summary>
        public static RSA LoadPkcs8PrivateContent(string keyContent)
        {
            if (string.IsNullOrEmpty(keyContent)) return null;
            try
            {
                var derData = RsaParamUtils.ExtractDerData(keyContent, PemTypes.Pkcs8PrivateKey);
                var @params = RsaParamUtils.DecodePkcs8Params(derData);
                var rsa = RSA.Create();
                rsa.ImportParameters(@params);
                return rsa;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 加载 PKCS#1 v1.5 RSA公钥
        /// </summary>
        public static RSA LoadPkcs1V15Public(string keyPath)
        {
            try
            {
                var keyContent = File.ReadAllText(keyPath);
                return LoadPkcs1V15PublicContent(keyContent);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 加载 PKCS#1 v1.5 RSA公钥
        /// </summary>
        public static RSA LoadPkcs1V15PublicContent(string keyContent)
        {
            if (string.IsNullOrEmpty(keyContent)) return null;
            try
            {
                var derData = RsaParamUtils.ExtractDerData(keyContent, PemTypes.RsaPublicKey);
                var @params = RsaParamUtils.DecodePkcs1V15Public(derData);
                var rsa = RSA.Create();
                rsa.ImportParameters(@params);
                return rsa;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 加载 X509 RSA公钥
        /// </summary>
        public static RSA LoadX509Public(string keyPath)
        {
            try
            {
                var keyContent = File.ReadAllText(keyPath);
                return LoadX509PublicContent(keyContent);
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 加载 X509 RSA公钥
        /// </summary>
        public static RSA LoadX509PublicContent(string keyContent)
        {
            if (string.IsNullOrEmpty(keyContent)) return null;
            try
            {
                var derData = RsaParamUtils.ExtractDerData(keyContent, PemTypes.X509PublicKey);
                var @params = RsaParamUtils.DecodeX509Params(derData);
                var rsa = RSA.Create();
                rsa.ImportParameters(@params);
                return rsa;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}