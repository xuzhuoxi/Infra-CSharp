using System;
using System.Collections.Generic;
using System.Text;

namespace JLGames.Infra.Buffer
{
    public sealed class CodingMap
    {
        /// <summary>
        /// iteration function
        /// 遍历代理函数
        /// </summary>
        public delegate void FuncEach(string key, object value);

        private readonly Dictionary<string, object> m_KeyValue;
        private readonly IDataBuffer m_Buffer;
        private readonly List<string> m_TempKeys;

        public override string ToString()
        {
            if (null == m_KeyValue || m_KeyValue.Count == 0)
            {
                return "{}";
            }

            m_TempKeys.Clear();
            foreach (var pair in m_KeyValue)
            {
                m_TempKeys.Add(pair.Key);
            }

            m_TempKeys.Sort();

            var sb = new StringBuilder("{");
            foreach (var key in m_TempKeys)
            {
                if (m_KeyValue[key] is int[] arr1)
                    sb.Append($"{key}:[{string.Join(",", arr1)}],");
                else if (m_KeyValue[key] is uint[] arr2)
                    sb.Append($"{key}:[{string.Join(",", arr2)}],");
                else
                    sb.Append($"{key}:{m_KeyValue[key]}, ");
            }

            sb.Remove(sb.Length - 2, 2);
            sb.Append("}");
            return sb.ToString();
        }

        public CodingMap(bool littleEndian)
        {
            m_KeyValue = new Dictionary<string, object>();
            m_Buffer = new DataBuffer(littleEndian);
            m_TempKeys = new List<string>();
        }

        /// <summary>
        /// key-value size
        /// 键值对数量
        /// </summary>
        public int Size => m_KeyValue?.Count ?? 0;

        /// <summary>
        /// Clear the var set.
        /// 清理集合
        /// </summary>
        public void Clear()
        {
            m_KeyValue.Clear();
        }

        /// <summary>
        /// Set key-value.
        /// 设置键值对
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value">Basic data types and their array types\n基础数据类型及他们的数组类型</param>
        public void SetValue(string key, object value)
        {
            m_KeyValue[key] = value;
        }

        /// <summary>
        /// Remove key-value and return value.
        /// 删除键值对，并返回值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object DeleteValue(string key)
        {
            if (m_KeyValue.ContainsKey(key))
            {
                var rs = m_KeyValue[key];
                m_KeyValue.Remove(key);
                return rs;
            }

            return null;
        }

        /// <summary>
        /// Set key-value in batch
        /// 批量设置键值对
        /// </summary>
        /// <param name="vars"></param>
        public void SetValues(Dictionary<string, object> vars)
        {
            if (null == vars || vars.Count == 0)
            {
                return;
            }

            foreach (var pair in vars)
            {
                m_KeyValue[pair.Key] = pair.Value;
            }
        }

        /// <summary>
        /// Check key is exist or not.
        /// 检查键的存在性
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool CheckKey(string key)
        {
            return m_KeyValue.ContainsKey(key);
        }

        /// <summary>
        /// Get value
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns>Basic data types and their array types\n基础数据类型及他们的数组类型</returns>
        public object GetValue(string key)
        {
            if (m_KeyValue.Count == 0 || !m_KeyValue.ContainsKey(key)) return null;
            return m_KeyValue[key];
        }

        /// <summary>
        /// Get value
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <typeparam name="T">Basic data types and their array types\n基础数据类型及他们的数组类型</typeparam>
        /// <returns></returns>
        public T GetValue<T>(string key)
        {
            try
            {
                return (T)GetValue(key);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        /// <summary>
        /// iteration
        /// 遍历
        /// </summary>
        /// <param name="each"></param>
        public void ForEach(FuncEach each)
        {
            if (null == each) return;
            foreach (var pair in m_KeyValue)
            {
                each.Invoke(pair.Key, pair.Value);
            }
        }

        /// <summary>
        /// Serialized to a byte array.
        /// 序列化为字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToBinary()
        {
            var len = m_KeyValue.Count;
            m_Buffer.Clear();
            m_Buffer.WriteLen(len);
            if (len > 0)
            {
                foreach (var pair in m_KeyValue)
                {
                    m_Buffer.WriteBaseData(pair.Key);
                    var kind = ValueKindUtil.GetValueKind(pair.Value);
                    m_Buffer.WriteData((byte)kind);
                    m_Buffer.WriteBaseData(pair.Value);
                }
            }

            return m_Buffer.ReadBytes();
        }

        /// <summary>
        /// Update data from a byte array.
        /// 从字节数组更新数据
        /// </summary>
        /// <param name="bytes"></param>
        public void FromBinaryOverride(byte[] bytes)
        {
            m_Buffer.Clear();
            m_Buffer.Write(bytes);
            m_KeyValue.Clear();
            var len = m_Buffer.ReadLen();
            if (len <= 0) return;
            for (var index = 0; index < len; index++)
            {
                var key = m_Buffer.ReadString();
                var kind = (ValueKind)m_Buffer.ReadUInt8();
                if (ValueKindUtil.IsArrayKind(kind))
                {
                    var len1 = m_Buffer.ReadLen();
                    var value = ValueKindUtil.GetKindValue(kind, len1);
                    m_Buffer.ReadBaseDataTo(ref value);
                    m_KeyValue[key] = value;
                }
                else
                {
                    var value = ValueKindUtil.GetKindValue(kind, 0);
                    m_Buffer.ReadBaseDataTo(ref value);
                    m_KeyValue[key] = value;
                }
            }
        }
    }
}