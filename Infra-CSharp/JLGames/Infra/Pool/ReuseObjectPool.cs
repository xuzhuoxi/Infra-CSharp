using System;
using System.Collections.Generic;
using JLGames.Infra.Utils;

namespace JLGames.Infra.Pool
{
    /// <summary>
    /// Object subpool type
    /// 对象子池类型
    /// </summary>
    public enum ReusePoolSubType
    {
        /// <summary>
        /// Reusable
        /// 可重用
        /// </summary>
        Reusable,

        /// <summary>
        /// Using
        /// 使用中
        /// </summary>
        Using,

        /// <summary>
        /// Destroying
        /// 销毁处理中
        /// </summary>
        Destroying
    }


    /// <summary>
    /// 重用对象池
    /// 内部包含三个子池：可重用对象池、使用中对象池、准备销毁对象池
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ReuseObjectPool<T>
    {
        private readonly int m_MaxReuseCount;
        private readonly List<T>[] m_PoolList;

        /// <summary>
        /// Constructor
        /// 构造函数
        /// </summary>
        /// <param name="initCapacity">Capacity size of sub pool(子池初始容量)</param>
        /// <param name="maxReuseCount">Maximum number of reused objects(最大重用对象数量)</param>
        public ReuseObjectPool(int initCapacity = 8, int maxReuseCount = 100)
        {
            m_MaxReuseCount = maxReuseCount;
            m_PoolList = new List<T>[3];
            m_PoolList[(int) ReusePoolSubType.Reusable] = new List<T>(initCapacity);
            m_PoolList[(int) ReusePoolSubType.Using] = new List<T>(initCapacity);
            m_PoolList[(int) ReusePoolSubType.Destroying] = new List<T>(initCapacity);
        }

        #region Public Variables

        /// <summary>
        /// Reusable Object Pool
        /// 可重用对象池
        /// </summary>
        public List<T> SelfReusablePool => m_PoolList[(int) ReusePoolSubType.Reusable];

        /// <summary>
        /// Using Object Pool
        /// 使用中对象池
        /// </summary>
        public List<T> SelfUsingPool => m_PoolList[(int) ReusePoolSubType.Using];

        /// <summary>
        /// Destroying Object Pool
        /// 准备销毁对象池
        /// </summary>
        public List<T> SelfDestoryingPool => m_PoolList[(int) ReusePoolSubType.Destroying];

        /// <summary>
        /// Exist reusable objects
        /// 是否有可重用对象
        /// </summary>
        public bool HasReusableObject => SelfReusablePool.Count > 0;

        /// <summary>
        /// Whether the reused object pool is full
        /// 重用对象池是否已满
        /// </summary>
        public bool IsReusePoolFull => SelfReusablePool.Count >= m_MaxReuseCount;

        #endregion

        #region Public Functions

        /// <summary>
        /// Check if there is an object in the subpool
        /// 检查子池中是否有对象
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public bool IsPoolEmpty(ReusePoolSubType type)
        {
            return GetSubPool(type).Count <= 0;
        }

        /// <summary>
        /// Check if the object exists in the pool
        /// 检查对象是否存在于池中
        /// </summary>
        /// <param name="type"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public bool InPool(ReusePoolSubType type, T o)
        {
            if (null == o) return false;
            return GetSubPool(type).Contains(o);
        }

        /// <summary>
        /// Move the object to the target pool
        /// 移动对象到目标池中
        /// If the object itself is in the pool, return failure.
        /// 如果对象本身就在池中，返回失败。
        /// If the object is in another pool, remove it and add it to the target pool
        /// 如果对象在其它池中，移除后增加到目标池中
        /// </summary>
        /// <param name="targetType"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public bool TransferTo(ReusePoolSubType targetType, T o)
        {
            if (InPool(targetType, o))
            {
                return false;
            }

            RemoveFormPool(o);
            return AddToPool(targetType, o);
        }

        /// <summary>
        /// Reuse an object
        /// 重用一个对象
        /// Remove an object from the reuse pool and add it to the usage pool
        /// 从重用池中移除一个对象，并加入到使用池中
        /// </summary>
        /// <returns></returns>
        public T TransferResueToUsing()
        {
            if (SelfReusablePool.Count <= 0) return default(T);

            return TransferBetween(SelfReusablePool, SelfUsingPool, 0);
        }

        /// <summary>
        /// Remove the object from the pool
        /// 从池中移除对象
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public bool RemoveFormPool(ReusePoolSubType sourceType, T o)
        {
            var pool = GetSubPool(sourceType);
            return RemoveFormPool(pool, o);
        }

        /// <summary>
        /// Add object to target pool.
        /// 转移对象
        /// </summary>
        /// <param name="targetType"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public bool AddToPool(ReusePoolSubType targetType, T o)
        {
            var pool = GetSubPool(targetType);

            return AddToPool(pool, o);
        }

        /// <summary>
        /// Transfer object to target pool.
        /// 转移对象
        /// </summary>
        /// <param name="sourceType"></param>
        /// <param name="targetType"></param>
        /// <param name="o"></param>
        /// <returns></returns>
        public T TransferBetween(ReusePoolSubType sourceType, ReusePoolSubType targetType, T o)
        {
            if (sourceType == targetType)
            {
                return default(T);
            }

            var sourcePool = GetSubPool(sourceType);
            var targetPool = GetSubPool(targetType);

            return TransferBetween(sourcePool, targetPool, o);
        }


        /// <summary>
        /// Clear all objects in sub pool
        /// 清空子池对象
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public T[] ClearSubPool(ReusePoolSubType type)
        {
            var pool = GetSubPool(type);
            return null == pool ? null : ClearSubPool(pool);
        }

        /// <summary>
        /// Clear all objects
        /// 清空全部池内对象
        /// </summary>
        /// <returns></returns>
        public T[] ClearAll()
        {
            var rs = ArrayUtil.MergeArray(SelfReusablePool.ToArray(), SelfUsingPool.ToArray(),
                SelfDestoryingPool.ToArray());
            foreach (var cacheList in m_PoolList)
            {
                cacheList.Clear();
            }

            return rs;
        }

        /// <summary>
        /// Traverse all elements of the subpool
        /// 遍历子池全部元素
        /// </summary>
        /// <param name="poolType"></param>
        /// <param name="action"></param>
        public void ForeachElement(ReusePoolSubType poolType, Action<T> action)
        {
            var pool = GetSubPool(poolType);
            pool.ForEach(action);
        }

        #endregion

        #region protected

        protected T[] ClearSubPool(List<T> subPool)
        {
            var rs = subPool.ToArray();
            subPool.Clear();
            return rs;
        }

        protected List<T> GetSubPool(ReusePoolSubType type)
        {
            return m_PoolList[(int) type];
        }

        protected T TransferBetween(List<T> sourcePool, List<T> targetPool, T o)
        {
            if (sourcePool == targetPool) return default(T);

            if (RemoveFormPool(sourcePool, o))
            {
                var add = AddToPool(targetPool, o);
                if (add) return o;
            }

            return default(T);
        }

        protected T TransferBetween(List<T> sourcePool, List<T> targetPool, int sourceIndex)
        {
            if (sourcePool == targetPool) return default(T);

            var o = RemoveFormPool(sourcePool, sourceIndex);
            if (null == o) return default(T);
            var add = AddToPool(targetPool, o);
            if (!add) return default(T);
            return o;
        }

        protected T RemoveFormPool(List<T> subPool, int index)
        {
            if (index < 0 || index >= subPool.Count) return default(T);

            var rs = subPool[index];
            subPool.RemoveAt(index);
            return rs;
        }

        protected bool RemoveFormPool(List<T> subPool, T o)
        {
            if (null == o || !subPool.Contains(o)) return false;

            subPool.Remove(o);
            return true;
        }

        protected bool RemoveFormPool(T o)
        {
            foreach (var pool in m_PoolList)
            {
                if (RemoveFormPool(pool, o)) return true;
            }

            return false;
        }

        protected bool AddToPool(List<T> subPool, T o)
        {
            if (null == o || subPool.Contains(o)) return false;

            if (SelfReusablePool == subPool && subPool.Count >= m_MaxReuseCount) return false;

            subPool.Add(o);
            return true;
        }

        #endregion
    }
}