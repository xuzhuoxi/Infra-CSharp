// using System.Linq;
// using Org.BouncyCastle.Crypto;
// using Org.BouncyCastle.Crypto.Engines;
// using Org.BouncyCastle.Crypto.Modes;
// using Org.BouncyCastle.Crypto.Parameters;
//
// namespace JLGames.Infra.Crypto.Symmetric
// {
//     public sealed class BouncyCastleAesCtrEngine
//     {
//         public static BouncyCastleAesCtrEngine Default { get; private set; } = new BouncyCastleAesCtrEngine();
//
//         public byte[] EncryptCtr(byte[] plaintext, byte[] key, byte[] iv)
//         {
//             var cipher = new BufferedBlockCipher(new SicBlockCipher(new AesEngine()));
//             cipher.Init(true, new ParametersWithIV(new KeyParameter(key), iv));
//             var output = new byte[cipher.GetOutputSize(plaintext.Length)];
//             var processedBytes = cipher.ProcessBytes(plaintext, 0, plaintext.Length, output, 0);
//             var finalBytes = cipher.DoFinal(output, processedBytes); // 处理最后一块数据
//             return output.Take(processedBytes + finalBytes).ToArray();
//         }
//
//         public byte[] DecryptCtr(byte[] ciphertext, byte[] key, byte[] iv)
//         {
//             var cipher = new BufferedBlockCipher(new SicBlockCipher(new AesEngine()));
//             cipher.Init(false, new ParametersWithIV(new KeyParameter(key), iv));
//             var output = new byte[cipher.GetOutputSize(ciphertext.Length)];
//             var processedBytes = cipher.ProcessBytes(ciphertext, 0, ciphertext.Length, output, 0);
//             var finalBytes = cipher.DoFinal(output, processedBytes); // 处理最后一块数据
//             return output.Take(processedBytes + finalBytes).ToArray();
//         }
//     }
// }