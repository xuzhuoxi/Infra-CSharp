using System.Security.Cryptography;

namespace JLGames.Infra.Crypto.Symmetric
{
    /// <summary>
    /// AES cipher interface (Rijndael).
    /// AES 对称加密接口（高级加密标准，算法 Rijndael）
    /// </summary>
    public interface IAesCipher : ICipher
    {
        /// <summary>
        /// Gets a copy of the key.
        /// 获取密钥副本（只读）
        /// </summary>
        byte[] Key { get; }

        /// <summary>
        /// Block size in bytes (16 for AES).
        /// 块大小（字节），AES 为 16
        /// </summary>
        int BlockSize { get; }

        /// <summary>
        /// Sets PKCS#7 or other padding for CBC modes.
        /// 设置 CBC 等模式的填充方式
        /// </summary>
        /// <param name="paddingMode">Padding mode / 填充模式</param>
        void SetPadding(PaddingMode paddingMode);

        /// <summary>
        /// Encrypt with the given block mode (CBC, CTR, GCM).
        /// 按指定分组模式加密
        /// </summary>
        /// <param name="plaintext">Plaintext / 明文</param>
        /// <param name="blockMode">Block mode / 分组模式</param>
        /// <returns>Ciphertext; may prefix random IV/nonce / 密文</returns>
        byte[] EncryptMode(byte[] plaintext, BlockMode blockMode);

        /// <summary>
        /// Encrypt with explicit IV/nonce and block mode.
        /// 使用指定 IV/nonce 与分组模式加密
        /// </summary>
        /// <param name="plaintext">Plaintext / 明文</param>
        /// <param name="iv">IV or GCM nonce / 初始化向量或 GCM nonce</param>
        /// <param name="blockMode">Block mode / 分组模式</param>
        /// <returns>Ciphertext / 密文</returns>
        byte[] EncryptMode(byte[] plaintext, byte[] iv, BlockMode blockMode);

        /// <summary>
        /// Decrypt with the given block mode.
        /// 按指定分组模式解密
        /// </summary>
        /// <param name="ciphertext">Ciphertext (format must match encrypt) / 密文</param>
        /// <param name="blockMode">Block mode / 分组模式</param>
        /// <returns>Plaintext / 明文</returns>
        byte[] DecryptMode(byte[] ciphertext, BlockMode blockMode);

        /// <summary>
        /// Decrypt with explicit IV/nonce and block mode.
        /// 使用指定 IV/nonce 与分组模式解密
        /// </summary>
        /// <param name="ciphertext">Ciphertext / 密文</param>
        /// <param name="iv">IV or GCM nonce / 初始化向量或 GCM nonce</param>
        /// <param name="blockMode">Block mode / 分组模式</param>
        /// <returns>Plaintext / 明文</returns>
        byte[] DecryptMode(byte[] ciphertext, byte[] iv, BlockMode blockMode);

        /// <summary>CBC encrypt; random IV prepended. / CBC 加密，前缀 IV。</summary>
        /// <param name="plaintext">Plaintext / 明文</param>
        /// <returns>IV + ciphertext / IV + 密文</returns>
        byte[] EncryptCbc(byte[] plaintext);

        /// <summary>CBC encrypt with given IV. / CBC 加密，指定 IV。</summary>
        /// <param name="plaintext">Plaintext / 明文</param>
        /// <param name="iv">Initialization vector / 初始化向量</param>
        /// <returns>Ciphertext only / 密文（不含 IV）</returns>
        byte[] EncryptCbc(byte[] plaintext, byte[] iv);

        /// <summary>CBC decrypt; input is IV + ciphertext. / CBC 解密，输入 IV + 密文。</summary>
        /// <param name="ciphertext">IV + ciphertext / IV + 密文</param>
        /// <returns>Plaintext / 明文</returns>
        byte[] DecryptCbc(byte[] ciphertext);

        /// <summary>CBC decrypt with separate IV. / CBC 解密，IV 与密文分开。</summary>
        /// <param name="ciphertext">Ciphertext body / 密文</param>
        /// <param name="iv">Initialization vector / 初始化向量</param>
        /// <returns>Plaintext / 明文</returns>
        byte[] DecryptCbc(byte[] ciphertext, byte[] iv);

        /// <summary>CTR encrypt; random IV prepended. / CTR 加密，前缀 IV。</summary>
        /// <param name="plaintext">Plaintext / 明文</param>
        /// <returns>IV + ciphertext / IV + 密文</returns>
        byte[] EncryptCtr(byte[] plaintext);

        /// <summary>CTR encrypt with 16-byte IV. / CTR 加密，指定 16 字节 IV。</summary>
        /// <param name="plaintext">Plaintext / 明文</param>
        /// <param name="iv">16-byte IV / 初始化向量</param>
        /// <returns>Ciphertext / 密文</returns>
        byte[] EncryptCtr(byte[] plaintext, byte[] iv);

        /// <summary>CTR decrypt; input is IV + ciphertext. / CTR 解密，输入 IV + 密文。</summary>
        /// <param name="ciphertext">IV + ciphertext / IV + 密文</param>
        /// <returns>Plaintext / 明文</returns>
        byte[] DecryptCtr(byte[] ciphertext);

        /// <summary>CTR decrypt with separate IV. / CTR 解密，IV 与密文分开。</summary>
        /// <param name="ciphertext">Ciphertext / 密文</param>
        /// <param name="iv">16-byte IV / 初始化向量</param>
        /// <returns>Plaintext / 明文</returns>
        byte[] DecryptCtr(byte[] ciphertext, byte[] iv);

        /// <summary>GCM encrypt; 12-byte nonce prepended. / GCM 加密，前缀 12 字节 nonce。</summary>
        /// <param name="plaintext">Plaintext / 明文</param>
        /// <returns>nonce + ciphertext + tag / nonce + 密文 + 标签</returns>
        byte[] EncryptGcm(byte[] plaintext);

        /// <summary>GCM encrypt with given nonce. / GCM 加密，指定 nonce。</summary>
        /// <param name="plaintext">Plaintext / 明文</param>
        /// <param name="nonce">12-byte nonce recommended / nonce</param>
        /// <returns>ciphertext + authentication tag / 密文 + 认证标签</returns>
        byte[] EncryptGcm(byte[] plaintext, byte[] nonce);

        /// <summary>GCM decrypt; input is nonce + ciphertext + tag. / GCM 解密。</summary>
        /// <param name="ciphertext">nonce + ciphertext + tag / 完整密文包</param>
        /// <returns>Plaintext / 明文</returns>
        byte[] DecryptGcm(byte[] ciphertext);

        /// <summary>GCM decrypt with separate nonce. / GCM 解密，nonce 单独传入。</summary>
        /// <param name="ciphertext">Ciphertext + tag / 密文与标签</param>
        /// <param name="nonce">Nonce used for encryption / 加密时使用的 nonce</param>
        /// <returns>Plaintext / 明文</returns>
        byte[] DecryptGcm(byte[] ciphertext, byte[] nonce);
    }
}
