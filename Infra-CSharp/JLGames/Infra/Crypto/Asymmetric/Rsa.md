# RSA

## PKCS#1

### **PKCS#1 公钥结构**

公钥的 **ASN.1 结构** 只有 **两个整数（INTEGER）**：

```
SEQUENCE {
    modulus INTEGER      -- 公钥模数
    publicExponent INTEGER -- 公钥指数（通常是 65537）
}
```

- `modulus` 是 **公钥的模数**（N）。
- `publicExponent` 是 **公钥的指数**（E）。
- **没有私钥部分**，只有公钥参数。

### **PKCS#1 私钥结构**

PKCS#1 私钥格式比公钥复杂，包含 **完整的 RSA 私钥参数**：

```
SEQUENCE {
    version INTEGER (0 for v1, 1 for v2)
    modulus INTEGER           -- N
    publicExponent INTEGER     -- E
    privateExponent INTEGER    -- D
    prime1 INTEGER             -- P
    prime2 INTEGER             -- Q
    exponent1 INTEGER          -- D mod (P-1)
    exponent2 INTEGER          -- D mod (Q-1)
    coefficient INTEGER        -- (Q^-1) mod P
}
```

- 包含 **完整的 RSA 私钥参数**，包括 **modulus、exponent、prime factors**。
- `prime1` 和 `prime2` 是用于 **加速解密** 的两个质数。
- `coefficient` 计算辅助因子，提高私钥运算效率。

### **PKCS#8 私钥结构（适用于所有算法）**

PKCS#8 是 **通用私钥格式**，适用于 **RSA、ECDSA、DSA** 等多种算法：

```
SEQUENCE {
    version INTEGER (0)
    privateKeyAlgorithm SEQUENCE {
        algorithm OBJECT IDENTIFIER
        parameters ANY OPTIONAL
    }
    privateKey OCTET STRING
    attributes [0] IMPLICIT SET OF Attribute OPTIONAL
}
```

- **支持多种算法**，不像 PKCS#1 仅限于 RSA。
- `privateKeyAlgorithm` 通过 **OID** 指定算法（如 `rsaEncryption`）。
- `privateKey` 是一个 **OCTET STRING**，其中包含 **PKCS#1 私钥数据**（如果是 RSA）。

### **X.509 公钥结构（适用于所有算法）**

X.509公钥 是 **通用公钥格式**，适用于 **RSA、ECDSA、DSA** 等多种算法：

```
SubjectPublicKeyInfo ::= SEQUENCE {
    algorithm              AlgorithmIdentifier,
    subjectPublicKey       BIT STRING
}
AlgorithmIdentifier ::= SEQUENCE {
    algorithm              OBJECT IDENTIFIER,                -- 公钥算法
    parameters             ANY DEFINED BY algorithm OPTIONAL -- 算法相关的参数
}
subjectPublicKey ::= {
    [0] 前导比特数
    公钥参数 ::= SEQUENCE {
        modulus INTEGER        -- 公钥模数
        publicExponent INTEGER -- 公钥指数（通常是 65537）
    }
}
```

- **支持多种算法**，不像 PKCS#1 仅限于 RSA。
- `privateKeyAlgorithm` 通过 **OID** 指定算法（如 `rsaEncryption`）。
- `subjectPublicKey` 是一个 **BIT STRING**，其中包含 **前导比特数、公钥参数**（如果是 RSA）。
