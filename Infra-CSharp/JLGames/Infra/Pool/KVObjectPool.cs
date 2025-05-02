using System;
using System.Collections.Generic;

namespace JLGames.Infra.Pool
{
    /// <summary>
    /// Key-Value mapping object pool
    /// Key-Value 映射对象池
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public sealed class KVObjectPool<TKey, TValue> where TValue : class
    {
        private readonly Dictionary<TKey, TValue> m_CacheMap;

        public KVObjectPool()
        {
            m_CacheMap = new Dictionary<TKey, TValue>();
        }

        /// <summary>
        /// Constructor
        /// 构造函数
        /// </summary>
        /// <param name="size">默认映射字典初始容量</param>
        public KVObjectPool(int size)
        {
            m_CacheMap = new Dictionary<TKey, TValue>(size);
        }

        /// <summary>
        /// Add
        /// 增加
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(TKey key, TValue value)
        {
            m_CacheMap[key] = value;
        }

        /// <summary>
        /// Remove
        /// 删除
        /// </summary>
        /// <param name="key"></param>
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
        /// <param name="key"></param>
        /// <returns></returns>
        public bool ContainsKey(TKey key)
        {
            return m_CacheMap.ContainsKey(key);
        }

        /// <summary>
        /// Get object
        /// 取值 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public TValue GetValue(TKey key)
        {
            if (!m_CacheMap.ContainsKey(key)) return default(TValue);
            return m_CacheMap[key];
        }


        /// <summary>
        /// Clone object
        /// 克隆
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cloneAction"></param>
        /// <returns></returns>
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