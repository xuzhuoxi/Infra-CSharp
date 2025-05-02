using System;
using System.Collections.Generic;
using System.Text;

namespace JLGames.Infra.Buffer
{
    public sealed class CodingList
    {
        private class Item
        {
            public string Key;
            public object Value;
        }

        /// <summary>
        /// iteration function
        /// 遍历代理函数
        /// </summary>
        public delegate void FuncEach(string key, object value);

        private readonly List<Item> m_List;
        private readonly IDataBuffer m_Buffer;

        public override string ToString()
        {
            if (null == m_List || m_List.Count == 0)
            {
                return "{}";
            }

            var sb = new StringBuilder("{");
            foreach (var item in m_List)
            {
                sb.Append($"{item.Key}:{item.Value},");
            }

            sb.Remove(sb.Length - 1, 1);
            sb.Append("}");
            return sb.ToString();
        }

        public CodingList(bool littleEndian)
        {
            m_List = new List<Item>();
            m_Buffer = new DataBuffer(littleEndian);
        }

        /// <summary>
        /// key-value size
        /// 键值对数量
        /// </summary>
        public int Size => m_List?.Count ?? 0;

        /// <summary>
        /// Clear the var set.
        /// 清理集合
        /// </summary>
        public void Clear()
        {
            m_List.Clear();
        }

        /// <summary>
        /// Set key-value.
        /// 设置键值对
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetValue(string key, object value)
        {
            var item = FindItem(key);
            if (null == item)
                m_List.Add(new Item {Key = key, Value = value});
            else
                item.Value = value;
            SortList();
        }

        /// <summary>
        /// Remove key-value and return value.
        /// 删除键值对，并返回值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object DeleteValue(string key)
        {
            var index = FindItemIndex(key);
            if (-1 == index) return null;
            var temp = m_List[index];
            m_List.RemoveAt(index);
            return temp.Value;
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

            var newItem = false;
            foreach (var pair in vars)
            {
                var i = FindItem(pair.Key);
                if (null == i)
                {
                    m_List.Add(new Item {Key = pair.Key, Value = pair.Value});
                    newItem = true;
                }
                else
                {
                    i.Value = pair.Value;
                }
            }

            if (newItem)
            {
                SortList();
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
            return FindItem(key) != null;
        }

        /// <summary>
        /// Get value
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetValue(string key)
        {
            return FindItem(key)?.Value;
        }


        /// <summary>
        /// Get value
        /// 获取值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T GetValue<T>(string key)
        {
            try
            {
                return (T) GetValue(key);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        private Item FindItem(string key)
        {
            return m_List.Count == 0 ? null : m_List.Find((item => item.Key == key));
        }

        private int FindItemIndex(string key)
        {
            return m_List.Count == 0 ? -1 : m_List.FindIndex((item => item.Key == key));
        }

        private void SortList()
        {
            m_List.Sort(((item1, item2) => string.Compare(item1.Key, item2.Key, StringComparison.Ordinal)));
        }

        /// <summary>
        /// iteration
        /// 遍历
        /// </summary>
        /// <param name="each"></param>
        public void ForEach(FuncEach each)
        {
            if (null == each) return;
            foreach (var item in m_List)
            {
                each.Invoke(item.Key, item.Value);
            }
        }

        /// <summary>
        /// Serialized to a byte array.
        /// 序列化为字节数组
        /// </summary>
        /// <returns></returns>
        public byte[] ToBinary()
        {
            var len = m_List.Count;
            m_Buffer.Clear();
            m_Buffer.WriteLen(len);
            if (len > 0)
            {
                foreach (var item in m_List)
                {
                    m_Buffer.WriteBaseData(item.Key);
                    var kind = ValueKindUtil.GetValueKind(item.Value);
                    m_Buffer.WriteData((byte) kind);
                    m_Buffer.WriteBaseData(item.Value);
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
            m_List.Clear();
            var len = m_Buffer.ReadLen();
            if (len <= 0) return;
            for (var index = 0; index < len; index++)
            {
                var key = m_Buffer.ReadString();
                var kind = (ValueKind) m_Buffer.ReadUInt8();
                if (ValueKindUtil.IsArrayKind(kind))
                {
                    var len1 = m_Buffer.ReadLen();
                    var value = ValueKindUtil.GetKindValue(kind, len1);
                    m_Buffer.ReadBaseDataTo(ref value);
                    m_List.Add(new Item {Key = key, Value = value});
                }
                else
                {
                    var value = ValueKindUtil.GetKindValue(kind, 0);
                    m_Buffer.ReadBaseDataTo(ref value);
                    m_List.Add(new Item {Key = key, Value = value});
                }
            }
        }
    }
}