using System;
using System.Security.Cryptography;
using JLGames.Infra.Crypto.ASN1;

namespace JLGames.Infra.Crypto.Asymmetric
{
    public static class RsaParamUtils
    {
        /// <summary>
        /// 把Der数据解释为PKCS#1 v1.5的RSA公钥参数
        /// ASN.1结构：
        /// SEQUENCE {
        ///     modulus INTEGER      -- 公钥模数
        ///     publicExponent INTEGER -- 公钥指数（通常是 65537）
        /// }
        /// </summary>
        /// <param name="derBytes"></param>
        /// <returns></returns>
        public static RSAParameters DecodePkcs1V15Public(byte[] derBytes)
        {
            using (var reader = new TLVReader(derBytes))
            {
                reader.ReadBlockNoValue(DerTags.SEQUENCE);
                var modulus = reader.ReadInteger();
                var publicExponent = reader.ReadInteger();
                return new RSAParameters
                {
                    Modulus = modulus,
                    Exponent = publicExponent,
                };
            }
        }

        /// <summary>
        /// 把Der数据解释为PKCS#1 v1.5的RSA私钥参数
        /// ASN.1结构：
        /// SEQUENCE {
        ///     version INTEGER (0)        
        ///     modulus INTEGER            -- N
        ///     publicExponent INTEGER     -- E
        ///     privateExponent INTEGER    -- D
        ///     prime1 INTEGER             -- P
        ///     prime2 INTEGER             -- Q
        ///     exponent1 INTEGER          -- D mod (P-1)
        ///     exponent2 INTEGER          -- D mod (Q-1)
        ///     coefficient INTEGER        -- (Q^-1) mod P
        /// }
        /// 不支持多素数RSA结构:
        /// SEQUENCE {
        ///     version INTEGER (1)
        ///     modulus INTEGER            -- N
        ///     publicExponent INTEGER     -- E
        ///     privateExponent INTEGER    -- D
        ///     prime1 INTEGER             -- P
        ///     prime2 INTEGER             -- Q
        ///     exponent1 INTEGER          -- D mod (P-1)
        ///     exponent2 INTEGER          -- D mod (Q-1)
        ///     coefficient INTEGER        -- (Q^-1) mod P
        ///     SEQUENCE {
        ///         INTEGER prime3
        ///         INTEGER exponent3
        ///         INTEGER coefficient3
        ///     }
        /// }
        /// </summary>
        /// <param name="derBytes"></param>
        /// <returns></returns>
        public static RSAParameters DecodePkcs1V15Private(byte[] derBytes)
        {
            using (var reader = new TLVReader(derBytes))
            {
                reader.ReadBlockNoValue(DerTags.SEQUENCE);
                reader.ReadInteger(); // version
                var modulus = reader.ReadInteger();
                var publicExponent = reader.ReadInteger();
                var privateExponent = reader.ReadInteger();
                var prime1 = reader.ReadInteger();
                var prime2 = reader.ReadInteger();
                var exponent1 = reader.ReadInteger();
                var exponent2 = reader.ReadInteger();
                var coefficient = reader.ReadInteger();
                return new RSAParameters
                {
                    Modulus = modulus,
                    Exponent = publicExponent,
                    D = privateExponent,
                    P = prime1,
                    Q = prime2,
                    DP = exponent1,
                    DQ = exponent2,
                    InverseQ = coefficient,
                };
            }
        }

        /// <summary>
        /// 从 PEM 格式的内容中提取 Base64的Der编码数据
        /// </summary>
        /// <param name="keyFileText"></param>
        /// <param name="keyType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public static byte[] ExtractDerData(string keyFileText, string keyType)
        {
            var header = $"-----BEGIN {keyType}-----";
            var start = keyFileText.IndexOf(header, StringComparison.Ordinal) + header.Length;

            var footer = $"-----END {keyType}-----";
            var end = keyFileText.IndexOf(footer, start, StringComparison.Ordinal);

            if (start < header.Length || end < 0)
                throw new ArgumentException("Invalid PEM format");

            var base64Data = keyFileText.Substring(start, end - start)
                .Replace("\n", "").Replace("\r", "");
            return Convert.FromBase64String(base64Data);
        }

        // /// <summary>
        // /// 从 PEM 格式提取 Base64 编码数据
        // /// </summary>
        // public static byte[] ExtractDerData(string pemString, string keyType)
        // {
        //     string header = $"-----BEGIN {keyType}-----";
        //     string footer = $"-----END {keyType}-----";
        //     int start = pemString.IndexOf(header) + header.Length;
        //     int end = pemString.IndexOf(footer, start);
        //
        //     if (start < header.Length || end < 0)
        //         throw new ArgumentException("Invalid PEM format");
        //
        //     string base64Data = pemString.Substring(start, end - start)
        //         .Replace("\n", "")
        //         .Replace("\r", "");
        //     return Convert.FromBase64String(base64Data);
        // }
    }
}