using System;

namespace JLGames.Infra.Crypto
{
    /// <summary>
    /// PEM 封装中 <c>-----BEGIN …-----</c> / <c>-----END …-----</c> 标签常量。
    /// </summary>
    public static class PemTypes
    {
        /// <summary>
        /// OpenSshPrivateKey
        /// OpenSSH特定私钥
        /// 常用于 OpenSSH
        /// </summary>
        public const string OpenSshPrivateKey = "OPENSSH PRIVATE KEY";

        /// <summary>
        /// Ssh2PublicKey
        /// SSH2公钥
        /// 常用于 SecureCRT、Tectia等工具
        /// </summary>
        public const string Ssh2PublicKey = "SSH2 PUBLIC KEY";

        /// <summary>
        /// RsaPrivateKey
        /// PKCS#1 私钥
        /// 常用于 OpenSSL、Go
        /// </summary>
        public const string RsaPrivateKey = "RSA PRIVATE KEY";

        /// <summary>
        /// RsaPublicKey
        /// PKCS#1 公钥（较少用）
        /// 不常用，通常导出为 SubjectPublicKeyInfo
        /// </summary>
        public const string RsaPublicKey = "RSA PUBLIC KEY";

        /// <summary>
        /// Pkcs8PrivateKey
        /// PKCS#8 私钥
        /// 更通用，支持多算法
        /// </summary>
        public const string Pkcs8PrivateKey = "PRIVATE KEY";

        /// <summary>
        /// EncryptedPkcs8PrivateKey
        /// 加密的 PKCS#8 私钥
        /// 常用于 OpenSSL、Go
        /// </summary>
        public const string EncryptedPkcs8PrivateKey = "ENCRYPTED PRIVATE KEY";

        /// <summary>
        /// X509PublicKey
        /// X.509 公钥（SubjectPublicKeyInfo）
        /// 常用于 OpenSSL、Go
        /// </summary>
        public const string X509PublicKey = "PUBLIC KEY";

        /// <summary>
        /// Certificate
        /// X.509 证书
        /// 常用于 SSL/TLS
        /// </summary>
        public const string Certificate = "CERTIFICATE";

        /// <summary>
        /// CertRequest
        /// PKCS#10 证书签名请求（CSR）
        /// 常用于 OpenSSL、Go
        /// </summary>
        public const string CertRequest = "CERTIFICATE REQUEST";

        /// <summary>
        /// NewCertRequest
        /// OpenSSL CSR 别名
        /// </summary>
        public const string NewCertRequest = "NEW CERTIFICATE REQUEST";

        /// <summary>
        /// PEMTypeCRL
        /// X509 吊销列表（Certificate Revocation List）
        /// </summary>
        public const string Crl = "X509 CRL";

        /// <summary>
        /// EcPrivateKey
        /// ECDSA 私钥（SEC1格式）
        /// 常用于 OpenSSL
        /// </summary>
        public const string EcPrivateKey = "EC PRIVATE KEY";

        /// <summary>
        /// DsaPrivateKey
        /// DSA 私钥（少见）
        /// </summary>
        public const string DsaPrivateKey = "DSA PRIVATE KEY";

        /// <summary>
        /// AttributeCertificate
        /// 属性证书（稀有）
        /// </summary>
        public const string AttributeCertificate = "ATTRIBUTE CERTIFICATE";

        /// <summary>
        /// Pkcs7
        /// PKCS#7数据（签名/加密容器）
        /// </summary>
        public const string Pkcs7 = "PKCS7";

        /// <summary>
        /// Cms
        /// Cryptographic Message Syntax（PKCS#7 的替代）
        /// </summary>
        public const string Cms = "CMS";
    }

    /// <summary>
    /// 加密模块业务异常。
    /// </summary>
    public class CryptoException : Exception
    {
        /// <summary>
        /// 使用指定消息创建异常。
        /// </summary>
        /// <param name="message">异常描述</param>
        public CryptoException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// 加密模块预定义错误实例。
    /// </summary>
    public static class Errors
    {
        /// <summary>
        /// 不支持的 PEM 类型。
        /// </summary>
        public static readonly Exception ErrUnsupportedPemType = new CryptoException("unsupported pem type");
    }
}