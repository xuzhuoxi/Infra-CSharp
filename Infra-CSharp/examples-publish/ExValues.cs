using System;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Crypto.Paddings;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.Utilities.Encoders;
using Org.BouncyCastle.Crypto.Utilities;
using Org.BouncyCastle.Utilities.IO;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.Crypto.Prng;

public class ExValues
{
    public static readonly long thirty_days = 1000L * 60 * 60 * 24 * 30;
    public static readonly byte[] sampleAesKeyInput = Hex.Decode("000102030405060708090a0b0c0d0e0f");

    public static readonly byte[] sampleTripleDesKeyInput =
                Hex.Decode("000102030405060708090a0b0c0d0e0f1011121314151617");
                

    public static readonly byte[] sampleShortIVnonce = Strings.ToByteArray("12345678");
    public static readonly byte[] sampleIVnonce = Strings.ToByteArray("0123456789123456");
    public static readonly byte[] sampleIVcounter = Strings.ToByteArray("01234567891234");
    public static readonly byte[] sampleIVlongCounter = Strings.ToByteArray("0123456789012345678912345678");
    public static readonly byte[] sampleInput = Strings.ToByteArray("Hello World!");
    public static readonly byte[] sampleNonce = Strings.ToByteArray("number only used once");

    public static readonly byte[] sampleTwoBlockInput =
                Strings.ToByteArray("Some cipher modes require more than one block");

    public static readonly byte[] personalizationString = 
                Strings.ToByteArray("a constant personal marker");

    public static readonly byte[] additionalAuthenticatedDataA = 
                    Strings.ToByteArray("This message was sent 29th Feb at 11.00am - does not repeat");
                    
    public static readonly byte[] additionalAuthenticatedDataB = 
                    Strings.ToByteArray("This message was sent 30th Feb at 11.00am - does not repeat");

    public static readonly byte[] sampleSignatureMessage =
                    Strings.ToByteArray("Once Upon a midnight dreary, while I pondered weak and weary");        

    public static readonly byte[] initiator = Strings.ToByteArray("Initiator");
    public static readonly byte[] recipient = Strings.ToByteArray("Recipient");
    public static readonly byte[] sampleUKM = Strings.ToByteArray("User keying material");

    // sample byte array multiple of 8 bytes - used for demonstrating key wrapping block aligned no padding
    public static readonly byte[] sampleUnpaddedKey = Strings.ToByteArray("this is my secret key123");
    // sample byte array which is not a multiple of 8 bytes - used for demonstraing padded wrapping
    public static readonly byte[] samplePaddedKey = Strings.ToByteArray("this is my secret key123456");
    
 
    public static byte[] SubArray(byte[] sourceArray, int sourceStart, int length) 
    {
        byte[] result = new byte[length];
        Array.Copy(sourceArray,sourceStart,result,0,length);
        return result;
    }

    public static SecureRandom cSharpFixedRandom  = new SecureRandom(new FixedRandomGenerator());
    
    public static byte[] FixedRandomBytes(int size)
    {
        byte[] bytes = new byte[size];
        fixedRandom.NextBytes(bytes);        
        return bytes;
    }

    public static int fixedRandomSeed = 151;
    public static Random fixedRandom  = new Random(fixedRandomSeed); 
    
	public class FixedRandomGenerator : IRandomGenerator
	{
        private static int _fixedRandomSeed = fixedRandomSeed;
        private static Random _fixedRandom = fixedRandom;

        public FixedRandomGenerator() {}
        
        public FixedRandomGenerator(int seed)
        {
            _fixedRandomSeed = seed;
            _fixedRandom = new Random(_fixedRandomSeed);            
        }
        
        public void AddSeedMaterial(byte[] seed) {}
        
        public void AddSeedMaterial(long seed) 
        {
            _fixedRandomSeed = (int)(seed);
            _fixedRandom = new Random(_fixedRandomSeed);
        }
        
#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
        public void AddSeedMaterial(ReadOnlySpan<byte> seed) {}
#endif

		public void NextBytes(byte[] bytes) 
        {
            _fixedRandom.NextBytes(bytes);
        }
        
        public void NextBytes(byte[] bytes, int start, int len)
        {
            byte[] result = new byte[len];
            _fixedRandom.NextBytes(result);
            Array.Copy(bytes, start, result, 0,len);
        }

		public byte[] NextBytes(int len) 
        {
            byte[] result = new byte[len];
            _fixedRandom.NextBytes(result);
            return result;
        }

#if NETCOREAPP2_1_OR_GREATER || NETSTANDARD2_1_OR_GREATER
		public void NextBytes(Span<byte> bytes)
        {
            byte[] result = bytes.ToArray();
            this.NextBytes(result);
            result.AsSpan(0, result.Length).CopyTo(bytes);
        }
#endif

	}    
    
}