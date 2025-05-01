// using System;
// using System.Linq;
// using System.Text;
//
// using Org.BouncyCastle.Crypto;
// using Org.BouncyCastle.Crypto.Engines;
// using Org.BouncyCastle.Crypto.Parameters;
// using Org.BouncyCastle.Security;
// using Org.BouncyCastle.Crypto.Paddings;
// using Org.BouncyCastle.Utilities;
// using Org.BouncyCastle.Utilities.Encoders;
// using Org.BouncyCastle.Crypto.Utilities;
// using Org.BouncyCastle.Crypto.Modes;
// using Org.BouncyCastle.Utilities.IO;
//
//
// public class SymmetricCipherExamples
// {
//  
//     public static void Main(String[] args)
//     {
//         
//         Console.WriteLine();
//         Console.WriteLine();
//         Console.WriteLine("====================================");
//         Console.WriteLine("Start Symmetric Cipher Examples.Main");
//         Console.WriteLine("====================================");
//
//
//         {
//             //Example 7 – ECB Mode Symmetric Cipher
//             // Basic Block Cipher Encryption using AES engine, ECB mode and PKCS7 Padding
//             // Returns the encrypted plain text as an array of bytes
//             Console.WriteLine();
//             Console.WriteLine("*** Example 7 – ECB Mode Symmetric Cipher ***");
//             ExValues.cSharpFixedRandom.SetSeed(151);
//             ICipherParameters keyParam = keyParameterGeneration(256);
//             byte[] cipherTextData =  ecbPaddedEncrypt(keyParam, ExValues.sampleInput.Concat(Encoding.Default.GetBytes(" AES ECB")).ToArray());
//             Console.WriteLine(string.Format("Encrypt ECB MODE padded\tplainTextData:[{0}]\tLength:[{1}]\tcipherTextData:[{2}]\tLength:[{3}]",
//                 Encoding.Default.GetString(ExValues.sampleInput.Concat(Encoding.Default.GetBytes(" AES ECB")).ToArray()),
//                 Encoding.Default.GetString(ExValues.sampleInput.Concat(Encoding.Default.GetBytes(" AES ECB")).ToArray()).Length,
//                 string.Join(",",cipherTextData),
//                 cipherTextData.Length));
//             byte[] plainTextData =  ecbPaddedDecrypt(keyParam, cipherTextData);
//             Console.WriteLine(string.Format("Decrypt ECB MODE padded\tcipherTextData:[{0}]\tLength:[{1}]\tplainTextData:[{2}]\tLength:[{3}]",
//                 string.Join(",",cipherTextData),
//                 cipherTextData.Length.ToString(),
//                 Encoding.Default.GetString(plainTextData),
//                 Encoding.Default.GetString(plainTextData).Length
//                 ));
//         }
//
//
//         {
//             //Example 7a – ECB Mode Symmetric Cipher
//             // Here we use our "own" key compare the outcome with Example 7 above
//             ExValues.cSharpFixedRandom.SetSeed(151);
//             byte[] myKey = new byte[32];
//             ExValues.cSharpFixedRandom.NextBytes(myKey);
//             ICipherParameters keyParam = keyParameterGeneration(myKey);
//             byte[] cipherTextData =  ecbPaddedEncrypt(keyParam, ExValues.sampleInput.Concat(Encoding.Default.GetBytes(" AES ECB")).ToArray());
//             Console.WriteLine(string.Format("Encrypt ECB MODE padded\tplainTextData:[{0}]\tLength:[{1}]\tcipherTextData:[{2}]\tLength:[{3}]",
//                 Encoding.Default.GetString(ExValues.sampleInput.Concat(Encoding.Default.GetBytes(" AES ECB")).ToArray()),
//                 Encoding.Default.GetString(ExValues.sampleInput.Concat(Encoding.Default.GetBytes(" AES ECB")).ToArray()).Length,
//                 string.Join(",",cipherTextData),
//                 cipherTextData.Length));
//             byte[] plainTextData =  ecbPaddedDecrypt(keyParam, cipherTextData);
//             Console.WriteLine(string.Format("Decrypt ECB MODE padded\tcipherTextData:[{0}]\tLength:[{1}]\tplainTextData:[{2}]\tLength:[{3}]",
//                 string.Join(",",cipherTextData),
//                 cipherTextData.Length.ToString(),
//                 Encoding.Default.GetString(plainTextData),
//                 Encoding.Default.GetString(plainTextData).Length
//                 ));
//         }
//        
//         {
//             //*** Example 8 – Showing how key and IV are generated together ***
//             Console.WriteLine();
//             Console.WriteLine("*** Example 8 – Showing how key and IV are generated together ***");
//             ExValues.cSharpFixedRandom.SetSeed(151);
//             ParametersWithIV keyParamWithIV = keyParameterGenerationWithIV(128, ExValues.sampleIVnonce);
//             KeyParameter keyParam = (KeyParameter)(keyParamWithIV.Parameters);
//             Console.WriteLine(string.Format("Key genration with IV\t\tIV:[{0}]\tLength:[{1}]\tKey=[{2}]\tLength[{3}]",
//                 Encoding.Default.GetString(keyParamWithIV.GetIV()),
//                 Encoding.Default.GetString(keyParamWithIV.GetIV()).Length,
//                 string.Join(",",keyParam.GetKey()),
//                 keyParam.GetKey().Length
//                 ));
//         }
//         
//         {            
//             //  *** Example 9 – CBC Mode Encryption/Decryption ***
//             Console.WriteLine();
//             Console.WriteLine("*** Example 9 – CBC Mode Encryption/Decryption ***");
//             ExValues.cSharpFixedRandom.SetSeed(151);
//             ParametersWithIV keyParamWithIV = keyParameterGenerationWithIV(128, ExValues.sampleIVnonce);
//             byte[] cipherTextData =  cbcPaddedEncrypt(keyParamWithIV, ExValues.sampleInput.Concat(Encoding.Default.GetBytes(" AES CBC")).ToArray());
//             Console.WriteLine(string.Format("Encrypt CBC MODE padded\tplainTextData:[{0}]\tLength:[{1}]\tcipherTextData:[{2}]\tLength:[{3}]",
//                 Encoding.Default.GetString(ExValues.sampleInput.Concat(Encoding.Default.GetBytes(" AES CBC")).ToArray()),
//                 Encoding.Default.GetString(ExValues.sampleInput.Concat(Encoding.Default.GetBytes(" AES CBC")).ToArray()).Length,
//                 string.Join(",",cipherTextData),
//                 cipherTextData.Length));
//             byte[] plainTextData =  cbcPaddedDecrypt(keyParamWithIV, cipherTextData);
//             Console.WriteLine(string.Format("Decrypt CBC MODE padded\tcipherTextData:[{0}]\tLength:[{1}]\tplainTextData:[{2}]\tLength:[{3}]",
//                 string.Join(",",cipherTextData),
//                 cipherTextData.Length.ToString(),
//                 Encoding.Default.GetString(plainTextData),
//                 Encoding.Default.GetString(plainTextData).Length
//                 ));
//         }          
//
//         {
//             //  *** Example 10 – CFB Stream Mode Encryption/Decryption ***
//             Console.WriteLine();
//             Console.WriteLine("*** Example 10 – CFB Stream Mode Encryption/Decryption ***");
//             ExValues.cSharpFixedRandom.SetSeed(151);
//             ParametersWithIV keyParamWithIV = keyParameterGenerationWithIV(128, ExValues.sampleShortIVnonce);
//             byte[] cipherTextData =  cfbByteStreamEncrypt(keyParamWithIV, ExValues.sampleInput.Concat(Encoding.Default.GetBytes(" STREAM IDEA CFB")).ToArray());
//             Console.WriteLine(string.Format("Encrypt CFB Stream\tplainTextData:[{0}]\tLength:[{1}]\tcipherTextData:[{2}]\tLength:[{3}]",
//                 Encoding.Default.GetString(ExValues.sampleInput.Concat(Encoding.Default.GetBytes(" STREAM IDEA CFB")).ToArray()),
//                 Encoding.Default.GetString(ExValues.sampleInput.Concat(Encoding.Default.GetBytes(" STREAM IDEA CFB")).ToArray()).Length,
//                 string.Join(",",cipherTextData),
//                 cipherTextData.Length));
//             byte[] plainTextData =  cfbByteStreamDecrypt(keyParamWithIV, cipherTextData);
//             Console.WriteLine(string.Format("Decrypt CFB Stream\tcipherTextData:[{0}]\tLength:[{1}]\tplainTextData:[{2}]\tLength:[{3}]",
//                 string.Join(",",cipherTextData),
//                 cipherTextData.Length.ToString(),
//                 Encoding.Default.GetString(plainTextData),
//                 Encoding.Default.GetString(plainTextData).Length
//                 ));            
//         }
//
//
//         {
//             //  *** Example 11 – CTR Mode Without Padding Encryption/Decryption ***
//             Console.WriteLine();
//             Console.WriteLine("*** Example 11 – CTR Mode Without Padding Encryption/Decryption ***");
//             ExValues.cSharpFixedRandom.SetSeed(151);
//             ParametersWithIV keyParamWithIV = keyParameterGenerationWithIV(256, ExValues.sampleIVlongCounter);
//             byte[] cipherTextData =  ctrNoPaddingEncrypt(keyParamWithIV, ExValues.sampleInput.Concat(Encoding.Default.GetBytes(" BLOCK CIPHER THREEFISH CTR")).ToArray());
//             Console.WriteLine(string.Format("Encrypt CTR block\tplainTextData:[{0}]\tLength:[{1}]\tcipherTextData:[{2}]\tLength:[{3}]",
//                 Encoding.Default.GetString(ExValues.sampleInput.Concat(Encoding.Default.GetBytes(" BLOCK CIPHER THREEFISH CTR")).ToArray()),
//                 Encoding.Default.GetString(ExValues.sampleInput.Concat(Encoding.Default.GetBytes(" BLOCK CIPHER THREEFISH CTR")).ToArray()).Length,
//                 string.Join(",",cipherTextData),
//                 cipherTextData.Length));
//             byte[] plainTextData =  ctrNoPaddingDecrypt(keyParamWithIV, cipherTextData);
//             Console.WriteLine(string.Format("Decrypt CTR block\tcipherTextData:[{0}]\tLength:[{1}]\tplainTextData:[{2}]\tLength:[{3}]",
//                 string.Join(",",cipherTextData),
//                 cipherTextData.Length.ToString(),
//                 Encoding.Default.GetString(plainTextData),
//                 Encoding.Default.GetString(plainTextData).Length
//                 ));            
//         }
//         
//         {
//             //  *** Example 12 – CCM AEAD Mode Without Padding Encryption/Decryption ***
//             Console.WriteLine();
//             Console.WriteLine("*** Example 12 – CCM AEAD Mode Without Padding Encryption/Decryption ***");
//             ExValues.cSharpFixedRandom.SetSeed(151);
//             KeyParameter keyParam = (KeyParameter)(keyParameterGeneration(256));
//             byte[] cipherTextData = ccmAEADNoPaddingEncrypt(keyParam,ExValues.sampleInput.Concat(Encoding.Default.GetBytes(" AEAD WITH AES CIPHER CCM")).ToArray());
//             Console.WriteLine(string.Format("Encrypt CCM AEAD \tplainTextData:[{0}]\tLength:[{1}]\tcipherTextData:[{2}]\tLength:[{3}]",
//                 Encoding.Default.GetString(ExValues.sampleInput.Concat(Encoding.Default.GetBytes(" AEAD WITH AES CIPHER CCM")).ToArray()),
//                 Encoding.Default.GetString(ExValues.sampleInput.Concat(Encoding.Default.GetBytes(" AEAD WITH AES CIPHER CCM")).ToArray()).Length,
//                 string.Join(",",cipherTextData),
//                 cipherTextData.Length));
//             byte[] plainTextData =  ccmAEADNoPaddingDecrypt(keyParam, cipherTextData);
//             Console.WriteLine(string.Format("Decrypt CCM AEAD\tcipherTextData:[{0}]\tLength:[{1}]\tplainTextData:[{2}]\tLength:[{3}]",
//                 string.Join(",",cipherTextData),
//                 cipherTextData.Length.ToString(),
//                 Encoding.Default.GetString(plainTextData),
//                 Encoding.Default.GetString(plainTextData).Length
//                 ));            
//         }
//         
//         Console.WriteLine("====================================");
//         Console.WriteLine("End   Symmetric Cipher Examples.Main");
//         Console.WriteLine("====================================");
//         Console.WriteLine();
//         Console.WriteLine();        
//     }
//     
//     /// <summary>
//     /// Generate appropriate random key 
//     /// </summary>        
//     public static ICipherParameters keyParameterGeneration(int keySize)
//     {
//         CipherKeyGenerator keyGen = new CipherKeyGenerator();        
//         keyGen.Init(new KeyGenerationParameters(ExValues.cSharpFixedRandom, keySize));
//         KeyParameter keyParam = keyGen.GenerateKeyParameter();
//         return keyParam;
//     }
//
//     /// <summary>
//     /// Generate key from a given set of bytes
//     /// </summary>        
//     public static ICipherParameters keyParameterGeneration(byte[] myKey)
//     {
//         ICipherParameters keyParam = new KeyParameter(myKey);        
//         return keyParam;
//     }
//     
//     /// <summary>
//     /// Example 7 – ECB Mode Symmetric Cipher Encryption
//     /// </summary>
//     public static byte[] ecbPaddedEncrypt(ICipherParameters keyParam, byte[] plainTextData)
//     {
//         // First choose the "engine", in this case AES 
//         IBlockCipher symmetricBlockCipher = new AesEngine();
//         
//         // Next select the mode compatible with the "engine", in this case we use the simple ECB mode
//         IBlockCipherMode symmetricBlockMode = new EcbBlockCipher(symmetricBlockCipher);
//
//         // Finally select a compatible padding, PKCS7 which is the default
//         IBlockCipherPadding padding  = new Pkcs7Padding();
//         
//         // apply the mode and engine on the plainTextData
//         PaddedBufferedBlockCipher ecbCipher = new PaddedBufferedBlockCipher(symmetricBlockMode, padding);            
//         ecbCipher.Init(true, keyParam);
//         int blockSize = ecbCipher.GetBlockSize();        
//         byte[]  cipherTextData = new byte[ecbCipher.GetOutputSize(plainTextData.Length)];
//         int processLength = ecbCipher.ProcessBytes(plainTextData, 0, plainTextData.Length, cipherTextData, 0);
//         int finalLength = ecbCipher.DoFinal(cipherTextData, processLength);
//         byte[] finalCipherTextData = new byte[cipherTextData.Length - (blockSize - finalLength)];
//         Array.Copy(cipherTextData,0,finalCipherTextData,0,finalCipherTextData.Length);
//         return finalCipherTextData;
//     }
//     
//     /// <summary>
//     /// Example 7 – ECB Mode Symmetric Cipher Decryption
//     /// </summary>
//     public static byte[] ecbPaddedDecrypt(ICipherParameters keyParam, byte[] cipherTextData)
//     {
//         // First choose the "engine", in this case AES 
//         IBlockCipher symmetricBlockCipher = new AesEngine();
//         
//         // Next select the mode compatible with the "engine", in this case we use the simple ECB mode
//         IBlockCipherMode symmetricBlockMode = new EcbBlockCipher(symmetricBlockCipher);
//
//         // Finally select a compatible padding, PKCS7 which is the default
//         IBlockCipherPadding padding  = new Pkcs7Padding();
//         
//         // apply the mode and engine on the cipherTextData
//         PaddedBufferedBlockCipher ecbCipher = new PaddedBufferedBlockCipher(symmetricBlockMode, padding);            
//         ecbCipher.Init(false, keyParam);
//         int blockSize = ecbCipher.GetBlockSize();
//         byte[] plainTextData = new byte[ecbCipher.GetOutputSize(cipherTextData.Length)];
//         int processLength  = ecbCipher.ProcessBytes(cipherTextData, 0, cipherTextData.Length, plainTextData, 0);
//         int finalLength = ecbCipher.DoFinal(plainTextData, processLength);
//         byte[] finalPlainTextData = new byte[plainTextData.Length - (blockSize - finalLength)];
//         Array.Copy(plainTextData,0,finalPlainTextData,0,finalPlainTextData.Length);
//         return finalPlainTextData;
//     }   
//     
//     /// <summary>
//     /// Example 8 – Key and Initialisation Vector Generation
//     /// </summary>        
//     public static ParametersWithIV keyParameterGenerationWithIV(int keySize, byte[] IV)
//     {
//         CipherKeyGenerator keyGen = new CipherKeyGenerator();        
//         keyGen.Init(new KeyGenerationParameters(ExValues.cSharpFixedRandom, keySize));
//         KeyParameter keyParam = keyGen.GenerateKeyParameter();
//         ParametersWithIV keyParamWithIV = new ParametersWithIV(keyParam, IV);
//         return keyParamWithIV;
//     }
//     
//     /// <summary>
//     /// Example 9 – CBC Mode With IV Encryption
//     /// </summary>        
//     public static byte[] cbcPaddedEncrypt(ICipherParameters keyParamWithIV, byte[] plainTextData)
//     {
//         IBlockCipher symmetricBlockCipher = new AesEngine();       
//         IBlockCipherMode symmetricBlockMode = new CbcBlockCipher(symmetricBlockCipher);
//         IBlockCipherPadding padding  = new Pkcs7Padding();
//         
//         PaddedBufferedBlockCipher cbcCipher = new PaddedBufferedBlockCipher(symmetricBlockMode, padding);            
//         
//         cbcCipher.Init(true, keyParamWithIV);
//         int blockSize = cbcCipher.GetBlockSize();        
//         byte[]  cipherTextData = new byte[cbcCipher.GetOutputSize(plainTextData.Length)];
//         int processLength = cbcCipher.ProcessBytes(plainTextData, 0, plainTextData.Length, cipherTextData, 0);
//         int finalLength = cbcCipher.DoFinal(cipherTextData, processLength);
//         byte[] finalCipherTextData = new byte[cipherTextData.Length - (blockSize - finalLength)];
//         Array.Copy(cipherTextData,0,finalCipherTextData,0,finalCipherTextData.Length);
//         return finalCipherTextData;
//     }
//   
//
//     /// <summary>
//     /// Example 9 – CBC Mode With IV Decryption
//     /// </summary>        
//     public static byte[] cbcPaddedDecrypt(ICipherParameters keyParamWithIV, byte[] cipherTextData)
//     {
//         IBlockCipher symmetricBlockCipher = new AesEngine();        
//         IBlockCipherMode symmetricBlockMode = new CbcBlockCipher(symmetricBlockCipher);
//         IBlockCipherPadding padding  = new Pkcs7Padding();
//         
//         PaddedBufferedBlockCipher cbcCipher = new PaddedBufferedBlockCipher(symmetricBlockMode, padding);            
//         
//         cbcCipher.Init(false, keyParamWithIV);
//         int blockSize = cbcCipher.GetBlockSize();        
//         byte[]  plainTextData = new byte[cbcCipher.GetOutputSize(cipherTextData.Length)];
//         int processLength = cbcCipher.ProcessBytes(cipherTextData, 0, cipherTextData.Length, plainTextData, 0);
//         int finalLength = cbcCipher.DoFinal(plainTextData, processLength);
//         byte[] finalPlainTextData = new byte[plainTextData.Length - (blockSize - finalLength)];
//         Array.Copy(plainTextData,0,finalPlainTextData,0,finalPlainTextData.Length);
//         return finalPlainTextData;
//     }
//   
//   
//     /// <summary>
//     /// Example 10 – CBC Mode Used as a Stream Encryption
//     /// </summary>    
//     public static byte[] cfbByteStreamEncrypt(ICipherParameters keyParamWithIV, byte[] plainTextData)
//     {
//         // First choose the "engine", in this case IDEA 
//         IBlockCipher symmetricBlockCipher = new IdeaEngine();
//         
//         // Next select the mode compatible with the "engine", in this case we 
//         // use CFB mode as a streaming cipher - set the block size to 1 byte
//         IBlockCipherMode symmetricBlockMode = new CfbBlockCipher(symmetricBlockCipher, 8);
//         
//         // apply the mode and engine on the cipherTextData
//         StreamBlockCipher cfbCipher = new StreamBlockCipher(symmetricBlockMode);            
//         
//         cfbCipher.Init(true, keyParamWithIV);
//         byte[] cipherTextData = new byte[plainTextData.Length];
//         
//         // simulate stream
//         for (int j = 0; j < plainTextData.Length; j++)
//         {
//             cipherTextData[j] = cfbCipher.ReturnByte(plainTextData[j]);
//         }
//         return cipherTextData;
//     }
//     
//
//     /// <summary>
//     /// Example 10 – CBC Mode Used as a Stream Deccryption
//     /// </summary>        
//     public static byte[] cfbByteStreamDecrypt(ICipherParameters keyParamWithIV, byte[] cipherTextData)
//     {
//         // First choose the "engine", in this case IDEA 
//         IBlockCipher symmetricBlockCipher = new IdeaEngine();
//         
//         // Next select the mode compatible with the "engine", in this case we 
//         // use CFB mode as a streaming cipher - set the block size to 1 byte
//         IBlockCipherMode symmetricBlockMode = new CfbBlockCipher(symmetricBlockCipher, 8);
//         
//         // apply the mode and engine on the cipherTextData
//         StreamBlockCipher cfbCipher = new StreamBlockCipher(symmetricBlockMode);            
//         
//         cfbCipher.Init(false, keyParamWithIV);
//         byte[] plainTextData = new byte[cipherTextData.Length];
//
//         // simulate stream
//         for (int j = 0; j < cipherTextData.Length; j++)
//         {
//             plainTextData[j] = cfbCipher.ReturnByte(cipherTextData[j]);
//         }
//         return plainTextData;
//     }
//
//  
//     /// <summary>
//     /// Example 11 – CTR Mode Without Padding Encryption
//     /// </summary>        
//     public static byte[] ctrNoPaddingEncrypt(ICipherParameters keyParamWithIV, byte[] plainTextData)
//     {
//         // First choose the "engine", in this case  
//         IBlockCipher symmetricBlockCipher = new ThreefishEngine(256);
//         
//         // Next select the mode compatible with the "engine"
//         IBlockCipherMode symmetricBlockMode = new KCtrBlockCipher(symmetricBlockCipher);
//         
//         // apply the mode and engine on the cipherTextData
//         BufferedBlockCipher ctrCipher = new BufferedBlockCipher(symmetricBlockMode);            
//         
//         ctrCipher.Init(true, keyParamWithIV);
//         int blockSize = ctrCipher.GetBlockSize();        
//         byte[]  cipherTextData = new byte[ctrCipher.GetOutputSize(plainTextData.Length)];
//         int processLength = ctrCipher.ProcessBytes(plainTextData, 0, plainTextData.Length, cipherTextData, 0);
//         int finalLength = ctrCipher.DoFinal(cipherTextData, processLength);
//         byte[] finalCipherTextData = new byte[cipherTextData.Length - (blockSize - finalLength)];
//         Array.Copy(cipherTextData,0,finalCipherTextData,0,finalCipherTextData.Length);
//         return cipherTextData;
//     }
//
//     /// <summary>
//     /// Example 11 – CTR Mode Without Padding Decryption
//     /// </summary>        
//     public static byte[] ctrNoPaddingDecrypt(ICipherParameters keyParamWithIV, byte[] cipherTextData)
//     {
//         // First choose the "engine"
//         IBlockCipher symmetricBlockCipher = new ThreefishEngine(256);
//         
//         // Next select the mode compatible with the "engine"
//         IBlockCipherMode symmetricBlockMode = new KCtrBlockCipher(symmetricBlockCipher);
//         
//         // apply the mode and engine on the cipherTextData
//         BufferedBlockCipher ctrCipher = new BufferedBlockCipher(symmetricBlockMode);            
//         
//         ctrCipher.Init(false, keyParamWithIV);
//         int blockSize = ctrCipher.GetBlockSize();        
//         byte[]  plainTextData = new byte[ctrCipher.GetOutputSize(cipherTextData.Length)];
//         int processLength = ctrCipher.ProcessBytes(cipherTextData, 0, cipherTextData.Length, plainTextData, 0);
//         int finalLength = ctrCipher.DoFinal(plainTextData, processLength);
//         byte[] finalPlainTextData = new byte[plainTextData.Length - (blockSize - finalLength)];
//         Array.Copy(plainTextData,0,finalPlainTextData,0,finalPlainTextData.Length);
//         return plainTextData;
//     }
//     
//     
//     /// <summary>
//     /// Example 12 – CCM AEAD Mode Without Padding Encryption
//     /// </summary>        
//     public static byte[] ccmAEADNoPaddingEncrypt(KeyParameter keyParam, byte[] plainTextData)
//     {
//         IBlockCipher symmetricBlockCipher = new AesEngine();        
//         int macSize = 8*symmetricBlockCipher.GetBlockSize();
//         byte[] nonce = new byte[12];
//         byte[] associatedText = ExValues.additionalAuthenticatedDataA;
//         Array.Copy(ExValues.sampleIVnonce, nonce, nonce.Length);
//         AeadParameters keyParamAead = new AeadParameters(keyParam, macSize, nonce, associatedText);
//
//         CcmBlockCipher cipherMode = new CcmBlockCipher(symmetricBlockCipher);        
//         cipherMode.Init(true,keyParamAead);
//         cipherMode.ProcessBytes(plainTextData, 0, plainTextData.Length, null, 0);
//         
//         int outputSize = cipherMode.GetOutputSize(0);
//         byte[]  cipherTextData = new byte[outputSize];
//         cipherMode.DoFinal(cipherTextData, 0);
//         return cipherTextData;
//     }
//
//     /// <summary>
//     /// Example 12 – CCM AEAD Mode Without Padding Decryption
//     /// </summary>        
//     public static byte[] ccmAEADNoPaddingDecrypt(KeyParameter keyParam, byte[] cipherTextData)
//     {
//         IBlockCipher symmetricBlockCipher = new AesEngine();        
//         int macSize = 8*symmetricBlockCipher.GetBlockSize();
//         byte[] nonce = new byte[12];
//         byte[] associatedText = ExValues.additionalAuthenticatedDataA;
//         Array.Copy(ExValues.sampleIVnonce, nonce, nonce.Length);
//         AeadParameters keyParamAead = new AeadParameters(keyParam, macSize, nonce, associatedText);
//
//         CcmBlockCipher cipherMode = new CcmBlockCipher(symmetricBlockCipher);        
//         cipherMode.Init(false,keyParamAead);
//         cipherMode.ProcessBytes(cipherTextData, 0, cipherTextData.Length, null, 0);
//         
//         int outputSize = cipherMode.GetOutputSize(0);
//         byte[]  plainTextData = new byte[outputSize];
//         cipherMode.DoFinal(plainTextData , 0);
//         return plainTextData;
//     }
//
//
//
// }
