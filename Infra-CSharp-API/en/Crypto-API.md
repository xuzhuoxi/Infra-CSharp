# Crypto API Documentation

## Namespace: JLGames.Infra.Crypto

### Interfaces

#### IEncryptCipher
Encryption processor interface

```csharp
/// <summary>
/// Encryption processor interface
/// </summary>
public interface IEncryptCipher
{
    /// <summary>
    /// Encrypt data
    /// </summary>
    /// <param name="plaintext">Plaintext data to be encrypted</param>
    /// <returns>Encrypted ciphertext data</returns>
    /// <exception cref="Exception">Error occurred during encryption</exception>
    byte[] Encrypt(byte[] plaintext);
}
```

#### IDecryptCipher
Decryption processor interface

```csharp
/// <summary>
/// Decryption processor interface
/// </summary>
public interface IDecryptCipher
{
    /// <summary>
    /// Decrypt data
    /// </summary>
    /// <param name="ciphertext">Ciphertext data to be decrypted</param>
    /// <returns>Decrypted plaintext data</returns>
    /// <exception cref="Exception">Error occurred during decryption</exception>
    byte[] Decrypt(byte[] ciphertext);
}
```

#### ICipher
Encryption and decryption processor interface, inherits from encryption and decryption interfaces

```csharp
/// <summary>
/// Encryption and decryption processor interface, inherits from encryption and decryption interfaces
/// </summary>
public interface ICipher : IEncryptCipher, IDecryptCipher
{
}
```

### Enums

#### BlockMode
Encryption mode enumeration

```csharp
/// <summary>
/// Encryption mode enumeration
/// </summary>
public enum BlockMode
{
    /// <summary>
    /// Electronic Codebook mode (insecure)
    /// Each block is encrypted independently without context. Same plaintext blocks always generate same ciphertext blocks, easily leaking structure.
    /// Not recommended for practical applications.
    /// </summary>
    ECB,

    /// <summary>
    /// Cipher Block Chaining mode
    /// Each block is XORed with the previous ciphertext block before encryption, first block needs a random IV.
    /// Commonly used for file encryption, good security, but encryption cannot be parallelized.
    /// </summary>
    CBC,

    /// <summary>
    /// Cipher Feedback mode (stream cipher)
    /// Uses previous ciphertext as input stream to generate key stream, suitable for byte data transmission scenarios.
    /// One bit error affects current and next block.
    /// </summary>
    CFB,

    /// <summary>
    /// Output Feedback mode (pre-computed key stream)
    /// Similar to CFB, but only uses encryption output instead of ciphertext. Sensitive to IV.
    /// Errors do not propagate to other blocks.
    /// </summary>
    OFB,

    /// <summary>
    /// Counter mode (high performance)
    /// Generates key stream by encrypting incrementing "counter", supports parallel encryption/decryption.
    /// Suitable for high-performance communication stream encryption.
    /// </summary>
    CTR,

    /// <summary>
    /// Galois/Counter mode (high security)
    /// Supports authenticated encryption, combines encryption and verification, suitable for high security requirement scenarios.
    /// Commonly used in TLS, VPN, HTTPS, etc.
    /// </summary>
    GCM
}
```

### Static Classes

#### BlockModeHelper
Encryption mode helper utility class

```csharp
/// <summary>
/// Encryption mode helper utility class
/// </summary>
public static class BlockModeHelper
{
    /// <summary>
    /// Get description of encryption mode
    /// </summary>
    /// <param name="mode">Encryption mode enumeration value</param>
    /// <returns>Description information</returns>
    public static string GetDescription(BlockMode mode);
}
```

## Namespace: JLGames.Infra.Crypto.Symmetric

### Interfaces

#### IAesCipher
AES encryption interface

```csharp
/// <summary>
/// IAESCipher interface
/// AES: Advanced Encryption Standard, corresponding algorithm Rijndael
/// Features:
/// 1. Symmetric encryption
/// 2. One key expands into multiple sub-keys, multi-round encryption
/// </summary>
public interface IAesCipher : ICipher
{
    /// <summary>
    /// Get key (read-only)
    /// </summary>
    byte[] Key { get; }

    /// <summary>
    /// BlockSize
    /// </summary>
    int BlockSize { get; }

    /// <summary>
    /// Set padding mode
    /// </summary>
    /// <param name="paddingMode">Padding mode</param>
    void SetPadding(PaddingMode paddingMode);

    /// <summary>
    /// Encrypt with specified BlockMode
    /// </summary>
    byte[] EncryptMode(byte[] plaintext, BlockMode blockMode);

    /// <summary>
    /// Encrypt with specified BlockMode
    /// </summary>
    byte[] EncryptMode(byte[] plaintext, byte[] iv, BlockMode blockMode);

    /// <summary>
    /// Decrypt with specified BlockMode
    /// </summary>
    byte[] DecryptMode(byte[] ciphertext, BlockMode blockMode);

    /// <summary>
    /// Decrypt with specified BlockMode
    /// </summary>
    byte[] DecryptMode(byte[] ciphertext, byte[] iv, BlockMode blockMode);

    /// <summary>
    /// CBC mode encryption
    /// </summary>
    byte[] EncryptCbc(byte[] plaintext);

    /// <summary>
    /// CBC mode encryption
    /// </summary>
    byte[] EncryptCbc(byte[] plaintext, byte[] iv);

    /// <summary>
    /// CBC mode decryption
    /// </summary>
    byte[] DecryptCbc(byte[] ciphertext);

    /// <summary>
    /// CBC mode decryption
    /// </summary>
    byte[] DecryptCbc(byte[] ciphertext, byte[] iv);

    /// <summary>
    /// CTR mode encryption
    /// </summary>
    byte[] EncryptCtr(byte[] plaintext);

    /// <summary>
    /// CTR mode encryption
    /// </summary>
    byte[] EncryptCtr(byte[] plaintext, byte[] iv);

    /// <summary>
    /// CTR mode decryption
    /// </summary>
    byte[] DecryptCtr(byte[] ciphertext);

    /// <summary>
    /// CTR mode decryption
    /// </summary>
    byte[] DecryptCtr(byte[] ciphertext, byte[] iv);

    /// <summary>
    /// GCM mode encryption
    /// </summary>
    byte[] EncryptGcm(byte[] plaintext);

    /// <summary>
    /// GCM mode encryption
    /// </summary>
    byte[] EncryptGcm(byte[] plaintext, byte[] nonce);

    /// <summary>
    /// GCM mode decryption
    /// </summary>
    byte[] DecryptGcm(byte[] ciphertext);

    /// <summary>
    /// GCM mode decryption
    /// </summary>
    byte[] DecryptGcm(byte[] ciphertext, byte[] nonce);
}
```

#### IDesCipher
DES encryption interface

```csharp
/// <summary>
/// DES encryption interface
/// DES: Data Encryption Standard
/// Features:
/// 1. Symmetric encryption
/// 2. 64-bit block, 56-bit key
/// 3. Considered insecure, not recommended for use
/// </summary>
public interface IDesCipher : ICipher
{
    // Specific implementation requires further analysis of file content
}
```

### Classes

#### AesCipher
AES encryption implementation class

```csharp
/// <summary>
/// AES encryption implementation class
/// Supports multiple encryption modes and padding methods
/// </summary>
public class AesCipher : IAesCipher
{
    // Specific implementation requires further analysis of file content
}
```

#### DesCipher
DES encryption implementation class

```csharp
/// <summary>
/// DES encryption implementation class
/// Note: DES is considered insecure, AES is recommended
/// </summary>
public class DesCipher : IDesCipher
{
    // Specific implementation requires further analysis of file content
}
```

## Namespace: JLGames.Infra.Crypto.Asymmetric

### Interfaces

#### IRsaCipher
RSA encryption interface

```csharp
/// <summary>
/// RSA encryption interface
/// RSA: Rivest-Shamir-Adleman, asymmetric encryption algorithm
/// Features:
/// 1. Asymmetric encryption
/// 2. Public key encryption, private key decryption
/// 3. Security based on difficulty of large number factorization
/// </summary>
public interface IRsaCipher : ICipher
{
    // Specific implementation requires further analysis of file content
}
```

### Classes

#### RsaCipher
RSA encryption implementation class

```csharp
/// <summary>
/// RSA encryption implementation class
/// Supports public key encryption and private key decryption
/// </summary>
public class RsaCipher : IRsaCipher
{
    // Specific implementation requires further analysis of file content
}
```

### Function Description

#### Encryption Mode Details

**ECB (Electronic Codebook)**
- Electronic codebook mode, each block encrypted independently
- Same plaintext blocks always generate same ciphertext blocks
- Easily leaks data structure and patterns
- **Not recommended for practical applications**

**CBC (Cipher Block Chaining)**
- Cipher block chaining mode
- Each block is XORed with the previous ciphertext block before encryption
- First block needs a random initialization vector (IV)
- Good security, decryption can be parallelized, but encryption cannot
- **Commonly used for file encryption**

**CFB (Cipher Feedback)**
- Cipher feedback mode, stream cipher
- Uses previous ciphertext as input stream to generate key stream
- Suitable for byte data transmission scenarios
- One bit error affects current and next block

**OFB (Output Feedback)**
- Output feedback mode, pre-computed key stream
- Similar to CFB, but only uses encryption output instead of ciphertext
- Errors do not propagate to other blocks
- Very sensitive to IV

**CTR (Counter)**
- Counter mode, high performance
- Generates key stream by encrypting incrementing counter
- Supports parallel encryption/decryption
- **Suitable for high-performance communication stream encryption**

**GCM (Galois/Counter Mode)**
- Galois/Counter mode, high security
- Supports authenticated encryption, combines encryption and verification
- **Commonly used in TLS, VPN, HTTPS and other high security requirement scenarios**

#### AES Encryption Features

1. **Symmetric Encryption**: Uses the same key for encryption and decryption
2. **Key Expansion**: One key expands into multiple sub-keys
3. **Multi-round Encryption**: Improves security through multiple rounds of transformation
4. **Standard Algorithm**: Widely adopted as Advanced Encryption Standard

#### RSA Encryption Features

1. **Asymmetric Encryption**: Public key encryption, private key decryption
2. **Mathematical Foundation**: Based on difficulty of large number factorization
3. **Key Management**: Public key can be public, private key must be kept secret
4. **Application Scenarios**: Digital signatures, key exchange, etc.

### Usage Examples

#### AES Encryption Example
```csharp
// Create AES cipher
var aesCipher = new AesCipher();

// CBC mode encryption
byte[] plaintext = Encoding.UTF8.GetBytes("Hello, World!");
byte[] encrypted = aesCipher.EncryptCbc(plaintext);

// CBC mode decryption
byte[] decrypted = aesCipher.DecryptCbc(encrypted);
string result = Encoding.UTF8.GetString(decrypted);

// Use custom IV
byte[] iv = new byte[16]; // AES block size
RandomNumberGenerator.Fill(iv);
byte[] encryptedWithIV = aesCipher.EncryptCbc(plaintext, iv);
```

#### Different Encryption Modes
```csharp
var aesCipher = new AesCipher();

// CTR mode (high performance)
byte[] ctrEncrypted = aesCipher.EncryptCtr(plaintext);

// GCM mode (high security)
byte[] gcmEncrypted = aesCipher.EncryptGcm(plaintext);

// Generic mode methods
byte[] cbcEncrypted = aesCipher.EncryptMode(plaintext, BlockMode.CBC);
byte[] ctrEncrypted2 = aesCipher.EncryptMode(plaintext, BlockMode.CTR);
```

#### RSA Encryption Example
```csharp
// Create RSA cipher
var rsaCipher = new RsaCipher();

// Generate key pair
rsaCipher.GenerateKeyPair();

// Encrypt with public key
byte[] encrypted = rsaCipher.Encrypt(plaintext);

// Decrypt with private key
byte[] decrypted = rsaCipher.Decrypt(encrypted);
```

#### Encryption Mode Information
```csharp
// Get encryption mode descriptions
string cbcDesc = BlockModeHelper.GetDescription(BlockMode.CBC);
string gcmDesc = BlockModeHelper.GetDescription(BlockMode.GCM);

Console.WriteLine($"CBC mode: {cbcDesc}");
Console.WriteLine($"GCM mode: {gcmDesc}");
```

### Security Recommendations

1. **Avoid ECB Mode**: Easily leaks data patterns
2. **Use Random IV**: CBC mode must use random initialization vector
3. **Choose Appropriate Mode**:
   - File encryption: CBC
   - Stream encryption: CTR
   - High security requirements: GCM
4. **Key Management**: Properly secure keys and rotate regularly
5. **Algorithm Selection**: Prefer AES, avoid DES

### Performance Considerations

1. **AES Performance**: Modern CPUs usually have AES hardware acceleration
2. **RSA Performance**: Slower than symmetric encryption, suitable for small data
3. **Parallel Processing**: CTR mode supports parallel encryption/decryption
4. **Memory Usage**: Different modes have different memory requirements 