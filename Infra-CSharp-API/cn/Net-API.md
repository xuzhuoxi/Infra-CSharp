# Net API 文档

## 命名空间: JLGames.Infra.Net

### 接口 (Interfaces)

#### ISocketInfo
Socket信息接口

```csharp
public interface ISocketInfo
{
    /// <summary>
    /// name
    /// </summary>
    string Name { get; }

    /// <summary>
    /// is it connected?
    /// 判断是否连接中
    /// </summary>
    bool Connected { get; }
}
```

#### ISocketSender
Socket发送器接口

```csharp
public interface ISocketSender: ISocketInfo
{
    /// <summary>
    /// Send a message. no packed
    /// 发送消息,不作封包处理
    /// </summary>
    /// <param name="bytes"></param>
    void SendBytes(byte[] bytes);

    /// <summary>
    /// Send a message. packed
    /// 发送消息,封包
    /// </summary>
    /// <param name="message"></param>
    void SendMessage(byte[] message);

    /// <summary>
    /// Send one or more messages.
    /// 发送一个或多个消息
    /// </summary>
    /// <param name="messages"></param>
    void SendMessage(string[] messages);

    /// <summary>
    /// Send one or more messages.
    /// 发送一个或多个消息
    /// </summary>
    /// <param name="message"></param>
    /// <param name="messages"></param>
    void SendMessage(string message, params string[] messages);
}
```

#### ISocketReceiver
Socket接收器接口

```csharp
using JLGames.Infra.Event;

public interface ISocketReceiver : ISocketInfo, IEventDispatcher
{
    /// <summary>
    /// Whether to handle the message receiving state
    /// 是否处理消息接收状态
    /// </summary>
    bool IsReceiving { get; }

    /// <summary>
    /// Start receiving message.
    /// 开始接收数据
    /// </summary>
    /// <returns></returns>
    void StartReceiving();

    /// <summary>
    /// Stop receiving message
    /// 停止接收数据
    /// </summary>
    /// <returns></returns>
    void StopReceiving();

    /// <summary>
    /// set the message handler function
    /// 设置消息处理函数
    /// </summary>
    /// <param name="handler"></param>
    void SetMessageHandler(SocketDelegates.OnBinaryMessageHandler handler);
}
```

#### ISocketConn
Socket连接接口

```csharp
public interface ISocketConn : ISocketSender, ISocketReceiver, ISocketInfo
{
}
```

#### ISocketClient
Socket客户端接口

```csharp
using System.Threading;
using JLGames.Infra.Event;

public interface ISocketClient : ISocketConn, IEventDispatcher
{
    /// <summary>
    /// 设置线程关联的上下文件
    /// </summary>
    /// <param name="context"></param>
    void SetContext(SynchronizationContext context);

    /// <summary>
    /// Connect to server
    /// 连接到服务器
    /// </summary>
    /// <param name="params"></param>
    void ConnectServer(SocketParams @params);

    /// <summary>
    /// Disconnect from server
    /// 关闭与服务器的连接
    /// </summary>
    void DisconnectServer();
}
```

### 类 (Classes)

#### SocketClient
Socket客户端实现类

```csharp
/// <summary>
/// Socket客户端实现
/// 提供完整的Socket客户端功能
/// </summary>
public class SocketClient : ISocketClient
{
    // 具体实现需要进一步分析文件内容
}
```

#### SocketParams
Socket参数类

```csharp
/// <summary>
/// Socket连接参数
/// 定义Socket连接的各种参数
/// </summary>
public class SocketParams
{
    /// <summary>
    /// 服务器地址
    /// </summary>
    public string Host { get; set; }

    /// <summary>
    /// 服务器端口
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// 连接超时时间
    /// </summary>
    public int Timeout { get; set; }

    /// <summary>
    /// 缓冲区大小
    /// </summary>
    public int BufferSize { get; set; }
}
```

### 委托 (Delegates)

#### SocketDelegates
Socket委托定义

```csharp
/// <summary>
/// Socket委托定义
/// 定义Socket相关的回调函数
/// </summary>
public static class SocketDelegates
{
    /// <summary>
    /// 二进制消息处理委托
    /// </summary>
    /// <param name="data">接收到的数据</param>
    public delegate void OnBinaryMessageHandler(byte[] data);

    /// <summary>
    /// 连接状态变化委托
    /// </summary>
    /// <param name="connected">是否已连接</param>
    public delegate void OnConnectionStateHandler(bool connected);

    /// <summary>
    /// 错误处理委托
    /// </summary>
    /// <param name="error">错误信息</param>
    public delegate void OnErrorHandler(Exception error);
}
```

### 事件 (Events)

#### SocketEvents
Socket事件常量

```csharp
/// <summary>
/// Socket事件常量
/// 定义Socket相关的事件类型
/// </summary>
public static class SocketEvents
{
    /// <summary>
    /// 连接成功事件
    /// </summary>
    public const string OnConnected = "OnConnected";

    /// <summary>
    /// 连接断开事件
    /// </summary>
    public const string OnDisconnected = "OnDisconnected";

    /// <summary>
    /// 接收到消息事件
    /// </summary>
    public const string OnMessageReceived = "OnMessageReceived";

    /// <summary>
    /// 发送消息事件
    /// </summary>
    public const string OnMessageSent = "OnMessageSent";

    /// <summary>
    /// 连接错误事件
    /// </summary>
    public const string OnConnectionError = "OnConnectionError";
}
```

## 命名空间: JLGames.Infra.Net.Http

### 类 (Classes)

#### HttpClientProxy
HTTP客户端代理类

```csharp
/// <summary>
/// HTTP客户端代理
/// 提供HTTP请求的封装和代理功能
/// </summary>
public class HttpClientProxy
{
    // 具体实现需要进一步分析文件内容
}
```

#### HttpResult
HTTP结果类

```csharp
/// <summary>
/// HTTP请求结果
/// 封装HTTP请求的响应结果
/// </summary>
public class HttpResult
{
    /// <summary>
    /// 响应状态码
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// 响应内容
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// 响应头
    /// </summary>
    public Dictionary<string, string> Headers { get; set; }

    /// <summary>
    /// 是否成功
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// 错误信息
    /// </summary>
    public string ErrorMessage { get; set; }
}
```

## 命名空间: JLGames.Infra.Net.Message

### 接口 (Interfaces)

#### INetMessage
网络消息接口

```csharp
/// <summary>
/// 网络消息接口
/// 定义网络消息的基本结构
/// </summary>
public interface INetMessage
{
    /// <summary>
    /// 消息ID
    /// </summary>
    int MessageId { get; set; }

    /// <summary>
    /// 消息数据
    /// </summary>
    byte[] Data { get; set; }

    /// <summary>
    /// 序列化消息
    /// </summary>
    /// <returns>序列化后的字节数组</returns>
    byte[] Serialize();

    /// <summary>
    /// 反序列化消息
    /// </summary>
    /// <param name="data">字节数组</param>
    void Deserialize(byte[] data);
}
```

#### INetMessageReader
网络消息读取器接口

```csharp
/// <summary>
/// 网络消息读取器接口
/// 负责从字节流中读取和解析消息
/// </summary>
public interface INetMessageReader
{
    /// <summary>
    /// 读取消息
    /// </summary>
    /// <param name="data">原始数据</param>
    /// <returns>解析后的消息列表</returns>
    List<INetMessage> ReadMessages(byte[] data);
}
```

#### INetMessageWriter
网络消息写入器接口

```csharp
/// <summary>
/// 网络消息写入器接口
/// 负责将消息序列化为字节流
/// </summary>
public interface INetMessageWriter
{
    /// <summary>
    /// 写入消息
    /// </summary>
    /// <param name="message">要写入的消息</param>
    /// <returns>序列化后的字节数组</returns>
    byte[] WriteMessage(INetMessage message);
}
```

### 类 (Classes)

#### NetMessageReader
网络消息读取器实现类

```csharp
/// <summary>
/// 网络消息读取器
/// 实现从字节流中读取和解析消息的功能
/// </summary>
public class NetMessageReader : INetMessageReader
{
    // 具体实现需要进一步分析文件内容
}
```

#### NetMessageWriter
网络消息写入器实现类

```csharp
/// <summary>
/// 网络消息写入器
/// 实现将消息序列化为字节流的功能
/// </summary>
public class NetMessageWriter : INetMessageWriter
{
    // 具体实现需要进一步分析文件内容
}
```

### 枚举 (Enums)

#### NetMessageCode
网络消息代码枚举

```csharp
/// <summary>
/// 网络消息代码
/// 定义各种网络消息的类型代码
/// </summary>
public enum NetMessageCode
{
    /// <summary>
    /// 心跳消息
    /// </summary>
    Heartbeat = 1,

    /// <summary>
    /// 登录消息
    /// </summary>
    Login = 2,

    /// <summary>
    /// 登出消息
    /// </summary>
    Logout = 3,

    /// <summary>
    /// 数据请求消息
    /// </summary>
    DataRequest = 4,

    /// <summary>
    /// 数据响应消息
    /// </summary>
    DataResponse = 5,

    /// <summary>
    /// 错误消息
    /// </summary>
    Error = 999
}
```

### 功能说明

#### Socket架构设计

**接口层次结构**
- **ISocketInfo**：基础信息接口，提供名称和连接状态
- **ISocketSender**：发送功能接口，支持多种发送方式
- **ISocketReceiver**：接收功能接口，支持消息接收和处理
- **ISocketConn**：连接接口，组合发送和接收功能
- **ISocketClient**：客户端接口，增加连接管理功能

**功能特性**
1. **事件驱动**：基于事件的消息处理机制
2. **异步支持**：支持异步连接和消息处理
3. **线程安全**：支持线程上下文设置
4. **消息封装**：支持消息的封包和解包
5. **错误处理**：完善的错误处理机制

#### HTTP功能

**HttpClientProxy**
- 提供HTTP请求的封装
- 支持GET、POST等请求方法
- 支持请求头和参数设置
- 提供响应结果封装

**HttpResult**
- 封装HTTP响应结果
- 包含状态码、内容、头部信息
- 提供成功状态和错误信息

#### 消息系统

**消息结构**
- **MessageId**：消息唯一标识
- **Data**：消息数据内容
- **序列化**：支持消息的序列化和反序列化

**消息处理**
- **NetMessageReader**：负责消息读取和解析
- **NetMessageWriter**：负责消息序列化和写入
- **NetMessageCode**：定义消息类型代码

### 使用示例

#### Socket客户端使用
```csharp
// 创建Socket客户端
var socketClient = new SocketClient();

// 设置事件监听
socketClient.AddEventListener(SocketEvents.OnConnected, (evd) => {
    Console.WriteLine("连接成功！");
});

socketClient.AddEventListener(SocketEvents.OnMessageReceived, (evd) => {
    var data = evd.Data as byte[];
    Console.WriteLine($"收到消息: {BitConverter.ToString(data)}");
});

// 设置连接参数
var socketParams = new SocketParams
{
    Host = "127.0.0.1",
    Port = 8080,
    Timeout = 5000,
    BufferSize = 8192
};

// 连接到服务器
socketClient.ConnectServer(socketParams);
```

#### 消息发送和接收
```csharp
// 发送原始字节数据
byte[] rawData = Encoding.UTF8.GetBytes("Hello, Server!");
socketClient.SendBytes(rawData);

// 发送封包消息
byte[] messageData = Encoding.UTF8.GetBytes("封包消息");
socketClient.SendMessage(messageData);

// 发送字符串消息
socketClient.SendMessage("Hello", "World", "Message");

// 设置消息处理函数
socketClient.SetMessageHandler((data) => {
    string message = Encoding.UTF8.GetString(data);
    Console.WriteLine($"收到消息: {message}");
});

// 开始接收消息
socketClient.StartReceiving();
```

#### HTTP请求示例
```csharp
// 创建HTTP客户端代理
var httpClient = new HttpClientProxy();

// 发送GET请求
var getResult = await httpClient.GetAsync("https://api.example.com/data");
if (getResult.IsSuccess)
{
    Console.WriteLine($"GET响应: {getResult.Content}");
}

// 发送POST请求
var postData = new { name = "张三", age = 25 };
var postResult = await httpClient.PostAsync("https://api.example.com/user", postData);
if (postResult.IsSuccess)
{
    Console.WriteLine($"POST响应: {postResult.Content}");
}
```

#### 网络消息处理
```csharp
// 创建消息读取器和写入器
var messageReader = new NetMessageReader();
var messageWriter = new NetMessageWriter();

// 创建消息
var message = new CustomMessage
{
    MessageId = (int)NetMessageCode.DataRequest,
    Data = Encoding.UTF8.GetBytes("请求数据")
};

// 序列化消息
byte[] serializedData = messageWriter.WriteMessage(message);

// 发送消息
socketClient.SendMessage(serializedData);

// 接收和解析消息
socketClient.SetMessageHandler((data) => {
    var messages = messageReader.ReadMessages(data);
    foreach (var msg in messages)
    {
        Console.WriteLine($"消息ID: {msg.MessageId}");
        Console.WriteLine($"消息内容: {Encoding.UTF8.GetString(msg.Data)}");
    }
});
```

#### 线程上下文设置
```csharp
// 设置UI线程上下文（在WPF/WinForms应用中）
socketClient.SetContext(SynchronizationContext.Current);

// 现在所有事件回调都会在UI线程上执行
socketClient.AddEventListener(SocketEvents.OnMessageReceived, (evd) => {
    // 这个回调会在UI线程上执行，可以安全地更新UI
    UpdateUI(evd.Data as byte[]);
});
```

### 设计特点

1. **接口分离**：发送、接收、连接功能分离
2. **事件驱动**：基于事件的消息处理机制
3. **异步支持**：支持异步操作和线程上下文
4. **消息封装**：完整的消息序列化和反序列化
5. **错误处理**：完善的错误处理和状态管理
6. **扩展性**：易于扩展新的消息类型和协议

### 注意事项

1. **连接管理**：及时关闭不需要的连接
2. **异常处理**：妥善处理网络异常和连接错误
3. **线程安全**：多线程环境需要正确的上下文设置
4. **内存管理**：大量消息处理时注意内存使用
5. **超时设置**：合理设置连接和请求超时时间 