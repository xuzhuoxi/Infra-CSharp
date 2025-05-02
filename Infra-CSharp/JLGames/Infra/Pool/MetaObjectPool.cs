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
    /// <typeparam name="T"></typeparam>
    public class MetaObjectPool<T> where T : class
    {
        /// <summary>
        /// object constructor
        /// 对象构造器
        /// </summary>
        public delegate T OriginGenFunc();

        /// <summary>
        /// Callback on creation
        /// 创建时回调
        /// </summary>
        /// <param name="o"></param>
        public delegate void CreateCallback(T o);

        /// <summary>
        /// Callback on destroy
        /// 移除时销毁回调
        /// </summary>
        /// <param name="o"></param>
        public delegate void DestroyCallback(T o);

        protected readonly T m_Original;
        protected readonly OriginGenFunc m_OriginGenFuncGen;
        protected readonly List<T> m_ObjectPool;

        protected CreateCallback m_CreateCallback = null;
        protected DestroyCallback m_DestroyCallback = null;

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
        /// <param name="callback"></param>
        public void SetCreateCallback(CreateCallback callback)
        {
            m_CreateCallback = callback;
        }

        /// <summary>
        /// Setting callback on destroy
        /// 设置删除对象时回调
        /// </summary>
        /// <param name="callback"></param>
        public void SetDestroyCallback(DestroyCallback callback)
        {
            m_DestroyCallback = callback;
        }

        /// <summary>
        /// Updata number of objects
        /// 更新对象数量
        /// </summary>
        /// <param name="size">对象数量</param>
        /// <returns>size==原来对象数量时:null</returns>
        public T[] UpdateToSize(int size)
        {
            return m_ObjectPool.Count == size ? null : Offset(size - m_ObjectPool.Count);
        }

        /// <summary>
        /// Update number of objects by offset
        /// 通过差值更新对象数量
        /// </summary>
        /// <param name="offset">数量偏差值</param>
        /// <returns>offset==0;null</returns>
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
        /// <param name="removeSize">删除的数量</param>
        /// <returns>removeSize少于0: null</returns>
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
        /// <param name="addSize">>增加的数量</param>
        /// <returns>addSize少于0: null</returns>
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

        public T this[int index]
        {
            get { return m_ObjectPool[index]; }
            set { m_ObjectPool[index] = value; }
        }

        /// <summary>
        /// find the first matched element.
        /// 查找第一个匹配项
        /// </summary>
        /// <param name="match"></param>
        /// <returns></returns>
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
        /// <param name="match"></param>
        /// <returns></returns>
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
        /// <param name="match"></param>
        /// <returns></returns>
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
        /// <param name="match"></param>
        /// <returns></returns>
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