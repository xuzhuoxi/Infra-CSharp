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
    /// Reusable object pool with three sub-pools: reusable, in-use, and pending destroy.
    /// 重用对象池，内含三个子池：可重用、使用中、待销毁。
    /// </summary>
    /// <typeparam name="T">Pooled object type.<br/>池内对象类型。</typeparam>
    public class ReuseObjectPool<T>
    {
        private readonly int m_MaxReuseCount;
        private readonly List<T>[] m_PoolList;

        /// <summary>
        /// Constructor
        /// 构造函数
        /// </summary>
        /// <param name="initCapacity">Initial capacity of each sub-pool.<br/>各子池初始容量。</param>
        /// <param name="maxReuseCount">Maximum objects allowed in the reusable sub-pool.<br/>可重用子池允许的最大对象数。</param>
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
        /// <param name="type">Sub-pool to check.<br/>要检查的子池类型。</param>
        /// <returns>True if the sub-pool has no objects.<br/>子池为空时返回 true。</returns>
        public bool IsPoolEmpty(ReusePoolSubType type)
        {
            return GetSubPool(type).Count <= 0;
        }

        /// <summary>
        /// Check if the object exists in the pool
        /// 检查对象是否存在于池中
        /// </summary>
        /// <param name="type">Sub-pool to search.<br/>要检索的子池类型。</param>
        /// <param name="o">Object to look up.<br/>待查找的对象。</param>
        /// <returns>True if the object exists in the sub-pool.<br/>对象存在于该子池时返回 true。</returns>
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
        /// <param name="targetType">Destination sub-pool.<br/>目标子池类型。</param>
        /// <param name="o">Object to move.<br/>要移动的对象。</param>
        /// <returns>False if already in target pool; otherwise whether the move succeeded.<br/>对象已在目标池时返回 false；否则表示是否移动成功。</returns>
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
        /// <returns>Reused object moved to the in-use pool, or default if reusable pool is empty.<br/>从可重用池取出并移入使用池的对象；可重用池为空时为 default。</returns>
        public T TransferResueToUsing()
        {
            if (SelfReusablePool.Count <= 0) return default(T);

            return TransferBetween(SelfReusablePool, SelfUsingPool, 0);
        }

        /// <summary>
        /// Remove the object from the pool
        /// 从池中移除对象
        /// </summary>
        /// <param name="sourceType">Sub-pool to remove from.<br/>源子池类型。</param>
        /// <param name="o">Object to remove.<br/>要移除的对象。</param>
        /// <returns>True if the object was removed.<br/>成功移除时返回 true。</returns>
        public bool RemoveFormPool(ReusePoolSubType sourceType, T o)
        {
            var pool = GetSubPool(sourceType);
            return RemoveFormPool(pool, o);
        }

        /// <summary>
        /// Add object to target pool.
        /// 将对象加入目标子池。
        /// </summary>
        /// <param name="targetType">Destination sub-pool.<br/>目标子池类型。</param>
        /// <param name="o">Object to add.<br/>要加入的对象。</param>
        /// <returns>True if added; false if null, duplicate, or reusable pool is full.<br/>加入成功返回 true；对象为 null、已存在或可重用池已满时返回 false。</returns>
        public bool AddToPool(ReusePoolSubType targetType, T o)
        {
            var pool = GetSubPool(targetType);

            return AddToPool(pool, o);
        }

        /// <summary>
        /// Transfer an object from one sub-pool to another.
        /// 将对象从一个子池转移到另一个子池。
        /// </summary>
        /// <param name="sourceType">Source sub-pool.<br/>源子池类型。</param>
        /// <param name="targetType">Destination sub-pool.<br/>目标子池类型。</param>
        /// <param name="o">Object to transfer.<br/>要转移的对象。</param>
        /// <returns>Transferred object on success, or default on failure.<br/>转移成功返回该对象，失败时为 default。</returns>
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
        /// <param name="type">Sub-pool to clear.<br/>要清空的子池类型。</param>
        /// <returns>Objects that were in the sub-pool before clearing; null if sub-pool not found.<br/>清空前子池中的对象数组；子池不存在时为 null。</returns>
        public T[] ClearSubPool(ReusePoolSubType type)
        {
            var pool = GetSubPool(type);
            return null == pool ? null : ClearSubPool(pool);
        }

        /// <summary>
        /// Clear all objects
        /// 清空全部池内对象
        /// </summary>
        /// <returns>All objects from every sub-pool before clearing.<br/>清空前所有子池中的对象合并数组。</returns>
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
        /// <param name="poolType">Sub-pool to traverse.<br/>要遍历的子池类型。</param>
        /// <param name="action">Action invoked per element.<br/>对每个元素执行的回调。</param>
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