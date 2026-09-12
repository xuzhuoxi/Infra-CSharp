# Crypto API 文档

## 概述

Crypto 模块提供对称加密（AES / DES / 3DES / XOR）、非对称加密（RSA）、密钥交换与派生（Diffie-Hellman、PBKDF2）、哈希、PEM/DER 解析以及 ASN.1 TLV 读取等能力。推荐使用 `AesCipher`、`RsaCipher` / `RsaUtils` 等现行类型；`Deleted/` 下的历史工具见文末 **已弃用** 一节。

CTR / GCM 底层由公开的 `AesCtrEngine`、`AesGcmEngine` 实现。`BouncyCastleAesCtrEngine`、`BouncyCastleAesGcmEngine` 源码已全部注释，不是可用 API。

## 命名空间

- `JLGames.Infra.Crypto`
- `JLGames.Infra.Crypto.Symmetric`
- `JLGames.Infra.Crypto.Asymmetric`
- `JLGames.Infra.Crypto.Key`
- `JLGames.Infra.Crypto.ASN1`

---

## 命名空间: JLGames.Infra.Crypto

### 接口 (Interfaces)

#### IEncryptCipher

加密处理器接口。

```csharp
/// <summary>
/// 加密处理器接口
/// </summary>
public interface IEncryptCipher
{
    /// <summary>
    /// 加密数据
    /// </summary>
    /// <param name="plaintext">待加密的明文数据</param>
    /// <returns>加密后的密文数据</returns>
    /// <exception cref="Exception">加密过程中发生错误</exception>
    byte[] Encrypt(byte[] plaintext);
}
```

#### IDecryptCipher

解密处理器接口。

```csharp
/// <summary>
/// 解密处理器接口
/// </summary>
public interface IDecryptCipher
{
    /// <summary>
    /// 解密数据
    /// </summary>
    /// <param name="ciphertext">待解密的密文数据</param>
    /// <returns>解密后的明文数据</returns>
    /// <exception cref="Exception">解密过程中发生错误</exception>
    byte[] Decrypt(byte[] ciphertext);
}
```

#### ICipher

加密解密处理器接口，同时继承加密与解密接口。

```csharp
/// <summary>
/// 加密解密处理器接口，继承了加密和解密接口
/// </summary>
public interface ICipher : IEncryptCipher, IDecryptCipher
{
}
```

### 枚举 (Enums)

#### BlockMode

分组密码工作模式。AES 实现实际支持 CBC / CTR / GCM；DES 实现实际支持 ECB / CBC / CTR。枚举中的 CFB、OFB 为模式定义，现行实现调用时会抛出不支持异常。

```csharp
/// <summary>
/// 分组密码工作模式枚举
/// </summary>
public enum BlockMode
{
    /// <summary>
    /// 电子密码本模式（不安全，不推荐实际使用）
    /// </summary>
    ECB,

    /// <summary>
    /// 加密分组链接模式，首块需随机 IV
    /// </summary>
    CBC,

    /// <summary>
    /// 加密反馈模式（流式）
    /// </summary>
    CFB,

    /// <summary>
    /// 输出反馈模式
    /// </summary>
    OFB,

    /// <summary>
    /// 计数器模式，支持并行
    /// </summary>
    CTR,

    /// <summary>
    /// Galois/Counter 认证加密模式
    /// </summary>
    GCM
}
```

### 静态类 (Static Classes)

#### BlockModeHelper

分组模式辅助工具。`GetDescription` 返回中文描述字符串。

```csharp
/// <summary>
/// 分组模式辅助工具
/// </summary>
public static class BlockModeHelper
{
    /// <summary>
    /// 获取加密模式的中文描述
    /// </summary>
    /// <param name="mode">加密模式枚举值</param>
    /// <returns>描述信息</returns>
    public static string GetDescription(BlockMode mode);
}
```

返回值：

| 模式 | 描述 |
|------|------|
| `ECB` | 电子密码本模式：不安全，每个分组独立加密，容易泄漏结构，不推荐使用 |
| `CBC` | 加密分组链接模式：常用，安全性较好，解密可并行，但加密不可并行 |
| `CFB` | 加密反馈模式：流式加密，适合字节数据传输，一位错误会影响当前和下一个块 |
| `OFB` | 输出反馈模式：预计算密钥流，错误不会扩散，对 IV 非常敏感 |
| `CTR` | 计数器模式：高性能，支持并行加解密，常用于高性能通信流加密 |
| `GCM` | Galois/Counter 模式：高安全性，支持认证加密，常用于 TLS、VPN、HTTPS 等高安全需求场景 |
| 其他 | 未知加密模式 |

#### CryptoUtils

加密模块通用字节数组与 OID 校验工具。OID 比较方法名为 `Assert*`，实际返回 `bool`，不会抛出异常。

```csharp
/// <summary>
/// 加密模块通用字节数组与 OID 校验工具。
/// </summary>
public static class CryptoUtils
{
    /// <summary>
    /// 判断是否为 Rsa算法标识(OID)
    /// </summary>
    /// <param name="rsaOid">待校验的 OID 字节序列</param>
    /// <returns>与内置 RSA OID 一致时返回 <c>true</c></returns>
    public static bool AssertIsRsaOid(byte[] rsaOid);

    /// <summary>
    /// 判断是否为 Dsa算法标识(OID)
    /// </summary>
    /// <param name="dsaOid">待校验的 OID 字节序列</param>
    /// <returns>与内置 DSA OID 一致时返回 <c>true</c></returns>
    public static bool AssertIsDsaOid(byte[] dsaOid);

    /// <summary>
    /// 判断是否为 Ecdsa算法标识(OID)
    /// </summary>
    /// <param name="ecdsaOid">待校验的 OID 字节序列</param>
    /// <returns>与内置 ECDSA OID 一致时返回 <c>true</c></returns>
    public static bool AssertIsEcdsaOid(byte[] ecdsaOid);

    /// <summary>
    /// 合并两个字节数组
    /// </summary>
    /// <param name="bs">第一个数组</param>
    /// <param name="bs1">第二个数组</param>
    /// <returns>按顺序拼接后的新数组</returns>
    public static byte[] Combine(byte[] bs, byte[] bs1);

    /// <summary>
    /// 拆分数组为两个
    /// </summary>
    /// <param name="data">源数组</param>
    /// <param name="firstSize">第一段长度（字节）</param>
    /// <param name="first">输出：前 <paramref name="firstSize"/> 字节</param>
    /// <param name="second">输出：剩余字节</param>
    public static void Extract(byte[] data, int firstSize, out byte[] first, out byte[] second);

    /// <summary>
    /// 合并多个字节数组
    /// </summary>
    /// <param name="bs">第一个数组</param>
    /// <param name="bs1">第二个数组</param>
    /// <param name="bs2">第三个数组</param>
    /// <param name="others">后续待拼接的数组</param>
    /// <returns>按顺序拼接后的新数组</returns>
    public static byte[] CombineMulti(byte[] bs, byte[] bs1, byte[] bs2, params byte[][] others);
}
```

内置 OID（十六进制）：

- RSA：`2A 86 48 86 F7 0D 01 01 01`（1.2.840.113549.1.1.1）
- DSA / ECDSA：`2A 86 48 CE 3D 02 01`（当前源码中二者常量相同）

#### PaddingDelegate

块密码填充/去填充的委托类型定义。

```csharp
/// <summary>
/// 块密码填充/去填充的委托类型定义。
/// </summary>
public static class PaddingDelegate
{
    /// <summary>
    /// 填充函数类型
    /// </summary>
    public delegate byte[] FuncPadding(byte[] data, int blockSize);

    /// <summary>
    /// 去填充函数类型
    /// </summary>
    public delegate byte[] FuncUnPadding(byte[] data);
}
```

#### PemTypes

PEM 封装中 `-----BEGIN …-----` / `-----END …-----` 标签常量。

```csharp
/// <summary>
/// PEM 封装中 <c>-----BEGIN …-----</c> / <c>-----END …-----</c> 标签常量。
/// </summary>
public static class PemTypes
{
    /// <summary>OpenSSH 特定私钥，常用于 OpenSSH</summary>
    public const string OpenSshPrivateKey = "OPENSSH PRIVATE KEY";

    /// <summary>SSH2 公钥，常用于 SecureCRT、Tectia 等工具</summary>
    public const string Ssh2PublicKey = "SSH2 PUBLIC KEY";

    /// <summary>PKCS#1 私钥，常用于 OpenSSL、Go</summary>
    public const string RsaPrivateKey = "RSA PRIVATE KEY";

    /// <summary>PKCS#1 公钥（较少用），通常导出为 SubjectPublicKeyInfo</summary>
    public const string RsaPublicKey = "RSA PUBLIC KEY";

    /// <summary>PKCS#8 私钥，更通用，支持多算法</summary>
    public const string Pkcs8PrivateKey = "PRIVATE KEY";

    /// <summary>加密的 PKCS#8 私钥，常用于 OpenSSL、Go</summary>
    public const string EncryptedPkcs8PrivateKey = "ENCRYPTED PRIVATE KEY";

    /// <summary>X.509 公钥（SubjectPublicKeyInfo），常用于 OpenSSL、Go</summary>
    public const string X509PublicKey = "PUBLIC KEY";

    /// <summary>X.509 证书，常用于 SSL/TLS</summary>
    public const string Certificate = "CERTIFICATE";

    /// <summary>PKCS#10 证书签名请求（CSR），常用于 OpenSSL、Go</summary>
    public const string CertRequest = "CERTIFICATE REQUEST";

    /// <summary>OpenSSL CSR 别名</summary>
    public const string NewCertRequest = "NEW CERTIFICATE REQUEST";

    /// <summary>X509 吊销列表（Certificate Revocation List）</summary>
    public const string Crl = "X509 CRL";

    /// <summary>ECDSA 私钥（SEC1 格式），常用于 OpenSSL</summary>
    public const string EcPrivateKey = "EC PRIVATE KEY";

    /// <summary>DSA 私钥（少见）</summary>
    public const string DsaPrivateKey = "DSA PRIVATE KEY";

    /// <summary>属性证书（稀有）</summary>
    public const string AttributeCertificate = "ATTRIBUTE CERTIFICATE";

    /// <summary>PKCS#7 数据（签名/加密容器）</summary>
    public const string Pkcs7 = "PKCS7";

    /// <summary>Cryptographic Message Syntax（PKCS#7 的替代）</summary>
    public const string Cms = "CMS";
}
```

#### Errors

加密模块预定义错误实例。

```csharp
/// <summary>
/// 加密模块预定义错误实例。
/// </summary>
public static class Errors
{
    /// <summary>
    /// 不支持的 PEM 类型。
    /// </summary>
    public static readonly Exception ErrUnsupportedPemType;
}
```

`ErrUnsupportedPemType` 为 `CryptoException("unsupported pem type")`。

### 类 (Classes)

#### HashHelper

常用哈希算法的便捷封装。十六进制结果为无分隔符小写字符串。文件读取失败时：`Md5File` / `Sha1File` / `HashFile2Hex` 返回空字符串，`HashFile` 返回 `null`。

```csharp
/// <summary>
/// 常用哈希算法的便捷封装
/// </summary>
public class HashHelper
{
    /// <summary>
    /// 对字节数组计算 MD5，返回小写十六进制字符串（无分隔符）。
    /// </summary>
    public static string Md5(byte[] data);

    /// <summary>
    /// 对 UTF-8 字符串计算 MD5，返回小写十六进制字符串。
    /// </summary>
    public static string Md5String(string data);

    /// <summary>
    /// 读取文件全文并计算 MD5；读取失败时返回空字符串。
    /// </summary>
    public static string Md5File(string filePath);

    /// <summary>
    /// 对字节数组计算 SHA-1，返回小写十六进制字符串（无分隔符）。
    /// </summary>
    public static string Sha1(byte[] data);

    /// <summary>
    /// 对 UTF-8 字符串计算 SHA-1，返回小写十六进制字符串。
    /// </summary>
    public static string Sha1String(string data);

    /// <summary>
    /// 读取文件全文并计算 SHA-1；读取失败时返回空字符串。
    /// </summary>
    public static string Sha1File(string filePath);

    /// <summary>
    /// 使用指定 <see cref="HashAlgorithm"/> 实例计算哈希。
    /// </summary>
    /// <returns>原始哈希字节；算法为 <c>null</c> 时返回 <c>null</c></returns>
    public static byte[] Hash(HashAlgorithm hashAlgorithm, byte[] data);

    /// <summary>
    /// 计算哈希并格式化为小写十六进制字符串。
    /// </summary>
    /// <returns>十六进制摘要；失败或算法为 <c>null</c> 时返回空字符串</returns>
    public static string Hash2Hex(HashAlgorithm hashAlgorithm, byte[] data);

    /// <summary>
    /// 对 UTF-8 字符串计算哈希。
    /// </summary>
    public static byte[] HashString(HashAlgorithm hashAlgorithm, string data);

    /// <summary>
    /// 对 UTF-8 字符串计算哈希并格式化为小写十六进制字符串。
    /// </summary>
    public static string HashString2Hex(HashAlgorithm hashAlgorithm, string data);

    /// <summary>
    /// 读取文件全文并计算哈希；读取失败时返回 <c>null</c>。
    /// </summary>
    public static byte[] HashFile(HashAlgorithm hashAlgorithm, string filePath);

    /// <summary>
    /// 读取文件全文并计算哈希，格式化为小写十六进制字符串；读取失败时返回空字符串。
    /// </summary>
    public static string HashFile2Hex(HashAlgorithm hashAlgorithm, string filePath);
}
```

MD5 / SHA-1 仅适合完整性校验，不适合作为安全摘要。

#### PaddingUtils

对称加密常用的块填充与去填充实现。`blockSize` 必须为正数。PKCS#7 / ISO 10126 / ANSI X9.23 去填充在数据为空或填充长度非法时抛出 `ArgumentException`。已对齐时仍会再补一整块。

```csharp
/// <summary>
/// 对称加密常用的块填充与去填充实现
/// </summary>
public class PaddingUtils
{
    /// <summary>
    /// PKCS#7 填充：不足块大小时在末尾追加填充字节，每个填充字节的值等于填充长度。
    /// </summary>
    public static byte[] Pkcs7Padding(byte[] data, int blockSize);

    /// <summary>
    /// 移除 PKCS#7 填充。
    /// </summary>
    public static byte[] Pkcs7UnPadding(byte[] data);

    /// <summary>
    /// 零填充：在末尾补 0 至块边界；若已对齐则补一整块。
    /// </summary>
    public static byte[] ZeroPadding(byte[] data, int blockSize);

    /// <summary>
    /// 移除末尾的零字节填充。
    /// </summary>
    public static byte[] ZeroUnPadding(byte[] data);

    /// <summary>
    /// ISO 10126 填充：随机字节 + 最后一字节为填充长度。
    /// </summary>
    public static byte[] Iso10126Padding(byte[] data, int blockSize);

    /// <summary>
    /// 移除 ISO 10126 填充。
    /// </summary>
    public static byte[] Iso10126UnPadding(byte[] data);

    /// <summary>
    /// ANSI X9.23 填充：前若干字节为 0，最后一字节为填充长度。
    /// </summary>
    public static byte[] AnsiX923Padding(byte[] data, int blockSize);

    /// <summary>
    /// 移除 ANSI X9.23 填充。
    /// </summary>
    public static byte[] AnsiX923UnPadding(byte[] data);
}
```

#### CryptoException

加密模块业务异常。

```csharp
/// <summary>
/// 加密模块业务异常。
/// </summary>
public class CryptoException : Exception
{
    /// <summary>
    /// 使用指定消息创建异常。
    /// </summary>
    /// <param name="message">异常描述</param>
    public CryptoException(string message);
}
```

---

## 命名空间: JLGames.Infra.Crypto.Symmetric

### 接口 (Interfaces)

#### IAesCipher

AES 对称加密接口（高级加密标准，算法 Rijndael）。继承 `ICipher`。`Encrypt` / `Decrypt` 在 `AesCipher` 中默认走 GCM。

无 IV 重载：CBC/CTR 输出前缀随机 IV；GCM 前缀 12 字节 nonce。带 IV/nonce 的重载不把 IV/nonce 写入返回值。

```csharp
/// <summary>
/// AES 对称加密接口（高级加密标准，算法 Rijndael）
/// </summary>
public interface IAesCipher : ICipher
{
    /// <summary>
    /// 获取密钥副本（只读）
    /// </summary>
    byte[] Key { get; }

    /// <summary>
    /// 块大小（字节），AES 为 16
    /// </summary>
    int BlockSize { get; }

    /// <summary>
    /// 设置 CBC 等模式的填充方式
    /// </summary>
    /// <param name="paddingMode">填充模式</param>
    void SetPadding(PaddingMode paddingMode);

    /// <summary>
    /// 按指定分组模式加密
    /// </summary>
    /// <returns>密文；可能前缀随机 IV/nonce</returns>
    byte[] EncryptMode(byte[] plaintext, BlockMode blockMode);

    /// <summary>
    /// 使用指定 IV/nonce 与分组模式加密
    /// </summary>
    byte[] EncryptMode(byte[] plaintext, byte[] iv, BlockMode blockMode);

    /// <summary>
    /// 按指定分组模式解密
    /// </summary>
    byte[] DecryptMode(byte[] ciphertext, BlockMode blockMode);

    /// <summary>
    /// 使用指定 IV/nonce 与分组模式解密
    /// </summary>
    byte[] DecryptMode(byte[] ciphertext, byte[] iv, BlockMode blockMode);

    /// <summary>CBC 加密，前缀 IV。</summary>
    /// <returns>IV + 密文</returns>
    byte[] EncryptCbc(byte[] plaintext);

    /// <summary>CBC 加密，指定 IV。</summary>
    /// <returns>密文（不含 IV）</returns>
    byte[] EncryptCbc(byte[] plaintext, byte[] iv);

    /// <summary>CBC 解密，输入 IV + 密文。</summary>
    byte[] DecryptCbc(byte[] ciphertext);

    /// <summary>CBC 解密，IV 与密文分开。</summary>
    byte[] DecryptCbc(byte[] ciphertext, byte[] iv);

    /// <summary>CTR 加密，前缀 IV。</summary>
    /// <returns>IV + 密文</returns>
    byte[] EncryptCtr(byte[] plaintext);

    /// <summary>CTR 加密，指定 16 字节 IV。</summary>
    byte[] EncryptCtr(byte[] plaintext, byte[] iv);

    /// <summary>CTR 解密，输入 IV + 密文。</summary>
    byte[] DecryptCtr(byte[] ciphertext);

    /// <summary>CTR 解密，IV 与密文分开。</summary>
    byte[] DecryptCtr(byte[] ciphertext, byte[] iv);

    /// <summary>GCM 加密，前缀 12 字节 nonce。</summary>
    /// <returns>nonce + 密文 + 标签</returns>
    byte[] EncryptGcm(byte[] plaintext);

    /// <summary>GCM 加密，指定 nonce。</summary>
    /// <returns>密文 + 认证标签</returns>
    byte[] EncryptGcm(byte[] plaintext, byte[] nonce);

    /// <summary>GCM 解密。</summary>
    /// <param name="ciphertext">nonce + 密文 + 标签</param>
    byte[] DecryptGcm(byte[] ciphertext);

    /// <summary>GCM 解密，nonce 单独传入。</summary>
    /// <param name="ciphertext">密文与标签</param>
    byte[] DecryptGcm(byte[] ciphertext, byte[] nonce);
}
```

`EncryptMode` / `DecryptMode` 仅支持 `CBC`、`CTR`、`GCM`；其他模式抛出 `InvalidOperationException("Unsupported AES block mode!")`。GCM 标签校验失败抛出 `Exception("Tag verification failed. Decryption aborted.")`。

#### IDesCipher

DES 对称加密接口（数据加密标准，算法 DEA）。`Encrypt` / `Decrypt` 在 `DesCipher` 中默认走 CBC。支持 ECB / CBC / CTR。

```csharp
/// <summary>
/// DES 对称加密接口（数据加密标准，算法 DEA）
/// </summary>
public interface IDesCipher : ICipher
{
    /// <summary>密钥长度（字节）</summary>
    int KeySize { get; }

    /// <summary>块大小（字节）</summary>
    int BlockSize { get; }

    /// <summary>
    /// 设置填充模式
    /// </summary>
    void SetPaddingMode(PaddingMode paddingMode);

    /// <summary>
    /// 指定 BlockMode 加密
    /// </summary>
    byte[] EncryptMode(byte[] plaintext, BlockMode blockMode);

    /// <summary>
    /// 指定 BlockMode 加密
    /// </summary>
    byte[] EncryptMode(byte[] plaintext, byte[] iv, BlockMode blockMode);

    /// <summary>
    /// 指定 BlockMode 解密
    /// </summary>
    byte[] DecryptMode(byte[] ciphertext, BlockMode blockMode);

    /// <summary>
    /// 指定 BlockMode 解密
    /// </summary>
    byte[] DecryptMode(byte[] ciphertext, byte[] iv, BlockMode blockMode);

    /// <summary>
    /// 使用 ECB 模式加密
    /// </summary>
    byte[] EncryptEcb(byte[] plaintext);

    /// <summary>
    /// 使用 ECB 模式解密
    /// </summary>
    byte[] DecryptEcb(byte[] ciphertext);

    /// <summary>
    /// 使用 CBC 模式加密
    /// </summary>
    byte[] EncryptCbc(byte[] plaintext);

    /// <summary>
    /// 使用 CBC 模式加密
    /// </summary>
    byte[] EncryptCbc(byte[] plaintext, byte[] iv);

    /// <summary>
    /// 使用 CBC 模式解密
    /// </summary>
    byte[] DecryptCbc(byte[] ciphertext);

    /// <summary>
    /// 使用 CBC 模式解密
    /// </summary>
    byte[] DecryptCbc(byte[] ciphertext, byte[] iv);

    /// <summary>
    /// 使用 CTR 模式加密
    /// </summary>
    byte[] EncryptCtr(byte[] plaintext);

    /// <summary>
    /// 使用 CTR 模式加密
    /// </summary>
    byte[] EncryptCtr(byte[] plaintext, byte[] iv);

    /// <summary>
    /// 使用 CTR 模式解密
    /// </summary>
    byte[] DecryptCtr(byte[] ciphertext);

    /// <summary>
    /// 使用 CTR 模式解密
    /// </summary>
    byte[] DecryptCtr(byte[] ciphertext, byte[] iv);
}
```

不支持的模式抛出 `Exception("Unsupported DES block mode!")`。DES 已被认为不够安全，新代码请优先使用 AES。

#### IXorCipher

按字节循环异或的轻量混淆接口；实现快，不适合高安全场景。无额外成员，加解密为同一运算。

```csharp
/// <summary>
/// 按字节循环异或的轻量混淆接口；实现快，不适合高安全场景。
/// </summary>
public interface IXorCipher : ICipher
{
}
```

### 静态类 (Static Classes)

#### AesDefines

AES 块大小与默认密钥长度常量。

```csharp
/// <summary>
/// AES 块大小与默认密钥长度常量。
/// </summary>
public static class AesDefines
{
    /// <summary>
    /// AES的块大小, 字节数
    /// </summary>
    public const int BlockSize = 16;

    /// <summary>
    /// AES的默认密钥长，字节数
    /// </summary>
    public const int DefaultKeyLength = 32;
}
```

#### DesDefines

DES / 3DES 块大小与密钥长度常量。

```csharp
/// <summary>
/// DES / 3DES 块大小与密钥长度常量。
/// </summary>
public static class DesDefines
{
    /// <summary>
    /// DES的块大小，字节数
    /// </summary>
    public const int BlockSize = 8;

    /// <summary>
    /// DES的密钥长度，字节数
    /// </summary>
    public const int KeySize = 8;

    /// <summary>
    /// 3DES的密钥长度，字节数
    /// 112位密钥会自动追加到168位
    /// </summary>
    public const int TripleKeySize = 24;
}
```

### 类 (Classes)

#### AesCipher

AES 对称加解密（默认 GCM）；支持 CBC、CTR、GCM。密钥长度 16 / 24 / 32 字节对应 AES-128 / 192 / 256。`Key` 返回密钥副本。CBC 默认填充为 `PaddingMode.PKCS7`。CTR 使用 `AesCtrEngine.Default`，GCM 使用 `AesGcmEngine.Default`（标签 16 字节）。

无 IV 的 CBC/CTR/GCM 会生成随机前缀；当前实现使用 `System.Random`，生产环境建议自行用 `RandomNumberGenerator` 生成 IV/nonce 并调用带 IV 的重载。

```csharp
/// <summary>
/// AES 对称加解密（默认 GCM）；支持 CBC、CTR、GCM 等分组模式。
/// </summary>
public class AesCipher : IAesCipher
{
    /// <summary>
    /// 使用指定密钥创建 AES 实例（密钥长度 16/24/32 字节对应 AES-128/192/256）。
    /// </summary>
    /// <param name="key">对称密钥</param>
    public AesCipher(byte[] key);

    // 其余成员实现 IAesCipher
}
```

#### DesCipher

DES 加密类。密钥须为 8 字节（DES）、16 或 24 字节（内部转为 3DES）。`KeySize` 恒为 8；`BlockSize` 为 8。默认填充 `PaddingMode.PKCS7`。CBC 无论是否传入 IV，密文均前缀 IV。CTR 无填充，加解密同一运算。

```csharp
/// <summary>
/// DES 加密类
/// </summary>
public class DesCipher : IDesCipher
{
    /// <summary>
    /// 使用 8 字节（DES）、16 或 24 字节（3DES）密钥创建实例。
    /// </summary>
    /// <param name="key">对称密钥</param>
    /// <exception cref="ArgumentException">密钥为 null，或长度不是 8/16/24</exception>
    public DesCipher(byte[] key);

    // 其余成员实现 IDesCipher
}
```

#### TripleDesCipher

三重 DES（3DES）对称加密，密钥 8 / 16 / 24 字节自动展开为 24 字节。继承 `DesCipher`。`BlockSize` 重写为报告 3DES 密钥长度（24 字节），与 DES 块大小（8 字节）不同。

```csharp
/// <summary>
/// 三重 DES（3DES）对称加密，密钥 8/16/24 字节自动展开为 24 字节。
/// </summary>
public class TripleDesCipher : DesCipher
{
    /// <summary>报告 3DES 密钥长度（24 字节），与 DES 块大小（8 字节）不同。</summary>
    public override int BlockSize { get; }

    /// <summary>
    /// 使用 8、16 或 24 字节密钥创建 3DES 实例。
    /// </summary>
    /// <param name="key">原始密钥</param>
    /// <exception cref="ArgumentException">密钥为 null，或长度不是 8/16/24</exception>
    public TripleDesCipher(byte[] key);
}
```

#### XorCipher

循环密钥异或实现，加解密为同一运算。密钥长度为 0 或 `null` 时原样返回输入。

```csharp
/// <summary>
/// 循环密钥异或实现，加解密为同一运算。
/// </summary>
public class XorCipher : IXorCipher
{
    /// <summary>异或密钥字节</summary>
    public byte[] Key { get; }

    /// <summary>密钥长度；为 0 时加解密原样返回输入</summary>
    public int KeyLen { get; }

    /// <summary>
    /// 使用指定密钥创建实例。
    /// </summary>
    /// <param name="key">异或密钥，可为 <c>null</c>（等价于空密钥）</param>
    public XorCipher(byte[] key);

    public byte[] Encrypt(byte[] plaintext);
    public byte[] Decrypt(byte[] ciphertext);
}
```

#### AesCtrEngine

AES-CTR 模式底层引擎（nonce + counter 组成 16 字节 IV）。公开类型，供 `AesCipher` 使用，也可直接调用。默认 counter 为 `{0,0,0,1}`；递增仅作用于 IV 最后 4 字节。

```csharp
/// <summary>
/// AES-CTR 模式底层引擎（nonce + counter 组成 16 字节 IV）。
/// </summary>
public sealed class AesCtrEngine
{
    /// <summary>默认单例实例。</summary>
    public static AesCtrEngine Default { get; }

    /// <summary>
    /// 使用随机 nonce 加密，返回 IV + 密文。
    /// </summary>
    /// <param name="data">明文或密文</param>
    /// <param name="key">AES 密钥</param>
    /// <returns>16 字节 IV + 处理后的数据</returns>
    public byte[] EncryptRandom(byte[] data, byte[] key);

    /// <summary>
    /// 从 IV + 密文格式中解析 IV 并解密/还原。
    /// </summary>
    /// <exception cref="CryptoException">长度不足一块</exception>
    public byte[] DecryptRandom(byte[] data, byte[] key);

    /// <summary>
    /// 使用 12 字节 nonce 与默认 counter 进行 CTR 加/解密。
    /// </summary>
    public byte[] ProcessWithNonce(byte[] data, byte[] key, byte[] nonce);

    /// <summary>
    /// CTR 加/解密；nonce 与 counter 拼接为 16 字节 IV。
    /// </summary>
    /// <param name="counter">counter 字节；len(nonce)+len(counter)=16</param>
    public byte[] ProcessWithNonce(byte[] data, byte[] key, byte[] nonce, byte[] counter);

    /// <summary>
    /// 使用完整 16 字节 IV 进行 CTR 加/解密。
    /// </summary>
    /// <exception cref="CryptoException">IV 长度不为 16</exception>
    public byte[] ProcessWithIv(byte[] data, byte[] key, byte[] iv);
}
```

#### AesGcmEngine

AES-GCM 认证加密引擎（纯托管实现）。公开类型。`Default` 使用 16 字节标签。构造时可指定标签长度 12–16；非法长度抛出 `ArgumentException("Invalid authentication tag size")`。当前 `Encrypt` 输出标签固定为 16 字节。

```csharp
/// <summary>
/// AES-GCM 认证加密引擎（纯托管实现）
/// </summary>
public sealed class AesGcmEngine
{
    /// <summary>默认单例（16 字节标签）。</summary>
    public static AesGcmEngine Default { get; }

    /// <summary>认证标签长度（字节），有效范围 12–16。</summary>
    public int TagSize { get; }

    /// <summary>使用默认 16 字节认证标签创建引擎。</summary>
    public AesGcmEngine();

    /// <summary>
    /// 使用指定认证标签长度创建引擎。
    /// </summary>
    /// <param name="authenticationTagSize">标签长度（12–16 字节）</param>
    public AesGcmEngine(int authenticationTagSize);

    /// <summary>
    /// GCM 加密并生成认证标签
    /// </summary>
    /// <param name="ciphertext">输出密文（与明文等长）</param>
    /// <param name="tag">认证标签</param>
    public void Encrypt(byte[] plaintext, byte[] key, byte[] nonce, out byte[] ciphertext, out byte[] tag);

    /// <summary>
    /// 验证标签后解密
    /// </summary>
    /// <returns>验证通过返回 true</returns>
    public bool Decrypt(byte[] ciphertext, byte[] key, byte[] nonce, byte[] tag, out byte[] plaintext);
}
```

`BouncyCastleAesCtrEngine` / `BouncyCastleAesGcmEngine` 文件内容已全部注释，不是编译后的公开 API。

---

## 命名空间: JLGames.Infra.Crypto.Asymmetric

### 接口 (Interfaces)

#### IRsaPublicCipher

RSA 公钥操作：加密、混合加密与签名验证（PKCS#1 v1.5）。实现 `IDisposable`。

```csharp
/// <summary>
/// RSA 公钥操作：加密、混合加密与签名验证
/// </summary>
public interface IRsaPublicCipher : IDisposable
{
    /// <summary>获取公钥</summary>
    RSA PublicKey { get; }

    /// <summary>
    /// 加密（支持分组）
    /// </summary>
    byte[] Encrypt(byte[] plaintext);

    /// <summary>
    /// 混合加密
    /// 步骤：
    /// 1. 不足分组长度，不生成随机AES密钥，直接使用RSA加密
    /// 2. 长于分组长度的执行以下步骤:
    ///   2.1 生成32位随机AES密钥与16位随机IV，
    ///   2.2 使用RSA加密AES密钥与IV，得到AES密钥密文
    ///   2.3 使用AES密钥与IV，使用AES-CTR算法加密明文
    ///   2.4 返回(AES密钥密文, 密文)
    /// </summary>
    /// <returns>密文数据，包含RSA加密的AES密钥与IV，以及AES加密后的密文</returns>
    byte[] EncryptHybrid(byte[] plaintext);

    /// <summary>
    /// 使用SHA256进行签名验证
    /// </summary>
    bool VerifySign(byte[] origData, byte[] signature);

    /// <summary>
    /// 使用SHA256进行签名验证
    /// 签名数据为Base64编码字符串
    /// </summary>
    bool VerifySignBase64(byte[] origData, string base64Signature);

    /// <summary>
    /// 指定Hash算法进行签名验证
    /// 注意：MD5和SHA1不可用
    /// </summary>
    bool VerifySignHash(byte[] origData, byte[] signature, HashAlgorithmName hashAlgorithm);

    /// <summary>
    /// 使用指定的Hash算法进行签名验证
    /// 签名数据为Base64编码字符串
    /// </summary>
    bool VerifySignHashBase64(byte[] origData, string base64Signature, HashAlgorithmName hashAlgorithm);
}
```

明文不超过 `EncryptPartLen`（密钥长度/8 − 11）时单段 RSA 加密；更长则按该长度切分后逐段加密再拼接。混合加密短数据同样退化为纯 RSA。签名填充为 PKCS#1 v1.5。验证失败（含 `CryptographicException`）返回 `false`。

#### IRsaPrivateCipher

RSA 私钥操作：解密、混合解密与签名（PKCS#1 v1.5）。实现 `IDisposable`。

```csharp
/// <summary>
/// RSA 私钥操作：解密、混合解密与签名
/// </summary>
public interface IRsaPrivateCipher : IDisposable
{
    /// <summary>获取私钥</summary>
    RSA PrivateKey { get; }

    /// <summary>
    /// 解密
    /// </summary>
    byte[] Decrypt(byte[] ciphertext);

    /// <summary>
    /// 混合解密
    /// </summary>
    /// <param name="ciphertext">密文数据，包含RSA加密的AES密钥与IV，以及AES加密后的密文</param>
    byte[] DecryptHybrid(byte[] ciphertext);

    /// <summary>
    /// 使用SHA256进行签名
    /// </summary>
    byte[] Sign(byte[] origData);

    /// <summary>
    /// 使用SHA256进行签名，并将结果转化为Base64编码字符串
    /// </summary>
    string SignBase64(byte[] origData);

    /// <summary>
    /// 指定Hash算法进行签名
    /// 注意：MD5和SHA1不可用
    /// </summary>
    byte[] SignHash(byte[] origData, HashAlgorithmName hashAlgorithm);

    /// <summary>
    /// 指定Hash算法进行签名，并将结果转化为Base64编码字符串
    /// 注意：MD5和SHA1不可用
    /// </summary>
    string SignHashBase64(byte[] origData, HashAlgorithmName hashAlgorithm);
}
```

密文长度等于 `DecryptPartLen`（密钥长度/8）时单段解密，否则按该长度切分。混合解密：前 `DecryptPartLen` 字节为 RSA 加密的 AES-256 密钥 + IV，其余为 AES-CTR 密文。`SignHash` 在无法创建哈希算法时返回 `null`。

#### IRsaCipher

同时具备 RSA 公钥与私钥能力的完整接口。

```csharp
/// <summary>
/// 同时具备 RSA 公钥与私钥能力的完整接口
/// </summary>
public interface IRsaCipher : IRsaPrivateCipher, IRsaPublicCipher
{
}
```

无 `GenerateKeyPair` 方法。生成密钥请使用 `RSA.Create`，或从 PEM 用 `RsaUtils` 加载。

### 静态类 (Static Classes)

#### RsaDefines

RSA 相关常量占位（可扩展密钥长度、默认填充等）。当前无成员。

```csharp
/// <summary>
/// RSA 相关常量占位（可扩展密钥长度、默认填充等）。
/// </summary>
public static class RsaDefines
{
}
```

#### RsaParamUtils

PEM/DER 与 `RSAParameters` 之间的解析工具。

```csharp
/// <summary>
/// PEM/DER 与 RSAParameters 之间的解析工具。
/// </summary>
public static class RsaParamUtils
{
    /// <summary>
    /// 把Der数据解释为 X.509 公钥参数
    /// </summary>
    /// <exception cref="Exception">算法 OID 非 RSA 或 ASN.1 结构非法</exception>
    public static RSAParameters DecodeX509Params(byte[] derBytes);

    /// <summary>
    /// 把Der数据解释为 PKCS#8 私钥参数
    /// </summary>
    public static RSAParameters DecodePkcs8Params(byte[] deBytes);

    /// <summary>
    /// 把Der数据解释为 PKCS#1 v1.5 的RSA公钥参数
    /// </summary>
    public static RSAParameters DecodePkcs1V15Public(byte[] derBytes);

    /// <summary>
    /// 把Der数据解释为 PKCS#1 v1.5 的RSA私钥参数
    /// </summary>
    /// <remarks>仅支持 version 0 的双素数结构，不支持多素数 RSA</remarks>
    public static RSAParameters DecodePkcs1V15Private(byte[] derBytes);

    /// <summary>
    /// 从 PEM 文本中提取 Base64 的 DER 编码数据。
    /// </summary>
    /// <param name="keyFileText">完整 PEM 文本（含 BEGIN/END 行）</param>
    /// <param name="keyType"><see cref="PemTypes"/> 中的标签，如 RSA PRIVATE KEY</param>
    /// <exception cref="ArgumentException">PEM 头尾不匹配或格式非法</exception>
    public static byte[] ExtractDerData(string keyFileText, string keyType);
}
```

#### RsaUtils

从 PEM 文件或文本加载 RSA 密钥并创建 `IRsaPrivateCipher` / `IRsaPublicCipher`。加载失败（含空内容、IO、解析错误）返回 `null`。

```csharp
/// <summary>
/// 从 PEM 文件或文本加载 RSA 密钥并创建 IRsaPrivateCipher / IRsaPublicCipher。
/// </summary>
public static class RsaUtils
{
    public static IRsaPrivateCipher LoadPrivateCipherPkcs1V15(string keyPath);
    public static IRsaPrivateCipher LoadPrivateCipherPkcs1V15Content(string keyContent);
    public static IRsaPublicCipher LoadPublicCipherPkcs1V15(string keyPath);
    public static IRsaPublicCipher LoadPublicCipherPkcs1V15Content(string keyContent);

    public static IRsaPrivateCipher LoadPrivateCipherPkcs8(string keyPath);
    public static IRsaPrivateCipher LoadPrivateCipherPkcs8Content(string keyContent);
    public static IRsaPublicCipher LoadPublicCipherX509(string keyPath);
    public static IRsaPublicCipher LoadPublicCipherX509Content(string keyContent);

    public static RSA LoadPkcs1V15Private(string keyPath);
    public static RSA LoadPkcs1V15PrivateContent(string keyContent);
    public static RSA LoadPkcs8Private(string keyPath);
    public static RSA LoadPkcs8PrivateContent(string keyContent);
    public static RSA LoadPkcs1V15Public(string keyPath);
    public static RSA LoadPkcs1V15PublicContent(string keyContent);
    public static RSA LoadX509Public(string keyPath);
    public static RSA LoadX509PublicContent(string keyContent);
}
```

PEM 标签对应：`RSA PRIVATE KEY`、`RSA PUBLIC KEY`、`PRIVATE KEY`、`PUBLIC KEY`（见 `PemTypes`）。

### 类 (Classes)

#### RsaGroup

将长字节流按 RSA 块大小切分为多个分组，用于分段加解密。

```csharp
/// <summary>
/// 将长字节流按 RSA 块大小切分为多个分组，用于分段加解密。
/// </summary>
public sealed class RsaGroup : IDisposable
{
    /// <summary>创建空分组读取器，稍后通过 ResetData 装载数据。</summary>
    public RsaGroup();

    /// <summary>
    /// 创建分组读取器并绑定缓冲区与分组大小。
    /// </summary>
    public RsaGroup(byte[] buff, int groupSize);

    /// <summary>
    /// 重置
    /// </summary>
    public void Reset();

    /// <summary>
    /// 重新装载数据并重置读取位置。
    /// </summary>
    public void ResetData(byte[] buff, int groupSize);

    /// <summary>
    /// 是否还有下一分组
    /// </summary>
    public bool HasNext { get; }

    /// <summary>
    /// 读取下一分组
    /// </summary>
    /// <returns>下一分组字节；最后一组可能短于分组大小</returns>
    public byte[] ReadNext();

    /// <summary>释放内部缓冲区引用。</summary>
    public void Dispose();
}
```

#### RsaPrivateCipher

RSA 私钥解密与签名实现（PKCS#1 v1.5 填充）。`Dispose` 会释放内部 `RSA` 实例。

```csharp
/// <summary>
/// RSA 私钥解密与签名实现（PKCS#1 v1.5 填充）。
/// </summary>
public class RsaPrivateCipher : IRsaPrivateCipher
{
    /// <summary>单段 RSA 密文字节数（密钥长度/8）。</summary>
    public int DecryptPartLen { get; }

    /// <summary>获取私钥</summary>
    public RSA PrivateKey { get; }

    /// <summary>
    /// 使用 .NET RSA 私钥实例创建处理器。
    /// </summary>
    public RsaPrivateCipher(RSA privateKey);

    // 其余成员实现 IRsaPrivateCipher
}
```

#### RsaPublicCipher

RSA 公钥加密与签名验证实现（PKCS#1 v1.5 填充）。`Dispose` 会释放内部 `RSA` 实例。

```csharp
/// <summary>
/// RSA 公钥加解密与签名验证实现（PKCS#1 v1.5 填充）。
/// </summary>
public class RsaPublicCipher : IRsaPublicCipher
{
    /// <summary>单次 RSA 加密可容纳的最大明文字节数（密钥长度/8 - 11）。</summary>
    public int EncryptPartLen { get; }

    /// <summary>获取公钥</summary>
    public RSA PublicKey { get; }

    /// <summary>
    /// 使用 .NET RSA 公钥实例创建处理器。
    /// </summary>
    public RsaPublicCipher(RSA publicKey);

    // 其余成员实现 IRsaPublicCipher
}
```

#### RsaCipher

组合 `RsaPrivateCipher` 与 `RsaPublicCipher` 的 RSA 完整实现。`Dispose` 会依次释放二者（进而释放各自持有的 `RSA`）。若公私钥封装共享同一 `RSA` 实例，可能重复释放，建议分别导入参数。

```csharp
/// <summary>
/// 组合 RsaPrivateCipher 与 RsaPublicCipher 的 RSA 完整实现。
/// </summary>
public class RsaCipher : IRsaCipher
{
    /// <summary>
    /// 使用已有私钥与公钥封装创建实例。
    /// </summary>
    public RsaCipher(RsaPrivateCipher privateCipher, RsaPublicCipher publicCipher);

    // 其余成员实现 IRsaCipher
}
```

无无参构造函数，也无 `GenerateKeyPair`。

---

## 命名空间: JLGames.Infra.Crypto.Key

### 类 (Classes)

#### DhKeyPair

Diffie-Hellman 密钥对（私钥与公钥大整数）。

```csharp
/// <summary>
/// Diffie-Hellman 密钥对（私钥与公钥大整数）。
/// </summary>
public class DhKeyPair
{
    /// <summary>私钥 x</summary>
    public BigInteger Private { get; set; }

    /// <summary>公钥 g^x mod p</summary>
    public BigInteger Public { get; set; }
}
```

### 静态类 (Static Classes)

#### DiffieHellman

RFC 3526 Group 14（2048 位 MODP）Diffie-Hellman 密钥交换。生成元 g = 2。

```csharp
/// <summary>
/// RFC 3526 Group 14（2048 位 MODP）Diffie-Hellman 密钥交换。
/// </summary>
public static class DiffieHellman
{
    /// <summary>
    /// 生成 DH 密钥对（私钥 x 与公钥 g^x mod p）。
    /// </summary>
    public static DhKeyPair GenerateDhKeyPair();

    /// <summary>
    /// 计算共享密钥 K = theirPublic^myPrivate mod p。
    /// </summary>
    /// <returns>共享大整数（通常需再经 KDF 派生为对称密钥）</returns>
    public static BigInteger ComputeDhSharedK(BigInteger theirPublic, BigInteger myPrivate);
}
```

#### KeyDerivation

口令/共享材料到对称密钥的派生（SHA-256 与 PBKDF2）。默认 PBKDF2：内置盐 `"JLGames.Infra.Crypto.Key"`（UTF-8）、迭代 100000、输出 32 字节。PBKDF2 使用 HMAC-SHA1（.NET Standard 2.0 的 `Rfc2898DeriveBytes` 不支持指定哈希算法）。无盐 SHA-256 仅适合非生产场景。

```csharp
/// <summary>
/// 口令/共享材料到对称密钥的派生（SHA-256 与 PBKDF2）。
/// </summary>
public static class KeyDerivation
{
    /// <summary>
    /// 将 UTF-8 口令经 SHA-256 哈希为 32 字节密钥（无盐，仅适合非生产场景）。
    /// </summary>
    public static byte[] SharedKeySha256Str(string passphrase);

    /// <summary>
    /// 将口令字节经 SHA-256 哈希为 32 字节密钥（无盐）。
    /// </summary>
    public static byte[] SharedKeySha256(byte[] passphrase);

    /// <summary>
    /// 使用内置盐、迭代次数（100000）与 32 字节长度，从字符串派生 PBKDF2 密钥（推荐生产使用）。
    /// </summary>
    public static byte[] DeriveKeyPbkdf2StrDefault(string passphrase);

    /// <summary>
    /// 使用内置盐、迭代次数与 32 字节长度，从字节口令派生 PBKDF2 密钥。
    /// </summary>
    public static byte[] DeriveKeyPbkdf2Default(byte[] passphrase);

    /// <summary>
    /// 使用 PBKDF2（HMAC-SHA1）从字符串口令、盐与迭代次数派生密钥。
    /// </summary>
    public static byte[] DeriveKeyPbkdf2Str(string passphrase, byte[] salt, int iterations, int keyLen);

    /// <summary>
    /// 使用 PBKDF2（HMAC-SHA1）从字节口令、盐与迭代次数派生密钥。
    /// </summary>
    public static byte[] DeriveKeyPbkdf2(byte[] passphrase, byte[] salt, int iterations, int keyLen);
}
```

---

## 命名空间: JLGames.Infra.Crypto.ASN1

用于解析 RSA PEM/DER。`DerTags` 为 DER/BER 实际编码中的 Universal Tag（含构造位，如 SEQUENCE 为 `0x30`）；`Asn1Tags` 为 ASN.1 标准逻辑 Tag 编号（SEQUENCE 为 `0x10`）。

### 静态类 (Static Classes)

#### DerTags

```csharp
/// <summary>
/// DER/BER 实际编码中的 Universal Tag 常量（含构造/上下文位，如 SEQUENCE 为 0x30）。
/// 与 ASN.1 标准中的逻辑 Tag 编号可能不同，见 Asn1Tags。
/// </summary>
public static class DerTags
{
    public const byte BOOLEAN = 0x01;
    public const byte INTEGER = 0x02;
    public const byte BIT_STRING = 0x03;
    public const byte OCTET_STRING = 0x04;
    public const byte NULL = 0x05;
    public const byte OBJECT_IDENTIFIER = 0x06;
    public const byte ObjectDescriptor = 0x07;
    public const byte EXTERNAL = 0x08;
    public const byte REAL = 0x09;
    public const byte ENUMERATED = 0x0A;
    public const byte EMBEDDED_PDV = 0x0B;
    public const byte UTF8String = 0x0C;
    public const byte RELATIVE_OID = 0x0D;
    public const byte TIME = 0x0E;
    public const byte SEQUENCE = 0x30;
    public const byte SEQUENCE_OF = 0x30;
    public const byte SET = 0x31;
    public const byte SET_OF = 0x31;
    public const byte NumericString = 0x12;
    public const byte PrintableString = 0x13;
    public const byte TeletexString = 0x14;
    public const byte VideotexString = 0x15;
    public const byte IA5String = 0x16;
    public const byte UTCTime = 0x17;
    public const byte GeneralizedTime = 0x18;
    public const byte GraphicString = 0x19;
    public const byte VisibleString = 0x1A;
    public const byte GeneralString = 0x1B;
    public const byte UniversalString = 0x1C;
    public const byte CHARACTER_STRING = 0x1D;
    public const byte BMPString = 0x1E;
}
```

#### Asn1Tags

```csharp
/// <summary>
/// ASN.1 标准定义中的 Universal Tag 编号（未含 DER 构造位）。
/// </summary>
public static class Asn1Tags
{
    public const byte BOOLEAN = 0x01;
    public const byte INTEGER = 0x02;
    public const byte BIT_STRING = 0x03;
    public const byte OCTET_STRING = 0x04;
    public const byte NULL = 0x05;
    public const byte OBJECT_IDENTIFIER = 0x06;
    public const byte ObjectDescriptor = 0x07;
    public const byte EXTERNAL = 0x08;
    public const byte REAL = 0x09;
    public const byte ENUMERATED = 0x0A;
    public const byte EMBEDDED_PDV = 0x0B;
    public const byte UTF8String = 0x0C;
    public const byte RELATIVE_OID = 0x0D;
    public const byte TIME = 0x0E;
    public const byte SEQUENCE = 0x10;
    public const byte SEQUENCE_OF = 0x10;
    public const byte SET = 0x11;
    public const byte SET_OF = 0x11;
    public const byte NumericString = 0x12;
    public const byte PrintableString = 0x13;
    public const byte TeletexString = 0x14;
    public const byte T61String = 0x14;
    public const byte VideotexString = 0x15;
    public const byte IA5String = 0x16;
    public const byte UTCTime = 0x17;
    public const byte GeneralizedTime = 0x18;
    public const byte GraphicString = 0x19;
    public const byte VisibleString = 0x1A;
    public const byte GeneralString = 0x1B;
    public const byte UniversalString = 0x1C;
    public const byte CHARACTER_STRING = 0x1D;
    public const byte BMPString = 0x1E;
    public const byte DATE = 0x1F;
    public const byte TIME_OF_DAY = 0x20;
    public const byte DATE_TIME = 0x21;
    public const byte DURATION = 0x22;
}
```

### 结构体 (Structs)

#### TLVBlock

ASN.1 TLV 数据块（Tag-Length-Value）。字段均为公开字段。

```csharp
/// <summary>
/// ASN.1 TLV 数据块（Tag-Length-Value）。
/// </summary>
public struct TLVBlock
{
    /// <summary>类型标签（Tag）</summary>
    public byte Tag;

    /// <summary>长度字段的原始 DER 编码</summary>
    public byte[] Length;

    /// <summary>解析后的 Value 字节长度</summary>
    public int LengthValue;

    /// <summary>值字段；未读取 Value 时为 null</summary>
    public byte[] Value;

    /// <summary>
    /// 返回便于调试的十六进制字符串表示。
    /// </summary>
    public override string ToString();
}
```

### 类 (Classes)

#### TLVReader

ASN.1 DER/BER 的 TLV（Tag-Length-Value）流式读取器。实现 `IDisposable`，释放底层 `BinaryReader`。

```csharp
/// <summary>
/// ASN.1 DER/BER 的 TLV（Tag-Length-Value）流式读取器。
/// </summary>
public class TLVReader : IDisposable
{
    /// <summary>
    /// 基于已有 BinaryReader 创建读取器。
    /// </summary>
    public TLVReader(BinaryReader reader);

    /// <summary>
    /// 基于字节数组创建读取器。
    /// </summary>
    public TLVReader(byte[] data);

    public void Dispose();

    /// <summary>
    /// 跳过指定字节数（相对当前流位置向前移动）。
    /// </summary>
    public void SkipLength(int length);

    /// <summary>
    /// 读取 ASN.1 数据块
    /// </summary>
    public TLVBlock ReadBlock();

    /// <summary>
    /// 验证标记后读取 ASN.1 数据块
    /// </summary>
    /// <exception cref="ArgumentException">Tag 与期望值不符</exception>
    public TLVBlock ReadBlock(byte expectedTag);

    /// <summary>
    /// 读取 ASN.1 数据块（不包含 Value 部分）
    /// </summary>
    public TLVBlock ReadBlockNoValue();

    /// <summary>
    /// 验证标记后读取 ASN.1 数据块（不包含 Value 部分）
    /// </summary>
    public TLVBlock ReadBlockNoValue(byte expectedTag);

    /// <summary>
    /// 读取 ASN.1 标记（Tag）
    /// </summary>
    /// <exception cref="EndOfStreamException">数据意外结束</exception>
    public byte ReadTag();

    /// <summary>
    /// 读取 ASN.1 标记（Tag）并验证
    /// </summary>
    public byte ReadTag(byte expectedTag);

    /// <summary>
    /// 读取 ASN.1 数据长度（Length）
    /// 短格式: 第一个字节记录长度（小于 128）
    /// 长格式: 第一个字节记录后续长度字节数 n，余下 n 字节为大端长度
    /// </summary>
    /// <param name="lengthValue">解析出的 Value 字节长度</param>
    /// <returns>Length 字段的原始编码字节</returns>
    public byte[] ReadLength(out int lengthValue);

    /// <summary>
    /// 读取 ASN.1 数据（Value）
    /// </summary>
    public byte[] ReadValue(int length);

    /// <summary>
    /// 读取一个字节
    /// </summary>
    public byte ReadByte();

    /// <summary>
    /// 是否还有数据未读取
    /// </summary>
    public bool HasData();
}
```

`ReadBlock(expectedTag)` 在长度为 0 时 Value 为 `null`。

#### TLVAdvancedReader

针对常见 ASN.1 类型的 TLV 高级读取器（布尔、整数、OID 等）。

```csharp
/// <summary>
/// 针对常见 ASN.1 类型的 TLV 高级读取器（布尔、整数、OID 等）。
/// </summary>
public class TLVAdvancedReader : TLVReader
{
    public TLVAdvancedReader(BinaryReader reader);
    public TLVAdvancedReader(byte[] data);

    /// <summary>
    /// 验证标记后读取一个布尔数据块
    /// </summary>
    public byte[] ReadBoolean();

    /// <summary>
    /// 验证标记后读取一个 NULL 数据块
    /// </summary>
    public TLVBlock ReadNull();

    /// <summary>
    /// 验证标记后读取一个整数数据块；若有无意义前导 0x00 则移除
    /// </summary>
    public byte[] ReadInteger();

    /// <summary>
    /// 验证标记后读取一个 SEQUENCE 数据块。
    /// </summary>
    /// <param name="includeValue">为 true 时读取 Value，否则仅 Tag/Length</param>
    public TLVBlock ReadSequence(bool includeValue);

    /// <summary>
    /// 读取算法标识符 AlgorithmIdentifier，并返回 RSA 算法 OID 数据块
    /// </summary>
    public TLVBlock ReadRsaOid();

    /// <summary>
    /// 读取 OCTET STRING 数据块（不含前导比特数）
    /// </summary>
    public TLVBlock ReadOctetString(bool includeValue);

    /// <summary>
    /// 读取 BIT STRING 数据块；Value[0] 为前导比特数，返回值已去除该字节并裁剪无效位
    /// </summary>
    public TLVBlock ReadBitString();

    /// <summary>
    /// 读取 OBJECT IDENTIFIER 数据块。
    /// </summary>
    public TLVBlock ReadObjectIdentifier(bool includeValue);
}
```

---

## 已弃用 (Deprecated)

以下类型位于 `JLGames.Infra.Crypto.Deleted/`。SDK 风格工程会包含这些 `.cs` 文件，但**当前文件内容全部被注释**，类型不会进入程序集，也不是推荐 API。请改用 `DesCipher` / `AesCipher` / `RsaCipher`（及 `RsaUtils`）。不要把它们当作 Utils 文档的一部分。

### DESUtil（已弃用）

历史 DES 字符串加解密工具（默认 8 字节密钥与固定 IV，CBC）。失败时返回源串。请改用 `DesCipher`（新代码更推荐 `AesCipher`）。

```csharp
// 命名空间: JLGames.Infra.Crypto（历史）
public static class DESUtil
{
    public static string Encrypted(string encryptString);
    public static string Encrypted(string encryptString, string encryptKey, string encryptIV);
    /// <summary>DES 加密字符串。密钥要求 8 位。成功返回 Base64，失败返回源串。</summary>
    public static string Encrypted(string encryptString, byte[] encryptKey, byte[] encryptIV);

    public static string Decrypted(string decryptString);
    public static string Decrypted(string decryptString, string encryptKey, string encryptIV);
    /// <summary>DES 解密字符串。失败返回源串。</summary>
    public static string Decrypted(string decryptString, byte[] decryptKey, byte[] decryptIV);
}
```

### RijndaelUtil（已弃用）

历史 Rijndael 字符串加解密工具。无参重载使用每次进程内不同的默认密钥/IV。请改用 `AesCipher`。

```csharp
// 命名空间: JLGames.Infra.Crypto（历史）
public static class RijndaelUtil
{
    public static byte[] Encrypted(string plainText);
    public static byte[] Encrypted(string plainText, string key, string iv);
    public static byte[] Encrypted(string plainText, byte[] key, byte[] iv);

    public static string Decrypted(byte[] cipherText);
    public static string Decrypted(byte[] cipherText, string key, string iv);
    public static string Decrypted(byte[] cipherText, byte[] key, byte[] iv);
}
```

### RSAUtil（已弃用）

历史 RSA 字符串加解密工具，依赖固定密钥容器名 `"oa_erp_dowork"`。效率不高。请改用 `RsaCipher` / `RsaUtils`。

```csharp
// 命名空间: JLGames.Infra.Crypto（历史）
public static class RSAUtil
{
    public static string Encrypted(string express);
    public static string Decrypted(string ciphertext);
}
```

---

## 功能说明

### 加密模式详解

**ECB (Electronic Codebook)**  
每个分组独立加密，同一明文块总是生成相同密文块，容易泄漏结构。**不推荐用于实际应用**。AES 现行实现不支持；DES 支持。

**CBC (Cipher Block Chaining)**  
每个块与上一密文块异或后再加密，首块需要 IV。AES 无 IV 重载返回 `IV + 密文`；带 IV 重载只返回密文。DES 的 CBC 输出均前缀 IV。

**CFB / OFB**  
枚举中有定义；AES / DES 现行 `EncryptMode` / `DecryptMode` 均不支持。

**CTR (Counter)**  
通过递增计数器生成密钥流，加解密同一运算，可并行。AES 使用 16 字节 IV。

**GCM (Galois/Counter Mode)**  
认证加密。`AesCipher` 默认模式。无 nonce 重载返回 `nonce(12) + 密文 + 标签(16)`；带 nonce 重载返回 `密文 + 标签`。仅 AES 支持。

### AES

1. 对称加密；密钥 16 / 24 / 32 字节  
2. 默认 GCM  
3. CBC 可通过 `SetPadding` 设置填充  

### RSA

1. 公钥加密、私钥解密；PKCS#1 v1.5  
2. 超长明文自动分组或走 AES-CTR 混合加密  
3. 默认 SHA-256 签名；`SignHash` 注释标明 MD5 / SHA-1 不可用  
4. 从 PEM 加载请用 `RsaUtils`，不要使用已弃用的 `RSAUtil`

---

## 使用示例

#### AES（GCM 默认）

```csharp
byte[] key = new byte[AesDefines.DefaultKeyLength];
RandomNumberGenerator.Fill(key);

var aes = new AesCipher(key);
byte[] plaintext = Encoding.UTF8.GetBytes("Hello, World!");

byte[] encrypted = aes.Encrypt(plaintext);          // 默认 GCM：nonce + 密文 + tag
byte[] decrypted = aes.Decrypt(encrypted);
string result = Encoding.UTF8.GetString(decrypted);

byte[] nonce = new byte[12];
RandomNumberGenerator.Fill(nonce);
byte[] body = aes.EncryptGcm(plaintext, nonce);     // 密文 + tag
byte[] roundtrip = aes.DecryptGcm(body, nonce);
```

#### AES CBC / CTR / 通用模式

```csharp
var aes = new AesCipher(key);
aes.SetPadding(PaddingMode.PKCS7);

byte[] iv = new byte[AesDefines.BlockSize];
RandomNumberGenerator.Fill(iv);

byte[] packed = aes.EncryptCbc(plaintext);          // IV + 密文
byte[] plain1 = aes.DecryptCbc(packed);

byte[] cbcBody = aes.EncryptCbc(plaintext, iv);     // 仅密文
byte[] plain2 = aes.DecryptCbc(cbcBody, iv);

byte[] ctrPacked = aes.EncryptCtr(plaintext);
byte[] gcmPacked = aes.EncryptGcm(plaintext);
byte[] viaMode = aes.EncryptMode(plaintext, BlockMode.CBC);
```

#### DES / 3DES / XOR

```csharp
byte[] desKey = new byte[DesDefines.KeySize];
RandomNumberGenerator.Fill(desKey);
var des = new DesCipher(desKey);
byte[] desCt = des.Encrypt(plaintext);              // 默认 CBC，前缀 IV
byte[] desPt = des.Decrypt(desCt);

byte[] tdesKey = new byte[DesDefines.TripleKeySize];
RandomNumberGenerator.Fill(tdesKey);
var tdes = new TripleDesCipher(tdesKey);

var xor = new XorCipher(Encoding.UTF8.GetBytes("obf-key"));
byte[] obfuscated = xor.Encrypt(plaintext);
byte[] restored = xor.Decrypt(obfuscated);
```

#### RSA 加解密与签名

```csharp
using (var rsa = RSA.Create(2048))
{
    var rsaPub = RSA.Create();
    rsaPub.ImportParameters(rsa.ExportParameters(false));

    using (var rsaCipher = new RsaCipher(
        new RsaPrivateCipher(rsa),
        new RsaPublicCipher(rsaPub)))
    {
        byte[] encrypted = rsaCipher.Encrypt(plaintext);
        byte[] decrypted = rsaCipher.Decrypt(encrypted);

        byte[] hybrid = rsaCipher.EncryptHybrid(plaintext);
        byte[] hybridPt = rsaCipher.DecryptHybrid(hybrid);

        byte[] signature = rsaCipher.Sign(plaintext);
        bool ok = rsaCipher.VerifySign(plaintext, signature);

        string b64 = rsaCipher.SignBase64(plaintext);
        bool okB64 = rsaCipher.VerifySignBase64(plaintext, b64);
    }
}
```

#### 从 PEM 加载 RSA

```csharp
IRsaPrivateCipher priv = RsaUtils.LoadPrivateCipherPkcs8("private.pem");
IRsaPublicCipher pub = RsaUtils.LoadPublicCipherX509("public.pem");
if (priv == null || pub == null)
    throw new InvalidOperationException("failed to load PEM");

byte[] encrypted = pub.Encrypt(plaintext);
byte[] decrypted = priv.Decrypt(encrypted);
```

#### Diffie-Hellman 与密钥派生

```csharp
DhKeyPair alice = DiffieHellman.GenerateDhKeyPair();
DhKeyPair bob = DiffieHellman.GenerateDhKeyPair();
BigInteger sharedA = DiffieHellman.ComputeDhSharedK(bob.Public, alice.Private);
BigInteger sharedB = DiffieHellman.ComputeDhSharedK(alice.Public, bob.Private);

byte[] aesKey = KeyDerivation.DeriveKeyPbkdf2StrDefault("user-passphrase");
byte[] shaKey = KeyDerivation.SharedKeySha256Str("dev-only");
```

#### 哈希与填充

```csharp
string md5 = HashHelper.Md5String("hello");
string sha1 = HashHelper.Sha1(plaintext);

byte[] padded = PaddingUtils.Pkcs7Padding(plaintext, AesDefines.BlockSize);
byte[] unpadded = PaddingUtils.Pkcs7UnPadding(padded);

string desc = BlockModeHelper.GetDescription(BlockMode.GCM);
```

---

## 安全建议

1. **避免 ECB**：容易泄漏数据模式  
2. **使用随机 IV/nonce**：CBC / CTR / GCM 均需要；优先 `RandomNumberGenerator`  
3. **选择模式**：文件加密可用 CBC；流式可用 CTR；需认证完整性用 GCM（默认）  
4. **密钥管理**：妥善保管并轮换；口令派生优先 `KeyDerivation.DeriveKeyPbkdf2*`  
5. **算法选择**：优先 AES-256 与 RSA-2048+；避免 DES / XOR 作为安全加密；MD5 / SHA-1 仅作校验  
6. **RSA 签名**：使用 SHA-256；不要使用已弃用的 `RSAUtil` 固定密钥容器  

## 性能考虑

1. **AES**：现代 CPU 常有硬件加速；CBC 走 `Aes.Create()`  
2. **RSA**：慢于对称算法，适合短数据或混合加密中的密钥封装  
3. **CTR**：可并行；AES-CTR 由 `AesCtrEngine` 实现  
4. **GCM**：托管 GHASH，大数据量成本高于硬件 GCM  
5. **DH**：2048-bit MODP，共享密钥需再经 KDF 再作对称密钥  
