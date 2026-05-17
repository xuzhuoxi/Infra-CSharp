using System;
using System.Collections.Generic;

namespace JLGames.Infra.Pool
{
    /// <summary>
    /// Meta object pool.
    /// 原型对象池
    /// Modify the pool size to automatically increase or decrease objects.
    /// 通过调整池Size，自动增加或减少对象
    /// </summary>
    /// <typeparam name="T">Pooled reference type.<br/>池内管理的引用类型。</typeparam>
    public class MetaObjectPool<T> where T : class
    {
        /// <summary>
        /// Factory that creates a new pooled instance.
        /// 创建新池对象的工厂委托。
        /// </summary>
        /// <returns>New instance.<br/>新创建的对象实例。</returns>
        public delegate T OriginGenFunc();

        /// <summary>
        /// Callback on creation
        /// 创建时回调
        /// </summary>
        /// <param name="o">Newly created instance.<br/>新创建的对象实例。</param>
        public delegate void CreateCallback(T o);

        /// <summary>
        /// Callback on destroy
        /// 移除时销毁回调
        /// </summary>
        /// <param name="o">Instance being removed from the pool.<br/>从池中移除的对象实例。</param>
        public delegate void DestroyCallback(T o);

        protected readonly T m_Original;
        protected readonly OriginGenFunc m_OriginGenFuncGen;
        protected readonly List<T> m_ObjectPool;

        protected CreateCallback m_CreateCallback = null;
        protected DestroyCallback m_DestroyCallback = null;

        /// <summary>
        /// Current number of objects in the pool.
        /// 池中当前对象数量。
        /// </summary>
        public int Count => m_ObjectPool.Count;

        /// <summary>
        /// constructor
        /// 构造函数
        /// </summary>
        /// <param name="original">Meta Object(原型对象)</param>
        /// <param name="size">Number of initial objects(初始对象数量)</param>
        /// <param name="capacity">Object pool initial capacity(对象池初始容量)</param>
        public MetaObjectPool(T original, int size = 0, int capacity = 0)
        {
            m_Original = original;
            m_ObjectPool = new List<T>(Math.Max(size, capacity));
            UpdateToSize(size);
        }

        /// <summary>
        /// constructor
        /// 构造函数
        /// </summary>
        /// <param name="originGenFuncGen">Object constructor(对象构造器)</param>
        /// <param name="size">Number of initial objects(初始对象数量)</param>
        /// <param name="capacity">Object pool initial capacity(对象池初始容量)</param>
        public MetaObjectPool(OriginGenFunc originGenFuncGen, int size = 0, int capacity = 0)
        {
            m_OriginGenFuncGen = originGenFuncGen;
            m_ObjectPool = new List<T>(Math.Max(size, capacity));
            UpdateToSize(size);
        }

        /// <summary>
        /// constructor
        /// 构造函数
        /// </summary>
        /// <param name="original">Meta Object(原型对象)</param>
        /// <param name="size">Number of initial objects(初始对象数量)</param>
        public MetaObjectPool(T original, int size) : this(original, size, size)
        {
        }

        /// <summary>
        /// constructor
        /// 构造函数
        /// </summary>
        /// <param name="originGenFuncGen">Object constructor(对象构造器)</param>
        /// <param name="size">Number of initial objects(初始对象数量)</param>
        public MetaObjectPool(OriginGenFunc originGenFuncGen, int size) : this(originGenFuncGen, size, size)
        {
        }

        /// <summary>
        /// Setting callback on creation
        /// 设置创建对象时回调
        /// </summary>
        /// <param name="callback">Callback invoked when an object is added.<br/>对象被添加时触发的回调。</param>
        public void SetCreateCallback(CreateCallback callback)
        {
            m_CreateCallback = callback;
        }

        /// <summary>
        /// Setting callback on destroy
        /// 设置删除对象时回调
        /// </summary>
        /// <param name="callback">Callback invoked when an object is removed.<br/>对象被移除时触发的回调。</param>
        public void SetDestroyCallback(DestroyCallback callback)
        {
            m_DestroyCallback = callback;
        }

        /// <summary>
        /// Resize the pool to the target count.
        /// 将池调整到目标对象数量。
        /// </summary>
        /// <param name="size">Target object count.<br/>目标对象数量。</param>
        /// <returns>Added or removed instances; null if count unchanged.<br/>新增或移除的对象数组；数量未变时为 null。</returns>
        public T[] UpdateToSize(int size)
        {
            return m_ObjectPool.Count == size ? null : Offset(size - m_ObjectPool.Count);
        }

        /// <summary>
        /// Update number of objects by offset
        /// 通过差值更新对象数量
        /// </summary>
        /// <param name="offset">Delta count (positive to add, negative to remove).<br/>数量偏差（正数增加，负数减少）。</param>
        /// <returns>Added or removed instances; null if offset is 0.<br/>新增或移除的对象数组；offset 为 0 时为 null。</returns>
        public T[] Offset(int offset)
        {
            if (0 == offset) return null;
            if (offset > 0) return Add(offset);
            return Remove(-offset);
        }

        /// <summary>
        /// Remove number of objects.
        /// 删除对象数量
        /// </summary>
        /// <param name="removeSize">Number of instances to remove.<br/>要移除的对象数量。</param>
        /// <returns>Removed instances; null if removeSize is less than or equal to 0.<br/>被移除的对象数组；removeSize 小于等于 0 时为 null。</returns>
        public virtual T[] Remove(int removeSize)
        {
            if (removeSize <= 0) return null;
            removeSize = removeSize > m_ObjectPool.Count ? m_ObjectPool.Count : removeSize;
            var index = m_ObjectPool.Count - removeSize;
            var rs = new T[removeSize];
            m_ObjectPool.CopyTo(index, rs, 0, removeSize);
            m_ObjectPool.RemoveRange(index, removeSize);
            if (null != m_DestroyCallback)
            {
                for (var i = rs.Length - 1; i >= 0; i--)
                {
                    var o = rs[i];
                    m_DestroyCallback.Invoke(o);
                }
            }

            return rs;
        }

        /// <summary>
        /// Add number of objects.
        /// 增加对象数量
        /// </summary>
        /// <param name="addSize">Number of instances to add.<br/>要新增的对象数量。</param>
        /// <returns>Newly added instances; null if addSize is less than or equal to 0.<br/>新增的对象数组；addSize 小于等于 0 时为 null。</returns>
        public virtual T[] Add(int addSize)
        {
            if (addSize <= 0) return null;
            var rs = new T[addSize];
            for (var index = 0; index < addSize; index++)
            {
                rs[index] = NewObject();
                m_ObjectPool.Add(rs[index]);
                m_CreateCallback?.Invoke(rs[index]);
            }

            return rs;
        }

        /// <summary>
        /// Gets or sets the object at the specified index.
        /// 按索引获取或设置池中对象。
        /// </summary>
        /// <param name="index">Zero-based index.<br/>从 0 开始的索引。</param>
        public T this[int index]
        {
            get { return m_ObjectPool[index]; }
            set { m_ObjectPool[index] = value; }
        }

        /// <summary>
        /// get the first element
        /// 取第一个元素
        /// </summary>
        public T First => m_ObjectPool.Count == 0 ? null : m_ObjectPool[0];

        /// <summary>
        /// get the last element
        /// 取最后一个元素
        /// </summary>
        public T Last => m_ObjectPool.Count == 0 ? null : m_ObjectPool[m_ObjectPool.Count - 1];

        
        /// <summary>
        /// find the first matched element.
        /// 查找第一个匹配项
        /// </summary>
        /// <param name="match">Predicate for matching.<br/>匹配条件。</param>
        /// <returns>First match, or default if none.<br/>第一个匹配项；无匹配时为 default。</returns>
        public T FindFirst(Predicate<T> match)
        {
            for (var index = 0; index < m_ObjectPool.Count; index++)
            {
                if (match.Invoke(m_ObjectPool[index]))
                {
                    return m_ObjectPool[index];
                }
            }

            return default(T);
        }

        /// <summary>
        /// find the last matched element.
        /// 查找最后一个匹配项
        /// </summary>
        /// <param name="match">Predicate for matching.<br/>匹配条件。</param>
        /// <returns>Last match, or default if none.<br/>最后一个匹配项；无匹配时为 default。</returns>
        public T FindLast(Predicate<T> match)
        {
            for (var index = m_ObjectPool.Count - 1; index >= 0; index--)
            {
                if (match.Invoke(m_ObjectPool[index]))
                {
                    return m_ObjectPool[index];
                }
            }

            return default(T);
        }

        /// <summary>
        /// remove the first matched element.
        /// 移除第一个匹配项
        /// </summary>
        /// <param name="match">Predicate for matching.<br/>匹配条件。</param>
        /// <returns>Removed instance, or default if none matched.<br/>被移除的实例；无匹配时为 default。</returns>
        public virtual T RemoveFirst(Predicate<T> match)
        {
            for (var index = 0; index < m_ObjectPool.Count; index++)
            {
                if (match.Invoke(m_ObjectPool[index]))
                {
                    var rs = m_ObjectPool[index];
                    m_ObjectPool.RemoveAt(index);
                    return rs;
                }
            }

            return default(T);
        }

        /// <summary>
        /// remove the last matched element.
        /// 删除最后一个匹配项
        /// </summary>
        /// <param name="match">Predicate for matching.<br/>匹配条件。</param>
        /// <returns>Removed instance, or default if none matched.<br/>被移除的实例；无匹配时为 default。</returns>
        public virtual T RemoveLast(Predicate<T> match)
        {
            for (var index = m_ObjectPool.Count - 1; index >= 0; index--)
            {
                if (match.Invoke(m_ObjectPool[index]))
                {
                    var rs = m_ObjectPool[index];
                    m_ObjectPool.RemoveAt(index);
                    return rs;
                }
            }

            return default(T);
        }

        //-----------------------------

        protected virtual T NewObject()
        {
            if (null != m_OriginGenFuncGen)
            {
                return GenObject();
            }

            return CloneObject();
        }

        //-----------------------------

        private T CloneObject()
        {
            if (null == m_Original) return default(T);

            object origin = m_Original;

            var cloneable1 = origin as ICloneable<T>;
            if (cloneable1 != null) return cloneable1.Clone();

            var cloneable2 = origin as ICloneable;
            if (cloneable2 != null) return cloneable2.Clone() as T;

            return default(T);
        }

        private T GenObject()
        {
            return m_OriginGenFuncGen?.Invoke();
        }
    }
}