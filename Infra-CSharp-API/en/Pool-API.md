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
/// Reusable object pool with three sub-pools: reusable, in-use, and pending destroy.
/// </summary>
/// <typeparam name="T">Pooled object type.</typeparam>
public class ReuseObjectPool<T>
{
    private readonly int m_MaxReuseCount;
    private readonly List<T>[] m_PoolList;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="initCapacity">Initial capacity of each sub-pool.</param>
    /// <param name="maxReuseCount">Maximum objects allowed in the reusable sub-pool.</param>
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
    /// <param name="type">Sub-pool to check.</param>
    /// <returns>True if the sub-pool has no objects.</returns>
    public bool IsPoolEmpty(ReusePoolSubType type);

    /// <summary>
    /// Check if the object exists in the pool
    /// </summary>
    /// <param name="type">Sub-pool to search.</param>
    /// <param name="o">Object to look up.</param>
    /// <returns>True if the object exists in the sub-pool.</returns>
    public bool InPool(ReusePoolSubType type, T o);

    /// <summary>
    /// Move the object to the target pool
    /// If the object itself is in the pool, return failure.
    /// If the object is in another pool, remove it and add it to the target pool
    /// </summary>
    /// <param name="targetType">Destination sub-pool.</param>
    /// <param name="o">Object to move.</param>
    /// <returns>False if already in target pool; otherwise whether the move succeeded.</returns>
    public bool TransferTo(ReusePoolSubType targetType, T o);

    /// <summary>
    /// Reuse an object
    /// Remove an object from the reuse pool and add it to the usage pool
    /// </summary>
    /// <returns>Reused object moved to the in-use pool, or default if reusable pool is empty.</returns>
    public T TransferResueToUsing();

    /// <summary>
    /// Remove the object from the pool
    /// </summary>
    /// <param name="sourceType">Sub-pool to remove from.</param>
    /// <param name="o">Object to remove.</param>
    /// <returns>True if the object was removed.</returns>
    public bool RemoveFormPool(ReusePoolSubType sourceType, T o);

    /// <summary>
    /// Add object to target pool.
    /// </summary>
    /// <param name="targetType">Destination sub-pool.</param>
    /// <param name="o">Object to add.</param>
    /// <returns>True if added; false if null, duplicate, or reusable pool is full.</returns>
    public bool AddToPool(ReusePoolSubType targetType, T o);

    /// <summary>
    /// Transfer an object from one sub-pool to another.
    /// </summary>
    /// <param name="sourceType">Source sub-pool.</param>
    /// <param name="targetType">Destination sub-pool.</param>
    /// <param name="o">Object to transfer.</param>
    /// <returns>Transferred object on success, or default on failure.</returns>
    public T TransferBetween(ReusePoolSubType sourceType, ReusePoolSubType targetType, T o);

    /// <summary>
    /// Clear all objects in sub pool
    /// </summary>
    /// <param name="type">Sub-pool to clear.</param>
    /// <returns>Objects that were in the sub-pool before clearing; null if sub-pool not found.</returns>
    public T[] ClearSubPool(ReusePoolSubType type);

    /// <summary>
    /// Clear all objects
    /// </summary>
    /// <returns>All objects from every sub-pool before clearing.</returns>
    public T[] ClearAll();

    /// <summary>
    /// Traverse all elements of the subpool
    /// </summary>
    /// <param name="poolType">Sub-pool to traverse.</param>
    /// <param name="action">Action invoked per element.</param>
    public void ForeachElement(ReusePoolSubType poolType, Action<T> action);

    // Protected methods
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
Key-value mapping object pool class

```csharp
/// <summary>
/// Key-Value mapping object pool
/// </summary>
/// <typeparam name="TKey">Dictionary key type.</typeparam>
/// <typeparam name="TValue">Stored reference type.</typeparam>
public sealed class KVObjectPool<TKey, TValue> where TValue : class
{
    private readonly Dictionary<TKey, TValue> m_CacheMap;

    /// <summary>
    /// Create a pool with default dictionary capacity.
    /// </summary>
    public KVObjectPool();

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="size">Initial dictionary capacity.</param>
    public KVObjectPool(int size);

    /// <summary>
    /// Add or replace an entry by key.
    /// </summary>
    /// <param name="key">Entry key.</param>
    /// <param name="value">Object to store.</param>
    public void Add(TKey key, TValue value);

    /// <summary>
    /// Remove
    /// </summary>
    /// <param name="key">Entry key to remove.</param>
    public void Remove(TKey key);

    /// <summary>
    /// Remove all
    /// </summary>
    public void RemoveAll();

    /// <summary>
    /// Check exist
    /// </summary>
    /// <param name="key">Entry key.</param>
    /// <returns>True if the key exists.</returns>
    public bool ContainsKey(TKey key);

    /// <summary>
    /// Get object
    /// </summary>
    /// <param name="key">Entry key.</param>
    /// <returns>Stored value, or default if key not found.</returns>
    public TValue GetValue(TKey key);

    /// <summary>
    /// Clone object
    /// </summary>
    /// <param name="key">Entry key whose value to clone.</param>
    /// <param name="cloneAction">Optional custom clone delegate; uses ICloneable when null.</param>
    /// <returns>Cloned value, or default if key missing or cloning unsupported.</returns>
    public TValue CloneValue(TKey key, PoolDelegate.CloneObject<TValue> cloneAction = null);
}
```

#### MetaObjectPool<T>
Meta / prototype object pool class

```csharp
/// <summary>
/// Meta object pool.
/// Modify the pool size to automatically increase or decrease objects.
/// </summary>
/// <typeparam name="T">Pooled reference type.</typeparam>
public class MetaObjectPool<T> where T : class
{
    /// <summary>
    /// Factory that creates a new pooled instance.
    /// </summary>
    /// <returns>New instance.</returns>
    public delegate T OriginGenFunc();

    /// <summary>
    /// Callback on creation
    /// </summary>
    /// <param name="o">Newly created instance.</param>
    public delegate void CreateCallback(T o);

    /// <summary>
    /// Callback on destroy
    /// </summary>
    /// <param name="o">Instance being removed from the pool.</param>
    public delegate void DestroyCallback(T o);

    protected readonly T m_Original;
    protected readonly OriginGenFunc m_OriginGenFuncGen;
    protected readonly List<T> m_ObjectPool;
    protected CreateCallback m_CreateCallback = null;
    protected DestroyCallback m_DestroyCallback = null;

    /// <summary>
    /// Current number of objects in the pool.
    /// </summary>
    public int Count { get; }

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="original">Meta Object</param>
    /// <param name="size">Number of initial objects</param>
    /// <param name="capacity">Object pool initial capacity</param>
    public MetaObjectPool(T original, int size = 0, int capacity = 0);

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="originGenFuncGen">Object constructor</param>
    /// <param name="size">Number of initial objects</param>
    /// <param name="capacity">Object pool initial capacity</param>
    public MetaObjectPool(OriginGenFunc originGenFuncGen, int size = 0, int capacity = 0);

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="original">Meta Object</param>
    /// <param name="size">Number of initial objects</param>
    public MetaObjectPool(T original, int size);

    /// <summary>
    /// constructor
    /// </summary>
    /// <param name="originGenFuncGen">Object constructor</param>
    /// <param name="size">Number of initial objects</param>
    public MetaObjectPool(OriginGenFunc originGenFuncGen, int size);

    /// <summary>
    /// Setting callback on creation
    /// </summary>
    /// <param name="callback">Callback invoked when an object is added.</param>
    public void SetCreateCallback(CreateCallback callback);

    /// <summary>
    /// Setting callback on destroy
    /// </summary>
    /// <param name="callback">Callback invoked when an object is removed.</param>
    public void SetDestroyCallback(DestroyCallback callback);

    /// <summary>
    /// Resize the pool to the target count.
    /// </summary>
    /// <param name="size">Target object count.</param>
    /// <returns>Added or removed instances; null if count unchanged.</returns>
    public T[] UpdateToSize(int size);

    /// <summary>
    /// Update number of objects by offset
    /// </summary>
    /// <param name="offset">Delta count (positive to add, negative to remove).</param>
    /// <returns>Added or removed instances; null if offset is 0.</returns>
    public T[] Offset(int offset);

    /// <summary>
    /// Remove number of objects.
    /// </summary>
    /// <param name="removeSize">Number of instances to remove.</param>
    /// <returns>Removed instances; null if removeSize is less than or equal to 0.</returns>
    public virtual T[] Remove(int removeSize);

    /// <summary>
    /// Add number of objects.
    /// </summary>
    /// <param name="addSize">Number of instances to add.</param>
    /// <returns>Newly added instances; null if addSize is less than or equal to 0.</returns>
    public virtual T[] Add(int addSize);

    /// <summary>
    /// Gets or sets the object at the specified index.
    /// </summary>
    /// <param name="index">Zero-based index.</param>
    public T this[int index] { get; set; }

    /// <summary>
    /// get the first element
    /// </summary>
    public T First { get; }

    /// <summary>
    /// get the last element
    /// </summary>
    public T Last { get; }

    /// <summary>
    /// find the first matched element.
    /// </summary>
    /// <param name="match">Predicate for matching.</param>
    /// <returns>First match, or default if none.</returns>
    public T FindFirst(Predicate<T> match);

    /// <summary>
    /// find the last matched element.
    /// </summary>
    /// <param name="match">Predicate for matching.</param>
    /// <returns>Last match, or default if none.</returns>
    public T FindLast(Predicate<T> match);

    /// <summary>
    /// remove the first matched element.
    /// </summary>
    /// <param name="match">Predicate for matching.</param>
    /// <returns>Removed instance, or default if none matched.</returns>
    public virtual T RemoveFirst(Predicate<T> match);

    /// <summary>
    /// remove the last matched element.
    /// </summary>
    /// <param name="match">Predicate for matching.</param>
    /// <returns>Removed instance, or default if none matched.</returns>
    public virtual T RemoveLast(Predicate<T> match);

    protected virtual T NewObject();
}
```

### Static Classes

#### PoolDelegate
Pool-related delegates

```csharp
/// <summary>
/// Pool-related delegates.
/// </summary>
public static class PoolDelegate
{
    /// <summary>
    /// Clone an object instance.
    /// </summary>
    /// <param name="origin">Source instance to clone.</param>
    /// <typeparam name="T">Reference type to clone.</typeparam>
    /// <returns>Cloned instance.</returns>
    public delegate T CloneObject<T>(T origin) where T : class;
}
```

### Function Description

#### Object Pool Types

**ReuseObjectPool<T>**
- **Three-pool design**: Reusable pool, in-use pool, pending-destroy pool
- **Object lifecycle management**: Tracks object state across the three sub-pools
- **Object transfer**: `TransferTo`, `TransferBetween`, `TransferResueToUsing`
- **Capacity control**: Configurable maximum objects in the reusable sub-pool; adding to a full reusable pool fails

**KVObjectPool<TKey, TValue>**
- **Key-value mapping**: Stores reference-type values by key (`TValue : class`)
- **Add or replace**: `Add` writes by key and overwrites an existing entry
- **Get and clone**: `GetValue` looks up by key; `CloneValue` clones via a custom delegate or `ICloneable` / `ICloneable<TValue>`
- **Sealed type**: `sealed` class, not inheritable

**MetaObjectPool<T>**
- **Prototype pool**: Creates instances from a prototype (must be cloneable) or a factory delegate (`T : class`)
- **Automatic resize**: Adjusts instance count via `UpdateToSize`, `Offset`, `Add`, `Remove`
- **Create/destroy callbacks**: `SetCreateCallback`, `SetDestroyCallback`
- **Lookup and indexing**: Indexer, `First` / `Last`, `FindFirst` / `FindLast`, `RemoveFirst` / `RemoveLast`

#### Object Pool Features

**Performance Optimization**
1. **Memory reuse**: Avoids frequent object creation and destruction
2. **GC pressure reduction**: Reduces garbage collection pressure
3. **Fast allocation**: Pre-allocates objects for fast retrieval

**Lifecycle Management**
1. **State tracking**: `ReuseObjectPool<T>` tracks which sub-pool an object is in
2. **Count adjustment**: `MetaObjectPool<T>` grows or shrinks to a target count
3. **Resource cleanup**: Clear, remove, and destroy-callback support

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

// Add or replace objects
kvPool.Add("player", new GameObject("Player"));
kvPool.Add("enemy", new GameObject("Enemy"));
kvPool.Add("item", new GameObject("Item"));

// Check if key exists
bool hasEnemy = kvPool.ContainsKey("enemy");
Console.WriteLine($"Contains enemy key: {hasEnemy}");

// Get by key
GameObject player = kvPool.GetValue("player");
if (player != null)
{
    Console.WriteLine($"Retrieved player object: {player.Name}");
}

// Clone with a custom delegate
GameObject cloned = kvPool.CloneValue("player", origin => new GameObject(origin.Name));
Console.WriteLine($"Cloned object: {cloned?.Name}");

// Remove a single entry
kvPool.Remove("item");
Console.WriteLine($"Still contains item: {kvPool.ContainsKey("item")}");

// Remove all
kvPool.RemoveAll();
```

#### Prototype Object Pool Usage
```csharp
// Create a prototype pool with a factory
var metaPool = new MetaObjectPool<GameObject>(
    () => new GameObject("Bullet"),
    size: 3,
    capacity: 10
);

Console.WriteLine($"Objects in pool: {metaPool.Count}");

// Set create / destroy callbacks
metaPool.SetCreateCallback(obj => Console.WriteLine($"Created: {obj.Name}"));
metaPool.SetDestroyCallback(obj => Console.WriteLine($"Destroyed: {obj.Name}"));

// Add / remove objects
GameObject[] added = metaPool.Add(2);
Console.WriteLine($"Added {added.Length} objects");

GameObject[] removed = metaPool.Remove(1);
Console.WriteLine($"Removed {removed.Length} objects");

// Resize to a target count
metaPool.UpdateToSize(5);

// Access objects
GameObject first = metaPool.First;
GameObject last = metaPool.Last;
GameObject byIndex = metaPool[0];
Console.WriteLine($"First: {first?.Name}, Last: {last?.Name}, [0]: {byIndex?.Name}");

// Find / remove by predicate
GameObject found = metaPool.FindFirst(o => o.Name == "Bullet");
GameObject lastFound = metaPool.FindLast(o => o.Name == "Bullet");
GameObject removedFirst = metaPool.RemoveFirst(o => o.Name == "Bullet");
```

#### Creating from a Cloneable Prototype
```csharp
public class Bullet : ICloneable<Bullet>
{
    public int Damage { get; set; }
    public Bullet Clone() => new Bullet { Damage = Damage };
}

var prototype = new Bullet { Damage = 10 };
var pool = new MetaObjectPool<Bullet>(prototype, size: 4);

Console.WriteLine($"Objects in pool: {pool.Count}"); // 4
Console.WriteLine($"First object damage: {pool.First?.Damage}");
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

1. **Three-pool design**: `ReuseObjectPool<T>` provides reusable, in-use, and pending-destroy state pools
2. **Type safety**: Generic design; `KVObjectPool` / `MetaObjectPool` constrain values to reference types
3. **Performance optimization**: Reduces object creation and GC pressure
4. **Flexible management**: Reuse pool, key-value map pool, and prototype count pool
5. **Lifecycle tracking**: Sub-pool transfer, create/destroy callbacks, predicate find/remove
6. **Easy to use**: Constructors with defaults and a focused public API

### Notes

1. **Object state**: Pay attention to object state management across `ReuseObjectPool<T>` sub-pools
2. **Memory usage**: Set the reusable cap and `MetaObjectPool<T>` initial size/capacity reasonably
3. **Thread safety**: Multi-threaded environments require additional synchronization
4. **Object cleanup**: Clean up unused objects in time; `MetaObjectPool<T>.Remove` invokes the destroy callback
5. **Pool size control**: Avoid oversized pools; adding to a full reusable pool returns false
6. **Object reuse**: Ensure reused objects are reset correctly
7. **Cloning requirements**: `KVObjectPool.CloneValue` without `cloneAction` requires `ICloneable<TValue>` or `ICloneable`; a prototype-based `MetaObjectPool<T>` likewise needs a cloneable original, otherwise use `OriginGenFunc`
8. **Null handling**: Adding `null` to `ReuseObjectPool<T>` fails; lookups and empty-pool reuse return `default`
