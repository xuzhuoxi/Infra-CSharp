using System;
using System.Collections.Generic;

namespace JLGames.Infra.Pool
{
    /// <summary>
    /// Key-Value mapping object pool
    /// Key-Value 映射对象池
    /// </summary>
    /// <typeparam name="TKey">Dictionary key type.<br/>字典键类型。</typeparam>
    /// <typeparam name="TValue">Stored reference type.<br/>存储的引用类型。</typeparam>
    public sealed class KVObjectPool<TKey, TValue> where TValue : class
    {
        private readonly Dictionary<TKey, TValue> m_CacheMap;

        /// <summary>
        /// Create a pool with default dictionary capacity.
        /// 使用默认字典容量创建对象池。
        /// </summary>
        public KVObjectPool()
        {
            m_CacheMap = new Dictionary<TKey, TValue>();
        }

        /// <summary>
        /// Constructor
        /// 构造函数
        /// </summary>
        /// <param name="size">Initial dictionary capacity.<br/>字典初始容量。</param>
        public KVObjectPool(int size)
        {
            m_CacheMap = new Dictionary<TKey, TValue>(size);
        }

        /// <summary>
        /// Add or replace an entry by key.
        /// 按键添加或覆盖条目。
        /// </summary>
        /// <param name="key">Entry key.<br/>条目键。</param>
        /// <param name="value">Object to store.<br/>要存储的对象。</param>
        public void Add(TKey key, TValue value)
        {
            m_CacheMap[key] = value;
        }

        /// <summary>
        /// Remove
        /// 删除
        /// </summary>
        /// <param name="key">Entry key to remove.<br/>要删除的条目键。</param>
        public void Remove(TKey key)
        {
            if (!m_CacheMap.ContainsKey(key))
            {
                return;
            }

            m_CacheMap.Remove(key);
        }

        /// <summary>
        /// Remove all
        /// 删除全部
        /// </summary>
        public void RemoveAll()
        {
            m_CacheMap.Clear();
        }

        /// <summary>
        /// Check exist
        /// 检测
        /// </summary>
        /// <param name="key">Entry key.<br/>条目键。</param>
        /// <returns>True if the key exists.<br/>键存在时返回 true。</returns>
        public bool ContainsKey(TKey key)
        {
            return m_CacheMap.ContainsKey(key);
        }

        /// <summary>
        /// Get object
        /// 取值 
        /// </summary>
        /// <param name="key">Entry key.<br/>条目键。</param>
        /// <returns>Stored value, or default if key not found.<br/>已存储的对象；键不存在时为 default。</returns>
        public TValue GetValue(TKey key)
        {
            if (!m_CacheMap.ContainsKey(key)) return default(TValue);
            return m_CacheMap[key];
        }


        /// <summary>
        /// Clone object
        /// 克隆
        /// </summary>
        /// <param name="key">Entry key whose value to clone.<br/>要克隆其值的条目键。</param>
        /// <param name="cloneAction">Optional custom clone delegate; uses ICloneable when null.<br/>可选自定义克隆委托；为 null 时使用 ICloneable。</param>
        /// <returns>Cloned value, or default if key missing or cloning unsupported.<br/>克隆结果；键不存在或不支持克隆时为 default。</returns>
        public TValue CloneValue(TKey key, PoolDelegate.CloneObject<TValue> cloneAction = null)
        {
            var value = GetValue(key);
            if (null == value) return default(TValue);

            if (null != cloneAction) return cloneAction.Invoke(value);

            var cloneable1 = value as ICloneable<TValue>;
            if (cloneable1 != null) return cloneable1.Clone();

            var cloneable2 = value as ICloneable;
            if (cloneable2 != null) return cloneable2.Clone() as TValue;

            return default(TValue);
        }
    }
}
