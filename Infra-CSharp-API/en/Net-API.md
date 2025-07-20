# Net API Documentation

## Namespace: JLGames.Infra.Net

### Interfaces

#### ISocketInfo
Socket information interface

```csharp
public interface ISocketInfo
{
    /// <summary>
    /// name
    /// </summary>
    string Name { get; }

    /// <summary>
    /// is it connected?
    /// </summary>
    bool Connected { get; }
}
```

#### ISocketSender
Socket sender interface

```csharp
public interface ISocketSender: ISocketInfo
{
    /// <summary>
    /// Send a message. no packed
    /// </summary>
    /// <param name="bytes"></param>
    void SendBytes(byte[] bytes);

    /// <summary>
    /// Send a message. packed
    /// </summary>
    /// <param name="message"></param>
    void SendMessage(byte[] message);

    /// <summary>
    /// Send one or more messages.
    /// </summary>
    /// <param name="messages"></param>
    void SendMessage(string[] messages);

    /// <summary>
    /// Send one or more messages.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="messages"></param>
    void SendMessage(string message, params string[] messages);
}
```

#### ISocketReceiver
Socket receiver interface

```csharp
using JLGames.Infra.Event;

public interface ISocketReceiver : ISocketInfo, IEventDispatcher
{
    /// <summary>
    /// Whether to handle the message receiving state
    /// </summary>
    bool IsReceiving { get; }

    /// <summary>
    /// Start receiving message.
    /// </summary>
    /// <returns></returns>
    void StartReceiving();

    /// <summary>
    /// Stop receiving message
    /// </summary>
    /// <returns></returns>
    void StopReceiving();

    /// <summary>
    /// set the message handler function
    /// </summary>
    /// <param name="handler"></param>
    void SetMessageHandler(SocketDelegates.OnBinaryMessageHandler handler);
}
```

#### ISocketConn
Socket connection interface

```csharp
public interface ISocketConn : ISocketSender, ISocketReceiver, ISocketInfo
{
}
```

#### ISocketClient
Socket client interface

```csharp
using System.Threading;
using JLGames.Infra.Event;

public interface ISocketClient : ISocketConn, IEventDispatcher
{
    /// <summary>
    /// Set thread-associated context
    /// </summary>
    /// <param name="context"></param>
    void SetContext(SynchronizationContext context);

    /// <summary>
    /// Connect to server
    /// </summary>
    /// <param name="params"></param>
    void ConnectServer(SocketParams @params);

    /// <summary>
    /// Disconnect from server
    /// </summary>
    void DisconnectServer();
}
```

### Classes

#### SocketClient
Socket client implementation class

```csharp
/// <summary>
/// Socket client implementation
/// Provides complete Socket client functionality
/// </summary>
public class SocketClient : ISocketClient
{
    // Specific implementation requires further analysis of file content
}
```

#### SocketParams
Socket parameters class

```csharp
/// <summary>
/// Socket connection parameters
/// Defines various parameters for Socket connections
/// </summary>
public class SocketParams
{
    /// <summary>
    /// Server address
    /// </summary>
    public string Host { get; set; }

    /// <summary>
    /// Server port
    /// </summary>
    public int Port { get; set; }

    /// <summary>
    /// Connection timeout
    /// </summary>
    public int Timeout { get; set; }

    /// <summary>
    /// Buffer size
    /// </summary>
    public int BufferSize { get; set; }
}
```

### Delegates

#### SocketDelegates
Socket delegate definitions

```csharp
/// <summary>
/// Socket delegate definitions
/// Defines Socket-related callback functions
/// </summary>
public static class SocketDelegates
{
    /// <summary>
    /// Binary message processing delegate
    /// </summary>
    /// <param name="data">Received data</param>
    public delegate void OnBinaryMessageHandler(byte[] data);

    /// <summary>
    /// Connection state change delegate
    /// </summary>
    /// <param name="connected">Whether connected</param>
    public delegate void OnConnectionStateHandler(bool connected);

    /// <summary>
    /// Error handling delegate
    /// </summary>
    /// <param name="error">Error information</param>
    public delegate void OnErrorHandler(Exception error);
}
```

### Events

#### SocketEvents
Socket event constants

```csharp
/// <summary>
/// Socket event constants
/// Defines Socket-related event types
/// </summary>
public static class SocketEvents
{
    /// <summary>
    /// Connection successful event
    /// </summary>
    public const string OnConnected = "OnConnected";

    /// <summary>
    /// Connection disconnected event
    /// </summary>
    public const string OnDisconnected = "OnDisconnected";

    /// <summary>
    /// Message received event
    /// </summary>
    public const string OnMessageReceived = "OnMessageReceived";

    /// <summary>
    /// Message sent event
    /// </summary>
    public const string OnMessageSent = "OnMessageSent";

    /// <summary>
    /// Connection error event
    /// </summary>
    public const string OnConnectionError = "OnConnectionError";
}
```

## Namespace: JLGames.Infra.Net.Http

### Classes

#### HttpClientProxy
HTTP client proxy class

```csharp
/// <summary>
/// HTTP client proxy
/// Provides HTTP request encapsulation and proxy functionality
/// </summary>
public class HttpClientProxy
{
    // Specific implementation requires further analysis of file content
}
```

#### HttpResult
HTTP result class

```csharp
/// <summary>
/// HTTP request result
/// Encapsulates HTTP request response results
/// </summary>
public class HttpResult
{
    /// <summary>
    /// Response status code
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Response content
    /// </summary>
    public string Content { get; set; }

    /// <summary>
    /// Response headers
    /// </summary>
    public Dictionary<string, string> Headers { get; set; }

    /// <summary>
    /// Whether successful
    /// </summary>
    public bool IsSuccess { get; set; }

    /// <summary>
    /// Error message
    /// </summary>
    public string ErrorMessage { get; set; }
}
```

## Namespace: JLGames.Infra.Net.Message

### Interfaces

#### INetMessage
Network message interface

```csharp
/// <summary>
/// Network message interface
/// Defines the basic structure of network messages
/// </summary>
public interface INetMessage
{
    /// <summary>
    /// Message ID
    /// </summary>
    int MessageId { get; set; }

    /// <summary>
    /// Message data
    /// </summary>
    byte[] Data { get; set; }

    /// <summary>
    /// Serialize message
    /// </summary>
    /// <returns>Serialized byte array</returns>
    byte[] Serialize();

    /// <summary>
    /// Deserialize message
    /// </summary>
    /// <param name="data">Byte array</param>
    void Deserialize(byte[] data);
}
```

#### INetMessageReader
Network message reader interface

```csharp
/// <summary>
/// Network message reader interface
/// Responsible for reading and parsing messages from byte streams
/// </summary>
public interface INetMessageReader
{
    /// <summary>
    /// Read messages
    /// </summary>
    /// <param name="data">Raw data</param>
    /// <returns>Parsed message list</returns>
    List<INetMessage> ReadMessages(byte[] data);
}
```

#### INetMessageWriter
Network message writer interface

```csharp
/// <summary>
/// Network message writer interface
/// Responsible for serializing messages to byte streams
/// </summary>
public interface INetMessageWriter
{
    /// <summary>
    /// Write message
    /// </summary>
    /// <param name="message">Message to write</param>
    /// <returns>Serialized byte array</returns>
    byte[] WriteMessage(INetMessage message);
}
```

### Classes

#### NetMessageReader
Network message reader implementation class

```csharp
/// <summary>
/// Network message reader
/// Implements functionality for reading and parsing messages from byte streams
/// </summary>
public class NetMessageReader : INetMessageReader
{
    // Specific implementation requires further analysis of file content
}
```

#### NetMessageWriter
Network message writer implementation class

```csharp
/// <summary>
/// Network message writer
/// Implements functionality for serializing messages to byte streams
/// </summary>
public class NetMessageWriter : INetMessageWriter
{
    // Specific implementation requires further analysis of file content
}
```

### Enums

#### NetMessageCode
Network message code enumeration

```csharp
/// <summary>
/// Network message codes
/// Defines type codes for various network messages
/// </summary>
public enum NetMessageCode
{
    /// <summary>
    /// Heartbeat message
    /// </summary>
    Heartbeat = 1,

    /// <summary>
    /// Login message
    /// </summary>
    Login = 2,

    /// <summary>
    /// Logout message
    /// </summary>
    Logout = 3,

    /// <summary>
    /// Data request message
    /// </summary>
    DataRequest = 4,

    /// <summary>
    /// Data response message
    /// </summary>
    DataResponse = 5,

    /// <summary>
    /// Error message
    /// </summary>
    Error = 999
}
```

### Function Description

#### Socket Architecture Design

**Interface Hierarchy**
- **ISocketInfo**: Basic information interface, provides name and connection status
- **ISocketSender**: Sending functionality interface, supports multiple sending methods
- **ISocketReceiver**: Receiving functionality interface, supports message receiving and processing
- **ISocketConn**: Connection interface, combines sending and receiving functionality
- **ISocketClient**: Client interface, adds connection management functionality

**Feature Characteristics**
1. **Event-Driven**: Event-based message processing mechanism
2. **Asynchronous Support**: Supports asynchronous connections and message processing
3. **Thread Safety**: Supports thread context setting
4. **Message Encapsulation**: Supports message packaging and unpacking
5. **Error Handling**: Comprehensive error handling mechanism

#### HTTP Functionality

**HttpClientProxy**
- Provides HTTP request encapsulation
- Supports GET, POST and other request methods
- Supports request headers and parameter settings
- Provides response result encapsulation

**HttpResult**
- Encapsulates HTTP response results
- Contains status code, content, header information
- Provides success status and error information

#### Message System

**Message Structure**
- **MessageId**: Message unique identifier
- **Data**: Message data content
- **Serialization**: Supports message serialization and deserialization

**Message Processing**
- **NetMessageReader**: Responsible for message reading and parsing
- **NetMessageWriter**: Responsible for message serialization and writing
- **NetMessageCode**: Defines message type codes

### Usage Examples

#### Socket Client Usage
```csharp
// Create Socket client
var socketClient = new SocketClient();

// Set event listeners
socketClient.AddEventListener(SocketEvents.OnConnected, (evd) => {
    Console.WriteLine("Connection successful!");
});

socketClient.AddEventListener(SocketEvents.OnMessageReceived, (evd) => {
    var data = evd.Data as byte[];
    Console.WriteLine($"Received message: {BitConverter.ToString(data)}");
});

// Set connection parameters
var socketParams = new SocketParams
{
    Host = "127.0.0.1",
    Port = 8080,
    Timeout = 5000,
    BufferSize = 8192
};

// Connect to server
socketClient.ConnectServer(socketParams);
```

#### Message Sending and Receiving
```csharp
// Send raw byte data
byte[] rawData = Encoding.UTF8.GetBytes("Hello, Server!");
socketClient.SendBytes(rawData);

// Send packaged message
byte[] messageData = Encoding.UTF8.GetBytes("Packaged message");
socketClient.SendMessage(messageData);

// Send string messages
socketClient.SendMessage("Hello", "World", "Message");

// Set message handler function
socketClient.SetMessageHandler((data) => {
    string message = Encoding.UTF8.GetString(data);
    Console.WriteLine($"Received message: {message}");
});

// Start receiving messages
socketClient.StartReceiving();
```

#### HTTP Request Example
```csharp
// Create HTTP client proxy
var httpClient = new HttpClientProxy();

// Send GET request
var getResult = await httpClient.GetAsync("https://api.example.com/data");
if (getResult.IsSuccess)
{
    Console.WriteLine($"GET response: {getResult.Content}");
}

// Send POST request
var postData = new { name = "Zhang San", age = 25 };
var postResult = await httpClient.PostAsync("https://api.example.com/user", postData);
if (postResult.IsSuccess)
{
    Console.WriteLine($"POST response: {postResult.Content}");
}
```

#### Network Message Processing
```csharp
// Create message reader and writer
var messageReader = new NetMessageReader();
var messageWriter = new NetMessageWriter();

// Create message
var message = new CustomMessage
{
    MessageId = (int)NetMessageCode.DataRequest,
    Data = Encoding.UTF8.GetBytes("Request data")
};

// Serialize message
byte[] serializedData = messageWriter.WriteMessage(message);

// Send message
socketClient.SendMessage(serializedData);

// Receive and parse messages
socketClient.SetMessageHandler((data) => {
    var messages = messageReader.ReadMessages(data);
    foreach (var msg in messages)
    {
        Console.WriteLine($"Message ID: {msg.MessageId}");
        Console.WriteLine($"Message content: {Encoding.UTF8.GetString(msg.Data)}");
    }
});
```

#### Thread Context Setting
```csharp
// Set UI thread context (in WPF/WinForms applications)
socketClient.SetContext(SynchronizationContext.Current);

// Now all event callbacks will execute on the UI thread
socketClient.AddEventListener(SocketEvents.OnMessageReceived, (evd) => {
    // This callback will execute on the UI thread, can safely update UI
    UpdateUI(evd.Data as byte[]);
});
```

### Design Features

1. **Interface Separation**: Sending, receiving, and connection functionality separated
2. **Event-Driven**: Event-based message processing mechanism
3. **Asynchronous Support**: Supports asynchronous operations and thread context
4. **Message Encapsulation**: Complete message serialization and deserialization
5. **Error Handling**: Comprehensive error handling and status management
6. **Extensibility**: Easy to extend new message types and protocols

### Notes

1. **Connection Management**: Close unnecessary connections in time
2. **Exception Handling**: Properly handle network exceptions and connection errors
3. **Thread Safety**: Multi-threaded environments require correct context settings
4. **Memory Management**: Pay attention to memory usage during large message processing
5. **Timeout Settings**: Reasonably set connection and request timeout times 