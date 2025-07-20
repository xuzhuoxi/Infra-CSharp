# Pool API Documentation

## Namespace: JLGames.Infra.Pool

### Enums

#### ReusePoolSubType
Object subpool type enumeration

```csharp
/// <summary>
/// Object subpool type
/// </summary>
public enum ReusePoolSubType
{
    /// <summary>
    /// Reusable
    /// </summary>
    Reusable,

    /// <summary>
    /// Using
    /// </summary>
    Using,

    /// <summary>
    /// Destroying
    /// </summary>
    Destroying
}
```

### Classes

#### ReuseObjectPool<T>
Reusable object pool class

```csharp
/// <summary>
/// Reusable object pool
/// Contains three sub-pools internally: reusable object pool, in-use object pool, and destroying object pool
/// </summary>
/// <typeparam name="T">Object type</typeparam>
public class ReuseObjectPool<T>
{
    private readonly int m_MaxReuseCount;
    private readonly List<T>[] m_PoolList;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="initCapacity">Capacity size of sub pool</param>
    /// <param name="maxReuseCount">Maximum number of reused objects</param>
    public ReuseObjectPool(int initCapacity = 8, int maxReuseCount = 100);

    // Public properties
    /// <summary>
    /// Reusable Object Pool
    /// </summary>
    public List<T> SelfReusablePool { get; }

    /// <summary>
    /// Using Object Pool
    /// </summary>
    public List<T> SelfUsingPool { get; }

    /// <summary>
    /// Destroying Object Pool
    /// </summary>
    public List<T> SelfDestoryingPool { get; }

    /// <summary>
    /// Exist reusable objects
    /// </summary>
    public bool HasReusableObject { get; }

    /// <summary>
    /// Whether the reused object pool is full
    /// </summary>
    public bool IsReusePoolFull { get; }

    // Public methods
    /// <summary>
    /// Check if there is an object in the subpool
    /// </summary>
    /// <param name="type">Subpool type</param>
    /// <returns>Whether empty</returns>
    public bool IsPoolEmpty(ReusePoolSubType type);

    /// <summary>
    /// Check if the object exists in the pool
    /// </summary>
    /// <param name="type">Subpool type</param>
    /// <param name="o">Object to check</param>
    /// <returns>Whether exists in pool</returns>
    public bool InPool(ReusePoolSubType type, T o);

    /// <summary>
    /// Move the object to the target pool
    /// If the object itself is in the pool, return failure.
    /// If the object is in another pool, remove it and add it to the target pool
    /// </summary>
    /// <param name="targetType">Target pool type</param>
    /// <param name="o">Object to move</param>
    /// <returns>Whether successful</returns>
    public bool TransferTo(ReusePoolSubType targetType, T o);

    /// <summary>
    /// Reuse an object
    /// Remove an object from the reuse pool and add it to the usage pool
    /// </summary>
    /// <returns>Reused object</returns>
    public T TransferResueToUsing();

    /// <summary>
    /// Remove the object from the pool
    /// </summary>
    /// <param name="sourceType">Source pool type</param>
    /// <param name="o">Object to remove</param>
    /// <returns>Whether successfully removed</returns>
    public bool RemoveFormPool(ReusePoolSubType sourceType, T o);

    /// <summary>
    /// Add object to target pool.
    /// </summary>
    /// <param name="targetType">Target pool type</param>
    /// <param name="o">Object to add</param>
    /// <returns>Whether successfully added</returns>
    public bool AddToPool(ReusePoolSubType targetType, T o);

    /// <summary>
    /// Transfer object to target pool.
    /// </summary>
    /// <param name="sourceType">Source pool type</param>
    /// <param name="targetType">Target pool type</param>
    /// <param name="o">Object to transfer</param>
    /// <returns>Transferred object</returns>
    public T TransferBetween(ReusePoolSubType sourceType, ReusePoolSubType targetType, T o);

    /// <summary>
    /// Clear all objects in sub pool
    /// </summary>
    /// <param name="type">Subpool type</param>
    /// <returns>Cleared object array</returns>
    public T[] ClearSubPool(ReusePoolSubType type);

    /// <summary>
    /// Clear all objects
    /// </summary>
    /// <returns>Cleared object array</returns>
    public T[] ClearAll();

    /// <summary>
    /// Traverse all elements of the subpool
    /// </summary>
    /// <param name="poolType">Pool type</param>
    /// <param name="action">Traversal action</param>
    public void ForeachElement(ReusePoolSubType poolType, Action<T> action);

    // Protected methods
    /// <summary>
    /// Clear subpool
    /// </summary>
    /// <param name="subPool">Subpool</param>
    /// <returns>Cleared object array</returns>
    protected T[] ClearSubPool(List<T> subPool);

    /// <summary>
    /// Get subpool
    /// </summary>
    /// <param name="type">Pool type</param>
    /// <returns>Subpool</returns>
    protected List<T> GetSubPool(ReusePoolSubType type);

    /// <summary>
    /// Transfer object between two pools
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
    /// Remove object from pool
    /// </summary>
    /// <param name="subPool">Subpool</param>
    /// <param name="o">Object to remove</param>
    /// <returns>Whether successfully removed</returns>
    protected bool RemoveFormPool(List<T> subPool, T o);

    /// <summary>
    /// Remove object from all pools
    /// </summary>
    /// <param name="o">Object to remove</param>
    /// <returns>Whether successfully removed</returns>
    protected bool RemoveFormPool(T o);

    /// <summary>
    /// Add object to pool
    /// </summary>
    /// <param name="subPool">Subpool</param>
    /// <param name="o">Object to add</param>
    /// <returns>Whether successfully added</returns>
    protected bool AddToPool(List<T> subPool, T o);
}
```

#### KVObjectPool<K, V>
Key-value object pool class

```csharp
/// <summary>
/// Key-value object pool class
/// Provides key-value pair based object pool management
/// </summary>
/// <typeparam name="K">Key type</typeparam>
/// <typeparam name="V">Value type</typeparam>
public class KVObjectPool<K, V>
{
    private readonly Dictionary<K, V> m_Pool;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="capacity">Initial capacity</param>
    public KVObjectPool(int capacity = 16);

    /// <summary>
    /// Number of objects in pool
    /// </summary>
    public int Count { get; }

    /// <summary>
    /// Add object to pool
    /// </summary>
    /// <param name="key">Key</param>
    /// <param name="value">Value</param>
    /// <returns>Whether successfully added</returns>
    public bool Add(K key, V value);

    /// <summary>
    /// Get object from pool
    /// </summary>
    /// <param name="key">Key</param>
    /// <param name="value">Output value</param>
    /// <returns>Whether successfully retrieved</returns>
    public bool TryGet(K key, out V value);

    /// <summary>
    /// Remove object from pool
    /// </summary>
    /// <param name="key">Key</param>
    /// <returns>Whether successfully removed</returns>
    public bool Remove(K key);

    /// <summary>
    /// Check if pool contains specified key
    /// </summary>
    /// <param name="key">Key</param>
    /// <returns>Whether contains</returns>
    public bool ContainsKey(K key);

    /// <summary>
    /// Clear pool
    /// </summary>
    public void Clear();

    /// <summary>
    /// Traverse all objects in pool
    /// </summary>
    /// <param name="action">Traversal action</param>
    public void ForEach(Action<K, V> action);
}
```

#### MetaObjectPool<T>
Meta object pool class

```csharp
/// <summary>
/// Meta object pool class
/// Provides metadata-based object pool management
/// </summary>
/// <typeparam name="T">Object type</typeparam>
public class MetaObjectPool<T>
{
    private readonly Dictionary<string, List<T>> m_Pool;
    private readonly int m_MaxPoolSize;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="maxPoolSize">Maximum pool size</param>
    public MetaObjectPool(int maxPoolSize = 100);

    /// <summary>
    /// Add object to specified metadata pool
    /// </summary>
    /// <param name="metaKey">Metadata key</param>
    /// <param name="obj">Object</param>
    /// <returns>Whether successfully added</returns>
    public bool Add(string metaKey, T obj);

    /// <summary>
    /// Get object from specified metadata pool
    /// </summary>
    /// <param name="metaKey">Metadata key</param>
    /// <returns>Object, returns default value if pool is empty</returns>
    public T Get(string metaKey);

    /// <summary>
    /// Remove object from specified metadata pool
    /// </summary>
    /// <param name="metaKey">Metadata key</param>
    /// <returns>Removed object</returns>
    public T Remove(string metaKey);

    /// <summary>
    /// Check if specified metadata pool is empty
    /// </summary>
    /// <param name="metaKey">Metadata key</param>
    /// <returns>Whether empty</returns>
    public bool IsEmpty(string metaKey);

    /// <summary>
    /// Get size of specified metadata pool
    /// </summary>
    /// <param name="metaKey">Metadata key</param>
    /// <returns>Pool size</returns>
    public int GetPoolSize(string metaKey);

    /// <summary>
    /// Clear specified metadata pool
    /// </summary>
    /// <param name="metaKey">Metadata key</param>
    /// <returns>Cleared object array</returns>
    public T[] ClearPool(string metaKey);

    /// <summary>
    /// Clear all pools
    /// </summary>
    public void ClearAll();

    /// <summary>
    /// Traverse all objects in specified metadata pool
    /// </summary>
    /// <param name="metaKey">Metadata key</param>
    /// <param name="action">Traversal action</param>
    public void ForEach(string metaKey, Action<T> action);
}
```

### Static Classes

#### PoolDelegate
Object pool delegate definitions

```csharp
/// <summary>
/// Object pool delegate definitions
/// Defines various callback functions used in object pools
/// </summary>
public static class PoolDelegate
{
    /// <summary>
    /// Clone object
    /// </summary>
    /// <param name="origin">Original object</param>
    /// <typeparam name="T">Object type</typeparam>
    /// <returns>Cloned object</returns>
    public delegate T CloneObject<T>(T origin) where T : class;
}
```

### Function Description

#### Object Pool Types

**ReuseObjectPool<T>**
- **Three-pool design**: Reusable pool, in-use pool, destroying pool
- **Object lifecycle management**: Complete object lifecycle tracking
- **Automatic transfer**: Supports automatic transfer of objects between different pools
- **Capacity control**: Configurable maximum number of reusable objects

**KVObjectPool<K, V>**
- **Key-value management**: Key-value pair based object storage
- **Fast lookup**: O(1) time complexity object lookup
- **Flexible storage**: Supports arbitrary key and value types

**MetaObjectPool<T>**
- **Metadata grouping**: Object grouping based on metadata keys
- **Multi-pool management**: Supports multiple independent object pools
- **Capacity limits**: Configurable maximum capacity for each pool

#### Object Pool Features

**Performance Optimization**
1. **Memory reuse**: Avoids frequent object creation and destruction
2. **GC pressure reduction**: Reduces garbage collection pressure
3. **Fast allocation**: Pre-allocates objects for fast retrieval

**Lifecycle Management**
1. **State tracking**: Tracks object usage state
2. **Automatic recycling**: Supports automatic object recycling mechanism
3. **Resource cleanup**: Provides complete resource cleanup functionality

### Usage Examples

#### Basic Object Pool Usage
```csharp
// Create object pool
var pool = new ReuseObjectPool<GameObject>(initCapacity: 10, maxReuseCount: 50);

// Add objects to reusable pool
var obj1 = new GameObject("Player");
var obj2 = new GameObject("Enemy");
pool.AddToPool(ReusePoolSubType.Reusable, obj1);
pool.AddToPool(ReusePoolSubType.Reusable, obj2);

// Check pool status
Console.WriteLine($"Reusable objects: {pool.SelfReusablePool.Count}");
Console.WriteLine($"Has reusable objects: {pool.HasReusableObject}");
Console.WriteLine($"Pool is full: {pool.IsReusePoolFull}");

// Reuse object
GameObject reusedObj = pool.TransferResueToUsing();
if (reusedObj != null)
{
    Console.WriteLine($"Reused object: {reusedObj.Name}");
}

// Check if object is in pool
bool inReusablePool = pool.InPool(ReusePoolSubType.Reusable, obj1);
bool inUsingPool = pool.InPool(ReusePoolSubType.Using, reusedObj);
Console.WriteLine($"obj1 in reusable pool: {inReusablePool}");
Console.WriteLine($"reusedObj in using pool: {inUsingPool}");
```

#### Object Transfer Operations
```csharp
var pool = new ReuseObjectPool<GameObject>();

// Add object to reusable pool
var obj = new GameObject("TestObject");
pool.AddToPool(ReusePoolSubType.Reusable, obj);

// Transfer to using pool
bool transferSuccess = pool.TransferTo(ReusePoolSubType.Using, obj);
Console.WriteLine($"Transfer to using pool: {transferSuccess}");

// Transfer to destroying pool
transferSuccess = pool.TransferTo(ReusePoolSubType.Destroying, obj);
Console.WriteLine($"Transfer to destroying pool: {transferSuccess}");

// Transfer between two pools
GameObject transferredObj = pool.TransferBetween(
    ReusePoolSubType.Destroying, 
    ReusePoolSubType.Reusable, 
    obj
);
Console.WriteLine($"Transfer back to reusable pool: {transferredObj != null}");
```

#### Pool Cleanup Operations
```csharp
var pool = new ReuseObjectPool<GameObject>();

// Add some objects
for (int i = 0; i < 5; i++)
{
    var obj = new GameObject($"Object_{i}");
    pool.AddToPool(ReusePoolSubType.Reusable, obj);
}

// Clear reusable pool
GameObject[] clearedObjects = pool.ClearSubPool(ReusePoolSubType.Reusable);
Console.WriteLine($"Cleared {clearedObjects.Length} objects");

// Clear all pools
GameObject[] allClearedObjects = pool.ClearAll();
Console.WriteLine($"Cleared all {allClearedObjects.Length} objects");
```

#### Traverse Pool Objects
```csharp
var pool = new ReuseObjectPool<GameObject>();

// Add objects to different pools
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

// Traverse reusable pool
Console.WriteLine("Objects in reusable pool:");
pool.ForeachElement(ReusePoolSubType.Reusable, obj => {
    Console.WriteLine($"  {obj.Name}");
});

// Traverse using pool
Console.WriteLine("Objects in using pool:");
pool.ForeachElement(ReusePoolSubType.Using, obj => {
    Console.WriteLine($"  {obj.Name}");
});
```

#### Key-Value Object Pool Usage
```csharp
// Create key-value object pool
var kvPool = new KVObjectPool<string, GameObject>();

// Add objects
kvPool.Add("player", new GameObject("Player"));
kvPool.Add("enemy", new GameObject("Enemy"));
kvPool.Add("item", new GameObject("Item"));

Console.WriteLine($"Objects in pool: {kvPool.Count}");

// Get object
if (kvPool.TryGet("player", out GameObject player))
{
    Console.WriteLine($"Retrieved player object: {player.Name}");
}

// Check if key exists
bool hasEnemy = kvPool.ContainsKey("enemy");
Console.WriteLine($"Contains enemy key: {hasEnemy}");

// Remove object
bool removed = kvPool.Remove("item");
Console.WriteLine($"Removed item object: {removed}");

// Traverse all objects
kvPool.ForEach((key, value) => {
    Console.WriteLine($"Key: {key}, Value: {value.Name}");
});
```

#### Meta Object Pool Usage
```csharp
// Create meta object pool
var metaPool = new MetaObjectPool<GameObject>(maxPoolSize: 20);

// Add objects to different metadata pools
metaPool.Add("players", new GameObject("Player1"));
metaPool.Add("players", new GameObject("Player2"));
metaPool.Add("enemies", new GameObject("Enemy1"));
metaPool.Add("items", new GameObject("Item1"));

// Get object
GameObject player = metaPool.Get("players");
Console.WriteLine($"Retrieved player: {player?.Name}");

// Check pool status
bool playersEmpty = metaPool.IsEmpty("players");
int playersCount = metaPool.GetPoolSize("players");
Console.WriteLine($"Players pool is empty: {playersEmpty}");
Console.WriteLine($"Players pool size: {playersCount}");

// Traverse specific pool
Console.WriteLine("Objects in players pool:");
metaPool.ForEach("players", obj => {
    Console.WriteLine($"  {obj.Name}");
});

// Clear specific pool
GameObject[] clearedPlayers = metaPool.ClearPool("players");
Console.WriteLine($"Cleared {clearedPlayers.Length} player objects");
```

#### Object Lifecycle Management
```csharp
// Game object lifecycle management example
var gameObjectPool = new ReuseObjectPool<GameObject>();

// Create object and add to reusable pool
GameObject CreateGameObject(string name)
{
    var obj = new GameObject(name);
    gameObjectPool.AddToPool(ReusePoolSubType.Reusable, obj);
    return obj;
}

// Activate object (transfer from reusable pool to using pool)
GameObject ActivateGameObject()
{
    return gameObjectPool.TransferResueToUsing();
}

// Deactivate object (transfer from using pool to reusable pool)
void DeactivateGameObject(GameObject obj)
{
    gameObjectPool.TransferTo(ReusePoolSubType.Reusable, obj);
}

// Destroy object (transfer from any pool to destroying pool)
void DestroyGameObject(GameObject obj)
{
    gameObjectPool.TransferTo(ReusePoolSubType.Destroying, obj);
}

// Usage example
var obj1 = CreateGameObject("Player");
var obj2 = CreateGameObject("Enemy");

Console.WriteLine($"Reusable objects: {gameObjectPool.SelfReusablePool.Count}"); // 2

var activeObj = ActivateGameObject();
Console.WriteLine($"Activated object: {activeObj.Name}");
Console.WriteLine($"Reusable objects: {gameObjectPool.SelfReusablePool.Count}"); // 1
Console.WriteLine($"Using objects: {gameObjectPool.SelfUsingPool.Count}"); // 1

DeactivateGameObject(activeObj);
Console.WriteLine($"Deactivated object: {activeObj.Name}");
Console.WriteLine($"Reusable objects: {gameObjectPool.SelfReusablePool.Count}"); // 2
Console.WriteLine($"Using objects: {gameObjectPool.SelfUsingPool.Count}"); // 0
```

### Design Features

1. **Three-pool design**: Reusable, in-use, and destroying state pools
2. **Type safety**: Generic design ensures type safety
3. **Performance optimization**: Reduces object creation and GC pressure
4. **Flexible management**: Supports multiple object pool management methods
  5. **Lifecycle tracking**: Complete object lifecycle management
  6. **Easy to use**: Clean API design

### Notes

1. **Object state**: Pay attention to object state management in different pools
2. **Memory usage**: Reasonably set pool size to avoid memory waste
3. **Thread safety**: Multi-threaded environments require additional synchronization mechanisms
4. **Object cleanup**: Clean up unnecessary objects in time
5. **Pool size control**: Avoid pools being too large affecting performance
6. **Object reuse**: Ensure reused objects are correctly reset 