using System;
using System.Security.Cryptography;
using JLGames.Infra.Crypto.ASN1;

namespace JLGames.Infra.Crypto.Asymmetric
{
    public static class RsaParamUtils
    {
        /// <summary>
        /// 把Der数据解释为 X.509 公钥参数
        /// ASN.1结构：
        /// SubjectPublicKeyInfo ::= SEQUENCE {
        ///     algorithm              AlgorithmIdentifier,
        ///     subjectPublicKey       BIT STRING
        /// }
        /// AlgorithmIdentifier ::= SEQUENCE {
        ///     algorithm              OBJECT IDENTIFIER,
        ///     parameters             ANY DEFINED BY algorithm OPTIONAL
        /// }
        /// </summary>
        /// <param name="derBytes"></param>
        /// <returns></returns>
        public static RSAParameters DecodeX509Params(byte[] derBytes)
        {
            using (var reader = new TLVAdvancedReader(derBytes))
            {
                reader.ReadSequence(false); // SEQUENCE

                // algorithm              AlgorithmIdentifier
                var oid = reader.ReadRsaOid(); // AlgorithmIdentifier
                if (!CryptoUtils.AssertIsRsaOid(oid.Value)) // 验证算法为RSA
                {
                    Console.WriteLine($"Oid:{oid}");
                    throw new Exception("Invalid algorithm identifier for RSA.");
                }

                // subjectPublicKey       BIT STRING
                var subjectPublicKey = reader.ReadBitString(); // BIT STRING
                using (var subReader = new TLVAdvancedReader(subjectPublicKey.Value))
                {
                    subReader.ReadSequence(false);
                    var modulus = subReader.ReadInteger();
                    var exponent = subReader.ReadInteger();

                    // Console.WriteLine($"X509: Modulus:[{string.Join(" ", modulus)}], Exponent:[{string.Join(" ", exponent)}],");
                    return new RSAParameters
                    {
                        Modulus = modulus,
                        Exponent = exponent,
                    };
                }
            }
        }


        /// <summary>
        /// 把Der数据解释为 PKCS#8 私钥参数
        /// ASN.1结构：
        /// PKCS8PrivateKeyInfo ::= SEQUENCE {
        ///     version         INTEGER,                         -- 版本号，通常为 0
        ///     privateKeyAlgorithm AlgorithmIdentifier,         -- 私钥算法标识符
        ///     privateKey      OCTET STRING,                    -- 私钥本体（DER编码的私钥数据）
        ///     attributes      [0] IMPLICIT SET OF Attribute OPTIONAL
        /// }
        /// AlgorithmIdentifier ::= SEQUENCE {
        ///     algorithm       OBJECT IDENTIFIER,              -- 公钥算法OID，如RSA的OID为 1.2.840.113549.1.1.1
        ///     parameters      ANY DEFINED BY algorithm OPTIONAL
        /// }
        /// privateKey ::= SEQUENCE {
        ///     version            INTEGER,                     -- 版本号，通常为 0
        ///     modulus            INTEGER,                     -- 模数 N
        ///     publicExponent     INTEGER,                     -- 公钥指数 e
        ///     privateExponent    INTEGER,                     -- 私钥指数 d
        ///     prime1             INTEGER,                     -- 第一个素数 p
        ///     prime2             INTEGER,                     -- 第二个素数 q
        ///     exponent1          INTEGER,                     -- 第一个指数 dp (d mod (p-1))
        ///     exponent2          INTEGER,                     -- 第二个指数 dq (d mod (q-1))
        ///     coefficient        INTEGER                      -- 反转系数 iq (q^-1 mod p)
        /// }
        /// 
        /// 详细解析
        /// 1. PKCS8PrivateKeyInfo：这是最外层的结构，表示PKCS#8格式的私钥信息。它是一个SEQUENCE，包含以下三个字段：
        ///    - version：版本号，通常是0。
        ///    - privateKeyAlgorithm：一个AlgorithmIdentifier，指示所使用的加密算法（如RSA、DSA等）。
        ///    - privateKey：这是一个OCTET STRING，包含编码后的私钥数据。
        /// 2. AlgorithmIdentifier：这是一个算法标识符，它包含：
        ///    - algorithm：一个OID（对象标识符），指示私钥使用的加密算法（例如RSA的OID是1.2.840.113549.1.1.1）。
        ///    - parameters：这部分是可选的，它可以包含与算法相关的参数。例如，在RSA算法中，它通常是空的。
        /// 3. PrivateKey：这是实际的私钥数据，存储在一个SEQUENCE中，包含了RSA私钥的所有必需参数：
        ///    - version：私钥的版本号，通常为0。
        ///    - modulus（N）：模数，是RSA密钥对的核心部分。
        ///    - publicExponent（e）：公钥指数。
        ///    - privateExponent（d）：私钥指数。
        ///    - prime1（p）：第一个素数。
        ///    - prime2（q）：第二个素数。
        ///    - exponent1（dp）：私钥指数d对(p-1)的模（即d mod (p-1)）。
        ///    - exponent2（dq）：私钥指数d对(q-1)的模（即d mod (q-1)）。
        ///    - coefficient（iq）：反转系数，计算q^-1 mod p。
        /// </summary>
        /// <param name="deBytes"></param>
        /// <returns></returns>
        public static RSAParameters DecodePkcs8Params(byte[] deBytes)
        {
            // Console.WriteLine($"Der:[{string.Join(" ", Array.ConvertAll(deBytes, b => $"0x{b:X2}"))}]");
            using (var reader = new TLVAdvancedReader(deBytes))
            {
                reader.ReadSequence(false);
                reader.ReadInteger();
                reader.ReadRsaOid();

                reader.ReadOctetString(false);
                reader.ReadSequence(false);
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
        /// 把Der数据解释为 PKCS#1 v1.5 的RSA公钥参数
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
            using (var reader = new TLVAdvancedReader(derBytes))
            {
                reader.ReadSequence(false);
                var modulus = reader.ReadInteger();
                var publicExponent = reader.ReadInteger();
                // Console.WriteLine($"Pkcs1: Modulus:[{string.Join(" ", modulus)}], Exponent:[{string.Join(" ", publicExponent)}],");
                return new RSAParameters
                {
                    Modulus = modulus,
                    Exponent = publicExponent,
                };
            }
        }

        /// <summary>
        /// 把Der数据解释为 PKCS#1 v1.5 的RSA私钥参数
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
            using (var reader = new TLVAdvancedReader(derBytes))
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