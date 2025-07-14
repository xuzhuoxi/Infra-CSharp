using Org.BouncyCastle.Crypto.Engines;
using Org.BouncyCastle.Crypto.Modes;
using Org.BouncyCastle.Crypto.Parameters;

namespace JLGames.Infra.Crypto.Symmetric
{
    public sealed class BouncyCastleAesGcmEngine
    {
        public static BouncyCastleAesGcmEngine Default { get; private set; } = new BouncyCastleAesGcmEngine();

        public byte[] EncryptGcm(byte[] plaintext, byte[] key, byte[] nonce)
        {
            var cipher = new AesLightEngine();
            var blockCipher = new GcmBlockCipher(cipher); // 使用 GCMBlockCipher

            var cipherParams = new ParametersWithIV(new KeyParameter(key), nonce);
            blockCipher.Init(true, cipherParams); // 初始化加密

            var output = new byte[blockCipher.GetOutputSize(plaintext.Length)];
            var length = blockCipher.ProcessBytes(plaintext, 0, plaintext.Length, output, 0);
            blockCipher.DoFinal(output, length); // 结束加密过程

            return output;
        }

        public byte[] DecryptGcm(byte[] ciphertext, byte[] key, byte[] nonce)
        {
            var cipher = new AesLightEngine(); // 使用 AesLightEngine
            var blockCipher = new GcmBlockCipher(cipher); // 使用 GCMBlockCipher

            var cipherParams = new ParametersWithIV(new KeyParameter(key), nonce);
            blockCipher.Init(false, cipherParams); // 初始化解密

            var output = new byte[blockCipher.GetOutputSize(ciphertext.Length)];
            var length = blockCipher.ProcessBytes(ciphertext, 0, ciphertext.Length, output, 0);
            blockCipher.DoFinal(output, length); // 结束解密过程

            return output;
        }
    }
}