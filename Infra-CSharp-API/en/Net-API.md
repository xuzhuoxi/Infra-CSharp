# Net API Documentation

## Namespace: JLGames.Infra.Net

All public types in this module live in `JLGames.Infra.Net`. Source files are grouped under Socket / Message / Http directories; this document follows that grouping.

Connect/receive adapters under `Socket/Internal` (`IConnectAdapter`, `IReceiveAdapter`, `BeginConnectAdapter`, `ConnectAsyncAdapter`, `BeginReceiveAdapter`, `ReceiveAsyncAdapter`, and `AdapterDelegates`) are `internal`. `SocketClient` / `SocketReceiver` pick APM (Begin/End) or TAP (`*Async`) from `apmMode`. They are not public API.

---

## Socket

### Interfaces

#### ISocketInfo
Basic socket identity and connection state.

```csharp
/// <summary>
/// Basic socket identity and connection state.
/// </summary>
public interface ISocketInfo
{
    /// <summary>
    /// Socket instance name.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Whether the socket is connected.
    /// </summary>
    bool Connected { get; }
}
```

#### ISocketSender
Socket message sender. Extends `ISocketInfo`.

```csharp
/// <summary>
/// Socket message sender.
/// </summary>
public interface ISocketSender : ISocketInfo
{
    /// <summary>
    /// Send raw bytes without framing/packing.
    /// </summary>
    /// <param name="bytes">Payload bytes.</param>
    void SendBytes(byte[] bytes);

    /// <summary>
    /// Send a framed message (length-prefixed).
    /// </summary>
    /// <param name="message">Message payload.</param>
    void SendMessage(byte[] message);

    /// <summary>
    /// Send one or more string messages (framed).
    /// </summary>
    /// <param name="messages">String messages.</param>
    void SendMessage(string[] messages);

    /// <summary>
    /// Send one or more string messages (framed).
    /// </summary>
    /// <param name="message">First message.</param>
    /// <param name="messages">Additional messages.</param>
    void SendMessage(string message, params string[] messages);
}
```

`SendMessage` writes a length prefix through `INetMessageWriter` (`DataBuffer.WriteData`) then sends; `SendBytes` calls `Socket.Send` directly.

#### ISocketReceiver
Socket message receiver with event dispatch. Extends `ISocketInfo` and `IEventDispatcher`.

```csharp
using JLGames.Infra.Event;

/// <summary>
/// Socket message receiver with event dispatch.
/// </summary>
public interface ISocketReceiver : ISocketInfo, IEventDispatcher
{
    /// <summary>
    /// Whether message receiving is active.
    /// </summary>
    bool IsReceiving { get; }

    /// <summary>
    /// Start receiving messages.
    /// </summary>
    void StartReceiving();

    /// <summary>
    /// Stop receiving messages.
    /// </summary>
    void StopReceiving();

    /// <summary>
    /// Set the binary message handler callback.
    /// </summary>
    /// <param name="handler">Message handler (may be null).</param>
    void SetMessageHandler(SocketDelegates.OnBinaryMessageHandler handler);
}
```

Listener/dispatch members are documented on `IEventDispatcher` in the [Event API](Event-API.md).

#### ISocketConn
Socket connection: send, receive, and connection state. Extends `ISocketSender`, `ISocketReceiver`, and `ISocketInfo`.

```csharp
/// <summary>
/// Socket connection: send, receive, and connection state.
/// </summary>
public interface ISocketConn : ISocketSender, ISocketReceiver, ISocketInfo
{
}
```

#### ISocketClient
Socket client: connect/disconnect, send/receive, and dispatch connection events. Extends `ISocketConn` and `IEventDispatcher`.

```csharp
using System.Threading;
using JLGames.Infra.Event;

/// <summary>
/// Socket client: connect/disconnect, send/receive, and dispatch connection events.
/// </summary>
public interface ISocketClient : ISocketConn, IEventDispatcher
{
    /// <summary>
    /// Set synchronization context for marshaling callbacks to a specific thread.
    /// </summary>
    /// <param name="context">Target synchronization context.</param>
    void SetContext(SynchronizationContext context);

    /// <summary>
    /// Connect to server.
    /// </summary>
    /// <param name="params">Connection parameters.</param>
    void ConnectServer(SocketParams @params);

    /// <summary>
    /// Disconnect from server.
    /// </summary>
    void DisconnectServer();
}
```

### Structs

#### AddressInfo
IP address and port pair.

```csharp
/// <summary>
/// IP address and port pair.
/// </summary>
public struct AddressInfo
{
    /// <summary>
    /// IP address string.
    /// </summary>
    public string IPAddress;

    /// <summary>
    /// Port number.
    /// </summary>
    public int Port;
}
```

#### SocketParams
Socket connection parameters (addresses, protocol, WebSocket options). `[Serializable]` struct.

```csharp
/// <summary>
/// Socket connection parameters (addresses, protocol, WebSocket options).
/// </summary>
[Serializable]
public struct SocketParams
{
    /// <summary>
    /// Network protocol type.
    /// </summary>
    public SocketNetworks.Network Network { get; set; }

    /// <summary>
    /// Local endpoint in "host:port" format.
    /// </summary>
    public string LocalAddress { get; set; }

    /// <summary>
    /// Remote endpoint in "host:port" format.
    /// </summary>
    public string RemoteAddress { get; set; }

    /// <summary>
    /// WebSocket path pattern (e.g. "/", "/echo").
    /// </summary>
    public string WSPattern { get; set; }

    /// <summary>
    /// WebSocket Origin header (e.g. "http://127.0.0.1/", must end with "/").
    /// </summary>
    public string WSOrigin { get; set; }

    /// <summary>
    /// WebSocket sub-protocol string (may be empty).
    /// </summary>
    public string WSProtocol { get; set; }

    public override string ToString();

    /// <summary>
    /// Parse LocalAddress into an EndPoint.
    /// </summary>
    public EndPoint LocalEndPoint();

    /// <summary>
    /// Parse RemoteAddress into an EndPoint.
    /// </summary>
    public EndPoint RemoteEndPoint();
}
```

`LocalEndPoint` / `RemoteEndPoint` split `"ip:port"` and build an `IPEndPoint` (`IPAddress.Parse` + `int.Parse`). `SocketFactory.CreateSocket` currently returns `null` for WebSocket / QUIC; `WSPattern` / `WSOrigin` / `WSProtocol` are reserved for later protocol support.

### Classes

#### SocketClient
Default implementation of `ISocketClient` (connect, send, receive, events). Extends `EventDispatcher`.

```csharp
/// <summary>
/// Default implementation of ISocketClient (connect, send, receive, events).
/// </summary>
public class SocketClient : EventDispatcher, ISocketClient
{
    /// <summary>
    /// Socket instance name.
    /// </summary>
    public string Name { get; }

    /// <summary>
    /// Whether the socket is connected.
    /// </summary>
    public bool Connected { get; }

    /// <summary>
    /// Whether message receiving is active.
    /// </summary>
    public bool IsReceiving { get; }

    /// <summary>
    /// Create a socket client.
    /// </summary>
    /// <param name="name">Client name.</param>
    /// <param name="littleEndian">Use little-endian for messages.</param>
    /// <param name="apmMode">Use APM async model when true.</param>
    public SocketClient(string name, bool littleEndian, bool apmMode);

    /// <summary>
    /// Update the client display name.
    /// </summary>
    /// <param name="name">New name.</param>
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

Notes:
- There is no parameterless constructor; use this constructor or `SocketFactory.CreateSocketClient`.
- `apmMode == true` uses `BeginConnectAdapter` / `BeginReceiveAdapter`; otherwise `ConnectAsyncAdapter` / `ReceiveAsyncAdapter`.
- On successful connect, the client creates `SocketSender` and `SocketReceiver` with `NetMessageWriter` / `NetMessageReader` using `littleEndian`.
- Calling `ConnectServer` while a connect adapter already exists dispatches `EventOnConnectionOpen` with `Suc = false` and does not replace the connection.
- Calling `DisconnectServer` with no adapter dispatches `EventOnConnectionClose` with `Suc = false`.
- After `SetContext`, connect/disconnect/receive events are marshaled with `SynchronizationContext.Send`; otherwise they run on the callback thread.
- Listener/dispatch members are inherited from `EventDispatcher`; see the [Event API](Event-API.md).

#### SocketSender
Default implementation of `ISocketSender`.

```csharp
/// <summary>
/// Default implementation of ISocketSender.
/// </summary>
public class SocketSender : ISocketSender
{
    public string Name { get; }
    public bool Connected { get; }

    /// <summary>
    /// Create a sender for the given socket and message writer.
    /// </summary>
    /// <param name="name">Sender name.</param>
    /// <param name="socket">Underlying socket.</param>
    /// <param name="netMessageWriter">Message packer.</param>
    public SocketSender(string name, Socket socket, INetMessageWriter netMessageWriter);

    public void SendBytes(byte[] bytes);
    public void SendMessage(byte[] message);
    public void SendMessage(string[] messages);
    public void SendMessage(string message, params string[] messages);
}
```

`SocketClient` creates this after a successful connect. You can also construct it against an already-connected `Socket`.

#### SocketReceiver
Default implementation of `ISocketReceiver` with async receive adapters. Extends `EventDispatcher`.

```csharp
/// <summary>
/// Default implementation of ISocketReceiver with async receive adapters.
/// </summary>
public class SocketReceiver : EventDispatcher, ISocketReceiver
{
    public string Name { get; }
    public bool Connected { get; }
    public bool IsReceiving { get; }

    /// <summary>
    /// Create a receiver for the given socket and message reader.
    /// </summary>
    /// <param name="name">Receiver name.</param>
    /// <param name="socket">Underlying socket.</param>
    /// <param name="reader">Message unpacker.</param>
    /// <param name="oldApi">Use APM (Begin/End) receive when true.</param>
    public SocketReceiver(string name, Socket socket, INetMessageReader reader, bool oldApi);

    public void SetMessageHandler(SocketDelegates.OnBinaryMessageHandler handler);
    public void StartReceiving();
    public void StopReceiving();
}
```

Receive buffer size is 8192. After a complete frame:
- Invokes `OnBinaryMessageHandler(msg, remoteAddress, other)` (`other` is currently `null`; `remoteAddress` comes from `Socket.RemoteEndPoint`)
- Dispatches `SocketEvents.EventOnMessageReceived` with `byte[]` event data
- Dispatches `SocketEvents.EventOnMessageReceivedEnd` with `SocketReceivedEndInfo` when 0 bytes are read or an error/exception occurs

`SocketClient.StartReceiving` re-dispatches those two events and calls `StopReceiving` on `EventOnMessageReceivedEnd`.

### Static Classes

#### SocketFactory
Factory for creating socket clients and `System.Net.Sockets.Socket` instances.

```csharp
/// <summary>
/// Factory for creating socket clients and Socket instances.
/// </summary>
public static class SocketFactory
{
    /// <summary>
    /// Create a socket client.
    /// </summary>
    /// <param name="name">Client name.</param>
    /// <param name="littleEndian">Use little-endian byte order for messages.</param>
    /// <param name="apmMode">Use APM (Begin/End) async model when true; otherwise TAP.</param>
    /// <returns>Socket client instance.</returns>
    public static ISocketClient CreateSocketClient(string name, bool littleEndian, bool apmMode);

    /// <summary>
    /// Create a Socket for the given parameters.
    /// </summary>
    /// <param name="params">Connection parameters.</param>
    /// <returns>Configured socket, or null for unsupported protocols (WebSocket, QUIC).</returns>
    public static Socket CreateSocket(SocketParams @params);
}
```

`CreateSocket` behavior:
- `Tcp` / `Tcp4`: `AddressFamily.InterNetwork` + `SocketType.Stream` + `ProtocolType.Tcp`, KeepAlive off, NoDelay on
- `Tcp6`: `AddressFamily.InterNetworkV6`, same options
- `Udp` / `Udp4`: `InterNetwork` + `Dgram` + `Udp`
- `Udp6`: `InterNetworkV6` + `Dgram` + `Udp`
- `WebSocket` / `WebSockets` / `Quic` / other: `null`

#### SocketNetworks
Network protocol type constants and conversions.

```csharp
/// <summary>
/// Network protocol type constants and conversions.
/// </summary>
public static class SocketNetworks
{
    /// <summary>
    /// Supported network protocol kinds.
    /// </summary>
    public enum Network
    {
        /// <summary>Undefined network type</summary>
        Undefined,
        /// <summary>TCP</summary>
        Tcp,
        /// <summary>IPv4 TCP</summary>
        Tcp4,
        /// <summary>IPv6 TCP</summary>
        Tcp6,
        /// <summary>UDP</summary>
        Udp,
        /// <summary>IPv4 UDP</summary>
        Udp4,
        /// <summary>IPv6 UDP</summary>
        Udp6,
        /// <summary>WebSocket</summary>
        WebSocket,
        /// <summary>WebSocket Secure</summary>
        WebSockets,
        /// <summary>QUIC</summary>
        Quic,
    }

    /// <summary>Undefined network type</summary>
    public const string Undefined = "";
    /// <summary>TCP</summary>
    public const string Tcp = "tcp";
    /// <summary>IPv4 TCP</summary>
    public const string Tcp4 = "tcp4";
    /// <summary>IPv6 TCP</summary>
    public const string Tcp6 = "tcp6";
    /// <summary>UDP</summary>
    public const string Udp = "udp";
    /// <summary>IPv4 UDP</summary>
    public const string Udp4 = "udp4";
    /// <summary>IPv6 UDP</summary>
    public const string Udp6 = "udp6";
    /// <summary>WebSocket</summary>
    public const string WebSocket = "ws";
    /// <summary>WebSocket Secure</summary>
    public const string WebSocketSecure = "wss";
    /// <summary>QUIC</summary>
    public const string Quic = "quic";

    /// <summary>
    /// Get the string identifier for a network enum value.
    /// </summary>
    /// <returns>Protocol string (e.g. "tcp", "udp").</returns>
    public static string GetNetworkValue(Network network);

    /// <summary>
    /// Parse a protocol string into a network enum value (case-insensitive).
    /// </summary>
    /// <returns>Matching Network; Network.Undefined if unknown.</returns>
    public static Network GetNetwork(string network);
}
```

Note: the enum member is `WebSockets`; the matching string constant is `WebSocketSecure` (`"wss"`).

#### SocketDelegates
Socket message handler delegate definitions.

```csharp
/// <summary>
/// Socket message handler delegate definitions.
/// </summary>
public static class SocketDelegates
{
    /// <summary>
    /// Binary message handler.
    /// </summary>
    /// <param name="msg">Message bytes.</param>
    /// <param name="remoteAddress">Remote endpoint address.</param>
    /// <param name="other">User-defined context (may be null).</param>
    public delegate void OnBinaryMessageHandler(byte[] msg, string remoteAddress, object other);

    /// <summary>
    /// String message handler.
    /// </summary>
    /// <param name="msg">Message text.</param>
    /// <param name="remoteAddress">Remote endpoint address.</param>
    /// <param name="other">User-defined context (may be null).</param>
    public delegate void OnStringMessageHandler(string msg, string remoteAddress, object other);
}
```

The receive path uses `OnBinaryMessageHandler`. `OnStringMessageHandler` is defined but not referenced by `SocketReceiver` / `ISocketReceiver`.

#### SocketEvents
Socket event type constants and event payload types.

```csharp
/// <summary>
/// Socket event type constants and event payload types.
/// </summary>
public static class SocketEvents
{
    /// <summary>
    /// Event payload for received messages (user/connection mapping).
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
    /// Event payload for connection open/close results.
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
    /// Event payload when message receiving ends.
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

    /// <summary>Enable connection result event. Event data: SocketConnEventInfo</summary>
    public const string EventOnConnectionOpen = "SockEvents.EventOnConnectOpen";

    /// <summary>Connection timeout event. Event data: SocketConnEventInfo</summary>
    public const string EventOnConnectionTimeout = "SockEvents.EventOnConnectionTimeout";

    /// <summary>Connection cancelled. Event data: SocketConnEventInfo</summary>
    public const string EventOnConnectionCancel = "SockEvents.EventOnConnectionCancel";

    /// <summary>Close connection result event. Event data: SocketConnEventInfo</summary>
    public const string EventOnConnectionClose = "SockEvents.EventOnConnectClose";

    /// <summary>Data reception processing completed. Event data: byte[] message</summary>
    public const string EventOnMessageReceived = "SockEvents.EventOnMessageReceived";

    /// <summary>Receive ended (typically when received data is empty). Event data: SocketReceivedEndInfo</summary>
    public const string EventOnMessageReceivedEnd = "SockEvents.EventOnMessageReceivedEnd";
}
```

`SocketClient` currently dispatches `EventOnConnectionOpen`, `EventOnConnectionClose`, `EventOnMessageReceived`, and `EventOnMessageReceivedEnd`. `EventOnConnectionTimeout` / `EventOnConnectionCancel` are defined constants. `SocketMessageEventInfo` is a public payload type; the current receive path dispatches `byte[]`, not that struct.

When only a success/disconnect flag is passed, the failure-side default `SocketError` is `TypeNotFound` (see `SocketConnEventInfo(bool)` / `SocketReceivedEndInfo(bool)`).

---

## Message

### Interfaces

#### INetMessage
Network message that can be encoded to / decoded from bytes or buffers.

```csharp
using JLGames.Infra.Buffer;

/// <summary>
/// Network message that can be encoded to / decoded from bytes or buffers.
/// </summary>
public interface INetMessage
{
    /// <summary>
    /// Serialize to byte array
    /// </summary>
    byte[] EncodeToBytes();

    /// <summary>
    /// Construct message from byte array.
    /// </summary>
    /// <param name="bytes">Source bytes.</param>
    void DecodeFromBytes(byte[] bytes);

    /// <summary>
    /// Serialized as a byte array and written to the IDataBuffer buffer
    /// </summary>
    /// <param name="buff">Target buffer writer.</param>
    void EncodeToBuff(IDataBufferWriter buff);

    /// <summary>
    /// Read from buffer and update current properties.
    /// </summary>
    /// <param name="buff">Source buffer reader.</param>
    void DecodeFromBuff(IDataBufferReader buff);
}
```

#### INetMessageReader
Message unpacker with buffer. Extends `IDataBufferReader`, `IDataBufferCopier`, `IByteBufferReader`, and `IByteBufferCopier` (see [Buffer API](Buffer-API.md)).

```csharp
using JLGames.Infra.Buffer;

/// <summary>
/// Message unpacker with buffer
/// </summary>
public interface INetMessageReader : IDataBufferReader, IDataBufferCopier, IByteBufferReader, IByteBufferCopier
{
    /// <summary>
    /// Check message exist
    /// </summary>
    bool CheckMessage();

    /// <summary>
    /// Read message
    /// </summary>
    byte[] ReadMessage();

    /// <summary>
    /// Unpack message to object
    /// </summary>
    void ReadMessageTo<T>(ref T o) where T : INetMessage;

    /// <summary>
    /// Unpack message to object
    /// </summary>
    void ReadMessageTo<T>(ref T[] o) where T : INetMessage;

    /// <summary>
    /// Read message, not move reader index
    /// </summary>
    byte[] CopyMessage();

    /// <summary>
    /// Unpack message to object, not move reader index
    /// </summary>
    void CopyMessageTo<T>(ref T o) where T : INetMessage;

    /// <summary>
    /// Unpack message to object, not move reader index
    /// </summary>
    void CopyMessageTo<T>(ref T[] o) where T : INetMessage;

    /// <summary>
    /// Write bytes data.
    /// </summary>
    /// <param name="src">Source bytes to write.</param>
    void WriteMessageBytes(byte[] src);

    /// <summary>
    /// Write bytes data.
    /// </summary>
    /// <param name="src">Source bytes to write.</param>
    /// <param name="srcIndex">Source offset.</param>
    /// <param name="size">Number of bytes to write.</param>
    void WriteMessageBytes(byte[] src, int srcIndex, int size);
}
```

Frame format is length prefix + payload. `CheckMessage` uses `CopyLen()` and returns whether remaining length is at least `ln + LenSize`.

#### INetMessageWriter
Message packer with buffer. Extends `IDataBufferWriter` and `IByteBufferWriter` (see [Buffer API](Buffer-API.md)).

```csharp
using JLGames.Infra.Buffer;

/// <summary>
/// Message packer with buffer
/// </summary>
public interface INetMessageWriter : IDataBufferWriter, IByteBufferWriter
{
    /// <summary>
    /// Pack message into self buffer, Contains byte length information
    /// </summary>
    void WriteMessage(INetMessage msg);

    /// <summary>
    /// Pack message array into self buffer, Contains array length information
    /// </summary>
    void WriteMessage<T>(T[] msg) where T : INetMessage;

    /// <summary>
    /// Read out all bytes.
    /// </summary>
    byte[] ReadMessageBytes();

    /// <summary>
    /// Clear all bytes
    /// </summary>
    void Clear();
}
```

`SocketSender.SendMessage` uses inherited `WriteData` to write one or more payloads (with length prefixes), then `ReadMessageBytes` and `Socket.Send`.

### Classes

#### NetMessageReader
Default implementation of `INetMessageReader` backed by `DataBuffer`.

```csharp
/// <summary>
/// Default implementation of INetMessageReader backed by DataBuffer.
/// </summary>
public class NetMessageReader : DataBuffer, INetMessageReader
{
    /// <summary>
    /// Create a message reader with the given endianness.
    /// </summary>
    /// <param name="littleEndian">Use little-endian when true.</param>
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

`DataBuffer` read/write/copy members are documented in the [Buffer API](Buffer-API.md).

#### NetMessageWriter
Default implementation of `INetMessageWriter` backed by `DataBuffer`.

```csharp
/// <summary>
/// Default implementation of INetMessageWriter backed by DataBuffer.
/// </summary>
public class NetMessageWriter : DataBuffer, INetMessageWriter
{
    /// <summary>
    /// Create a message writer with the given endianness.
    /// </summary>
    /// <param name="littleEndian">Use little-endian when true.</param>
    public NetMessageWriter(bool littleEndian);

    public void WriteMessage(INetMessage msg);
    public void WriteMessage<T>(T[] msg) where T : INetMessage;
    public byte[] ReadMessageBytes();
    public new void Clear();
}
```

`WriteMessage(INetMessage)` packs `EncodeToBytes()` via `WriteData`. An empty `WriteMessage<T>(T[])` writes a zero length field.

---

## Http

### Interfaces

#### IHttpClientProxy
HTTP client proxy for async GET/POST with configurable timeout and keep-alive. Extends `IDisposable`.

```csharp
/// <summary>
/// HTTP client proxy for async GET/POST with configurable timeout and keep-alive.
/// </summary>
public interface IHttpClientProxy : IDisposable
{
    /// <summary>
    /// Set request timeout (does not change the constructor default stored for reset).
    /// </summary>
    void SetTimeout(TimeSpan timeout);

    /// <summary>
    /// Reset timeout to the value passed at construction.
    /// </summary>
    void ResetTimeout();

    /// <summary>
    /// Enable or disable HTTP keep-alive.
    /// </summary>
    /// <param name="enable">True to enable keep-alive.</param>
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

GET/POST overload conventions from XML comments:
- Prefer `await`
- Overloads with only `pattern` use the constructor `BaseUri`; overloads with `Uri baseUri` ignore the default
- `pattern` is combined with `BaseUri` using `System.Uri` rules
- Overloads with `TimeSpan timeout` use that timeout (via `CancellationTokenSource`); others use the client's current timeout

POST `value` / `values` is a form field dictionary, sent as `FormUrlEncodedContent` (`null` means an empty body).

### Classes

#### HttpClientProxy
Sealed implementation of `IHttpClientProxy`. Unity support is limited; prefer `UnityWebRequest` on Unity.

URL combine rules:
- `"http://127.0.0.1:9000/api/sub"` + `"test"` = `"http://127.0.0.1:9000/api/test"`
- `"http://127.0.0.1:9000/api/sub/"` + `"test"` = `"http://127.0.0.1:9000/api/sub/test"`
- `"http://127.0.0.1:9000/api/sub"` + `"/test"` = `"http://127.0.0.1:9000/test"`
- `"http://127.0.0.1:9000/api/sub/"` + `"/test"` = `"http://127.0.0.1:9000/test"`

```csharp
/// <summary>
/// HTTP client proxy
/// </summary>
public sealed class HttpClientProxy : IHttpClientProxy
{
    /// <summary>
    /// Create an HTTP client proxy. Default timeout is 100 seconds.
    /// </summary>
    public HttpClientProxy();

    /// <summary>
    /// Create an HTTP client proxy with a base URL. Default timeout is 100 seconds. Performs a warmup request.
    /// </summary>
    public HttpClientProxy(string baseUrl);

    /// <summary>
    /// Create an HTTP client proxy with a base URL and custom timeout. Performs a warmup request.
    /// </summary>
    /// <param name="baseUrl">Should end with "/"; otherwise relative-path combination applies.</param>
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

Constructors with `baseUrl` add `keep-alive` and send a `HEAD` warmup to `{baseUrl}/`. Exception mapping:
- `UriFormatException` / other exceptions: `StatusCode = NotFound`, `Exception` set
- `TaskCanceledException`: `Timeout = true`, `StatusCode = RequestTimeout`

When `baseUri` is `null`, the request URL is `new Uri(pattern)`.

### Structs

#### HttpResult&lt;T&gt;
HTTP request result wrapper.

```csharp
/// <summary>
/// HTTP request result wrapper.
/// </summary>
/// <typeparam name="T">Response body type.</typeparam>
public struct HttpResult<T>
{
    /// <summary>
    /// HTTP status code.
    /// </summary>
    public HttpStatusCode StatusCode { get; set; }

    /// <summary>
    /// Whether the request timed out.
    /// </summary>
    public bool Timeout { get; set; }

    /// <summary>
    /// Response body content.
    /// </summary>
    public T Content { get; set; }

    /// <summary>
    /// Exception if the request failed (may be null).
    /// </summary>
    public Exception Exception { get; set; }
}
```

Byte GET/POST APIs use `T = byte[]`; string APIs use `T = string`.

---

## Response codes

#### NetResponseCode
Application-level network response / error codes (generated from Excel).

```csharp
/// <summary>
/// Application-level network response / error codes (generated from Excel).
/// </summary>
public static class NetResponseCode
{
    /// <summary>Success</summary>
    public const int Suc = 0;
    /// <summary>Extension error - extension does not exist</summary>
    public const int ExtensionNotExist = 1;
    /// <summary>Protocol error - protocol does not exist</summary>
    public const int ProtoNotExist = 2;
    /// <summary>Extension disabled</summary>
    public const int ExtensionDisable = 3;
    /// <summary>Argument error</summary>
    public const int Args = 4;
    /// <summary>Internal server error</summary>
    public const int Internal = 5;
    /// <summary>Database query error</summary>
    public const int DbQuery = 6;
    /// <summary>Request timeout</summary>
    public const int Timeout = 7;
    /// <summary>Insufficient permission</summary>
    public const int Right = 8;
    /// <summary>Status mismatch</summary>
    public const int Status = 9;
    /// <summary>Duplicate request</summary>
    public const int Repeat = 10;
    /// <summary>Request too frequent</summary>
    public const int Freq = 11;
    /// <summary>Other error</summary>
    public const int Other = 12;
}
```

---

### Function Description

#### Socket architecture

**Interface hierarchy**
- **ISocketInfo**: name and connection state
- **ISocketSender**: raw-byte send and length-prefixed framed send
- **ISocketReceiver**: async receive, message callback, and event dispatch
- **ISocketConn**: send + receive
- **ISocketClient**: adds `ConnectServer` / `DisconnectServer` / `SetContext`

**Implementations**
- **SocketFactory**: creates `ISocketClient` or a raw `Socket`
- **SocketClient**: connect adapter plus `SocketSender` / `SocketReceiver` after success
- **SocketParams**: `"host:port"` addresses and `SocketNetworks.Network`
- **Internal adapters**: APM vs TAP selected by `apmMode` (not public)

**Events**
- Connection: `EventOnConnectionOpen` / `EventOnConnectionClose` (`SocketConnEventInfo`)
- Messages: `EventOnMessageReceived` (`byte[]`), `EventOnMessageReceivedEnd` (`SocketReceivedEndInfo`)

#### Message framing

Frames are length prefix + payload. Endianness follows `littleEndian` (same as `DataBuffer`):
- **NetMessageWriter / INetMessageWriter**: pack `INetMessage` or raw data via `WriteData`
- **NetMessageReader / INetMessageReader**: `WriteMessageBytes` for incoming bytes, then `CheckMessage` / `ReadMessage`
- **INetMessage**: encode/decode between objects and bytes/buffers

#### HTTP

- **IHttpClientProxy / HttpClientProxy**: async GET/POST returning `HttpResult<T>`
- Default BaseUri, per-call BaseUri override, default timeout, per-call timeout, keep-alive
- POST uses `application/x-www-form-urlencoded` form dictionaries

---

### Usage Examples

#### Socket client connect and events

```csharp
using System.Threading;
using JLGames.Infra.Event;
using JLGames.Infra.Net;

// Prefer the factory (or new SocketClient(...))
ISocketClient client = SocketFactory.CreateSocketClient("game", littleEndian: true, apmMode: false);

client.AddEventListener(SocketEvents.EventOnConnectionOpen, evd =>
{
    var info = (SocketEvents.SocketConnEventInfo)evd.Data;
    if (info.Suc)
    {
        Console.WriteLine("Connected");
        client.StartReceiving();
        return;
    }
    Console.WriteLine($"Connect failed: {info.Error}, {info.Exception}");
});

client.AddEventListener(SocketEvents.EventOnConnectionClose, evd =>
{
    var info = (SocketEvents.SocketConnEventInfo)evd.Data;
    Console.WriteLine($"Disconnect Suc={info.Suc}, Error={info.Error}");
});

client.AddEventListener(SocketEvents.EventOnMessageReceived, evd =>
{
    var payload = evd.Data as byte[];
    Console.WriteLine($"Received {payload?.Length ?? 0} bytes");
});

client.AddEventListener(SocketEvents.EventOnMessageReceivedEnd, evd =>
{
    var end = (SocketEvents.SocketReceivedEndInfo)evd.Data;
    Console.WriteLine($"Receive ended Disconnect={end.Disconnect}, Error={end.Error}");
});

var socketParams = new SocketParams
{
    Network = SocketNetworks.Network.Tcp,
    RemoteAddress = "127.0.0.1:8080",
    LocalAddress = "0.0.0.0:0"
};

client.ConnectServer(socketParams);
```

#### Send and message handler

```csharp
client.SetMessageHandler((msg, remoteAddress, other) =>
{
    string text = Encoding.UTF8.GetString(msg);
    Console.WriteLine($"From {remoteAddress}: {text}");
});

// Unframed
client.SendBytes(Encoding.UTF8.GetBytes("raw"));

// Framed (length prefix + payload)
client.SendMessage(Encoding.UTF8.GetBytes("hello"));
client.SendMessage("Hello", "World");
client.SendMessage(new[] { "A", "B" });
```

#### Thread context

```csharp
// Marshal callbacks to the UI thread in WPF / WinForms
client.SetContext(SynchronizationContext.Current);

client.AddEventListener(SocketEvents.EventOnMessageReceived, evd =>
{
    var payload = evd.Data as byte[];
    UpdateUI(payload);
});
```

#### Protocol conversion

```csharp
string s = SocketNetworks.GetNetworkValue(SocketNetworks.Network.Tcp4); // "tcp4"
var n = SocketNetworks.GetNetwork("wss"); // SocketNetworks.Network.WebSockets
```

#### HTTP requests

```csharp
using (var http = new HttpClientProxy("https://api.example.com/"))
{
    HttpResult<string> getResult = await http.GetStringAsync("data");
    if (getResult.Exception == null)
        Console.WriteLine($"GET {getResult.StatusCode}: {getResult.Content}");

    var form = new Dictionary<string, string>
    {
        { "name", "Zhang San" },
        { "age", "25" }
    };
    HttpResult<string> postResult = await http.PostStringAsync("user", form);
    if (postResult.Timeout)
        Console.WriteLine("POST timed out");
    else
        Console.WriteLine($"POST {postResult.StatusCode}: {postResult.Content}");

    // Override BaseUri and timeout
    var bytes = await http.GetBytesAsync(new Uri("https://cdn.example.com/"), "file.bin", TimeSpan.FromSeconds(10));
}

using (var http = new HttpClientProxy())
{
    var result = await http.GetStringAsync(new Uri("https://api.example.com/"), "status");
}
```

#### Message pack / unpack

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

// Custom INetMessage
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

#### Application response codes

```csharp
if (code == NetResponseCode.Suc)
    Console.WriteLine("Success");
else if (code == NetResponseCode.Timeout)
    Console.WriteLine("Request timeout");
```

---

### Design Features

1. **Layered interfaces**: Info / Sender / Receiver / Conn / Client
2. **Event-driven**: connect and receive results dispatch through `IEventDispatcher`
3. **Optional async model**: `apmMode` selects APM or TAP (internal adapters)
4. **Length-prefixed frames**: shared with Buffer `DataBuffer` / `WriteData` / `ReadLen`
5. **Thread marshaling**: `SetContext` posts callbacks onto a `SynchronizationContext`
6. **HTTP proxy**: unified `HttpResult<T>`, timeout, keep-alive, and Uri combination rules

---

### Notes

1. **Construction**: `SocketClient` requires `name`, `littleEndian`, and `apmMode`. There is no `Connect(host, port)` or `SocketParams.Host/Port`.
2. **Address format**: `LocalAddress` / `RemoteAddress` are `"ip:port"` parsed with `IPAddress.Parse` (no DNS host names).
3. **Protocol support**: the factory can create TCP/UDP sockets; WebSocket / QUIC return `null`.
4. **Connection lifetime**: a second `ConnectServer` does not replace an existing connection; call `DisconnectServer` when done.
5. **Receiving**: call `StartReceiving` after a successful connect. `Connected` / `IsReceiving` are not safe before adapters exist.
6. **Handler signature**: `OnBinaryMessageHandler` is `(byte[] msg, string remoteAddress, object other)`, not a single `byte[]`.
7. **HTTP**: `HttpResult<T>` has no `IsSuccess`; check `Exception`, `Timeout`, and `StatusCode`. Prefer a `baseUrl` that ends with `/`.
8. **Unity**: `HttpClientProxy` support is limited; prefer `UnityWebRequest`.
9. **Event data**: `EventOnMessageReceived` `evd.Data` is `byte[]`, not `SocketMessageEventInfo`.
10. **Endianness**: client, reader, and writer `littleEndian` must match the peer protocol.
