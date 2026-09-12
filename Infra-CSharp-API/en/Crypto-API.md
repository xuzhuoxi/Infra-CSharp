# Crypto API Documentation

## Overview

The Crypto module provides symmetric ciphers (AES / DES / 3DES / XOR), asymmetric RSA, key exchange and derivation (Diffie-Hellman, PBKDF2), hashing, PEM/DER parsing, and ASN.1 TLV reading. Prefer `AesCipher`, `RsaCipher` / `RsaUtils`, and related current types. Historical helpers under `Deleted/` are listed in the **Deprecated** section at the end.

CTR and GCM are implemented by the public engines `AesCtrEngine` and `AesGcmEngine`. `BouncyCastleAesCtrEngine` and `BouncyCastleAesGcmEngine` are fully commented out and are not usable APIs.

## Namespaces

- `JLGames.Infra.Crypto`
- `JLGames.Infra.Crypto.Symmetric`
- `JLGames.Infra.Crypto.Asymmetric`
- `JLGames.Infra.Crypto.Key`
- `JLGames.Infra.Crypto.ASN1`

---

## Namespace: JLGames.Infra.Crypto

### Interfaces

#### IEncryptCipher

Encryption processor interface.

```csharp
/// <summary>
/// Encryption cipher interface.
/// </summary>
public interface IEncryptCipher
{
    /// <summary>
    /// Encrypt data.
    /// </summary>
    /// <param name="plaintext">Plaintext to encrypt</param>
    /// <returns>Ciphertext</returns>
    /// <exception cref="Exception">Thrown when encryption fails</exception>
    byte[] Encrypt(byte[] plaintext);
}
```

#### IDecryptCipher

Decryption processor interface.

```csharp
/// <summary>
/// Decryption cipher interface.
/// </summary>
public interface IDecryptCipher
{
    /// <summary>
    /// Decrypt data.
    /// </summary>
    /// <param name="ciphertext">Ciphertext to decrypt</param>
    /// <returns>Plaintext</returns>
    /// <exception cref="Exception">Thrown when decryption fails</exception>
    byte[] Decrypt(byte[] ciphertext);
}
```

#### ICipher

Symmetric or asymmetric cipher with both encrypt and decrypt.

```csharp
/// <summary>
/// Symmetric or asymmetric cipher with both encrypt and decrypt.
/// </summary>
public interface ICipher : IEncryptCipher, IDecryptCipher
{
}
```

### Enums

#### BlockMode

Block cipher modes of operation. The AES implementation supports CBC / CTR / GCM. The DES implementation supports ECB / CBC / CTR. CFB and OFB are defined on the enum; current implementations throw when they are requested.

```csharp
/// <summary>
/// Block cipher modes of operation.
/// </summary>
public enum BlockMode
{
    /// <summary>
    /// Electronic Codebook (insecure; identical blocks yield identical ciphertext).
    /// </summary>
    ECB,

    /// <summary>
    /// Cipher Block Chaining; requires a random IV for the first block.
    /// </summary>
    CBC,

    /// <summary>
    /// Cipher Feedback (stream-like); bit errors propagate locally.
    /// </summary>
    CFB,

    /// <summary>
    /// Output Feedback; keystream from cipher output, sensitive to IV.
    /// </summary>
    OFB,

    /// <summary>
    /// Counter mode; parallelizable, high performance.
    /// </summary>
    CTR,

    /// <summary>
    /// Galois/Counter Mode; authenticated encryption (e.g. TLS).
    /// </summary>
    GCM
}
```

### Static Classes

#### BlockModeHelper

Helpers for `BlockMode`. `GetDescription` returns a Chinese description string.

```csharp
/// <summary>
/// Helpers for BlockMode.
/// </summary>
public static class BlockModeHelper
{
    /// <summary>
    /// Returns a human-readable description of the mode.
    /// </summary>
    /// <param name="mode">Encryption mode enumeration value</param>
    /// <returns>Description information</returns>
    public static string GetDescription(BlockMode mode);
}
```

Return values:

| Mode | Description (as returned) |
|------|---------------------------|
| `ECB` | 电子密码本模式：不安全，每个分组独立加密，容易泄漏结构，不推荐使用 |
| `CBC` | 加密分组链接模式：常用，安全性较好，解密可并行，但加密不可并行 |
| `CFB` | 加密反馈模式：流式加密，适合字节数据传输，一位错误会影响当前和下一个块 |
| `OFB` | 输出反馈模式：预计算密钥流，错误不会扩散，对 IV 非常敏感 |
| `CTR` | 计数器模式：高性能，支持并行加解密，常用于高性能通信流加密 |
| `GCM` | Galois/Counter 模式：高安全性，支持认证加密，常用于 TLS、VPN、HTTPS 等高安全需求场景 |
| other | 未知加密模式 |

#### CryptoUtils

Common byte-array and OID helpers for the crypto module. Methods named `Assert*` return `bool` and do not throw.

```csharp
/// <summary>
/// Common byte-array and OID helpers for the crypto module.
/// </summary>
public static class CryptoUtils
{
    /// <summary>
    /// Returns whether the bytes match the built-in RSA algorithm OID.
    /// </summary>
    public static bool AssertIsRsaOid(byte[] rsaOid);

    /// <summary>
    /// Returns whether the bytes match the built-in DSA algorithm OID.
    /// </summary>
    public static bool AssertIsDsaOid(byte[] dsaOid);

    /// <summary>
    /// Returns whether the bytes match the built-in ECDSA algorithm OID.
    /// </summary>
    public static bool AssertIsEcdsaOid(byte[] ecdsaOid);

    /// <summary>
    /// Concatenate two byte arrays.
    /// </summary>
    public static byte[] Combine(byte[] bs, byte[] bs1);

    /// <summary>
    /// Split an array into two parts.
    /// </summary>
    /// <param name="data">Source array</param>
    /// <param name="firstSize">Length of the first segment in bytes</param>
    /// <param name="first">Output: the first <paramref name="firstSize"/> bytes</param>
    /// <param name="second">Output: remaining bytes</param>
    public static void Extract(byte[] data, int firstSize, out byte[] first, out byte[] second);

    /// <summary>
    /// Concatenate multiple byte arrays.
    /// </summary>
    public static byte[] CombineMulti(byte[] bs, byte[] bs1, byte[] bs2, params byte[][] others);
}
```

Built-in OIDs (hex):

- RSA: `2A 86 48 86 F7 0D 01 01 01` (1.2.840.113549.1.1.1)
- DSA / ECDSA: `2A 86 48 CE 3D 02 01` (the two constants are identical in the current source)

#### PaddingDelegate

Delegate types for block-cipher padding and unpadding.

```csharp
/// <summary>
/// Delegate types for block-cipher padding and unpadding.
/// </summary>
public static class PaddingDelegate
{
    /// <summary>
    /// Padding function type
    /// </summary>
    public delegate byte[] FuncPadding(byte[] data, int blockSize);

    /// <summary>
    /// Unpadding function type
    /// </summary>
    public delegate byte[] FuncUnPadding(byte[] data);
}
```

#### PemTypes

PEM `-----BEGIN …-----` / `-----END …-----` label constants.

```csharp
/// <summary>
/// PEM BEGIN/END label constants.
/// </summary>
public static class PemTypes
{
    /// <summary>OpenSSH-specific private key; commonly used with OpenSSH</summary>
    public const string OpenSshPrivateKey = "OPENSSH PRIVATE KEY";

    /// <summary>SSH2 public key; commonly used with SecureCRT, Tectia, etc.</summary>
    public const string Ssh2PublicKey = "SSH2 PUBLIC KEY";

    /// <summary>PKCS#1 private key; commonly used with OpenSSL, Go</summary>
    public const string RsaPrivateKey = "RSA PRIVATE KEY";

    /// <summary>PKCS#1 public key (less common); usually exported as SubjectPublicKeyInfo</summary>
    public const string RsaPublicKey = "RSA PUBLIC KEY";

    /// <summary>PKCS#8 private key; more generic, multi-algorithm</summary>
    public const string Pkcs8PrivateKey = "PRIVATE KEY";

    /// <summary>Encrypted PKCS#8 private key; commonly used with OpenSSL, Go</summary>
    public const string EncryptedPkcs8PrivateKey = "ENCRYPTED PRIVATE KEY";

    /// <summary>X.509 public key (SubjectPublicKeyInfo); commonly used with OpenSSL, Go</summary>
    public const string X509PublicKey = "PUBLIC KEY";

    /// <summary>X.509 certificate; commonly used with SSL/TLS</summary>
    public const string Certificate = "CERTIFICATE";

    /// <summary>PKCS#10 certificate signing request (CSR); commonly used with OpenSSL, Go</summary>
    public const string CertRequest = "CERTIFICATE REQUEST";

    /// <summary>OpenSSL CSR alias</summary>
    public const string NewCertRequest = "NEW CERTIFICATE REQUEST";

    /// <summary>X.509 Certificate Revocation List</summary>
    public const string Crl = "X509 CRL";

    /// <summary>ECDSA private key (SEC1 format); commonly used with OpenSSL</summary>
    public const string EcPrivateKey = "EC PRIVATE KEY";

    /// <summary>DSA private key (uncommon)</summary>
    public const string DsaPrivateKey = "DSA PRIVATE KEY";

    /// <summary>Attribute certificate (rare)</summary>
    public const string AttributeCertificate = "ATTRIBUTE CERTIFICATE";

    /// <summary>PKCS#7 data (signed/encrypted container)</summary>
    public const string Pkcs7 = "PKCS7";

    /// <summary>Cryptographic Message Syntax (PKCS#7 successor)</summary>
    public const string Cms = "CMS";
}
```

#### Errors

Predefined error instances for the crypto module.

```csharp
/// <summary>
/// Predefined error instances for the crypto module.
/// </summary>
public static class Errors
{
    /// <summary>
    /// Unsupported PEM type.
    /// </summary>
    public static readonly Exception ErrUnsupportedPemType;
}
```

`ErrUnsupportedPemType` is `CryptoException("unsupported pem type")`.

### Classes

#### HashHelper

Convenience wrappers for common hash algorithms (hex or raw bytes). Hex output is lowercase with no separators. On file-read failure: `Md5File` / `Sha1File` / `HashFile2Hex` return an empty string; `HashFile` returns `null`.

```csharp
/// <summary>
/// Convenience wrappers for common hash algorithms (hex or raw bytes).
/// </summary>
public class HashHelper
{
    /// <summary>
    /// MD5 of a byte array as a lowercase hex string (no separators).
    /// </summary>
    public static string Md5(byte[] data);

    /// <summary>
    /// MD5 of a UTF-8 string as a lowercase hex string.
    /// </summary>
    public static string Md5String(string data);

    /// <summary>
    /// MD5 of a file; returns an empty string on read failure.
    /// </summary>
    public static string Md5File(string filePath);

    /// <summary>
    /// SHA-1 of a byte array as a lowercase hex string (no separators).
    /// </summary>
    public static string Sha1(byte[] data);

    /// <summary>
    /// SHA-1 of a UTF-8 string as a lowercase hex string.
    /// </summary>
    public static string Sha1String(string data);

    /// <summary>
    /// SHA-1 of a file; returns an empty string on read failure.
    /// </summary>
    public static string Sha1File(string filePath);

    /// <summary>
    /// Hash with the given HashAlgorithm instance.
    /// </summary>
    /// <returns>Raw hash bytes; null if the algorithm is null</returns>
    public static byte[] Hash(HashAlgorithm hashAlgorithm, byte[] data);

    /// <summary>
    /// Hash and format as a lowercase hex string.
    /// </summary>
    /// <returns>Hex digest; empty string on failure or if the algorithm is null</returns>
    public static string Hash2Hex(HashAlgorithm hashAlgorithm, byte[] data);

    /// <summary>
    /// Hash a UTF-8 string.
    /// </summary>
    public static byte[] HashString(HashAlgorithm hashAlgorithm, string data);

    /// <summary>
    /// Hash a UTF-8 string and format as a lowercase hex string.
    /// </summary>
    public static string HashString2Hex(HashAlgorithm hashAlgorithm, string data);

    /// <summary>
    /// Hash a file; returns null on read failure.
    /// </summary>
    public static byte[] HashFile(HashAlgorithm hashAlgorithm, string filePath);

    /// <summary>
    /// Hash a file and format as a lowercase hex string; empty string on read failure.
    /// </summary>
    public static string HashFile2Hex(HashAlgorithm hashAlgorithm, string filePath);
}
```

MD5 and SHA-1 are suitable for integrity checks, not as security digests.

#### PaddingUtils

Block padding and unpadding for symmetric ciphers. `blockSize` must be positive. PKCS#7 / ISO 10126 / ANSI X9.23 unpadding throws `ArgumentException` when data is empty or padding is invalid. A full extra block is added when input is already aligned.

```csharp
/// <summary>
/// Block padding and unpadding for symmetric ciphers.
/// </summary>
public class PaddingUtils
{
    /// <summary>
    /// PKCS#7 padding: append bytes whose value equals the pad length.
    /// </summary>
    public static byte[] Pkcs7Padding(byte[] data, int blockSize);

    /// <summary>
    /// Remove PKCS#7 padding.
    /// </summary>
    public static byte[] Pkcs7UnPadding(byte[] data);

    /// <summary>
    /// Zero padding to the next block boundary; adds a full block if already aligned.
    /// </summary>
    public static byte[] ZeroPadding(byte[] data, int blockSize);

    /// <summary>
    /// Remove trailing zero padding.
    /// </summary>
    public static byte[] ZeroUnPadding(byte[] data);

    /// <summary>
    /// ISO 10126 padding: random bytes plus a final length byte.
    /// </summary>
    public static byte[] Iso10126Padding(byte[] data, int blockSize);

    /// <summary>
    /// Remove ISO 10126 padding.
    /// </summary>
    public static byte[] Iso10126UnPadding(byte[] data);

    /// <summary>
    /// ANSI X9.23 padding: zeros plus a final length byte.
    /// </summary>
    public static byte[] AnsiX923Padding(byte[] data, int blockSize);

    /// <summary>
    /// Remove ANSI X9.23 padding.
    /// </summary>
    public static byte[] AnsiX923UnPadding(byte[] data);
}
```

#### CryptoException

Business exception for the crypto module.

```csharp
/// <summary>
/// Business exception for the crypto module.
/// </summary>
public class CryptoException : Exception
{
    /// <summary>
    /// Create with the specified message.
    /// </summary>
    /// <param name="message">Exception description</param>
    public CryptoException(string message);
}
```

---

## Namespace: JLGames.Infra.Crypto.Symmetric

### Interfaces

#### IAesCipher

AES cipher interface (Rijndael). Inherits `ICipher`. On `AesCipher`, `Encrypt` / `Decrypt` default to GCM.

Overloads without IV: CBC/CTR prefix a random IV; GCM prefixes a 12-byte nonce. Overloads with IV/nonce do not include that IV/nonce in the return value.

```csharp
/// <summary>
/// AES cipher interface (Rijndael).
/// </summary>
public interface IAesCipher : ICipher
{
    /// <summary>
    /// Gets a copy of the key.
    /// </summary>
    byte[] Key { get; }

    /// <summary>
    /// Block size in bytes (16 for AES).
    /// </summary>
    int BlockSize { get; }

    /// <summary>
    /// Sets PKCS#7 or other padding for CBC modes.
    /// </summary>
    void SetPadding(PaddingMode paddingMode);

    /// <summary>
    /// Encrypt with the given block mode (CBC, CTR, GCM).
    /// </summary>
    /// <returns>Ciphertext; may prefix random IV/nonce</returns>
    byte[] EncryptMode(byte[] plaintext, BlockMode blockMode);

    /// <summary>
    /// Encrypt with explicit IV/nonce and block mode.
    /// </summary>
    byte[] EncryptMode(byte[] plaintext, byte[] iv, BlockMode blockMode);

    /// <summary>
    /// Decrypt with the given block mode.
    /// </summary>
    byte[] DecryptMode(byte[] ciphertext, BlockMode blockMode);

    /// <summary>
    /// Decrypt with explicit IV/nonce and block mode.
    /// </summary>
    byte[] DecryptMode(byte[] ciphertext, byte[] iv, BlockMode blockMode);

    /// <summary>CBC encrypt; random IV prepended.</summary>
    /// <returns>IV + ciphertext</returns>
    byte[] EncryptCbc(byte[] plaintext);

    /// <summary>CBC encrypt with given IV.</summary>
    /// <returns>Ciphertext only</returns>
    byte[] EncryptCbc(byte[] plaintext, byte[] iv);

    /// <summary>CBC decrypt; input is IV + ciphertext.</summary>
    byte[] DecryptCbc(byte[] ciphertext);

    /// <summary>CBC decrypt with separate IV.</summary>
    byte[] DecryptCbc(byte[] ciphertext, byte[] iv);

    /// <summary>CTR encrypt; random IV prepended.</summary>
    /// <returns>IV + ciphertext</returns>
    byte[] EncryptCtr(byte[] plaintext);

    /// <summary>CTR encrypt with 16-byte IV.</summary>
    byte[] EncryptCtr(byte[] plaintext, byte[] iv);

    /// <summary>CTR decrypt; input is IV + ciphertext.</summary>
    byte[] DecryptCtr(byte[] ciphertext);

    /// <summary>CTR decrypt with separate IV.</summary>
    byte[] DecryptCtr(byte[] ciphertext, byte[] iv);

    /// <summary>GCM encrypt; 12-byte nonce prepended.</summary>
    /// <returns>nonce + ciphertext + tag</returns>
    byte[] EncryptGcm(byte[] plaintext);

    /// <summary>GCM encrypt with given nonce.</summary>
    /// <returns>ciphertext + authentication tag</returns>
    byte[] EncryptGcm(byte[] plaintext, byte[] nonce);

    /// <summary>GCM decrypt; input is nonce + ciphertext + tag.</summary>
    byte[] DecryptGcm(byte[] ciphertext);

    /// <summary>GCM decrypt with separate nonce.</summary>
    /// <param name="ciphertext">Ciphertext + tag</param>
    byte[] DecryptGcm(byte[] ciphertext, byte[] nonce);
}
```

`EncryptMode` / `DecryptMode` support only `CBC`, `CTR`, and `GCM`; other modes throw `InvalidOperationException("Unsupported AES block mode!")`. GCM tag failure throws `Exception("Tag verification failed. Decryption aborted.")`.

#### IDesCipher

DES/3DES cipher interface (DEA algorithm). On `DesCipher`, `Encrypt` / `Decrypt` default to CBC. Supports ECB / CBC / CTR.

```csharp
/// <summary>
/// DES/3DES cipher interface (DEA algorithm).
/// </summary>
public interface IDesCipher : ICipher
{
    /// <summary>Key length in bytes.</summary>
    int KeySize { get; }

    /// <summary>Block size in bytes (8 for DES).</summary>
    int BlockSize { get; }

    /// <summary>
    /// Set padding mode
    /// </summary>
    void SetPaddingMode(PaddingMode paddingMode);

    byte[] EncryptMode(byte[] plaintext, BlockMode blockMode);
    byte[] EncryptMode(byte[] plaintext, byte[] iv, BlockMode blockMode);
    byte[] DecryptMode(byte[] ciphertext, BlockMode blockMode);
    byte[] DecryptMode(byte[] ciphertext, byte[] iv, BlockMode blockMode);

    byte[] EncryptEcb(byte[] plaintext);
    byte[] DecryptEcb(byte[] ciphertext);

    byte[] EncryptCbc(byte[] plaintext);
    byte[] EncryptCbc(byte[] plaintext, byte[] iv);
    byte[] DecryptCbc(byte[] ciphertext);
    byte[] DecryptCbc(byte[] ciphertext, byte[] iv);

    byte[] EncryptCtr(byte[] plaintext);
    byte[] EncryptCtr(byte[] plaintext, byte[] iv);
    byte[] DecryptCtr(byte[] ciphertext);
    byte[] DecryptCtr(byte[] ciphertext, byte[] iv);
}
```

Unsupported modes throw `Exception("Unsupported DES block mode!")`. DES is considered insecure; prefer AES for new code.

#### IXorCipher

Simple repeating XOR obfuscation (not a standard cipher). Fast; not suitable for high-security use. No extra members; encrypt and decrypt are the same operation.

```csharp
/// <summary>
/// Simple repeating XOR obfuscation (not a standard cipher).
/// </summary>
public interface IXorCipher : ICipher
{
}
```

### Static Classes

#### AesDefines

AES block size and default key-length constants.

```csharp
/// <summary>
/// AES block size and default key-length constants.
/// </summary>
public static class AesDefines
{
    /// <summary>
    /// AES block size in bytes
    /// </summary>
    public const int BlockSize = 16;

    /// <summary>
    /// Default AES key length in bytes
    /// </summary>
    public const int DefaultKeyLength = 32;
}
```

#### DesDefines

DES / 3DES block size and key-length constants.

```csharp
/// <summary>
/// DES / 3DES block size and key-length constants.
/// </summary>
public static class DesDefines
{
    /// <summary>
    /// DES block size in bytes
    /// </summary>
    public const int BlockSize = 8;

    /// <summary>
    /// DES key length in bytes
    /// </summary>
    public const int KeySize = 8;

    /// <summary>
    /// 3DES key length in bytes.
    /// A 112-bit key is expanded to 168 bits.
    /// </summary>
    public const int TripleKeySize = 24;
}
```

### Classes

#### AesCipher

AES encrypt/decrypt (GCM by default); supports CBC, CTR, and GCM. Key lengths 16 / 24 / 32 bytes map to AES-128 / 192 / 256. `Key` returns a copy. CBC default padding is `PaddingMode.PKCS7`. CTR uses `AesCtrEngine.Default`; GCM uses `AesGcmEngine.Default` (16-byte tag).

IV-less CBC/CTR/GCM generate a random prefix using `System.Random`. For production, prefer `RandomNumberGenerator` and the overloads that take an explicit IV/nonce.

```csharp
/// <summary>
/// AES encrypt/decrypt (GCM by default); supports CBC, CTR, GCM.
/// </summary>
public class AesCipher : IAesCipher
{
    /// <summary>
    /// Create an AES instance with the given key (16/24/32 bytes for AES-128/192/256).
    /// </summary>
    /// <param name="key">Symmetric key</param>
    public AesCipher(byte[] key);

    // Remaining members implement IAesCipher
}
```

#### DesCipher

DES cipher class. Key must be 8 bytes (DES), or 16 or 24 bytes (internally treated as 3DES). `KeySize` is always 8; `BlockSize` is 8. Default padding is `PaddingMode.PKCS7`. CBC output always prefixes the IV, even when an IV is passed in. CTR uses no padding; encrypt and decrypt are the same operation.

```csharp
/// <summary>
/// DES cipher class
/// </summary>
public class DesCipher : IDesCipher
{
    /// <summary>
    /// Create with an 8-byte (DES) or 16/24-byte (3DES) key.
    /// </summary>
    /// <param name="key">Symmetric key</param>
    /// <exception cref="ArgumentException">Key is null, or length is not 8/16/24</exception>
    public DesCipher(byte[] key);

    // Remaining members implement IDesCipher
}
```

#### TripleDesCipher

Triple DES (3DES). Keys of 8 / 16 / 24 bytes are expanded to 24 bytes. Inherits `DesCipher`. `BlockSize` is overridden to report the 3DES key length (24 bytes), which differs from the DES block size (8 bytes).

```csharp
/// <summary>
/// Triple DES (3DES); 8/16/24-byte keys are expanded to 24 bytes.
/// </summary>
public class TripleDesCipher : DesCipher
{
    /// <summary>Reports the 3DES key length (24 bytes), unlike the DES block size (8 bytes).</summary>
    public override int BlockSize { get; }

    /// <summary>
    /// Create a 3DES instance with an 8, 16, or 24-byte key.
    /// </summary>
    /// <param name="key">Raw key</param>
    /// <exception cref="ArgumentException">Key is null, or length is not 8/16/24</exception>
    public TripleDesCipher(byte[] key);
}
```

#### XorCipher

Repeating-key XOR. Encrypt and decrypt are the same operation. A null or zero-length key returns the input unchanged.

```csharp
/// <summary>
/// Repeating-key XOR; encrypt and decrypt are the same operation.
/// </summary>
public class XorCipher : IXorCipher
{
    /// <summary>XOR key bytes</summary>
    public byte[] Key { get; }

    /// <summary>Key length; 0 means encrypt/decrypt return the input unchanged</summary>
    public int KeyLen { get; }

    /// <summary>
    /// Create with the given key.
    /// </summary>
    /// <param name="key">XOR key; may be null (treated as empty)</param>
    public XorCipher(byte[] key);

    public byte[] Encrypt(byte[] plaintext);
    public byte[] Decrypt(byte[] ciphertext);
}
```

#### AesCtrEngine

Low-level AES-CTR engine (nonce + counter form a 16-byte IV). Public type used by `AesCipher`; may also be called directly. Default counter is `{0,0,0,1}`; increment applies only to the last 4 bytes of the IV.

```csharp
/// <summary>
/// Low-level AES-CTR engine (nonce + counter form a 16-byte IV).
/// </summary>
public sealed class AesCtrEngine
{
    /// <summary>Default singleton instance.</summary>
    public static AesCtrEngine Default { get; }

    /// <summary>
    /// Encrypt with a random 12-byte nonce; output is IV + ciphertext.
    /// </summary>
    public byte[] EncryptRandom(byte[] data, byte[] key);

    /// <summary>
    /// Decrypt input prefixed with a 16-byte IV (from EncryptRandom).
    /// </summary>
    /// <exception cref="CryptoException">Input shorter than one block</exception>
    public byte[] DecryptRandom(byte[] data, byte[] key);

    /// <summary>
    /// CTR encrypt or decrypt with 12-byte nonce and default 4-byte counter (0,0,0,1).
    /// </summary>
    public byte[] ProcessWithNonce(byte[] data, byte[] key, byte[] nonce);

    /// <summary>
    /// CTR encrypt or decrypt; nonce and counter are concatenated to form the 16-byte IV.
    /// </summary>
    /// <param name="counter">Counter bytes; len(nonce)+len(counter) must be 16</param>
    public byte[] ProcessWithNonce(byte[] data, byte[] key, byte[] nonce, byte[] counter);

    /// <summary>
    /// CTR encrypt or decrypt with a full 16-byte IV (nonce || counter).
    /// </summary>
    /// <exception cref="CryptoException">IV length is not 16</exception>
    public byte[] ProcessWithIv(byte[] data, byte[] key, byte[] iv);
}
```

#### AesGcmEngine

Managed AES-GCM authenticated encryption engine. Public type. `Default` uses a 16-byte tag. The constructor may specify tag size 12–16; invalid sizes throw `ArgumentException("Invalid authentication tag size")`. The current `Encrypt` implementation always emits a 16-byte tag.

```csharp
/// <summary>
/// Managed AES-GCM authenticated encryption engine.
/// </summary>
public sealed class AesGcmEngine
{
    /// <summary>Default singleton (16-byte tag).</summary>
    public static AesGcmEngine Default { get; }

    /// <summary>Authentication tag length in bytes; valid range 12–16.</summary>
    public int TagSize { get; }

    /// <summary>Create with the default 16-byte authentication tag.</summary>
    public AesGcmEngine();

    /// <summary>
    /// Create with the specified authentication tag length.
    /// </summary>
    /// <param name="authenticationTagSize">Tag length (12–16 bytes)</param>
    public AesGcmEngine(int authenticationTagSize);

    /// <summary>
    /// Authenticated encryption.
    /// </summary>
    /// <param name="ciphertext">Output ciphertext (same length as plaintext)</param>
    /// <param name="tag">Authentication tag</param>
    public void Encrypt(byte[] plaintext, byte[] key, byte[] nonce, out byte[] ciphertext, out byte[] tag);

    /// <summary>
    /// Verify tag and decrypt.
    /// </summary>
    /// <returns>true if the tag is valid and decryption succeeds</returns>
    public bool Decrypt(byte[] ciphertext, byte[] key, byte[] nonce, byte[] tag, out byte[] plaintext);
}
```

`BouncyCastleAesCtrEngine` / `BouncyCastleAesGcmEngine` are fully commented out and are not compiled public APIs.

---

## Namespace: JLGames.Infra.Crypto.Asymmetric

### Interfaces

#### IRsaPublicCipher

RSA public-key operations: encrypt, hybrid encrypt, verify (PKCS#1 v1.5). Implements `IDisposable`.

```csharp
/// <summary>
/// RSA public-key operations: encrypt, hybrid encrypt, verify (PKCS#1 v1.5).
/// </summary>
public interface IRsaPublicCipher : IDisposable
{
    /// <summary>RSA public key instance.</summary>
    RSA PublicKey { get; }

    /// <summary>
    /// Encrypt (supports grouping / chunking).
    /// </summary>
    byte[] Encrypt(byte[] plaintext);

    /// <summary>
    /// Hybrid encrypt.
    /// Steps:
    /// 1. If shorter than one RSA block, encrypt with RSA only (no AES key).
    /// 2. If longer:
    ///   2.1 Generate a 32-byte random AES key and 16-byte random IV
    ///   2.2 RSA-encrypt the AES key and IV
    ///   2.3 AES-CTR encrypt the plaintext
    ///   2.4 Return (RSA-wrapped AES key/IV, AES ciphertext)
    /// </summary>
    byte[] EncryptHybrid(byte[] plaintext);

    /// <summary>
    /// Verify a SHA-256 signature.
    /// </summary>
    bool VerifySign(byte[] origData, byte[] signature);

    /// <summary>
    /// Verify a SHA-256 signature given as a Base64 string.
    /// </summary>
    bool VerifySignBase64(byte[] origData, string base64Signature);

    /// <summary>
    /// Verify a signature with the specified hash algorithm.
    /// Note: MD5 and SHA1 are not available.
    /// </summary>
    bool VerifySignHash(byte[] origData, byte[] signature, HashAlgorithmName hashAlgorithm);

    /// <summary>
    /// Verify a Base64 signature with the specified hash algorithm.
    /// </summary>
    bool VerifySignHashBase64(byte[] origData, string base64Signature, HashAlgorithmName hashAlgorithm);
}
```

Plaintext up to `EncryptPartLen` (key size/8 − 11) is a single RSA block; longer data is split, encrypted per chunk, and concatenated. Hybrid encrypt also falls back to plain RSA for short data. Signature padding is PKCS#1 v1.5. Verification failure (including `CryptographicException`) returns `false`.

#### IRsaPrivateCipher

RSA private-key operations: decrypt, hybrid decrypt, sign (PKCS#1 v1.5). Implements `IDisposable`.

```csharp
/// <summary>
/// RSA private-key operations: decrypt, hybrid decrypt, sign (PKCS#1 v1.5).
/// </summary>
public interface IRsaPrivateCipher : IDisposable
{
    /// <summary>RSA private key instance.</summary>
    RSA PrivateKey { get; }

    /// <summary>
    /// Decrypt
    /// </summary>
    byte[] Decrypt(byte[] ciphertext);

    /// <summary>
    /// Hybrid decrypt
    /// </summary>
    /// <param name="ciphertext">RSA-wrapped AES key/IV followed by AES ciphertext</param>
    byte[] DecryptHybrid(byte[] ciphertext);

    /// <summary>
    /// Sign with SHA-256
    /// </summary>
    byte[] Sign(byte[] origData);

    /// <summary>
    /// Sign with SHA-256 and return Base64
    /// </summary>
    string SignBase64(byte[] origData);

    /// <summary>
    /// Sign with the specified hash algorithm.
    /// Note: MD5 and SHA1 are not available.
    /// </summary>
    byte[] SignHash(byte[] origData, HashAlgorithmName hashAlgorithm);

    /// <summary>
    /// Sign with the specified hash algorithm and return Base64.
    /// Note: MD5 and SHA1 are not available.
    /// </summary>
    string SignHashBase64(byte[] origData, HashAlgorithmName hashAlgorithm);
}
```

Ciphertext whose length equals `DecryptPartLen` (key size/8) is a single block; otherwise it is split on that length. Hybrid decrypt: the first `DecryptPartLen` bytes are the RSA-wrapped AES-256 key + IV; the remainder is AES-CTR ciphertext. `SignHash` returns `null` if the hash algorithm cannot be created.

#### IRsaCipher

Full RSA cipher with both public and private key operations.

```csharp
/// <summary>
/// Full RSA cipher with both public and private key operations.
/// </summary>
public interface IRsaCipher : IRsaPrivateCipher, IRsaPublicCipher
{
}
```

There is no `GenerateKeyPair` method. Generate keys with `RSA.Create`, or load PEM via `RsaUtils`.

### Static Classes

#### RsaDefines

Placeholder for RSA-related constants (key size, default padding, etc.). Currently has no members.

```csharp
/// <summary>
/// Placeholder for RSA-related constants (key size, default padding, etc.).
/// </summary>
public static class RsaDefines
{
}
```

#### RsaParamUtils

PEM/DER to `RSAParameters` parsing utilities.

```csharp
/// <summary>
/// PEM/DER to RSAParameters parsing utilities.
/// </summary>
public static class RsaParamUtils
{
    /// <summary>
    /// Interpret DER as X.509 SubjectPublicKeyInfo public-key parameters.
    /// </summary>
    /// <exception cref="Exception">Algorithm OID is not RSA, or ASN.1 structure is invalid</exception>
    public static RSAParameters DecodeX509Params(byte[] derBytes);

    /// <summary>
    /// Interpret DER as PKCS#8 private-key parameters.
    /// </summary>
    public static RSAParameters DecodePkcs8Params(byte[] deBytes);

    /// <summary>
    /// Interpret DER as PKCS#1 v1.5 RSA public-key parameters.
    /// </summary>
    public static RSAParameters DecodePkcs1V15Public(byte[] derBytes);

    /// <summary>
    /// Interpret DER as PKCS#1 v1.5 RSA private-key parameters.
    /// </summary>
    /// <remarks>Supports version 0 two-prime RSA only, not multi-prime RSA</remarks>
    public static RSAParameters DecodePkcs1V15Private(byte[] derBytes);

    /// <summary>
    /// Extract Base64 DER payload from PEM text.
    /// </summary>
    /// <param name="keyFileText">Full PEM text (including BEGIN/END lines)</param>
    /// <param name="keyType">A PemTypes label, e.g. RSA PRIVATE KEY</param>
    /// <exception cref="ArgumentException">PEM header/footer mismatch or invalid format</exception>
    public static byte[] ExtractDerData(string keyFileText, string keyType);
}
```

#### RsaUtils

Load RSA keys from PEM files or text and create `IRsaPrivateCipher` / `IRsaPublicCipher`. Returns `null` on failure (empty content, I/O, or parse errors).

```csharp
/// <summary>
/// Load RSA keys from PEM files or text and create IRsaPrivateCipher / IRsaPublicCipher.
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

PEM labels: `RSA PRIVATE KEY`, `RSA PUBLIC KEY`, `PRIVATE KEY`, `PUBLIC KEY` (see `PemTypes`).

### Classes

#### RsaGroup

Splits a long byte stream into RSA-sized groups for chunked encrypt/decrypt.

```csharp
/// <summary>
/// Splits a long byte stream into RSA-sized groups for chunked encrypt/decrypt.
/// </summary>
public sealed class RsaGroup : IDisposable
{
    /// <summary>Create an empty group reader; load data later with ResetData.</summary>
    public RsaGroup();

    /// <summary>
    /// Create a group reader bound to a buffer and group size.
    /// </summary>
    public RsaGroup(byte[] buff, int groupSize);

    /// <summary>
    /// Reset
    /// </summary>
    public void Reset();

    /// <summary>
    /// Reload data and reset the read position.
    /// </summary>
    public void ResetData(byte[] buff, int groupSize);

    /// <summary>
    /// Whether another group remains
    /// </summary>
    public bool HasNext { get; }

    /// <summary>
    /// Read the next group
    /// </summary>
    /// <returns>Next group bytes; the last group may be shorter than the group size</returns>
    public byte[] ReadNext();

    /// <summary>Release the internal buffer reference.</summary>
    public void Dispose();
}
```

#### RsaPrivateCipher

RSA private-key decrypt and sign (PKCS#1 v1.5 padding). `Dispose` disposes the inner `RSA` instance.

```csharp
/// <summary>
/// RSA private-key decrypt and sign (PKCS#1 v1.5 padding).
/// </summary>
public class RsaPrivateCipher : IRsaPrivateCipher
{
    /// <summary>Single RSA ciphertext block length in bytes (key size / 8).</summary>
    public int DecryptPartLen { get; }

    /// <summary>RSA private key instance.</summary>
    public RSA PrivateKey { get; }

    /// <summary>
    /// Create from a .NET RSA private-key instance.
    /// </summary>
    public RsaPrivateCipher(RSA privateKey);

    // Remaining members implement IRsaPrivateCipher
}
```

#### RsaPublicCipher

RSA public-key encrypt and verify (PKCS#1 v1.5 padding). `Dispose` disposes the inner `RSA` instance.

```csharp
/// <summary>
/// RSA public-key encrypt and verify (PKCS#1 v1.5 padding).
/// </summary>
public class RsaPublicCipher : IRsaPublicCipher
{
    /// <summary>Maximum plaintext bytes per RSA encrypt (key size / 8 - 11).</summary>
    public int EncryptPartLen { get; }

    /// <summary>RSA public key instance.</summary>
    public RSA PublicKey { get; }

    /// <summary>
    /// Create from a .NET RSA public-key instance.
    /// </summary>
    public RsaPublicCipher(RSA publicKey);

    // Remaining members implement IRsaPublicCipher
}
```

#### RsaCipher

Combines `RsaPrivateCipher` and `RsaPublicCipher`. `Dispose` disposes both (and thus each held `RSA`). If both wrappers share the same `RSA` instance, it may be disposed twice; import public parameters separately.

```csharp
/// <summary>
/// Combines RsaPrivateCipher and RsaPublicCipher.
/// </summary>
public class RsaCipher : IRsaCipher
{
    /// <summary>
    /// Create from existing private and public wrappers.
    /// </summary>
    public RsaCipher(RsaPrivateCipher privateCipher, RsaPublicCipher publicCipher);

    // Remaining members implement IRsaCipher
}
```

No parameterless constructor and no `GenerateKeyPair`.

---

## Namespace: JLGames.Infra.Crypto.Key

### Classes

#### DhKeyPair

Diffie-Hellman key pair (private and public big integers).

```csharp
/// <summary>
/// Diffie-Hellman key pair (private and public big integers).
/// </summary>
public class DhKeyPair
{
    /// <summary>Private key x</summary>
    public BigInteger Private { get; set; }

    /// <summary>Public key g^x mod p</summary>
    public BigInteger Public { get; set; }
}
```

### Static Classes

#### DiffieHellman

RFC 3526 Group 14 (2048-bit MODP) Diffie-Hellman key exchange. Generator g = 2.

```csharp
/// <summary>
/// RFC 3526 Group 14 (2048-bit MODP) Diffie-Hellman key exchange.
/// </summary>
public static class DiffieHellman
{
    /// <summary>
    /// Generate a DH key pair (private x and public g^x mod p).
    /// </summary>
    public static DhKeyPair GenerateDhKeyPair();

    /// <summary>
    /// Compute shared secret K = theirPublic^myPrivate mod p.
    /// </summary>
    /// <returns>Shared BigInteger (usually run through a KDF before use as a symmetric key)</returns>
    public static BigInteger ComputeDhSharedK(BigInteger theirPublic, BigInteger myPrivate);
}
```

#### KeyDerivation

Derive a symmetric key from a passphrase or shared material (SHA-256 and PBKDF2). Default PBKDF2: built-in salt `"JLGames.Infra.Crypto.Key"` (UTF-8), 100000 iterations, 32-byte output. PBKDF2 uses HMAC-SHA1 (.NET Standard 2.0 `Rfc2898DeriveBytes` cannot select the hash). Unsalted SHA-256 is not intended for production.

```csharp
/// <summary>
/// Derive a symmetric key from a passphrase or shared material (SHA-256 and PBKDF2).
/// </summary>
public static class KeyDerivation
{
    /// <summary>
    /// Hash a UTF-8 passphrase with SHA-256 to a 32-byte key (no salt; non-production only).
    /// </summary>
    public static byte[] SharedKeySha256Str(string passphrase);

    /// <summary>
    /// Hash passphrase bytes with SHA-256 to a 32-byte key (no salt).
    /// </summary>
    public static byte[] SharedKeySha256(byte[] passphrase);

    /// <summary>
    /// PBKDF2 from a string using the built-in salt, 100000 iterations, and 32-byte output (recommended for production).
    /// </summary>
    public static byte[] DeriveKeyPbkdf2StrDefault(string passphrase);

    /// <summary>
    /// PBKDF2 from bytes using the built-in salt, iteration count, and 32-byte output.
    /// </summary>
    public static byte[] DeriveKeyPbkdf2Default(byte[] passphrase);

    /// <summary>
    /// PBKDF2 (HMAC-SHA1) from a UTF-8 string passphrase, salt, and iteration count.
    /// </summary>
    public static byte[] DeriveKeyPbkdf2Str(string passphrase, byte[] salt, int iterations, int keyLen);

    /// <summary>
    /// PBKDF2 (HMAC-SHA1) from passphrase bytes, salt, and iteration count.
    /// </summary>
    public static byte[] DeriveKeyPbkdf2(byte[] passphrase, byte[] salt, int iterations, int keyLen);
}
```

---

## Namespace: JLGames.Infra.Crypto.ASN1

Used when parsing RSA PEM/DER. `DerTags` are Universal Tags as encoded in DER/BER (including constructed bits, e.g. SEQUENCE is `0x30`). `Asn1Tags` are ASN.1 logical tag numbers (SEQUENCE is `0x10`).

### Static Classes

#### DerTags

```csharp
/// <summary>
/// Universal Tag constants as encoded in DER/BER (including constructed/context bits; SEQUENCE is 0x30).
/// May differ from ASN.1 logical tag numbers; see Asn1Tags.
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
/// Universal Tag numbers as defined by ASN.1 (without DER constructed bits).
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

### Structs

#### TLVBlock

ASN.1 TLV block (Tag-Length-Value). Fields are public fields.

```csharp
/// <summary>
/// ASN.1 TLV block (Tag-Length-Value).
/// </summary>
public struct TLVBlock
{
    /// <summary>Type tag (Tag)</summary>
    public byte Tag;

    /// <summary>Raw DER encoding of the Length field</summary>
    public byte[] Length;

    /// <summary>Parsed Value length in bytes</summary>
    public int LengthValue;

    /// <summary>Value bytes; null when Value was not read</summary>
    public byte[] Value;

    /// <summary>
    /// Hex string representation for debugging.
    /// </summary>
    public override string ToString();
}
```

### Classes

#### TLVReader

Streaming TLV (Tag-Length-Value) reader for ASN.1 DER/BER. Implements `IDisposable` and disposes the underlying `BinaryReader`.

```csharp
/// <summary>
/// Streaming TLV (Tag-Length-Value) reader for ASN.1 DER/BER.
/// </summary>
public class TLVReader : IDisposable
{
    /// <summary>
    /// Create from an existing BinaryReader.
    /// </summary>
    public TLVReader(BinaryReader reader);

    /// <summary>
    /// Create from a byte array.
    /// </summary>
    public TLVReader(byte[] data);

    public void Dispose();

    /// <summary>
    /// Skip the given number of bytes (seek forward from the current position).
    /// </summary>
    public void SkipLength(int length);

    /// <summary>
    /// Read an ASN.1 block
    /// </summary>
    public TLVBlock ReadBlock();

    /// <summary>
    /// Read an ASN.1 block after verifying the tag
    /// </summary>
    /// <exception cref="ArgumentException">Tag does not match the expected value</exception>
    public TLVBlock ReadBlock(byte expectedTag);

    /// <summary>
    /// Read an ASN.1 block without the Value part
    /// </summary>
    public TLVBlock ReadBlockNoValue();

    /// <summary>
    /// Read an ASN.1 block after verifying the tag, without the Value part
    /// </summary>
    public TLVBlock ReadBlockNoValue(byte expectedTag);

    /// <summary>
    /// Read an ASN.1 tag
    /// </summary>
    /// <exception cref="EndOfStreamException">Unexpected end of data</exception>
    public byte ReadTag();

    /// <summary>
    /// Read an ASN.1 tag and verify it
    /// </summary>
    public byte ReadTag(byte expectedTag);

    /// <summary>
    /// Read ASN.1 length.
    /// Short form: first byte is the length (less than 128).
    /// Long form: first byte is the count n of following length bytes; remaining n bytes are big-endian length.
    /// </summary>
    /// <param name="lengthValue">Parsed Value length in bytes</param>
    /// <returns>Raw Length-field encoding</returns>
    public byte[] ReadLength(out int lengthValue);

    /// <summary>
    /// Read ASN.1 Value
    /// </summary>
    public byte[] ReadValue(int length);

    /// <summary>
    /// Read one byte
    /// </summary>
    public byte ReadByte();

    /// <summary>
    /// Whether unread data remains
    /// </summary>
    public bool HasData();
}
```

`ReadBlock(expectedTag)` leaves `Value` as `null` when the length is 0.

#### TLVAdvancedReader

Higher-level TLV reader for common ASN.1 types (boolean, integer, OID, etc.).

```csharp
/// <summary>
/// Higher-level TLV reader for common ASN.1 types (boolean, integer, OID, etc.).
/// </summary>
public class TLVAdvancedReader : TLVReader
{
    public TLVAdvancedReader(BinaryReader reader);
    public TLVAdvancedReader(byte[] data);

    /// <summary>
    /// Read a BOOLEAN block after verifying the tag
    /// </summary>
    public byte[] ReadBoolean();

    /// <summary>
    /// Read a NULL block after verifying the tag
    /// </summary>
    public TLVBlock ReadNull();

    /// <summary>
    /// Read an INTEGER block after verifying the tag; strip a leading insignificant 0x00 if present
    /// </summary>
    public byte[] ReadInteger();

    /// <summary>
    /// Read a SEQUENCE block after verifying the tag.
    /// </summary>
    /// <param name="includeValue">When true, read Value; otherwise Tag/Length only</param>
    public TLVBlock ReadSequence(bool includeValue);

    /// <summary>
    /// Read an AlgorithmIdentifier and return the RSA algorithm OID block
    /// </summary>
    public TLVBlock ReadRsaOid();

    /// <summary>
    /// Read an OCTET STRING block (no leading unused-bit count)
    /// </summary>
    public TLVBlock ReadOctetString(bool includeValue);

    /// <summary>
    /// Read a BIT STRING block; Value[0] is the unused-bit count. Returned Value has that byte removed and unused bits cleared.
    /// </summary>
    public TLVBlock ReadBitString();

    /// <summary>
    /// Read an OBJECT IDENTIFIER block.
    /// </summary>
    public TLVBlock ReadObjectIdentifier(bool includeValue);
}
```

---

## Deprecated

The following types live under `JLGames.Infra.Crypto.Deleted/`. An SDK-style project includes these `.cs` files, but **the current file contents are fully commented out**, so the types are not in the assembly and are not recommended APIs. Use `DesCipher` / `AesCipher` / `RsaCipher` (and `RsaUtils`) instead. Do not treat them as part of Utils documentation.

### DESUtil (deprecated)

Historical DES string encrypt/decrypt helper (default 8-byte key and fixed IV, CBC). On failure, returns the source string. Use `DesCipher` instead (prefer `AesCipher` for new code).

```csharp
// Namespace: JLGames.Infra.Crypto (historical)
public static class DESUtil
{
    public static string Encrypted(string encryptString);
    public static string Encrypted(string encryptString, string encryptKey, string encryptIV);
    /// <summary>DES-encrypt a string. Key must be 8 bytes. Returns Base64 on success, or the source string on failure.</summary>
    public static string Encrypted(string encryptString, byte[] encryptKey, byte[] encryptIV);

    public static string Decrypted(string decryptString);
    public static string Decrypted(string decryptString, string encryptKey, string encryptIV);
    /// <summary>DES-decrypt a string. Returns the source string on failure.</summary>
    public static string Decrypted(string decryptString, byte[] decryptKey, byte[] decryptIV);
}
```

### RijndaelUtil (deprecated)

Historical Rijndael string encrypt/decrypt helper. Parameterless overloads use a per-process default key/IV. Use `AesCipher` instead.

```csharp
// Namespace: JLGames.Infra.Crypto (historical)
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

### RSAUtil (deprecated)

Historical RSA string encrypt/decrypt helper that depends on a fixed key-container name `"oa_erp_dowork"`. Not very efficient. Use `RsaCipher` / `RsaUtils` instead.

```csharp
// Namespace: JLGames.Infra.Crypto (historical)
public static class RSAUtil
{
    public static string Encrypted(string express);
    public static string Decrypted(string ciphertext);
}
```

---

## Function Description

### Encryption Mode Details

**ECB (Electronic Codebook)**  
Each block is encrypted independently. Identical plaintext blocks yield identical ciphertext and leak structure. **Not recommended.** Supported by DES, not by the current AES implementation.

**CBC (Cipher Block Chaining)**  
Each block is XORed with the previous ciphertext; the first block needs an IV. AES overloads without IV return `IV + ciphertext`; overloads with IV return ciphertext only. DES CBC output always prefixes the IV.

**CFB / OFB**  
Defined on the enum; not supported by current AES / DES `EncryptMode` / `DecryptMode`.

**CTR (Counter)**  
Keystream from an incrementing counter; encrypt and decrypt are the same operation and can run in parallel. AES uses a 16-byte IV.

**GCM (Galois/Counter Mode)**  
Authenticated encryption. Default for `AesCipher`. Overload without nonce returns `nonce(12) + ciphertext + tag(16)`; overload with nonce returns `ciphertext + tag`. AES only.

### AES

1. Symmetric; key lengths 16 / 24 / 32 bytes  
2. Default mode is GCM  
3. CBC padding is configurable via `SetPadding`  

### RSA

1. Public-key encrypt, private-key decrypt; PKCS#1 v1.5  
2. Long plaintext is chunked or hybrid-encrypted with AES-CTR  
3. Default signature hash is SHA-256; `SignHash` comments mark MD5 / SHA-1 as unavailable  
4. Load PEM with `RsaUtils`; do not use deprecated `RSAUtil`

---

## Usage Examples

#### AES (GCM default)

```csharp
byte[] key = new byte[AesDefines.DefaultKeyLength];
RandomNumberGenerator.Fill(key);

var aes = new AesCipher(key);
byte[] plaintext = Encoding.UTF8.GetBytes("Hello, World!");

byte[] encrypted = aes.Encrypt(plaintext);          // default GCM: nonce + ciphertext + tag
byte[] decrypted = aes.Decrypt(encrypted);
string result = Encoding.UTF8.GetString(decrypted);

byte[] nonce = new byte[12];
RandomNumberGenerator.Fill(nonce);
byte[] body = aes.EncryptGcm(plaintext, nonce);     // ciphertext + tag
byte[] roundtrip = aes.DecryptGcm(body, nonce);
```

#### AES CBC / CTR / generic mode

```csharp
var aes = new AesCipher(key);
aes.SetPadding(PaddingMode.PKCS7);

byte[] iv = new byte[AesDefines.BlockSize];
RandomNumberGenerator.Fill(iv);

byte[] packed = aes.EncryptCbc(plaintext);          // IV + ciphertext
byte[] plain1 = aes.DecryptCbc(packed);

byte[] cbcBody = aes.EncryptCbc(plaintext, iv);     // ciphertext only
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
byte[] desCt = des.Encrypt(plaintext);              // default CBC, IV prefixed
byte[] desPt = des.Decrypt(desCt);

byte[] tdesKey = new byte[DesDefines.TripleKeySize];
RandomNumberGenerator.Fill(tdesKey);
var tdes = new TripleDesCipher(tdesKey);

var xor = new XorCipher(Encoding.UTF8.GetBytes("obf-key"));
byte[] obfuscated = xor.Encrypt(plaintext);
byte[] restored = xor.Decrypt(obfuscated);
```

#### RSA encrypt, decrypt, and sign

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

#### Load RSA from PEM

```csharp
IRsaPrivateCipher priv = RsaUtils.LoadPrivateCipherPkcs8("private.pem");
IRsaPublicCipher pub = RsaUtils.LoadPublicCipherX509("public.pem");
if (priv == null || pub == null)
    throw new InvalidOperationException("failed to load PEM");

byte[] encrypted = pub.Encrypt(plaintext);
byte[] decrypted = priv.Decrypt(encrypted);
```

#### Diffie-Hellman and key derivation

```csharp
DhKeyPair alice = DiffieHellman.GenerateDhKeyPair();
DhKeyPair bob = DiffieHellman.GenerateDhKeyPair();
BigInteger sharedA = DiffieHellman.ComputeDhSharedK(bob.Public, alice.Private);
BigInteger sharedB = DiffieHellman.ComputeDhSharedK(alice.Public, bob.Private);

byte[] aesKey = KeyDerivation.DeriveKeyPbkdf2StrDefault("user-passphrase");
byte[] shaKey = KeyDerivation.SharedKeySha256Str("dev-only");
```

#### Hashing and padding

```csharp
string md5 = HashHelper.Md5String("hello");
string sha1 = HashHelper.Sha1(plaintext);

byte[] padded = PaddingUtils.Pkcs7Padding(plaintext, AesDefines.BlockSize);
byte[] unpadded = PaddingUtils.Pkcs7UnPadding(padded);

string desc = BlockModeHelper.GetDescription(BlockMode.GCM);
```

---

## Security Recommendations

1. **Avoid ECB**: easily leaks data patterns  
2. **Use a random IV/nonce**: required for CBC / CTR / GCM; prefer `RandomNumberGenerator`  
3. **Choose a mode**: CBC for files; CTR for streams; GCM when you need authenticated encryption (the default)  
4. **Key management**: protect and rotate keys; prefer `KeyDerivation.DeriveKeyPbkdf2*` for passphrases  
5. **Algorithm choice**: prefer AES-256 and RSA-2048+; do not treat DES / XOR as secure encryption; use MD5 / SHA-1 only for checksums  
6. **RSA signatures**: use SHA-256; do not use deprecated `RSAUtil` with a fixed key container  

## Performance Considerations

1. **AES**: modern CPUs often have hardware acceleration; CBC uses `Aes.Create()`  
2. **RSA**: slower than symmetric ciphers; suitable for short data or wrapping keys in hybrid encryption  
3. **CTR**: parallelizable; AES-CTR is implemented by `AesCtrEngine`  
4. **GCM**: managed GHASH; large payloads cost more than hardware GCM  
5. **DH**: 2048-bit MODP; run the shared secret through a KDF before using it as a symmetric key  
