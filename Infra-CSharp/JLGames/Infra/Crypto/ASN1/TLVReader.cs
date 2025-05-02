using System;
using System.IO;

namespace JLGames.Infra.Crypto.ASN1
{
    public class TLVReader : IDisposable
    {
        private readonly BinaryReader m_Reader;

        public TLVReader(BinaryReader reader)
        {
            m_Reader = reader;
        }

        public TLVReader(byte[] data)
        {
            m_Reader = new BinaryReader(new MemoryStream(data));
        }

        public void Dispose()
        {
            m_Reader?.Dispose();
        }

        /// <summary>
        /// 读取 ASN.1 数据块
        /// </summary>
        /// <returns></returns>
        public TLVBlock ReadBlock()
        {
            var tag = ReadTag();
            var length = ReadLength();
            var value = ReadValue(length);
            return new TLVBlock
            {
                Tag = tag, Length = length, Value = value
            };
        }

        /// <summary>
        /// 验证标记后读取 ASN.1 数据块
        /// </summary>
        /// <param name="expectedTag"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public TLVBlock ReadBlock(byte expectedTag)
        {
            var tag = ReadTag(expectedTag);
            var length = ReadLength();
            if (length == 0)
            {
                return new TLVBlock
                {
                    Tag = tag, Length = length
                };
            }

            return new TLVBlock
            {
                Tag = tag, Length = length, Value = ReadValue(length)
            };
        }

        /// <summary>
        /// 读取 ASN.1 数据块
        /// 不包含Value部分
        /// </summary>
        /// <returns></returns>
        public TLVBlock ReadBlockNoValue()
        {
            var tag = ReadTag();
            var length = ReadLength();
            return new TLVBlock
            {
                Tag = tag, Length = length
            };
        }

        /// <summary>
        /// 验证标记后读取 ASN.1 数据块
        /// 不包含Value部分
        /// </summary>
        /// <returns></returns>
        public TLVBlock ReadBlockNoValue(byte expectedTag)
        {
            var tag = ReadTag(expectedTag);
            var length = ReadLength();
            return new TLVBlock
            {
                Tag = tag, Length = length
            };
        }

        /// <summary>
        /// 验证标记后读取一个整数数据块
        /// </summary>
        /// <returns></returns>
        public byte[] ReadInteger()
        {
            return ReadBlock(DerTags.INTEGER).Value;
        }

        /// <summary>
        /// 验证标记后读取一个布尔数据块
        /// </summary>
        /// <returns></returns>
        public byte[] ReadBoolean()
        {
            return ReadBlock(DerTags.BOOLEAN).Value;
        }

        /// <summary>
        /// 验证标记后读取一个整数数据块
        /// </summary>
        /// <param name="expectedTag"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public byte[] ReadInteger(byte expectedTag)
        {
            return ReadBlock(expectedTag).Value;
        }

        /// <summary>
        /// 读取 ASN.1 标记（Tag）
        /// </summary>
        /// <returns>标记值</returns>
        public byte ReadTag()
        {
            if (!HasData()) throw new EndOfStreamException("Unexpected end of ASN.1 data.");
            return m_Reader.ReadByte();
        }

        /// <summary>
        /// 读取 ASN.1 标记（Tag）并验证
        /// </summary>
        /// <param name="expectedTag"></param>
        /// <returns>标记值</returns>
        public byte ReadTag(byte expectedTag)
        {
            if (!HasData()) throw new EndOfStreamException("Unexpected end of ASN.1 data.");
            var tag = m_Reader.ReadByte();
            if (tag != expectedTag) throw new ArgumentException($"ASN.1 format error: Expected {expectedTag:X2}, but got {tag:X2}");
            return tag;
        }

        /// <summary>
        /// 读取 ASN.1 数据长度（Length）
        /// 短格式: 第一个1字节记录长度(小于128时，即小于0x80)
        /// 长格式: 第一个1字节记录长度数据字节数量n, 余下n个字节记录数量，强制大端 
        /// </summary>
        /// <returns>数据块字节数量</returns>
        public int ReadLength()
        {
            var len = m_Reader.ReadByte();

            // 短格式
            if ((len & 0x80) == 0)
            {
                return len;
            }

            // 长格式
            var lengthBytes = len & 0x7F;
            var length = 0;
            for (var i = 0; i < lengthBytes; i++)
                length = (length << 8) | m_Reader.ReadByte();
            return length;
        }

        /// <summary>
        /// 读取 ASN.1 数据（Value）
        /// 如果有前导零，去除前导0
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        public byte[] ReadValue(int length)
        {
            var value = m_Reader.ReadBytes(length);
            if (!HasLeadingZero(value)) return value;
            return RemoveLeadingZero(value);
        }

        /// <summary>
        /// 是否还有数据未读取
        /// </summary>
        /// <returns></returns>
        public bool HasData()
        {
            return m_Reader.BaseStream.Position < m_Reader.BaseStream.Length;
        }

        private bool HasLeadingZero(byte[] data)
        {
            return data.Length > 1 && data[0] == 0x00;
        }

        private byte[] RemoveLeadingZero(byte[] data)
        {
            var newData = new byte[data.Length - 1];
            System.Buffer.BlockCopy(data, 1, newData, 0, newData.Length);
            return newData;
        }
    }
}