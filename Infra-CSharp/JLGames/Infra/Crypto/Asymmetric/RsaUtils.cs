using System;
using System.IO;
using System.Security.Cryptography;

namespace JLGames.Infra.Crypto.Asymmetric
{
    /// <summary>
    /// 从 PEM 文件或文本加载 RSA 密钥并创建 <see cref="IRsaPrivateCipher"/> / <see cref="IRsaPublicCipher"/>。
    /// </summary>
    public static class RsaUtils
    {
        /// <summary>
        /// 从文件加载 PKCS#1 v1.5 RSA 私钥并创建私钥处理器。
        /// </summary>
        /// <param name="keyPath">PEM 文件路径</param>
        /// <returns>私钥处理器；失败时返回 <c>null</c></returns>
        public static IRsaPrivateCipher LoadPrivateCipherPkcs1V15(string keyPath)
        {
            var key = LoadPkcs1V15Private(keyPath);
            return null == key ? null : new RsaPrivateCipher(key);
        }

        /// <summary>
        /// 从 PEM 文本加载 PKCS#1 v1.5 RSA 私钥并创建私钥处理器。
        /// </summary>
        /// <param name="keyContent">PEM 全文</param>
        /// <returns>私钥处理器；失败时返回 <c>null</c></returns>
        public static IRsaPrivateCipher LoadPrivateCipherPkcs1V15Content(string keyContent)
        {
            var key = LoadPkcs1V15PrivateContent(keyContent);
            return null == key ? null : new RsaPrivateCipher(key);
        }

        /// <summary>
        /// 从文件加载 PKCS#1 v1.5 RSA 公钥并创建公钥处理器。
        /// </summary>
        /// <param name="keyPath">PEM 文件路径</param>
        /// <returns>公钥处理器；失败时返回 <c>null</c></returns>
        public static IRsaPublicCipher LoadPublicCipherPkcs1V15(string keyPath)
        {
            var key = LoadPkcs1V15Public(keyPath);
            return null == key ? null : new RsaPublicCipher(key);
        }

        /// <summary>
        /// 从 PEM 文本加载 PKCS#1 v1.5 RSA 公钥并创建公钥处理器。
        /// </summary>
        /// <param name="keyContent">PEM 全文</param>
        /// <returns>公钥处理器；失败时返回 <c>null</c></returns>
        public static IRsaPublicCipher LoadPublicCipherPkcs1V15Content(string keyContent)
        {
            var key = LoadPkcs1V15PublicContent(keyContent);
            return null == key ? null : new RsaPublicCipher(key);
        }

        /// <summary>
        /// 从文件加载 PKCS#8 RSA 私钥并创建私钥处理器。
        /// </summary>
        /// <param name="keyPath">PEM 文件路径</param>
        /// <returns>私钥处理器；失败时返回 <c>null</c></returns>
        public static IRsaPrivateCipher LoadPrivateCipherPkcs8(string keyPath)
        {
            var key = LoadPkcs8Private(keyPath);
            return null == key ? null : new RsaPrivateCipher(key);
        }

        /// <summary>
        /// 从 PEM 文本加载 PKCS#8 RSA 私钥并创建私钥处理器。
        /// </summary>
        /// <param name="keyContent">PEM 全文</param>
        /// <returns>私钥处理器；失败时返回 <c>null</c></returns>
        public static IRsaPrivateCipher LoadPrivateCipherPkcs8Content(string keyContent)
        {
            var key = LoadPkcs8PrivateContent(keyContent);
            return null == key ? null : new RsaPrivateCipher(key);
        }

        /// <summary>
        /// 从文件加载 X.509 SubjectPublicKeyInfo RSA 公钥并创建公钥处理器。
        /// </summary>
        /// <param name="keyPath">PEM 文件路径</param>
        /// <returns>公钥处理器；失败时返回 <c>null</c></returns>
        public static IRsaPublicCipher LoadPublicCipherX509(string keyPath)
        {
            var key = LoadX509Public(keyPath);
            return null == key ? null : new RsaPublicCipher(key);
        }

        /// <summary>
        /// 从 PEM 文本加载 X.509 RSA 公钥并创建公钥处理器。
        /// </summary>
        /// <param name="keyContent">PEM 全文</param>
        /// <returns>公钥处理器；失败时返回 <c>null</c></returns>
        public static IRsaPublicCipher LoadPublicCipherX509Content(string keyContent)
        {
            var key = LoadX509PublicContent(keyContent);
            return null == key ? null : new RsaPublicCipher(key);
        }

        /// <summary>
        /// 从文件加载 PKCS#1 v1.5 RSA 私钥为 <see cref="RSA"/> 实例。
        /// </summary>
        /// <param name="keyPath">PEM 文件路径</param>
        /// <returns>RSA 实例；失败时返回 <c>null</c></returns>
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
        /// 从 PEM 文本加载 PKCS#1 v1.5 RSA 私钥。
        /// </summary>
        /// <param name="keyContent">PEM 全文</param>
        /// <returns>RSA 实例；失败时返回 <c>null</c></returns>
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
        /// 从文件加载 PKCS#8 RSA 私钥。
        /// </summary>
        /// <param name="keyPath">PEM 文件路径</param>
        /// <returns>RSA 实例；失败时返回 <c>null</c></returns>
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
        /// 从 PEM 文本加载 PKCS#8 RSA 私钥。
        /// </summary>
        /// <param name="keyContent">PEM 全文</param>
        /// <returns>RSA 实例；失败时返回 <c>null</c></returns>
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
        /// 从文件加载 PKCS#1 v1.5 RSA 公钥。
        /// </summary>
        /// <param name="keyPath">PEM 文件路径</param>
        /// <returns>RSA 实例；失败时返回 <c>null</c></returns>
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
        /// 从 PEM 文本加载 PKCS#1 v1.5 RSA 公钥。
        /// </summary>
        /// <param name="keyContent">PEM 全文</param>
        /// <returns>RSA 实例；失败时返回 <c>null</c></returns>
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
        /// 从文件加载 X.509 SubjectPublicKeyInfo RSA 公钥。
        /// </summary>
        /// <param name="keyPath">PEM 文件路径</param>
        /// <returns>RSA 实例；失败时返回 <c>null</c></returns>
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
        /// 从 PEM 文本加载 X.509 RSA 公钥。
        /// </summary>
        /// <param name="keyContent">PEM 全文</param>
        /// <returns>RSA 实例；失败时返回 <c>null</c></returns>
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