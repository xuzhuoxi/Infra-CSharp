// using System;
// using System.IO;
// using System.Text;
// using System.Security.Cryptography;
//
// namespace JLGames.Infra.Crypto
// {
//     public static class RijndaelUtil
//     {
//         /// <summary>
//         /// Note: the object is different each time it is run
//         /// 注意：每次运行时对象是不同的
//         /// </summary>
//         private static readonly Rijndael m_DefaultRijndael = Rijndael.Create();
//
//         public static byte[] Encrypted(string plainText)
//         {
//             return Encrypted(plainText, m_DefaultRijndael.Key, m_DefaultRijndael.IV);
//         }
//
//         public static byte[] Encrypted(string plainText, string key, string iv)
//         {
//             if (key == null || key.Length <= 0)
//                 throw new ArgumentNullException("Key");
//             if (iv == null || iv.Length <= 0)
//                 throw new ArgumentNullException("IV");
//             var byteKey = Encoding.UTF8.GetBytes(key);
//             var byteIV = Encoding.UTF8.GetBytes(iv);
//             return Encrypted(plainText, byteKey, byteIV);
//         }
//
//         public static byte[] Encrypted(string plainText, byte[] key, byte[] iv)
//         {
//             // Check arguments.
//             if (plainText == null || plainText.Length <= 0)
//                 throw new ArgumentNullException("plainText");
//             if (key == null || key.Length <= 0)
//                 throw new ArgumentNullException("Key");
//             if (iv == null || iv.Length <= 0)
//                 throw new ArgumentNullException("IV");
//             byte[] encrypted;
//             // Create an Rijndael object
//             // with the specified key and IV.
//             using (var rijAlg = Rijndael.Create())
//             {
//                 rijAlg.Key = key;
//                 rijAlg.IV = iv;
//                 // Create an encryptor to perform the stream transform.
//                 var encryptor = rijAlg.CreateEncryptor(rijAlg.Key, rijAlg.IV);
//                 // Create the streams used for encryption.
//                 using (var msEncrypt = new MemoryStream())
//                 {
//                     using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
//                     {
//                         using (var swEncrypt = new StreamWriter(csEncrypt))
//                         {
//                             //Write all data to the stream.
//                             swEncrypt.Write(plainText);
//                         }
//
//                         encrypted = msEncrypt.ToArray();
//                     }
//                 }
//             }
//
//             // Return the encrypted bytes from the memory stream.
//             return encrypted;
//         }
//
//         public static string Decrypted(byte[] cipherText)
//         {
//             return Decrypted(cipherText, m_DefaultRijndael.Key, m_DefaultRijndael.IV);
//         }
//
//         public static string Decrypted(byte[] cipherText, string key, string iv)
//         {
//             if (key == null || key.Length <= 0)
//                 throw new ArgumentNullException("Key");
//             if (iv == null || iv.Length <= 0)
//                 throw new ArgumentNullException("IV");
//             var byteKey = Encoding.UTF8.GetBytes(key);
//             var byteIV = Encoding.UTF8.GetBytes(iv);
//             return Decrypted(cipherText, byteKey, byteIV);
//         }
//
//         public static string Decrypted(byte[] cipherText, byte[] key, byte[] iv)
//         {
//             // Check arguments.
//             if (cipherText == null || cipherText.Length <= 0)
//                 throw new ArgumentNullException("cipherText");
//             if (key == null || key.Length <= 0)
//                 throw new ArgumentNullException("key");
//             if (iv == null || iv.Length <= 0)
//                 throw new ArgumentNullException("iv");
//
//             // Declare the string used to hold
//             // the decrypted text.
//             string plaintext = null;
//
//             // Create an Rijndael object
//             // with the specified key and IV.
//             using (var rijAlg = Rijndael.Create())
//             {
//                 rijAlg.Key = key;
//                 rijAlg.IV = iv;
//                 // Create a decryptor to perform the stream transform.
//                 var decryptor = rijAlg.CreateDecryptor(rijAlg.Key, rijAlg.IV);
//                 // Create the streams used for decryption.
//                 using (var msDecrypt = new MemoryStream(cipherText))
//                 {
//                     using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
//                     {
//                         using (var srDecrypt = new StreamReader(csDecrypt))
//                         {
//                             // Read the decrypted bytes from the decrypting stream
//                             // and place them in a string.
//                             plaintext = srDecrypt.ReadToEnd();
//                         }
//                     }
//                 }
//             }
//
//             return plaintext;
//         }
//
//         /*
//     
//         /// <summary>
//         /// Rijndael加密算法
//         /// </summary>
//         /// <param name="pString">待加密的明文</param>
//         /// <param name="pKey">密钥,长度可以为:64位(byte[8]),128位(byte[16]),192位(byte[24]),256位(byte[32])</param>
//         /// <param name="iv">iv向量,长度为128（byte[16])</param>
//         /// <returns></returns>
//         public static string encrypted(string pString, string pKey)
//         {
//             //密钥
//             byte[] keyArray = UTF8Encoding.UTF8.GetBytes(pKey);
//             //待加密明文数组
//             byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(pString);
//     
//             //Rijndael解密算法
//             RijndaelManaged rDel = new RijndaelManaged();
//             rDel.Key = keyArray;
//             rDel.Mode = CipherMode.ECB;
//             rDel.Padding = PaddingMode.PKCS7;
//             ICryptoTransform cTransform = rDel.CreateEncryptor();
//     
//             //返回加密后的密文
//             byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
//             return Convert.ToBase64String(resultArray, 0, resultArray.Length);
//         }
//     
//         /// <summary>
//         /// ijndael解密算法
//         /// </summary>
//         /// <param name="pString">待解密的密文</param>
//         /// <param name="pKey">密钥,长度可以为:64位(byte[8]),128位(byte[16]),192位(byte[24]),256位(byte[32])</param>
//         /// <param name="iv">iv向量,长度为128（byte[16])</param>
//         /// <returns></returns>
//         public static String decrypted(string pString, string pKey)
//         {
//             //解密密钥
//             byte[] keyArray = UTF8Encoding.UTF8.GetBytes(pKey);
//             //待解密密文数组
//             byte[] toEncryptArray = Convert.FromBase64String(pString);
//     
//             //Rijndael解密算法
//             RijndaelManaged rDel = new RijndaelManaged();
//             rDel.Key = keyArray;
//             rDel.Mode = CipherMode.ECB;
//             rDel.Padding = PaddingMode.PKCS7;
//             ICryptoTransform cTransform = rDel.CreateDecryptor();
//     
//             //返回解密后的明文
//             byte[] resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
//             return UTF8Encoding.UTF8.GetString(resultArray);
//         }
//         */
//     }
// }