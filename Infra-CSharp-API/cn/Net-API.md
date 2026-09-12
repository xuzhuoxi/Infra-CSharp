# Net API 文档

## 命名空间: JLGames.Infra.Net

本模块全部公开类型均位于 `JLGames.Infra.Net`。源码按 Socket / Message / Http 目录组织，下文按该分组文档化。

`Socket/Internal` 下的连接/接收适配器（`IConnectAdapter`、`IReceiveAdapter`、`BeginConnectAdapter`、`ConnectAsyncAdapter`、`BeginReceiveAdapter`、`ReceiveAsyncAdapter` 及 `AdapterDelegates`）均为 `internal`，由 `SocketClient` / `SocketReceiver` 按 `apmMode` 选择 APM（Begin/End）或 TAP（`*Async`）实现，不作为公开 API。

---

## Socket

### 接口 (Interfaces)

#### ISocketInfo
Socket 基本信息与连接状态。

```csharp
/// <summary>
/// Socket 基本信息与连接状态。
/// </summary>
public interface ISocketInfo
{
    /// <summary>
    /// Socket 实例名称。
    /// </summary>
    string Name { get; }

    /// <summary>
    /// 是否已连接。
    /// </summary>
    bool Connected { get; }
}
```

#### ISocketSender
Socket 消息发送器。继承自 `ISocketInfo`。

```csharp
/// <summary>
/// Socket 消息发送器。
/// </summary>
public interface ISocketSender : ISocketInfo
{
    /// <summary>
    /// 发送原始字节，不进行封包。
    /// </summary>
    /// <param name="bytes">待发送字节数据。</param>
    void SendBytes(byte[] bytes);

    /// <summary>
    /// 发送已封包的消息（含长度前缀）。
    /// </summary>
    /// <param name="message">消息内容。</param>
    void SendMessage(byte[] message);

    /// <summary>
    /// 发送一个或多个字符串消息（封包）。
    /// </summary>
    /// <param name="messages">字符串消息数组。</param>
    void SendMessage(string[] messages);

    /// <summary>
    /// 发送一个或多个字符串消息（封包）。
    /// </summary>
    /// <param name="message">第一条消息。</param>
    /// <param name="messages">其余消息。</param>
    void SendMessage(string message, params string[] messages);
}
```

`SendMessage` 通过 `INetMessageWriter`（`DataBuffer.WriteData`）写入长度前缀后再发送；`SendBytes` 直接调用底层 `Socket.Send`。

#### ISocketReceiver
Socket 消息接收器，支持事件派发。继承自 `ISocketInfo`、`IEventDispatcher`。

```csharp
using JLGames.Infra.Event;

/// <summary>
/// Socket 消息接收器，支持事件派发。
/// </summary>
public interface ISocketReceiver : ISocketInfo, IEventDispatcher
{
    /// <summary>
    /// 是否正在接收消息。
    /// </summary>
    bool IsReceiving { get; }

    /// <summary>
    /// 开始接收数据。
    /// </summary>
    void StartReceiving();

    /// <summary>
    /// 停止接收数据。
    /// </summary>
    void StopReceiving();

    /// <summary>
    /// 设置二进制消息处理回调。
    /// </summary>
    /// <param name="handler">消息处理函数（可为 null）。</param>
    void SetMessageHandler(SocketDelegates.OnBinaryMessageHandler handler);
}
```

事件监听/派发成员见 [Event API](Event-API.md) 中的 `IEventDispatcher`。

#### ISocketConn
Socket 连接：发送、接收与连接状态。继承自 `ISocketSender`、`ISocketReceiver`、`ISocketInfo`。

```csharp
/// <summary>
/// Socket 连接：发送、接收与连接状态。
/// </summary>
public interface ISocketConn : ISocketSender, ISocketReceiver, ISocketInfo
{
}
```

#### ISocketClient
Socket 客户端：连接/断开、收发数据，并派发连接相关事件。继承自 `ISocketConn`、`IEventDispatcher`。

```csharp
using System.Threading;
using JLGames.Infra.Event;

/// <summary>
/// Socket 客户端：连接/断开、收发数据，并派发连接相关事件。
/// </summary>
public interface ISocketClient : ISocketConn, IEventDispatcher
{
    /// <summary>
    /// 设置同步上下文，将回调封送到指定线程。
    /// </summary>
    /// <param name="context">目标同步上下文。</param>
    void SetContext(SynchronizationContext context);

    /// <summary>
    /// 连接到服务器。
    /// </summary>
    /// <param name="params">连接参数。</param>
    void ConnectServer(SocketParams @params);

    /// <summary>
    /// 关闭与服务器的连接。
    /// </summary>
    void DisconnectServer();
}
```

### 结构体 (Structs)

#### AddressInfo
IP 地址与端口。

```csharp
/// <summary>
/// IP 地址与端口。
/// </summary>
public struct AddressInfo
{
    /// <summary>
    /// IP 地址字符串。
    /// </summary>
    public string IPAddress;

    /// <summary>
    /// 端口号。
    /// </summary>
    public int Port;
}
```

#### SocketParams
Socket 连接参数（地址、协议、WebSocket 选项等）。`[Serializable]` 结构体。

```csharp
/// <summary>
/// Socket 连接参数（地址、协议、WebSocket 选项等）。
/// </summary>
[Serializable]
public struct SocketParams
{
    /// <summary>
    /// 网络协议类型。
    /// </summary>
    public SocketNetworks.Network Network { get; set; }

    /// <summary>
    /// 本地端点，格式为 "host:port"。
    /// </summary>
    public string LocalAddress { get; set; }

    /// <summary>
    /// 远端端点，格式为 "host:port"。
    /// </summary>
    public string RemoteAddress { get; set; }

    /// <summary>
    /// WebSocket 路径模式（例如 "/"、"/echo"）。
    /// </summary>
    public string WSPattern { get; set; }

    /// <summary>
    /// WebSocket Origin 头（例如 "http://127.0.0.1/"，须以 "/" 结尾）。
    /// </summary>
    public string WSOrigin { get; set; }

    /// <summary>
    /// WebSocket 子协议字符串（可为空）。
    /// </summary>
    public string WSProtocol { get; set; }

    public override string ToString();

    /// <summary>
    /// 将 LocalAddress 解析为 EndPoint。
    /// </summary>
    public EndPoint LocalEndPoint();

    /// <summary>
    /// 将 RemoteAddress 解析为 EndPoint。
    /// </summary>
    public EndPoint RemoteEndPoint();
}
```

`LocalEndPoint` / `RemoteEndPoint` 按 `"ip:port"` 拆分并构造 `IPEndPoint`（`IPAddress.Parse` + `int.Parse`）。当前 `SocketFactory.CreateSocket` 对 WebSocket / QUIC 返回 `null`，`WSPattern` / `WSOrigin` / `WSProtocol` 预留给后续协议实现。

### 类 (Classes)

#### SocketClient
`ISocketClient` 的默认实现（连接、收发、事件）。继承 `EventDispatcher`。

```csharp
/// <summary>
/// ISocketClient 的默认实现（连接、收发、事件）。
/// </summary>
public class SocketClient : EventDispatcher, ISocketClient
{
    /// <summary>
    /// Socket 实例名称。
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// 是否已连接。
    /// </summary>
    public bool Connected { get; }

    /// <summary>
    /// 是否正在接收消息。
    /// </summary>
    public bool IsReceiving { get; }

    /// <summary>
    /// 创建 Socket 客户端。
    /// </summary>
    /// <param name="name">客户端名称。</param>
    /// <param name="littleEndian">消息是否小端。</param>
    /// <param name="apmMode">为 true 时使用 APM 异步模型。</param>
    public SocketClient(string name, bool littleEndian, bool apmMode);

    /// <summary>
    /// 更新客户端名称。
    /// </summary>
    /// <param name="name">新名称。</param>
    public void SetName(string name);

    /// <inheritdoc cref="ISocketSender.SendBytes"/>
    public void SendBytes(byte[] bytes);

    /// <inheritdoc cref="ISocketSender.SendMessage(byte[])"/>
    public void SendMessage(byte[] message);

    /// <inheritdoc cref="ISocketSender.SendMessage(string[])"/>
    public void SendMessage(string[] messages);

    /// <inheritdoc cref="ISocketSender.SendMessage(string, string[])"/>
    public void SendMessage(string message, params string[] messages);

    /// <inheritdoc cref="ISocketReceiver.SetMessageHandler"/>
    public void SetMessageHandler(SocketDelegates.OnBinaryMessageHandler handler);

    /// <inheritdoc cref="ISocketReceiver.StartReceiving"/>
    public void StartReceiving();

    /// <inheritdoc cref="ISocketReceiver.StopReceiving"/>
    public void StopReceiving();

    /// <inheritdoc cref="ISocketClient.SetContext"/>
    public void SetContext(SynchronizationContext context);

    /// <inheritdoc cref="ISocketClient.ConnectServer"/>
    public void ConnectServer(SocketParams @params);

    /// <inheritdoc cref="ISocketClient.DisconnectServer"/>
    public void DisconnectServer();
}
```

说明：
- 无无参构造；请使用本构造或 `SocketFactory.CreateSocketClient`。
- `apmMode == true` 时使用 `BeginConnectAdapter` / `BeginReceiveAdapter`，否则使用 `ConnectAsyncAdapter` / `ReceiveAsyncAdapter`。
- 连接成功后内部创建 `SocketSender` 与 `SocketReceiver`（配合 `NetMessageWriter` / `NetMessageReader`，字节序由 `littleEndian` 决定）。
- 已存在连接适配器时再次 `ConnectServer` 会派发 `EventOnConnectionOpen` 且 `Suc = false`，不会替换现有连接。
- 未连接时调用 `DisconnectServer` 会派发 `EventOnConnectionClose` 且 `Suc = false`。
- 已设置 `SetContext` 时，连接/断开/收发事件通过 `SynchronizationContext.Send` 封送；未设置则在回调线程上直接派发。
- 事件监听/派发成员继承自 `EventDispatcher`，见 [Event API](Event-API.md)。

#### SocketSender
`ISocketSender` 的默认实现。

```csharp
/// <summary>
/// ISocketSender 的默认实现。
/// </summary>
public class SocketSender : ISocketSender
{
    public string Name { get; }
    public bool Connected { get; }

    /// <summary>
    /// 为指定 Socket 与消息写入器创建发送器。
    /// </summary>
    /// <param name="name">发送器名称。</param>
    /// <param name="socket">底层 Socket。</param>
    /// <param name="netMessageWriter">消息封包器。</param>
    public SocketSender(string name, Socket socket, INetMessageWriter netMessageWriter);

    public void SendBytes(byte[] bytes);
    public void SendMessage(byte[] message);
    public void SendMessage(string[] messages);
    public void SendMessage(string message, params string[] messages);
}
```

`SocketClient` 在连接成功后会自行创建本类型；也可对已连接的 `Socket` 单独构造使用。

#### SocketReceiver
带异步接收适配器的 `ISocketReceiver` 默认实现。继承 `EventDispatcher`。

```csharp
/// <summary>
/// 带异步接收适配器的 ISocketReceiver 默认实现。
/// </summary>
public class SocketReceiver : EventDispatcher, ISocketReceiver
{
    public string Name { get; }
    public bool Connected { get; }
    public bool IsReceiving { get; }

    /// <summary>
    /// 为指定 Socket 与消息读取器创建接收器。
    /// </summary>
    /// <param name="name">接收器名称。</param>
    /// <param name="socket">底层 Socket。</param>
    /// <param name="reader">消息解包器。</param>
    /// <param name="oldApi">为 true 时使用 APM（Begin/End）接收。</param>
    public SocketReceiver(string name, Socket socket, INetMessageReader reader, bool oldApi);

    public void SetMessageHandler(SocketDelegates.OnBinaryMessageHandler handler);
    public void StartReceiving();
    public void StopReceiving();
}
```

接收缓冲区大小为 8192。解包成功后：
- 调用 `OnBinaryMessageHandler(msg, remoteAddress, other)`（`other` 当前为 `null`；`remoteAddress` 来自 `Socket.RemoteEndPoint`）
- 派发 `SocketEvents.EventOnMessageReceived`，事件数据为 `byte[]`
- 读到 0 字节或出现错误/异常时派发 `SocketEvents.EventOnMessageReceivedEnd`，事件数据为 `SocketReceivedEndInfo`

`SocketClient.StartReceiving` 会转发上述两个事件，并在 `EventOnMessageReceivedEnd` 时自动 `StopReceiving`。

### 静态类 (Static Classes)

#### SocketFactory
创建 Socket 客户端与 `System.Net.Sockets.Socket` 实例的工厂。

```csharp
/// <summary>
/// 创建 Socket 客户端与 Socket 实例的工厂。
/// </summary>
public static class SocketFactory
{
    /// <summary>
    /// 创建 Socket 客户端。
    /// </summary>
    /// <param name="name">客户端名称。</param>
    /// <param name="littleEndian">消息是否使用小端字节序。</param>
    /// <param name="apmMode">为 true 时使用 APM（Begin/End）异步模型，否则使用 TAP。</param>
    /// <returns>Socket 客户端实例。</returns>
    public static ISocketClient CreateSocketClient(string name, bool littleEndian, bool apmMode);

    /// <summary>
    /// 根据连接参数创建 Socket。
    /// </summary>
    /// <param name="params">连接参数。</param>
    /// <returns>已配置的 Socket；不支持的协议（WebSocket、QUIC）返回 null。</returns>
    public static Socket CreateSocket(SocketParams @params);
}
```

`CreateSocket` 行为：
- `Tcp` / `Tcp4`：`AddressFamily.InterNetwork` + `SocketType.Stream` + `ProtocolType.Tcp`，关闭 KeepAlive，开启 NoDelay
- `Tcp6`：`AddressFamily.InterNetworkV6`，其余同上
- `Udp` / `Udp4`：`InterNetwork` + `Dgram` + `Udp`
- `Udp6`：`InterNetworkV6` + `Dgram` + `Udp`
- `WebSocket` / `WebSockets` / `Quic` / 其他：返回 `null`

#### SocketNetworks
网络协议类型常量与转换工具。

```csharp
/// <summary>
/// 网络协议类型常量与转换工具。
/// </summary>
public static class SocketNetworks
{
    /// <summary>
    /// 支持的网络协议类型。
    /// </summary>
    public enum Network
    {
        /// <summary>未定义的网络通信类型</summary>
        Undefined,
        /// <summary>TCP 协议</summary>
        Tcp,
        /// <summary>IPv4 的 TCP 协议</summary>
        Tcp4,
        /// <summary>IPv6 的 TCP 协议</summary>
        Tcp6,
        /// <summary>UDP 协议</summary>
        Udp,
        /// <summary>IPv4 的 UDP 协议</summary>
        Udp4,
        /// <summary>IPv6 的 UDP 协议</summary>
        Udp6,
        /// <summary>WebSocket 协议</summary>
        WebSocket,
        /// <summary>WebSocket Secure 协议</summary>
        WebSockets,
        /// <summary>QUIC 协议</summary>
        Quic,
    }

    /// <summary>未定义的网络通信类型</summary>
    public const string Undefined = "";
    /// <summary>TCP 协议</summary>
    public const string Tcp = "tcp";
    /// <summary>IPv4 的 TCP 协议</summary>
    public const string Tcp4 = "tcp4";
    /// <summary>IPv6 的 TCP 协议</summary>
    public const string Tcp6 = "tcp6";
    /// <summary>UDP 协议</summary>
    public const string Udp = "udp";
    /// <summary>IPv4 的 UDP 协议</summary>
    public const string Udp4 = "udp4";
    /// <summary>IPv6 的 UDP 协议</summary>
    public const string Udp6 = "udp6";
    /// <summary>WebSocket 协议</summary>
    public const string WebSocket = "ws";
    /// <summary>WebSocket Secure 协议</summary>
    public const string WebSocketSecure = "wss";
    /// <summary>QUIC 协议</summary>
    public const string Quic = "quic";

    /// <summary>
    /// 将网络类型枚举转换为字符串标识。
    /// </summary>
    /// <returns>协议字符串（如 "tcp"、"udp"）。</returns>
    public static string GetNetworkValue(Network network);

    /// <summary>
    /// 将协议字符串解析为网络类型枚举（不区分大小写）。
    /// </summary>
    /// <returns>匹配的枚举值；未知时返回 Network.Undefined。</returns>
    public static Network GetNetwork(string network);
}
```

注意：枚举名为 `WebSockets`，对应字符串常量为 `WebSocketSecure`（`"wss"`）。

#### SocketDelegates
Socket 消息处理委托定义。

```csharp
/// <summary>
/// Socket 消息处理委托定义。
/// </summary>
public static class SocketDelegates
{
    /// <summary>
    /// 二进制消息处理。
    /// </summary>
    /// <param name="msg">消息字节。</param>
    /// <param name="remoteAddress">远端地址。</param>
    /// <param name="other">用户自定义上下文（可为 null）。</param>
    public delegate void OnBinaryMessageHandler(byte[] msg, string remoteAddress, object other);

    /// <summary>
    /// 字符串消息处理。
    /// </summary>
    /// <param name="msg">消息文本。</param>
    /// <param name="remoteAddress">远端地址。</param>
    /// <param name="other">用户自定义上下文（可为 null）。</param>
    public delegate void OnStringMessageHandler(string msg, string remoteAddress, object other);
}
```

当前接收路径使用 `OnBinaryMessageHandler`；`OnStringMessageHandler` 已定义但未被 `SocketReceiver` / `ISocketReceiver` 引用。

#### SocketEvents
Socket 事件类型常量与事件数据结构。

```csharp
/// <summary>
/// Socket 事件类型常量与事件数据结构。
/// </summary>
public static class SocketEvents
{
    /// <summary>
    /// 消息接收事件数据（用户与连接映射信息）。
    /// </summary>
    public readonly struct SocketMessageEventInfo
    {
        public string UserId { get; }
        public string ConnId { get; }
        public byte[] BinaryMessage { get; }
        public string StringMessage { get; }
        public SocketError Error { get; }
        public Exception Exception { get; }

        public SocketMessageEventInfo(string connId, string userId, byte[] binaryMessage, string stringMessage, SocketError error, Exception e);
    }

    /// <summary>
    /// 连接建立/关闭结果事件数据。
    /// </summary>
    public struct SocketConnEventInfo
    {
        public bool Suc { get; }
        public SocketError Error { get; }
        public Exception Exception { get; }

        public SocketConnEventInfo(bool suc);
        public SocketConnEventInfo(bool suc, SocketError error);
        public SocketConnEventInfo(bool suc, Exception exception);
        public SocketConnEventInfo(bool suc, SocketError error, Exception exception);

        public override string ToString();
    }

    /// <summary>
    /// 消息接收结束事件数据。
    /// </summary>
    public struct SocketReceivedEndInfo
    {
        public bool Disconnect { get; }
        public SocketError Error { get; }
        public Exception Exception { get; }

        public SocketReceivedEndInfo(bool disconnect);
        public SocketReceivedEndInfo(bool disconnect, SocketError error);
        public SocketReceivedEndInfo(bool disconnect, Exception exception);
        public SocketReceivedEndInfo(bool disconnect, SocketError error, Exception exception);
    }

    /// <summary>开启连接结果事件。事件数据：SocketConnEventInfo</summary>
    public const string EventOnConnectionOpen = "SockEvents.EventOnConnectOpen";

    /// <summary>连接超时事件。事件数据：SocketConnEventInfo</summary>
    public const string EventOnConnectionTimeout = "SockEvents.EventOnConnectionTimeout";

    /// <summary>连接取消。事件数据：SocketConnEventInfo</summary>
    public const string EventOnConnectionCancel = "SockEvents.EventOnConnectionCancel";

    /// <summary>关闭连接结果事件。事件数据：SocketConnEventInfo</summary>
    public const string EventOnConnectionClose = "SockEvents.EventOnConnectClose";

    /// <summary>数据接收处理结束。事件数据：byte[] message</summary>
    public const string EventOnMessageReceived = "SockEvents.EventOnMessageReceived";

    /// <summary>接收结束（通常依据收到的数据为空）。事件数据：SocketReceivedEndInfo</summary>
    public const string EventOnMessageReceivedEnd = "SockEvents.EventOnMessageReceivedEnd";
}
```

`SocketClient` 当前会派发：`EventOnConnectionOpen`、`EventOnConnectionClose`、`EventOnMessageReceived`、`EventOnMessageReceivedEnd`。`EventOnConnectionTimeout` / `EventOnConnectionCancel` 为已定义常量；`SocketMessageEventInfo` 为公开载荷类型，当前接收路径派发的是 `byte[]` 而非该结构。

仅传成功标志时，失败侧默认 `SocketError` 为 `TypeNotFound`（见 `SocketConnEventInfo(bool)` / `SocketReceivedEndInfo(bool)`）。

---

## Message

### 接口 (Interfaces)

#### INetMessage
可序列化/反序列化的网络消息。

```csharp
using JLGames.Infra.Buffer;

/// <summary>
/// 可序列化/反序列化的网络消息。
/// </summary>
public interface INetMessage
{
    /// <summary>
    /// 序列化为字节数组
    /// </summary>
    byte[] EncodeToBytes();

    /// <summary>
    /// 由字节数组构造消息。
    /// </summary>
    /// <param name="bytes">源字节数组。</param>
    void DecodeFromBytes(byte[] bytes);

    /// <summary>
    /// 序列化为字节数组并写入到 IDataBuffer 缓存区
    /// </summary>
    /// <param name="buff">目标缓冲区写入器。</param>
    void EncodeToBuff(IDataBufferWriter buff);

    /// <summary>
    /// 从缓冲区读取并更新当前属性。
    /// </summary>
    /// <param name="buff">源缓冲区读取器。</param>
    void DecodeFromBuff(IDataBufferReader buff);
}
```

#### INetMessageReader
带缓存的消息解包器。继承 `IDataBufferReader`、`IDataBufferCopier`、`IByteBufferReader`、`IByteBufferCopier`（缓冲读写见 [Buffer API](Buffer-API.md)）。

```csharp
using JLGames.Infra.Buffer;

/// <summary>
/// 带缓存的消息解包器
/// </summary>
public interface INetMessageReader : IDataBufferReader, IDataBufferCopier, IByteBufferReader, IByteBufferCopier
{
    /// <summary>
    /// 检查解包器中是否有消息
    /// </summary>
    bool CheckMessage();

    /// <summary>
    /// 读取整条消息
    /// </summary>
    byte[] ReadMessage();

    /// <summary>
    /// 解包消息到指定对象
    /// </summary>
    void ReadMessageTo<T>(ref T o) where T : INetMessage;

    /// <summary>
    /// 解包消息到指定对象
    /// </summary>
    void ReadMessageTo<T>(ref T[] o) where T : INetMessage;

    /// <summary>
    /// 读取整条消息，不移动读下标
    /// </summary>
    byte[] CopyMessage();

    /// <summary>
    /// 解包消息到指定对象，不移动读下标
    /// </summary>
    void CopyMessageTo<T>(ref T o) where T : INetMessage;

    /// <summary>
    /// 解包消息到指定对象，不移动读下标
    /// </summary>
    void CopyMessageTo<T>(ref T[] o) where T : INetMessage;

    /// <summary>
    /// 写入字节数据
    /// </summary>
    /// <param name="src">要写入的数据源</param>
    void WriteMessageBytes(byte[] src);

    /// <summary>
    /// 写入字节数据
    /// </summary>
    /// <param name="src">要写入的数据源</param>
    /// <param name="srcIndex">数据源的索引</param>
    /// <param name="size">数据写入长度</param>
    void WriteMessageBytes(byte[] src, int srcIndex, int size);
}
```

帧格式为长度前缀 + 载荷：`CheckMessage` 通过 `CopyLen()` 判断剩余长度是否足够 `ln + LenSize`。

#### INetMessageWriter
带缓存的消息封包器。继承 `IDataBufferWriter`、`IByteBufferWriter`（见 [Buffer API](Buffer-API.md)）。

```csharp
using JLGames.Infra.Buffer;

/// <summary>
/// 带缓存的消息封包器
/// </summary>
public interface INetMessageWriter : IDataBufferWriter, IByteBufferWriter
{
    /// <summary>
    /// 把消息内容封包进缓存，包含字节长度信息
    /// </summary>
    void WriteMessage(INetMessage msg);

    /// <summary>
    /// 把消息数组封包进缓存，包含数组长度信息
    /// </summary>
    void WriteMessage<T>(T[] msg) where T : INetMessage;

    /// <summary>
    /// 读出缓存中全部字节数据
    /// </summary>
    byte[] ReadMessageBytes();

    /// <summary>
    /// 清除全部字节
    /// </summary>
    void Clear();
}
```

`SocketSender.SendMessage` 使用继承的 `WriteData` 写入单条/多条载荷（含长度前缀），再 `ReadMessageBytes` 取出后 `Socket.Send`。

### 类 (Classes)

#### NetMessageReader
基于 `DataBuffer` 的 `INetMessageReader` 默认实现。

```csharp
/// <summary>
/// 基于 DataBuffer 的 INetMessageReader 默认实现。
/// </summary>
public class NetMessageReader : DataBuffer, INetMessageReader
{
    /// <summary>
    /// 创建指定字节序的消息读取器。
    /// </summary>
    /// <param name="littleEndian">为 true 时使用小端。</param>
    public NetMessageReader(bool littleEndian);

    public bool CheckMessage();
    public byte[] ReadMessage();
    public void ReadMessageTo<T>(ref T o) where T : INetMessage;
    public void ReadMessageTo<T>(ref T[] o) where T : INetMessage;
    public byte[] CopyMessage();
    public void CopyMessageTo<T>(ref T o) where T : INetMessage;
    public void CopyMessageTo<T>(ref T[] o) where T : INetMessage;
    public void WriteMessageBytes(byte[] src);
    public void WriteMessageBytes(byte[] src, int srcIndex, int size);
}
```

`DataBuffer` 的读写/复制成员见 [Buffer API](Buffer-API.md)。

#### NetMessageWriter
基于 `DataBuffer` 的 `INetMessageWriter` 默认实现。

```csharp
/// <summary>
/// 基于 DataBuffer 的 INetMessageWriter 默认实现。
/// </summary>
public class NetMessageWriter : DataBuffer, INetMessageWriter
{
    /// <summary>
    /// 创建指定字节序的消息写入器。
    /// </summary>
    /// <param name="littleEndian">为 true 时使用小端。</param>
    public NetMessageWriter(bool littleEndian);

    public void WriteMessage(INetMessage msg);
    public void WriteMessage<T>(T[] msg) where T : INetMessage;
    public byte[] ReadMessageBytes();
    public new void Clear();
}
```

`WriteMessage(INetMessage)` 将 `EncodeToBytes()` 结果经 `WriteData` 封包。空数组 `WriteMessage<T>(T[])` 写入长度为 0 的长度字段。

---

## Http

### 接口 (Interfaces)

#### IHttpClientProxy
HTTP 客户端代理，支持可配置超时与长连接的异步 GET/POST。继承 `IDisposable`。

```csharp
/// <summary>
/// HTTP 客户端代理，支持可配置超时与长连接的异步 GET/POST。
/// </summary>
public interface IHttpClientProxy : IDisposable
{
    /// <summary>
    /// 设置请求超时（不影响构造时保存的默认超时，Reset 仍恢复构造值）。
    /// </summary>
    void SetTimeout(TimeSpan timeout);

    /// <summary>
    /// 重置为构造时设置的超时。
    /// </summary>
    void ResetTimeout();

    /// <summary>
    /// 设置是否使用长连接。
    /// </summary>
    /// <param name="enable">为 true 时启用长连接。</param>
    void SetKeepAlive(bool enable);

    Task<HttpResult<byte[]>> GetBytesAsync(string pattern);
    Task<HttpResult<byte[]>> GetBytesAsync(string pattern, TimeSpan timeout);
    Task<HttpResult<byte[]>> GetBytesAsync(Uri baseUri, string pattern);
    Task<HttpResult<byte[]>> GetBytesAsync(Uri baseUri, string pattern, TimeSpan timeout);

    Task<HttpResult<string>> GetStringAsync(string pattern);
    Task<HttpResult<string>> GetStringAsync(string pattern, TimeSpan timeout);
    Task<HttpResult<string>> GetStringAsync(Uri baseUri, string pattern);
    Task<HttpResult<string>> GetStringAsync(Uri baseUri, string pattern, TimeSpan timeout);

    Task<HttpResult<byte[]>> PostBytesAsync(string pattern, Dictionary<string, string> value);
    Task<HttpResult<byte[]>> PostBytesAsync(string pattern, Dictionary<string, string> value, TimeSpan timeout);
    Task<HttpResult<byte[]>> PostBytesAsync(Uri baseUri, string pattern, Dictionary<string, string> value);
    Task<HttpResult<byte[]>> PostBytesAsync(Uri baseUri, string pattern, Dictionary<string, string> value, TimeSpan timeout);

    Task<HttpResult<string>> PostStringAsync(string pattern, Dictionary<string, string> value);
    Task<HttpResult<string>> PostStringAsync(string pattern, Dictionary<string, string> value, TimeSpan timeout);
    Task<HttpResult<string>> PostStringAsync(Uri baseUri, string pattern, Dictionary<string, string> values);
    Task<HttpResult<string>> PostStringAsync(Uri baseUri, string pattern, Dictionary<string, string> values, TimeSpan timeout);
}
```

GET/POST 各重载的 XML 注释约定：
- 建议调用时使用 `await`
- 仅 `pattern` 的重载使用构造时的默认 `BaseUri`；带 `Uri baseUri` 的重载忽略默认 `BaseUri`
- `pattern` 与 `BaseUri` 按 `System.Uri` 规则拼接
- 带 `TimeSpan timeout` 的重载使用该超时（经 `CancellationTokenSource`）；不带则使用客户端当前超时

POST 的 `value` / `values` 为表单参数集，实现中封装为 `FormUrlEncodedContent`（为 `null` 时请求体为空）。

### 类 (Classes)

#### HttpClientProxy
`IHttpClientProxy` 的密封实现。Unity 上支持有限制，建议在 Unity 上使用 `UnityWebRequest`。

Url 合并规则：
- `"http://127.0.0.1:9000/api/sub"` + `"test"` = `"http://127.0.0.1:9000/api/test"`
- `"http://127.0.0.1:9000/api/sub/"` + `"test"` = `"http://127.0.0.1:9000/api/sub/test"`
- `"http://127.0.0.1:9000/api/sub"` + `"/test"` = `"http://127.0.0.1:9000/test"`
- `"http://127.0.0.1:9000/api/sub/"` + `"/test"` = `"http://127.0.0.1:9000/test"`

```csharp
/// <summary>
/// Http 客户端代理
/// </summary>
public sealed class HttpClientProxy : IHttpClientProxy
{
    /// <summary>
    /// 创建 Http 客户端代理。使用默认超时时间 100 秒。
    /// </summary>
    public HttpClientProxy();

    /// <summary>
    /// 创建 Http 客户端代理。设置基础 URL，使用默认超时时间 100 秒。会进行预热，避免第一次请求时性能低。
    /// </summary>
    public HttpClientProxy(string baseUrl);

    /// <summary>
    /// 创建 Http 客户端代理。设置基础 URL，使用自定义超时。会进行预热。
    /// </summary>
    /// <param name="baseUrl">应该以 "/" 结尾，否则会使用相对路径</param>
    public HttpClientProxy(string baseUrl, TimeSpan timeout);

    public void SetTimeout(TimeSpan timeout);
    public void ResetTimeout();
    public void SetKeepAlive(bool enable);

    public Task<HttpResult<byte[]>> GetBytesAsync(string pattern);
    public Task<HttpResult<byte[]>> GetBytesAsync(string pattern, TimeSpan timeout);
    public Task<HttpResult<byte[]>> GetBytesAsync(Uri baseUri, string pattern);
    public Task<HttpResult<byte[]>> GetBytesAsync(Uri baseUri, string pattern, TimeSpan timeout);

    public Task<HttpResult<string>> GetStringAsync(string pattern);
    public Task<HttpResult<string>> GetStringAsync(string pattern, TimeSpan timeout);
    public Task<HttpResult<string>> GetStringAsync(Uri baseUri, string pattern);
    public Task<HttpResult<string>> GetStringAsync(Uri baseUri, string pattern, TimeSpan timeout);

    public Task<HttpResult<byte[]>> PostBytesAsync(string pattern, Dictionary<string, string> value);
    public Task<HttpResult<byte[]>> PostBytesAsync(string pattern, Dictionary<string, string> value, TimeSpan timeout);
    public Task<HttpResult<byte[]>> PostBytesAsync(Uri baseUri, string pattern, Dictionary<string, string> value);
    public Task<HttpResult<byte[]>> PostBytesAsync(Uri baseUri, string pattern, Dictionary<string, string> value, TimeSpan timeout);

    public Task<HttpResult<string>> PostStringAsync(string pattern, Dictionary<string, string> value);
    public Task<HttpResult<string>> PostStringAsync(string pattern, Dictionary<string, string> value, TimeSpan timeout);
    public Task<HttpResult<string>> PostStringAsync(Uri baseUri, string pattern, Dictionary<string, string> values);
    public Task<HttpResult<string>> PostStringAsync(Uri baseUri, string pattern, Dictionary<string, string> values, TimeSpan timeout);

    public void Dispose();
}
```

带 `baseUrl` 的构造会加入 `keep-alive` 并对 `{baseUrl}/` 发送 `HEAD` 预热。异常映射：
- `UriFormatException` / 其他异常：`StatusCode = NotFound`，写入 `Exception`
- `TaskCanceledException`：`Timeout = true`，`StatusCode = RequestTimeout`

`baseUri` 为 `null` 时按 `new Uri(pattern)` 作为完整 URL。

### 结构体 (Structs)

#### HttpResult&lt;T&gt;
HTTP 请求结果封装。

```csharp
/// <summary>
/// HTTP 请求结果封装。
/// </summary>
/// <typeparam name="T">响应体类型。</typeparam>
public struct HttpResult<T>
{
    /// <summary>
    /// HTTP 状态码。
    /// </summary>
    public HttpStatusCode StatusCode { get; set; }

    /// <summary>
    /// 是否请求超时。
    /// </summary>
    public bool Timeout { get; set; }

    /// <summary>
    /// 响应内容。
    /// </summary>
    public T Content { get; set; }

    /// <summary>
    /// 请求失败时的异常（可为 null）。
    /// </summary>
    public Exception Exception { get; set; }
}
```

GET/POST 字节接口的 `T` 为 `byte[]`，字符串接口的 `T` 为 `string`。

---

## 响应码

#### NetResponseCode
应用层网络响应/错误码（由 Excel 导出生成）。

```csharp
/// <summary>
/// 应用层网络响应/错误码（由 Excel 导出生成）。
/// </summary>
public static class NetResponseCode
{
    /// <summary>成功</summary>
    public const int Suc = 0;
    /// <summary>扩展错误-扩展不存在</summary>
    public const int ExtensionNotExist = 1;
    /// <summary>协议错误-协议不存在</summary>
    public const int ProtoNotExist = 2;
    /// <summary>扩展禁用</summary>
    public const int ExtensionDisable = 3;
    /// <summary>参数错误</summary>
    public const int Args = 4;
    /// <summary>服务器内部错误</summary>
    public const int Internal = 5;
    /// <summary>数据库执行错误</summary>
    public const int DbQuery = 6;
    /// <summary>请求超时</summary>
    public const int Timeout = 7;
    /// <summary>权限不足</summary>
    public const int Right = 8;
    /// <summary>状态不匹配</summary>
    public const int Status = 9;
    /// <summary>请求重复</summary>
    public const int Repeat = 10;
    /// <summary>请求过于频繁</summary>
    public const int Freq = 11;
    /// <summary>其它错误</summary>
    public const int Other = 12;
}
```

---

### 功能说明

#### Socket 架构

**接口层次**
- **ISocketInfo**：名称与连接状态
- **ISocketSender**：原始字节发送与长度前缀封包发送
- **ISocketReceiver**：异步接收、消息回调与事件派发
- **ISocketConn**：发送 + 接收
- **ISocketClient**：在连接之上增加 `ConnectServer` / `DisconnectServer` / `SetContext`

**实现**
- **SocketFactory**：创建 `ISocketClient` 或底层 `Socket`
- **SocketClient**：连接适配器 + 成功后的 `SocketSender` / `SocketReceiver`
- **SocketParams**：`"host:port"` 地址与 `SocketNetworks.Network`
- **内部适配器**：按 `apmMode` 在 APM 与 TAP 之间切换（非公开）

**事件**
- 连接结果：`EventOnConnectionOpen` / `EventOnConnectionClose`（`SocketConnEventInfo`）
- 消息：`EventOnMessageReceived`（`byte[]`）、`EventOnMessageReceivedEnd`（`SocketReceivedEndInfo`）

#### 消息封包

帧为长度前缀 + 载荷，字节序由 `littleEndian` 决定（与 `DataBuffer` 一致）：
- **NetMessageWriter / INetMessageWriter**：封包 `INetMessage` 或经 `WriteData` 写入原始数据
- **NetMessageReader / INetMessageReader**：`WriteMessageBytes` 写入收到的字节，`CheckMessage` / `ReadMessage` 解包
- **INetMessage**：对象与字节/缓冲区互转

#### HTTP

- **IHttpClientProxy / HttpClientProxy**：异步 GET/POST，返回 `HttpResult<T>`
- 支持默认 BaseUri、单次覆盖 BaseUri、默认超时与单次超时、keep-alive
- POST 使用 `application/x-www-form-urlencoded` 表单字典

---

### 使用示例

#### Socket 客户端连接与事件

```csharp
using System.Threading;
using JLGames.Infra.Event;
using JLGames.Infra.Net;

// 推荐通过工厂创建（也可 new SocketClient(...)）
ISocketClient client = SocketFactory.CreateSocketClient("game", littleEndian: true, apmMode: false);

client.AddEventListener(SocketEvents.EventOnConnectionOpen, evd =>
{
    var info = (SocketEvents.SocketConnEventInfo)evd.Data;
    if (info.Suc)
    {
        Console.WriteLine("连接成功");
        client.StartReceiving();
        return;
    }
    Console.WriteLine($"连接失败: {info.Error}, {info.Exception}");
});

client.AddEventListener(SocketEvents.EventOnConnectionClose, evd =>
{
    var info = (SocketEvents.SocketConnEventInfo)evd.Data;
    Console.WriteLine($"断开结果 Suc={info.Suc}, Error={info.Error}");
});

client.AddEventListener(SocketEvents.EventOnMessageReceived, evd =>
{
    var payload = evd.Data as byte[];
    Console.WriteLine($"收到 {payload?.Length ?? 0} 字节");
});

client.AddEventListener(SocketEvents.EventOnMessageReceivedEnd, evd =>
{
    var end = (SocketEvents.SocketReceivedEndInfo)evd.Data;
    Console.WriteLine($"接收结束 Disconnect={end.Disconnect}, Error={end.Error}");
});

var socketParams = new SocketParams
{
    Network = SocketNetworks.Network.Tcp,
    RemoteAddress = "127.0.0.1:8080",
    LocalAddress = "0.0.0.0:0"
};

client.ConnectServer(socketParams);
```

#### 发送与消息回调

```csharp
client.SetMessageHandler((msg, remoteAddress, other) =>
{
    string text = Encoding.UTF8.GetString(msg);
    Console.WriteLine($"来自 {remoteAddress}: {text}");
});

// 不封包
client.SendBytes(Encoding.UTF8.GetBytes("raw"));

// 封包（长度前缀 + 载荷）
client.SendMessage(Encoding.UTF8.GetBytes("hello"));
client.SendMessage("Hello", "World");
client.SendMessage(new[] { "A", "B" });
```

#### 线程上下文

```csharp
// WPF / WinForms 等可把回调封送到 UI 线程
client.SetContext(SynchronizationContext.Current);

client.AddEventListener(SocketEvents.EventOnMessageReceived, evd =>
{
    var payload = evd.Data as byte[];
    UpdateUI(payload);
});
```

#### 协议类型转换

```csharp
string s = SocketNetworks.GetNetworkValue(SocketNetworks.Network.Tcp4); // "tcp4"
var n = SocketNetworks.GetNetwork("wss"); // SocketNetworks.Network.WebSockets
```

#### HTTP 请求

```csharp
using (var http = new HttpClientProxy("https://api.example.com/"))
{
    HttpResult<string> getResult = await http.GetStringAsync("data");
    if (getResult.Exception == null)
        Console.WriteLine($"GET {getResult.StatusCode}: {getResult.Content}");

    var form = new Dictionary<string, string>
    {
        { "name", "张三" },
        { "age", "25" }
    };
    HttpResult<string> postResult = await http.PostStringAsync("user", form);
    if (postResult.Timeout)
        Console.WriteLine("POST 超时");
    else
        Console.WriteLine($"POST {postResult.StatusCode}: {postResult.Content}");

    // 覆盖 BaseUri 与超时
    var bytes = await http.GetBytesAsync(new Uri("https://cdn.example.com/"), "file.bin", TimeSpan.FromSeconds(10));
}

using (var http = new HttpClientProxy())
{
    var result = await http.GetStringAsync(new Uri("https://api.example.com/"), "status");
}
```

#### 消息封包 / 解包

```csharp
var writer = new NetMessageWriter(littleEndian: true);
writer.WriteData(Encoding.UTF8.GetBytes("hello"));
byte[] packed = writer.ReadMessageBytes();

var reader = new NetMessageReader(littleEndian: true);
reader.WriteMessageBytes(packed);
if (reader.CheckMessage())
{
    byte[] payload = reader.ReadMessage();
    Console.WriteLine(Encoding.UTF8.GetString(payload));
}

// 自定义 INetMessage
var msg = new CustomMessage();
writer.Clear();
writer.WriteMessage(msg);
byte[] framed = writer.ReadMessageBytes();
```

```csharp
public class CustomMessage : INetMessage
{
    public string Text { get; set; }

    public byte[] EncodeToBytes() => Encoding.UTF8.GetBytes(Text ?? "");

    public void DecodeFromBytes(byte[] bytes)
    {
        Text = Encoding.UTF8.GetString(bytes);
    }

    public void EncodeToBuff(IDataBufferWriter buff) => buff.WriteData(EncodeToBytes());

    public void DecodeFromBuff(IDataBufferReader buff)
    {
        DecodeFromBytes(buff.ReadBytes());
    }
}
```

#### 应用层响应码

```csharp
if (code == NetResponseCode.Suc)
    Console.WriteLine("成功");
else if (code == NetResponseCode.Timeout)
    Console.WriteLine("请求超时");
```

---

### 设计特点

1. **接口分层**：Info / Sender / Receiver / Conn / Client 职责分离
2. **事件驱动**：连接与收发结果通过 `IEventDispatcher` 派发
3. **异步模型可选**：`apmMode` 在 APM 与 TAP 之间切换（内部适配器）
4. **长度前缀封包**：与 Buffer 模块的 `DataBuffer` / `WriteData` / `ReadLen` 共用
5. **线程封送**：`SetContext` 将回调同步到指定 `SynchronizationContext`
6. **HTTP 代理**：统一的 `HttpResult<T>`，支持超时、keep-alive 与 URL 拼接规则

---

### 注意事项

1. **创建方式**：`SocketClient` 必须传入 `name`、`littleEndian`、`apmMode`；不要假设存在 `Connect(host, port)` 或 `SocketParams.Host/Port`
2. **地址格式**：`LocalAddress` / `RemoteAddress` 为 `"ip:port"`，解析使用 `IPAddress.Parse`，不支持主机名 DNS
3. **协议支持**：当前工厂可为 TCP/UDP 创建 `Socket`；WebSocket / QUIC 返回 `null`
4. **连接生命周期**：重复 `ConnectServer` 不会自动替换已有连接；及时 `DisconnectServer`
5. **接收启动**：连接成功后再 `StartReceiving`；`Connected` / `IsReceiving` 在适配器尚未创建时不可安全访问
6. **消息回调签名**：`OnBinaryMessageHandler` 为 `(byte[] msg, string remoteAddress, object other)`，不是单参数 `byte[]`
7. **HTTP**：`HttpResult<T>` 没有 `IsSuccess`；请检查 `Exception`、`Timeout` 与 `StatusCode`。`baseUrl` 建议以 `/` 结尾
8. **Unity**：`HttpClientProxy` 支持有限，建议使用 `UnityWebRequest`
9. **事件数据**：`EventOnMessageReceived` 的 `evd.Data` 为 `byte[]`，不是 `SocketMessageEventInfo`
10. **字节序**：客户端、Reader、Writer 的 `littleEndian` 必须与对端协议一致
