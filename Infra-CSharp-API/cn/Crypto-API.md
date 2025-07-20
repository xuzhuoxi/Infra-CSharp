# Crypto API 文档

## 命名空间: JLGames.Infra.Crypto

### 接口 (Interfaces)

#### IEncryptCipher
加密处理器接口

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
解密处理器接口

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
加密解密处理器接口，继承了加密和解密接口

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
加密模式枚举

```csharp
/// <summary>
/// 加密模式枚举
/// </summary>
public enum BlockMode
{
    /// <summary>
    /// 电子密码本模式（不安全）
    /// 每个分组独立加密，没有上下文。同一明文块总是会生成相同密文块，容易泄漏结构。
    /// 不推荐用于实际应用。
    /// </summary>
    ECB,

    /// <summary>
    /// 加密分组链接模式
    /// 每个块与上一个密文块异或后再加密，首个块需要一个随机 IV。
    /// 常用于文件加密，安全性较好，但加密不可并行。
    /// </summary>
    CBC,

    /// <summary>
    /// 加密反馈模式（流式加密）
    /// 用前一密文作为输入流来生成密钥流，适合字节数据传输场景。
    /// 一位错误会影响当前和下一个块。
    /// </summary>
    CFB,

    /// <summary>
    /// 输出反馈模式（预计算密钥流）
    /// 类似 CFB，但只用加密输出而非密文。对 IV 敏感。
    /// 错误不会扩散到其他块。
    /// </summary>
    OFB,

    /// <summary>
    /// 计数器模式（高性能）
    /// 通过递增的"计数器"加密生成密钥流，支持并行加解密。
    /// 适用于高性能通信流加密。
    /// </summary>
    CTR,

    /// <summary>
    /// Galois/Counter 模式（高安全性）
    /// 支持认证加密，结合加密和验证，适用于高安全需求场景。
    /// 常用于 TLS、VPN、HTTPS 等。
    /// </summary>
    GCM
}
```

### 静态类 (Static Classes)

#### BlockModeHelper
加密模式的辅助工具类

```csharp
/// <summary>
/// 加密模式的辅助工具类
/// </summary>
public static class BlockModeHelper
{
    /// <summary>
    /// 获取加密模式的描述
    /// </summary>
    /// <param name="mode">加密模式枚举值</param>
    /// <returns>描述信息</returns>
    public static string GetDescription(BlockMode mode);
}
```

## 命名空间: JLGames.Infra.Crypto.Symmetric

### 接口 (Interfaces)

#### IAesCipher
AES加密接口

```csharp
/// <summary>
/// IAESCipher 接口
/// AES：Advanced Encryption Standard（高级加密标准），对应算法 Rijndael
/// 特点：
/// 1. 对称加密
/// 2. 一个密钥扩展成多个子密钥，多轮加密
/// </summary>
public interface IAesCipher : ICipher
{
    /// <summary>
    /// 获取密钥（只读）
    /// </summary>
    byte[] Key { get; }

    /// <summary>
    /// BlockSize
    /// </summary>
    int BlockSize { get; }

    /// <summary>
    /// 设置填充模式
    /// </summary>
    /// <param name="paddingMode">填充模式</param>
    void SetPadding(PaddingMode paddingMode);

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
    /// CBC 模式加密
    /// </summary>
    byte[] EncryptCbc(byte[] plaintext);

    /// <summary>
    /// CBC 模式加密
    /// </summary>
    byte[] EncryptCbc(byte[] plaintext, byte[] iv);

    /// <summary>
    /// CBC 模式解密
    /// </summary>
    byte[] DecryptCbc(byte[] ciphertext);

    /// <summary>
    /// CBC 模式解密
    /// </summary>
    byte[] DecryptCbc(byte[] ciphertext, byte[] iv);

    /// <summary>
    /// CTR 模式加密
    /// </summary>
    byte[] EncryptCtr(byte[] plaintext);

    /// <summary>
    /// CTR 模式加密
    /// </summary>
    byte[] EncryptCtr(byte[] plaintext, byte[] iv);

    /// <summary>
    /// CTR 模式解密
    /// </summary>
    byte[] DecryptCtr(byte[] ciphertext);

    /// <summary>
    /// CTR 模式解密
    /// </summary>
    byte[] DecryptCtr(byte[] ciphertext, byte[] iv);

    /// <summary>
    /// GCM 模式加密
    /// </summary>
    byte[] EncryptGcm(byte[] plaintext);

    /// <summary>
    /// GCM 模式加密
    /// </summary>
    byte[] EncryptGcm(byte[] plaintext, byte[] nonce);

    /// <summary>
    /// GCM 模式解密
    /// </summary>
    byte[] DecryptGcm(byte[] ciphertext);

    /// <summary>
    /// GCM 模式解密
    /// </summary>
    byte[] DecryptGcm(byte[] ciphertext, byte[] nonce);
}
```

#### IDesCipher
DES加密接口

```csharp
/// <summary>
/// DES 加密接口
/// DES：Data Encryption Standard（数据加密标准）
/// 特点：
/// 1. 对称加密
/// 2. 64位分组，56位密钥
/// 3. 已被认为不够安全，不推荐使用
/// </summary>
public interface IDesCipher : ICipher
{
    // 具体实现需要进一步分析文件内容
}
```

### 类 (Classes)

#### AesCipher
AES加密实现类

```csharp
/// <summary>
/// AES 加密实现类
/// 支持多种加密模式和填充方式
/// </summary>
public class AesCipher : IAesCipher
{
    // 具体实现需要进一步分析文件内容
}
```

#### DesCipher
DES加密实现类

```csharp
/// <summary>
/// DES 加密实现类
/// 注意：DES已被认为不够安全，建议使用AES
/// </summary>
public class DesCipher : IDesCipher
{
    // 具体实现需要进一步分析文件内容
}
```

## 命名空间: JLGames.Infra.Crypto.Asymmetric

### 接口 (Interfaces)

#### IRsaCipher
RSA加密接口

```csharp
/// <summary>
/// RSA 加密接口
/// RSA：Rivest-Shamir-Adleman，非对称加密算法
/// 特点：
/// 1. 非对称加密
/// 2. 公钥加密，私钥解密
/// 3. 安全性基于大数分解困难性
/// </summary>
public interface IRsaCipher : ICipher
{
    // 具体实现需要进一步分析文件内容
}
```

### 类 (Classes)

#### RsaCipher
RSA加密实现类

```csharp
/// <summary>
/// RSA 加密实现类
/// 支持公钥加密、私钥解密
/// </summary>
public class RsaCipher : IRsaCipher
{
    // 具体实现需要进一步分析文件内容
}
```

### 功能说明

#### 加密模式详解

**ECB (Electronic Codebook)**
- 电子密码本模式，每个分组独立加密
- 同一明文块总是生成相同密文块
- 容易泄漏数据结构和模式
- **不推荐用于实际应用**

**CBC (Cipher Block Chaining)**
- 加密分组链接模式
- 每个块与上一个密文块异或后再加密
- 首个块需要随机初始化向量(IV)
- 安全性较好，解密可并行，但加密不可并行
- **常用于文件加密**

**CFB (Cipher Feedback)**
- 加密反馈模式，流式加密
- 用前一密文作为输入流生成密钥流
- 适合字节数据传输场景
- 一位错误会影响当前和下一个块

**OFB (Output Feedback)**
- 输出反馈模式，预计算密钥流
- 类似CFB，但只用加密输出而非密文
- 错误不会扩散到其他块
- 对IV非常敏感

**CTR (Counter)**
- 计数器模式，高性能
- 通过递增计数器加密生成密钥流
- 支持并行加解密
- **适用于高性能通信流加密**

**GCM (Galois/Counter Mode)**
- Galois/Counter模式，高安全性
- 支持认证加密，结合加密和验证
- **常用于TLS、VPN、HTTPS等高安全需求场景**

#### AES加密特点

1. **对称加密**：使用相同密钥进行加密和解密
2. **密钥扩展**：一个密钥扩展成多个子密钥
3. **多轮加密**：通过多轮变换提高安全性
4. **标准算法**：被广泛采用为高级加密标准

#### RSA加密特点

1. **非对称加密**：公钥加密，私钥解密
2. **数学基础**：基于大数分解困难性
3. **密钥管理**：公钥可公开，私钥需保密
4. **应用场景**：数字签名、密钥交换等

### 使用示例

#### AES加密示例
```csharp
// 创建AES加密器
var aesCipher = new AesCipher();

// CBC模式加密
byte[] plaintext = Encoding.UTF8.GetBytes("Hello, World!");
byte[] encrypted = aesCipher.EncryptCbc(plaintext);

// CBC模式解密
byte[] decrypted = aesCipher.DecryptCbc(encrypted);
string result = Encoding.UTF8.GetString(decrypted);

// 使用自定义IV
byte[] iv = new byte[16]; // AES块大小
RandomNumberGenerator.Fill(iv);
byte[] encryptedWithIV = aesCipher.EncryptCbc(plaintext, iv);
```

#### 不同加密模式
```csharp
var aesCipher = new AesCipher();

// CTR模式（高性能）
byte[] ctrEncrypted = aesCipher.EncryptCtr(plaintext);

// GCM模式（高安全性）
byte[] gcmEncrypted = aesCipher.EncryptGcm(plaintext);

// 通用模式方法
byte[] cbcEncrypted = aesCipher.EncryptMode(plaintext, BlockMode.CBC);
byte[] ctrEncrypted2 = aesCipher.EncryptMode(plaintext, BlockMode.CTR);
```

#### RSA加密示例
```csharp
// 创建RSA加密器
var rsaCipher = new RsaCipher();

// 生成密钥对
rsaCipher.GenerateKeyPair();

// 使用公钥加密
byte[] encrypted = rsaCipher.Encrypt(plaintext);

// 使用私钥解密
byte[] decrypted = rsaCipher.Decrypt(encrypted);
```

#### 加密模式信息
```csharp
// 获取加密模式描述
string cbcDesc = BlockModeHelper.GetDescription(BlockMode.CBC);
string gcmDesc = BlockModeHelper.GetDescription(BlockMode.GCM);

Console.WriteLine($"CBC模式: {cbcDesc}");
Console.WriteLine($"GCM模式: {gcmDesc}");
```

### 安全建议

1. **避免使用ECB模式**：容易泄漏数据模式
2. **使用随机IV**：CBC模式必须使用随机初始化向量
3. **选择合适模式**：
   - 文件加密：CBC
   - 流加密：CTR
   - 高安全需求：GCM
4. **密钥管理**：妥善保管密钥，定期更换
5. **算法选择**：优先使用AES，避免使用DES

### 性能考虑

1. **AES性能**：现代CPU通常有AES硬件加速
2. **RSA性能**：比对称加密慢，适合小数据量
3. **并行处理**：CTR模式支持并行加解密
4. **内存使用**：不同模式的内存需求不同 