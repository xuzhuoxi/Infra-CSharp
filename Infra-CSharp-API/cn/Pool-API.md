# Pool API 文档

## 命名空间: JLGames.Infra.Pool

### 枚举 (Enums)

#### ReusePoolSubType
对象子池类型枚举

```csharp
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
```

### 类 (Classes)

#### ReuseObjectPool<T>
重用对象池类

```csharp
/// <summary>
/// 重用对象池
/// 内部包含三个子池：可重用对象池、使用中对象池、准备销毁对象池
/// </summary>
/// <typeparam name="T">对象类型</typeparam>
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
    public ReuseObjectPool(int initCapacity = 8, int maxReuseCount = 100);

    // 公共属性
    /// <summary>
    /// Reusable Object Pool
    /// 可重用对象池
    /// </summary>
    public List<T> SelfReusablePool { get; }

    /// <summary>
    /// Using Object Pool
    /// 使用中对象池
    /// </summary>
    public List<T> SelfUsingPool { get; }

    /// <summary>
    /// Destroying Object Pool
    /// 准备销毁对象池
    /// </summary>
    public List<T> SelfDestoryingPool { get; }

    /// <summary>
    /// Exist reusable objects
    /// 是否有可重用对象
    /// </summary>
    public bool HasReusableObject { get; }

    /// <summary>
    /// Whether the reused object pool is full
    /// 重用对象池是否已满
    /// </summary>
    public bool IsReusePoolFull { get; }

    // 公共方法
    /// <summary>
    /// Check if there is an object in the subpool
    /// 检查子池中是否有对象
    /// </summary>
    /// <param name="type">子池类型</param>
    /// <returns>是否为空</returns>
    public bool IsPoolEmpty(ReusePoolSubType type);

    /// <summary>
    /// Check if the object exists in the pool
    /// 检查对象是否存在于池中
    /// </summary>
    /// <param name="type">子池类型</param>
    /// <param name="o">要检查的对象</param>
    /// <returns>是否存在于池中</returns>
    public bool InPool(ReusePoolSubType type, T o);

    /// <summary>
    /// Move the object to the target pool
    /// 移动对象到目标池中
    /// If the object itself is in the pool, return failure.
    /// 如果对象本身就在池中，返回失败。
    /// If the object is in another pool, remove it and add it to the target pool
    /// 如果对象在其它池中，移除后增加到目标池中
    /// </summary>
    /// <param name="targetType">目标池类型</param>
    /// <param name="o">要移动的对象</param>
    /// <returns>是否成功</returns>
    public bool TransferTo(ReusePoolSubType targetType, T o);

    /// <summary>
    /// Reuse an object
    /// 重用一个对象
    /// Remove an object from the reuse pool and add it to the usage pool
    /// 从重用池中移除一个对象，并加入到使用池中
    /// </summary>
    /// <returns>重用的对象</returns>
    public T TransferResueToUsing();

    /// <summary>
    /// Remove the object from the pool
    /// 从池中移除对象
    /// </summary>
    /// <param name="sourceType">源池类型</param>
    /// <param name="o">要移除的对象</param>
    /// <returns>是否成功移除</returns>
    public bool RemoveFormPool(ReusePoolSubType sourceType, T o);

    /// <summary>
    /// Add object to target pool.
    /// 添加对象到目标池
    /// </summary>
    /// <param name="targetType">目标池类型</param>
    /// <param name="o">要添加的对象</param>
    /// <returns>是否成功添加</returns>
    public bool AddToPool(ReusePoolSubType targetType, T o);

    /// <summary>
    /// Transfer object to target pool.
    /// 转移对象到目标池
    /// </summary>
    /// <param name="sourceType">源池类型</param>
    /// <param name="targetType">目标池类型</param>
    /// <param name="o">要转移的对象</param>
    /// <returns>转移的对象</returns>
    public T TransferBetween(ReusePoolSubType sourceType, ReusePoolSubType targetType, T o);

    /// <summary>
    /// Clear all objects in sub pool
    /// 清空子池对象
    /// </summary>
    /// <param name="type">子池类型</param>
    /// <returns>清空的对象数组</returns>
    public T[] ClearSubPool(ReusePoolSubType type);

    /// <summary>
    /// Clear all objects
    /// 清空全部池内对象
    /// </summary>
    /// <returns>清空的对象数组</returns>
    public T[] ClearAll();

    /// <summary>
    /// Traverse all elements of the subpool
    /// 遍历子池全部元素
    /// </summary>
    /// <param name="poolType">池类型</param>
    /// <param name="action">遍历动作</param>
    public void ForeachElement(ReusePoolSubType poolType, Action<T> action);

    // 保护方法
    /// <summary>
    /// 清空子池
    /// </summary>
    /// <param name="subPool">子池</param>
    /// <returns>清空的对象数组</returns>
    protected T[] ClearSubPool(List<T> subPool);

    /// <summary>
    /// 获取子池
    /// </summary>
    /// <param name="type">池类型</param>
    /// <returns>子池</returns>
    protected List<T> GetSubPool(ReusePoolSubType type);

    /// <summary>
    /// 在两个池之间转移对象
    /// </summary>
    /// <param name="sourcePool">源池</param>
    /// <param name="targetPool">目标池</param>
    /// <param name="o">要转移的对象</param>
    /// <returns>转移的对象</returns>
    protected T TransferBetween(List<T> sourcePool, List<T> targetPool, T o);

    /// <summary>
    /// 在两个池之间转移对象（按索引）
    /// </summary>
    /// <param name="sourcePool">源池</param>
    /// <param name="targetPool">目标池</param>
    /// <param name="sourceIndex">源索引</param>
    /// <returns>转移的对象</returns>
    protected T TransferBetween(List<T> sourcePool, List<T> targetPool, int sourceIndex);

    /// <summary>
    /// 从池中移除对象（按索引）
    /// </summary>
    /// <param name="subPool">子池</param>
    /// <param name="index">索引</param>
    /// <returns>移除的对象</returns>
    protected T RemoveFormPool(List<T> subPool, int index);

    /// <summary>
    /// 从池中移除对象
    /// </summary>
    /// <param name="subPool">子池</param>
    /// <param name="o">要移除的对象</param>
    /// <returns>是否成功移除</returns>
    protected bool RemoveFormPool(List<T> subPool, T o);

    /// <summary>
    /// 从所有池中移除对象
    /// </summary>
    /// <param name="o">要移除的对象</param>
    /// <returns>是否成功移除</returns>
    protected bool RemoveFormPool(T o);

    /// <summary>
    /// 添加对象到池中
    /// </summary>
    /// <param name="subPool">子池</param>
    /// <param name="o">要添加的对象</param>
    /// <returns>是否成功添加</returns>
    protected bool AddToPool(List<T> subPool, T o);
}
```

#### KVObjectPool<K, V>
键值对象池类

```csharp
/// <summary>
/// 键值对象池类
/// 提供基于键值对的对象池管理
/// </summary>
/// <typeparam name="K">键类型</typeparam>
/// <typeparam name="V">值类型</typeparam>
public class KVObjectPool<K, V>
{
    private readonly Dictionary<K, V> m_Pool;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="capacity">初始容量</param>
    public KVObjectPool(int capacity = 16);

    /// <summary>
    /// 池中对象数量
    /// </summary>
    public int Count { get; }

    /// <summary>
    /// 添加对象到池中
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">值</param>
    /// <returns>是否成功添加</returns>
    public bool Add(K key, V value);

    /// <summary>
    /// 从池中获取对象
    /// </summary>
    /// <param name="key">键</param>
    /// <param name="value">输出值</param>
    /// <returns>是否成功获取</returns>
    public bool TryGet(K key, out V value);

    /// <summary>
    /// 从池中移除对象
    /// </summary>
    /// <param name="key">键</param>
    /// <returns>是否成功移除</returns>
    public bool Remove(K key);

    /// <summary>
    /// 检查池中是否包含指定键
    /// </summary>
    /// <param name="key">键</param>
    /// <returns>是否包含</returns>
    public bool ContainsKey(K key);

    /// <summary>
    /// 清空池
    /// </summary>
    public void Clear();

    /// <summary>
    /// 遍历池中的所有对象
    /// </summary>
    /// <param name="action">遍历动作</param>
    public void ForEach(Action<K, V> action);
}
```

#### MetaObjectPool<T>
元对象池类

```csharp
/// <summary>
/// 元对象池类
/// 提供基于元数据的对象池管理
/// </summary>
/// <typeparam name="T">对象类型</typeparam>
public class MetaObjectPool<T>
{
    private readonly Dictionary<string, List<T>> m_Pool;
    private readonly int m_MaxPoolSize;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="maxPoolSize">最大池大小</param>
    public MetaObjectPool(int maxPoolSize = 100);

    /// <summary>
    /// 添加对象到指定元数据池
    /// </summary>
    /// <param name="metaKey">元数据键</param>
    /// <param name="obj">对象</param>
    /// <returns>是否成功添加</returns>
    public bool Add(string metaKey, T obj);

    /// <summary>
    /// 从指定元数据池获取对象
    /// </summary>
    /// <param name="metaKey">元数据键</param>
    /// <returns>对象，如果池为空则返回默认值</returns>
    public T Get(string metaKey);

    /// <summary>
    /// 从指定元数据池移除对象
    /// </summary>
    /// <param name="metaKey">元数据键</param>
    /// <returns>移除的对象</returns>
    public T Remove(string metaKey);

    /// <summary>
    /// 检查指定元数据池是否为空
    /// </summary>
    /// <param name="metaKey">元数据键</param>
    /// <returns>是否为空</returns>
    public bool IsEmpty(string metaKey);

    /// <summary>
    /// 获取指定元数据池的大小
    /// </summary>
    /// <param name="metaKey">元数据键</param>
    /// <returns>池大小</returns>
    public int GetPoolSize(string metaKey);

    /// <summary>
    /// 清空指定元数据池
    /// </summary>
    /// <param name="metaKey">元数据键</param>
    /// <returns>清空的对象数组</returns>
    public T[] ClearPool(string metaKey);

    /// <summary>
    /// 清空所有池
    /// </summary>
    public void ClearAll();

    /// <summary>
    /// 遍历指定元数据池中的所有对象
    /// </summary>
    /// <param name="metaKey">元数据键</param>
    /// <param name="action">遍历动作</param>
    public void ForEach(string metaKey, Action<T> action);
}
```

### 静态类 (Static Classes)

#### PoolDelegate
对象池委托定义

```csharp
/// <summary>
/// 对象池委托定义
/// 定义对象池中使用的各种回调函数
/// </summary>
public static class PoolDelegate
{
    /// <summary>
    /// Clone object
    /// 克隆对象
    /// </summary>
    /// <param name="origin">原始对象</param>
    /// <typeparam name="T">对象类型</typeparam>
    /// <returns>克隆的对象</returns>
    public delegate T CloneObject<T>(T origin) where T : class;
}
```

### 功能说明

#### 对象池类型

**ReuseObjectPool<T>**
- **三池设计**：可重用池、使用中池、销毁池
- **对象生命周期管理**：完整的对象生命周期跟踪
- **自动转移**：支持对象在不同池间的自动转移
- **容量控制**：可配置最大重用对象数量

**KVObjectPool<K, V>**
- **键值管理**：基于键值对的对象存储
- **快速查找**：O(1)时间复杂度的对象查找
- **灵活存储**：支持任意类型的键和值

**MetaObjectPool<T>**
- **元数据分组**：基于元数据键的对象分组
- **多池管理**：支持多个独立的对象池
- **容量限制**：可配置每个池的最大容量

#### 对象池特性

**性能优化**
1. **内存复用**：避免频繁的对象创建和销毁
2. **GC压力减少**：减少垃圾回收的压力
3. **快速分配**：预分配对象，快速获取

**生命周期管理**
1. **状态跟踪**：跟踪对象的使用状态
2. **自动回收**：支持对象的自动回收机制
3. **资源清理**：提供完整的资源清理功能

### 使用示例

#### 基本对象池使用
```csharp
// 创建对象池
var pool = new ReuseObjectPool<GameObject>(initCapacity: 10, maxReuseCount: 50);

// 添加对象到可重用池
var obj1 = new GameObject("Player");
var obj2 = new GameObject("Enemy");
pool.AddToPool(ReusePoolSubType.Reusable, obj1);
pool.AddToPool(ReusePoolSubType.Reusable, obj2);

// 检查池状态
Console.WriteLine($"可重用对象数: {pool.SelfReusablePool.Count}");
Console.WriteLine($"有可重用对象: {pool.HasReusableObject}");
Console.WriteLine($"池是否已满: {pool.IsReusePoolFull}");

// 重用对象
GameObject reusedObj = pool.TransferResueToUsing();
if (reusedObj != null)
{
    Console.WriteLine($"重用了对象: {reusedObj.Name}");
}

// 检查对象是否在池中
bool inReusablePool = pool.InPool(ReusePoolSubType.Reusable, obj1);
bool inUsingPool = pool.InPool(ReusePoolSubType.Using, reusedObj);
Console.WriteLine($"obj1在可重用池: {inReusablePool}");
Console.WriteLine($"reusedObj在使用池: {inUsingPool}");
```

#### 对象转移操作
```csharp
var pool = new ReuseObjectPool<GameObject>();

// 添加对象到可重用池
var obj = new GameObject("TestObject");
pool.AddToPool(ReusePoolSubType.Reusable, obj);

// 转移到使用池
bool transferSuccess = pool.TransferTo(ReusePoolSubType.Using, obj);
Console.WriteLine($"转移到使用池: {transferSuccess}");

// 转移到销毁池
transferSuccess = pool.TransferTo(ReusePoolSubType.Destroying, obj);
Console.WriteLine($"转移到销毁池: {transferSuccess}");

// 在两个池之间转移
GameObject transferredObj = pool.TransferBetween(
    ReusePoolSubType.Destroying, 
    ReusePoolSubType.Reusable, 
    obj
);
Console.WriteLine($"转移回可重用池: {transferredObj != null}");
```

#### 池清理操作
```csharp
var pool = new ReuseObjectPool<GameObject>();

// 添加一些对象
for (int i = 0; i < 5; i++)
{
    var obj = new GameObject($"Object_{i}");
    pool.AddToPool(ReusePoolSubType.Reusable, obj);
}

// 清空可重用池
GameObject[] clearedObjects = pool.ClearSubPool(ReusePoolSubType.Reusable);
Console.WriteLine($"清空了 {clearedObjects.Length} 个对象");

// 清空所有池
GameObject[] allClearedObjects = pool.ClearAll();
Console.WriteLine($"清空了所有 {allClearedObjects.Length} 个对象");
```

#### 遍历池对象
```csharp
var pool = new ReuseObjectPool<GameObject>();

// 添加对象到不同池
for (int i = 0; i < 3; i++)
{
    var obj = new GameObject($"Reusable_{i}");
    pool.AddToPool(ReusePoolSubType.Reusable, obj);
}

for (int i = 0; i < 2; i++)
{
    var obj = new GameObject($"Using_{i}");
    pool.AddToPool(ReusePoolSubType.Using, obj);
}

// 遍历可重用池
Console.WriteLine("可重用池中的对象:");
pool.ForeachElement(ReusePoolSubType.Reusable, obj => {
    Console.WriteLine($"  {obj.Name}");
});

// 遍历使用池
Console.WriteLine("使用池中的对象:");
pool.ForeachElement(ReusePoolSubType.Using, obj => {
    Console.WriteLine($"  {obj.Name}");
});
```

#### 键值对象池使用
```csharp
// 创建键值对象池
var kvPool = new KVObjectPool<string, GameObject>();

// 添加对象
kvPool.Add("player", new GameObject("Player"));
kvPool.Add("enemy", new GameObject("Enemy"));
kvPool.Add("item", new GameObject("Item"));

Console.WriteLine($"池中对象数: {kvPool.Count}");

// 获取对象
if (kvPool.TryGet("player", out GameObject player))
{
    Console.WriteLine($"获取到玩家对象: {player.Name}");
}

// 检查键是否存在
bool hasEnemy = kvPool.ContainsKey("enemy");
Console.WriteLine($"包含敌人键: {hasEnemy}");

// 移除对象
bool removed = kvPool.Remove("item");
Console.WriteLine($"移除物品对象: {removed}");

// 遍历所有对象
kvPool.ForEach((key, value) => {
    Console.WriteLine($"键: {key}, 值: {value.Name}");
});
```

#### 元对象池使用
```csharp
// 创建元对象池
var metaPool = new MetaObjectPool<GameObject>(maxPoolSize: 20);

// 添加对象到不同的元数据池
metaPool.Add("players", new GameObject("Player1"));
metaPool.Add("players", new GameObject("Player2"));
metaPool.Add("enemies", new GameObject("Enemy1"));
metaPool.Add("items", new GameObject("Item1"));

// 获取对象
GameObject player = metaPool.Get("players");
Console.WriteLine($"获取到玩家: {player?.Name}");

// 检查池状态
bool playersEmpty = metaPool.IsEmpty("players");
int playersCount = metaPool.GetPoolSize("players");
Console.WriteLine($"玩家池为空: {playersEmpty}");
Console.WriteLine($"玩家池大小: {playersCount}");

// 遍历特定池
Console.WriteLine("玩家池中的对象:");
metaPool.ForEach("players", obj => {
    Console.WriteLine($"  {obj.Name}");
});

// 清空特定池
GameObject[] clearedPlayers = metaPool.ClearPool("players");
Console.WriteLine($"清空了 {clearedPlayers.Length} 个玩家对象");
```

#### 对象生命周期管理
```csharp
// 游戏对象生命周期管理示例
var gameObjectPool = new ReuseObjectPool<GameObject>();

// 创建对象并添加到可重用池
GameObject CreateGameObject(string name)
{
    var obj = new GameObject(name);
    gameObjectPool.AddToPool(ReusePoolSubType.Reusable, obj);
    return obj;
}

// 激活对象（从可重用池转移到使用池）
GameObject ActivateGameObject()
{
    return gameObjectPool.TransferResueToUsing();
}

// 停用对象（从使用池转移到可重用池）
void DeactivateGameObject(GameObject obj)
{
    gameObjectPool.TransferTo(ReusePoolSubType.Reusable, obj);
}

// 销毁对象（从任何池转移到销毁池）
void DestroyGameObject(GameObject obj)
{
    gameObjectPool.TransferTo(ReusePoolSubType.Destroying, obj);
}

// 使用示例
var obj1 = CreateGameObject("Player");
var obj2 = CreateGameObject("Enemy");

Console.WriteLine($"可重用对象数: {gameObjectPool.SelfReusablePool.Count}"); // 2

var activeObj = ActivateGameObject();
Console.WriteLine($"激活对象: {activeObj.Name}");
Console.WriteLine($"可重用对象数: {gameObjectPool.SelfReusablePool.Count}"); // 1
Console.WriteLine($"使用中对象数: {gameObjectPool.SelfUsingPool.Count}"); // 1

DeactivateGameObject(activeObj);
Console.WriteLine($"停用对象: {activeObj.Name}");
Console.WriteLine($"可重用对象数: {gameObjectPool.SelfReusablePool.Count}"); // 2
Console.WriteLine($"使用中对象数: {gameObjectPool.SelfUsingPool.Count}"); // 0
```

### 设计特点

1. **三池设计**：可重用、使用中、销毁三个状态池
2. **类型安全**：泛型设计确保类型安全
3. **性能优化**：减少对象创建和GC压力
4. **灵活管理**：支持多种对象池管理方式
5. **生命周期跟踪**：完整的对象生命周期管理
6. **易于使用**：简洁的API设计

### 注意事项

1. **对象状态**：注意对象在不同池中的状态管理
2. **内存使用**：合理设置池大小避免内存浪费
3. **线程安全**：多线程环境下需要额外的同步机制
4. **对象清理**：及时清理不需要的对象
5. **池大小控制**：避免池过大影响性能
6. **对象复用**：确保复用的对象状态正确重置 