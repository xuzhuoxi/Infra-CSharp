# Pool API 文档

## 命名空间: JLGames.Infra.Pool

### 枚举 (Enums)

#### ReusePoolSubType
对象子池类型枚举

```csharp
/// <summary>
/// 对象子池类型
/// </summary>
public enum ReusePoolSubType
{
    /// <summary>
    /// 可重用
    /// </summary>
    Reusable,

    /// <summary>
    /// 使用中
    /// </summary>
    Using,

    /// <summary>
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
/// 重用对象池，内含三个子池：可重用、使用中、待销毁。
/// </summary>
/// <typeparam name="T">池内对象类型。</typeparam>
public class ReuseObjectPool<T>
{
    private readonly int m_MaxReuseCount;
    private readonly List<T>[] m_PoolList;

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="initCapacity">各子池初始容量。</param>
    /// <param name="maxReuseCount">可重用子池允许的最大对象数。</param>
    public ReuseObjectPool(int initCapacity = 8, int maxReuseCount = 100);

    // 公共属性
    /// <summary>
    /// 可重用对象池
    /// </summary>
    public List<T> SelfReusablePool { get; }

    /// <summary>
    /// 使用中对象池
    /// </summary>
    public List<T> SelfUsingPool { get; }

    /// <summary>
    /// 准备销毁对象池
    /// </summary>
    public List<T> SelfDestoryingPool { get; }

    /// <summary>
    /// 是否有可重用对象
    /// </summary>
    public bool HasReusableObject { get; }

    /// <summary>
    /// 重用对象池是否已满
    /// </summary>
    public bool IsReusePoolFull { get; }

    // 公共方法
    /// <summary>
    /// 检查子池中是否有对象
    /// </summary>
    /// <param name="type">要检查的子池类型。</param>
    /// <returns>子池为空时返回 true。</returns>
    public bool IsPoolEmpty(ReusePoolSubType type);

    /// <summary>
    /// 检查对象是否存在于池中
    /// </summary>
    /// <param name="type">要检索的子池类型。</param>
    /// <param name="o">待查找的对象。</param>
    /// <returns>对象存在于该子池时返回 true。</returns>
    public bool InPool(ReusePoolSubType type, T o);

    /// <summary>
    /// 移动对象到目标池中
    /// 如果对象本身就在池中，返回失败。
    /// 如果对象在其它池中，移除后增加到目标池中
    /// </summary>
    /// <param name="targetType">目标子池类型。</param>
    /// <param name="o">要移动的对象。</param>
    /// <returns>对象已在目标池时返回 false；否则表示是否移动成功。</returns>
    public bool TransferTo(ReusePoolSubType targetType, T o);

    /// <summary>
    /// 重用一个对象
    /// 从重用池中移除一个对象，并加入到使用池中
    /// </summary>
    /// <returns>从可重用池取出并移入使用池的对象；可重用池为空时为 default。</returns>
    public T TransferResueToUsing();

    /// <summary>
    /// 从池中移除对象
    /// </summary>
    /// <param name="sourceType">源子池类型。</param>
    /// <param name="o">要移除的对象。</param>
    /// <returns>成功移除时返回 true。</returns>
    public bool RemoveFormPool(ReusePoolSubType sourceType, T o);

    /// <summary>
    /// 将对象加入目标子池。
    /// </summary>
    /// <param name="targetType">目标子池类型。</param>
    /// <param name="o">要加入的对象。</param>
    /// <returns>加入成功返回 true；对象为 null、已存在或可重用池已满时返回 false。</returns>
    public bool AddToPool(ReusePoolSubType targetType, T o);

    /// <summary>
    /// 将对象从一个子池转移到另一个子池。
    /// </summary>
    /// <param name="sourceType">源子池类型。</param>
    /// <param name="targetType">目标子池类型。</param>
    /// <param name="o">要转移的对象。</param>
    /// <returns>转移成功返回该对象，失败时为 default。</returns>
    public T TransferBetween(ReusePoolSubType sourceType, ReusePoolSubType targetType, T o);

    /// <summary>
    /// 清空子池对象
    /// </summary>
    /// <param name="type">要清空的子池类型。</param>
    /// <returns>清空前子池中的对象数组；子池不存在时为 null。</returns>
    public T[] ClearSubPool(ReusePoolSubType type);

    /// <summary>
    /// 清空全部池内对象
    /// </summary>
    /// <returns>清空前所有子池中的对象合并数组。</returns>
    public T[] ClearAll();

    /// <summary>
    /// 遍历子池全部元素
    /// </summary>
    /// <param name="poolType">要遍历的子池类型。</param>
    /// <param name="action">对每个元素执行的回调。</param>
    public void ForeachElement(ReusePoolSubType poolType, Action<T> action);

    // 保护方法
    protected T[] ClearSubPool(List<T> subPool);
    protected List<T> GetSubPool(ReusePoolSubType type);
    protected T TransferBetween(List<T> sourcePool, List<T> targetPool, T o);
    protected T TransferBetween(List<T> sourcePool, List<T> targetPool, int sourceIndex);
    protected T RemoveFormPool(List<T> subPool, int index);
    protected bool RemoveFormPool(List<T> subPool, T o);
    protected bool RemoveFormPool(T o);
    protected bool AddToPool(List<T> subPool, T o);
}
```

#### KVObjectPool<TKey, TValue>
键值映射对象池类

```csharp
/// <summary>
/// Key-Value 映射对象池
/// </summary>
/// <typeparam name="TKey">字典键类型。</typeparam>
/// <typeparam name="TValue">存储的引用类型。</typeparam>
public sealed class KVObjectPool<TKey, TValue> where TValue : class
{
    private readonly Dictionary<TKey, TValue> m_CacheMap;

    /// <summary>
    /// 使用默认字典容量创建对象池。
    /// </summary>
    public KVObjectPool();

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="size">字典初始容量。</param>
    public KVObjectPool(int size);

    /// <summary>
    /// 按键添加或覆盖条目。
    /// </summary>
    /// <param name="key">条目键。</param>
    /// <param name="value">要存储的对象。</param>
    public void Add(TKey key, TValue value);

    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="key">要删除的条目键。</param>
    public void Remove(TKey key);

    /// <summary>
    /// 删除全部
    /// </summary>
    public void RemoveAll();

    /// <summary>
    /// 检测
    /// </summary>
    /// <param name="key">条目键。</param>
    /// <returns>键存在时返回 true。</returns>
    public bool ContainsKey(TKey key);

    /// <summary>
    /// 取值
    /// </summary>
    /// <param name="key">条目键。</param>
    /// <returns>已存储的对象；键不存在时为 default。</returns>
    public TValue GetValue(TKey key);

    /// <summary>
    /// 克隆
    /// </summary>
    /// <param name="key">要克隆其值的条目键。</param>
    /// <param name="cloneAction">可选自定义克隆委托；为 null 时使用 ICloneable。</param>
    /// <returns>克隆结果；键不存在或不支持克隆时为 default。</returns>
    public TValue CloneValue(TKey key, PoolDelegate.CloneObject<TValue> cloneAction = null);
}
```

#### MetaObjectPool<T>
原型对象池类

```csharp
/// <summary>
/// 原型对象池
/// 通过调整池Size，自动增加或减少对象
/// </summary>
/// <typeparam name="T">池内管理的引用类型。</typeparam>
public class MetaObjectPool<T> where T : class
{
    /// <summary>
    /// 创建新池对象的工厂委托。
    /// </summary>
    /// <returns>新创建的对象实例。</returns>
    public delegate T OriginGenFunc();

    /// <summary>
    /// 创建时回调
    /// </summary>
    /// <param name="o">新创建的对象实例。</param>
    public delegate void CreateCallback(T o);

    /// <summary>
    /// 移除时销毁回调
    /// </summary>
    /// <param name="o">从池中移除的对象实例。</param>
    public delegate void DestroyCallback(T o);

    protected readonly T m_Original;
    protected readonly OriginGenFunc m_OriginGenFuncGen;
    protected readonly List<T> m_ObjectPool;
    protected CreateCallback m_CreateCallback = null;
    protected DestroyCallback m_DestroyCallback = null;

    /// <summary>
    /// 池中当前对象数量。
    /// </summary>
    public int Count { get; }

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="original">原型对象</param>
    /// <param name="size">初始对象数量</param>
    /// <param name="capacity">对象池初始容量</param>
    public MetaObjectPool(T original, int size = 0, int capacity = 0);

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="originGenFuncGen">对象构造器</param>
    /// <param name="size">初始对象数量</param>
    /// <param name="capacity">对象池初始容量</param>
    public MetaObjectPool(OriginGenFunc originGenFuncGen, int size = 0, int capacity = 0);

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="original">原型对象</param>
    /// <param name="size">初始对象数量</param>
    public MetaObjectPool(T original, int size);

    /// <summary>
    /// 构造函数
    /// </summary>
    /// <param name="originGenFuncGen">对象构造器</param>
    /// <param name="size">初始对象数量</param>
    public MetaObjectPool(OriginGenFunc originGenFuncGen, int size);

    /// <summary>
    /// 设置创建对象时回调
    /// </summary>
    /// <param name="callback">对象被添加时触发的回调。</param>
    public void SetCreateCallback(CreateCallback callback);

    /// <summary>
    /// 设置删除对象时回调
    /// </summary>
    /// <param name="callback">对象被移除时触发的回调。</param>
    public void SetDestroyCallback(DestroyCallback callback);

    /// <summary>
    /// 将池调整到目标对象数量。
    /// </summary>
    /// <param name="size">目标对象数量。</param>
    /// <returns>新增或移除的对象数组；数量未变时为 null。</returns>
    public T[] UpdateToSize(int size);

    /// <summary>
    /// 通过差值更新对象数量
    /// </summary>
    /// <param name="offset">数量偏差（正数增加，负数减少）。</param>
    /// <returns>新增或移除的对象数组；offset 为 0 时为 null。</returns>
    public T[] Offset(int offset);

    /// <summary>
    /// 删除对象数量
    /// </summary>
    /// <param name="removeSize">要移除的对象数量。</param>
    /// <returns>被移除的对象数组；removeSize 小于等于 0 时为 null。</returns>
    public virtual T[] Remove(int removeSize);

    /// <summary>
    /// 增加对象数量
    /// </summary>
    /// <param name="addSize">要新增的对象数量。</param>
    /// <returns>新增的对象数组；addSize 小于等于 0 时为 null。</returns>
    public virtual T[] Add(int addSize);

    /// <summary>
    /// 按索引获取或设置池中对象。
    /// </summary>
    /// <param name="index">从 0 开始的索引。</param>
    public T this[int index] { get; set; }

    /// <summary>
    /// 取第一个元素
    /// </summary>
    public T First { get; }

    /// <summary>
    /// 取最后一个元素
    /// </summary>
    public T Last { get; }

    /// <summary>
    /// 查找第一个匹配项
    /// </summary>
    /// <param name="match">匹配条件。</param>
    /// <returns>第一个匹配项；无匹配时为 default。</returns>
    public T FindFirst(Predicate<T> match);

    /// <summary>
    /// 查找最后一个匹配项
    /// </summary>
    /// <param name="match">匹配条件。</param>
    /// <returns>最后一个匹配项；无匹配时为 default。</returns>
    public T FindLast(Predicate<T> match);

    /// <summary>
    /// 移除第一个匹配项
    /// </summary>
    /// <param name="match">匹配条件。</param>
    /// <returns>被移除的实例；无匹配时为 default。</returns>
    public virtual T RemoveFirst(Predicate<T> match);

    /// <summary>
    /// 删除最后一个匹配项
    /// </summary>
    /// <param name="match">匹配条件。</param>
    /// <returns>被移除的实例；无匹配时为 default。</returns>
    public virtual T RemoveLast(Predicate<T> match);

    protected virtual T NewObject();
}
```

### 静态类 (Static Classes)

#### PoolDelegate
对象池相关委托

```csharp
/// <summary>
/// 对象池相关委托。
/// </summary>
public static class PoolDelegate
{
    /// <summary>
    /// 克隆对象实例。
    /// </summary>
    /// <param name="origin">待克隆的源实例。</param>
    /// <typeparam name="T">待克隆的引用类型。</typeparam>
    /// <returns>克隆后的实例。</returns>
    public delegate T CloneObject<T>(T origin) where T : class;
}
```

### 功能说明

#### 对象池类型

**ReuseObjectPool<T>**
- **三池设计**：可重用池、使用中池、待销毁池
- **对象生命周期管理**：在三个子池之间跟踪对象状态
- **对象转移**：`TransferTo`、`TransferBetween`、`TransferResueToUsing`
- **容量控制**：可配置可重用子池的最大对象数量；已满时向可重用池添加会失败

**KVObjectPool<TKey, TValue>**
- **键值映射**：按键存储引用类型对象（`TValue : class`）
- **添加覆盖**：`Add` 会按键写入，已存在则覆盖
- **取值与克隆**：`GetValue` 按键取值；`CloneValue` 可通过自定义委托或 `ICloneable` / `ICloneable<TValue>` 克隆
- **密封类型**：`sealed` 类，不可继承

**MetaObjectPool<T>**
- **原型对象池**：基于原型对象（需可克隆）或工厂委托创建实例（`T : class`）
- **自动扩缩**：通过 `UpdateToSize`、`Offset`、`Add`、`Remove` 调整池内对象数量
- **创建/销毁回调**：`SetCreateCallback`、`SetDestroyCallback`
- **查找与索引**：索引器、`First` / `Last`、`FindFirst` / `FindLast`、`RemoveFirst` / `RemoveLast`

#### 对象池特性

**性能优化**
1. **内存复用**：避免频繁的对象创建和销毁
2. **GC压力减少**：减少垃圾回收的压力
3. **快速分配**：预分配对象，快速获取

**生命周期管理**
1. **状态跟踪**：`ReuseObjectPool<T>` 跟踪对象所在子池
2. **数量调节**：`MetaObjectPool<T>` 按目标数量增减实例
3. **资源清理**：提供清空、移除与销毁回调

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

// 添加或覆盖对象
kvPool.Add("player", new GameObject("Player"));
kvPool.Add("enemy", new GameObject("Enemy"));
kvPool.Add("item", new GameObject("Item"));

// 检测键是否存在
bool hasEnemy = kvPool.ContainsKey("enemy");
Console.WriteLine($"包含敌人键: {hasEnemy}");

// 按键取值
GameObject player = kvPool.GetValue("player");
if (player != null)
{
    Console.WriteLine($"获取到玩家对象: {player.Name}");
}

// 自定义克隆
GameObject cloned = kvPool.CloneValue("player", origin => new GameObject(origin.Name));
Console.WriteLine($"克隆对象: {cloned?.Name}");

// 删除单个条目
kvPool.Remove("item");
Console.WriteLine($"删除后仍包含 item: {kvPool.ContainsKey("item")}");

// 删除全部
kvPool.RemoveAll();
```

#### 原型对象池使用
```csharp
// 使用工厂委托创建原型对象池
var metaPool = new MetaObjectPool<GameObject>(
    () => new GameObject("Bullet"),
    size: 3,
    capacity: 10
);

Console.WriteLine($"池中对象数: {metaPool.Count}");

// 设置创建 / 销毁回调
metaPool.SetCreateCallback(obj => Console.WriteLine($"创建: {obj.Name}"));
metaPool.SetDestroyCallback(obj => Console.WriteLine($"销毁: {obj.Name}"));

// 增加 / 减少对象
GameObject[] added = metaPool.Add(2);
Console.WriteLine($"新增 {added.Length} 个对象");

GameObject[] removed = metaPool.Remove(1);
Console.WriteLine($"移除 {removed.Length} 个对象");

// 调整到目标数量
metaPool.UpdateToSize(5);

// 访问对象
GameObject first = metaPool.First;
GameObject last = metaPool.Last;
GameObject byIndex = metaPool[0];
Console.WriteLine($"First: {first?.Name}, Last: {last?.Name}, [0]: {byIndex?.Name}");

// 按条件查找 / 移除
GameObject found = metaPool.FindFirst(o => o.Name == "Bullet");
GameObject lastFound = metaPool.FindLast(o => o.Name == "Bullet");
GameObject removedFirst = metaPool.RemoveFirst(o => o.Name == "Bullet");
```

#### 通过原型对象克隆创建
```csharp
public class Bullet : ICloneable<Bullet>
{
    public int Damage { get; set; }
    public Bullet Clone() => new Bullet { Damage = Damage };
}

var prototype = new Bullet { Damage = 10 };
var pool = new MetaObjectPool<Bullet>(prototype, size: 4);

Console.WriteLine($"池中对象数: {pool.Count}"); // 4
Console.WriteLine($"首个对象伤害: {pool.First?.Damage}");
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

1. **三池设计**：`ReuseObjectPool<T>` 提供可重用、使用中、待销毁三个状态池
2. **类型安全**：泛型设计；`KVObjectPool` / `MetaObjectPool` 约束值为引用类型
3. **性能优化**：减少对象创建和 GC 压力
4. **灵活管理**：重用池、键值映射池、原型数量池三种管理方式
5. **生命周期跟踪**：子池转移、创建/销毁回调、按条件查找与移除
6. **易于使用**：构造参数带默认值，API 职责清晰

### 注意事项

1. **对象状态**：注意对象在 `ReuseObjectPool<T>` 不同子池中的状态管理
2. **内存使用**：合理设置可重用上限与 `MetaObjectPool<T>` 的初始数量/容量
3. **线程安全**：多线程环境下需要额外的同步机制
4. **对象清理**：及时清理不需要的对象；`MetaObjectPool<T>` 的 `Remove` 会触发销毁回调
5. **池大小控制**：避免池过大影响性能；可重用池已满时 `AddToPool` 返回 false
6. **对象复用**：确保复用的对象状态正确重置
7. **克隆要求**：`KVObjectPool.CloneValue` 在未传入 `cloneAction` 时需要值实现 `ICloneable<TValue>` 或 `ICloneable`；`MetaObjectPool<T>` 使用原型构造时同样需要原型可克隆，否则应使用 `OriginGenFunc`
8. **空值处理**：向 `ReuseObjectPool<T>` 加入 `null` 会失败；查找失败或池为空时返回 `default`
