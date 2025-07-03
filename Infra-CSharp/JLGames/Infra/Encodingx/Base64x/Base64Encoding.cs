namespace JLGames.Infra.Encodingx.Base64x
{
    public sealed class Base64StdEncoding : IBase64Encoding
    {
        public string EncodeToString(byte[] input)
        {
            return Base64Utils.EncodeToStdString(input);
        }

        public string EncodeToString(string input)
        {
            return Base64Utils.EncodeToStdString(input);
        }

        public byte[] EncodeToBytes(byte[] input)
        {
            return Base64Utils.EncodeToStdBytes(input);
        }

        public byte[] EncodeToBytes(string input)
        {
            return Base64Utils.EncodeToStdBytes(input);
        }

        public byte[] DecodeBytesFrom(byte[] input)
        {
            return Base64Utils.DecodeBytesFromStd(input);
        }

        public byte[] DecodeBytesFrom(string input)
        {
            return Base64Utils.DecodeBytesFromStd(input);
        }

        public string DecodeStringFrom(byte[] input)
        {
            return Base64Utils.DecodeStringFromStd(input);
        }

        public string DecodeStringFrom(string input)
        {
            return Base64Utils.DecodeStringFromStd(input);
        }
    }

    public sealed class Base64RawStdEncoding : IBase64Encoding
    {
        public string EncodeToString(byte[] input)
        {
            return Base64Utils.EncodeToRawStdString(input);
        }

        public string EncodeToString(string input)
        {
            return Base64Utils.EncodeToRawStdString(input);
        }

        public byte[] EncodeToBytes(byte[] input)
        {
            return Base64Utils.EncodeStrToRawStdBytes(input);
        }

        public byte[] EncodeToBytes(string input)
        {
            return Base64Utils.EncodeStrToRawStdBytes(input);
        }

        public byte[] DecodeBytesFrom(byte[] input)
        {
            return Base64Utils.DecodeBytesFromRawStd(input);
        }

        public byte[] DecodeBytesFrom(string input)
        {
            return Base64Utils.DecodeBytesFromRawStd(input);
        }

        public string DecodeStringFrom(byte[] input)
        {
            return Base64Utils.DecodeStringFromRawStd(input);
        }

        public string DecodeStringFrom(string input)
        {
            return Base64Utils.DecodeStringFromRawStd(input);
        }
    }

    public sealed class Base64UrlEncoding : IBase64Encoding
    {
        public string EncodeToString(byte[] input)
        {
            return Base64Utils.EncodeToUrlString(input);
        }

        public string EncodeToString(string input)
        {
            return Base64Utils.EncodeToUrlString(input);
        }

        public byte[] EncodeToBytes(byte[] input)
        {
            return Base64Utils.EncodeToUrlBytes(input);
        }

        public byte[] EncodeToBytes(string input)
        {
            return Base64Utils.EncodeToUrlBytes(input);
        }

        public byte[] DecodeBytesFrom(byte[] input)
        {
            return Base64Utils.DecodeBytesFromUrl(input);
        }

        public byte[] DecodeBytesFrom(string input)
        {
            return Base64Utils.DecodeBytesFromUrl(input);
        }

        public string DecodeStringFrom(byte[] input)
        {
            return Base64Utils.DecodeStringFromUrl(input);
        }

        public string DecodeStringFrom(string input)
        {
            return Base64Utils.DecodeStringFromUrl(input);
        }
    }

    public sealed class Base64RawUrlEncoding : IBase64Encoding
    {
        public string EncodeToString(byte[] input)
        {
            return Base64Utils.EncodeToRawUrlString(input);
        }

        public string EncodeToString(string input)
        {
            return Base64Utils.EncodeToRawUrlString(input);
        }

        public byte[] EncodeToBytes(byte[] input)
        {
            return Base64Utils.EncodeToRawUrlBytes(input);
        }

        public byte[] EncodeToBytes(string input)
        {
            return Base64Utils.EncodeToRawUrlBytes(input);
        }

        public byte[] DecodeBytesFrom(byte[] input)
        {
            return Base64Utils.DecodeBytesFromRawUrl(input);
        }

        public byte[] DecodeBytesFrom(string input)
        {
            return Base64Utils.DecodeBytesFromRawUrl(input);
        }

        public string DecodeStringFrom(byte[] input)
        {
            return Base64Utils.DecodeStringFromRawUrl(input);
        }

        public string DecodeStringFrom(string input)
        {
            return Base64Utils.DecodeStringFromRawUrl(input);
        }
    }
}