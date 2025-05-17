using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using JLGames.Infra.Event;
using JLGames.Infra.Net;
using JLGames.Infra.Threadx;

namespace JLGames.InfraTests.Net.Tcp;

[TestFixture]
public class TestTcp
{
    private ISocketClient m_Client;
    private readonly int m_SendMsgCount = 10;
    private readonly int m_SendDelay = 100;
    private CancellationTokenSource m_TokenSource;
    private FixedThreadContext m_Context;

    [SetUp]
    public void SetUp()
    {
        m_TokenSource = new CancellationTokenSource();
    }

    [TearDown]
    public void TearDown()
    {
        m_Context?.Dispose();
        m_TokenSource.Dispose();
    }

    [Test, Category("RunOnlyThis")]
    public async Task TestThreadTcpClient()
    {
        m_Context = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);
        m_Context.StartExec();
        await TestTcpClient();
    }

    [Test, Category("RunOnlyThis")]
    public async Task TestTcpClient()
    {
        TestContext.Progress.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] TestTcpQuery");

        var thread = new Thread(OpenClient);
        thread.Start();

        try
        {
            await Task.Delay(Timeout.Infinite, m_TokenSource.Token);
        }
        catch (Exception)
        {
            // ignored
        }
    }

    private void OpenClient()
    {
        TestContext.Progress.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] OpenClient");
        m_Client = SocketFactory.CreateSocketClient("TestTcpClient", true, true);
        m_Client.SetContext(m_Context);
        m_Client.AddEventListener(SocketEvents.EventOnConnectionOpen, OnConnect);
        m_Client.OpenClient(new SocketParams
        {
            Network = SocketNetworks.Network.Tcp,
            RemoteAddress = "127.0.0.1:9999",
        });
    }

    private void OnConnect(EventData evd)
    {
        var info = (SocketEvents.SocketConnEventInfo)evd.Data;
        TestContext.Progress.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] OnConnect:  {info}");
        m_Client.RemoveEventListener(SocketEvents.EventOnConnectionOpen, OnConnect);
        if (info.Suc)
        {
            ReceiveData();
            var sendThread = new Thread(() => SendData());
            sendThread.Start();
        }
        else
        {
            Assert.Fail("Connect Fail!");
        }
    }

    private void ReceiveData()
    {
        m_Client.AddEventListener(SocketEvents.EventOnMessageReceived, OnReceived);
        m_Client.AddEventListener(SocketEvents.EventOnMessageReceivedEnd, OnReceivedEnd);
        m_Client.StartReceiving();
    }

    private void OnReceived(EventData evd)
    {
        var message = (byte[])evd.Data;
        TestContext.Progress.WriteLine(
            $"[{Thread.CurrentThread.ManagedThreadId}] OnReceived: {Encoding.UTF8.GetString(message)}");
    }

    private void OnReceivedEnd(EventData evd)
    {
        m_Client.StopReceiving();
        m_Client.RemoveEventListener(SocketEvents.EventOnMessageReceivedEnd, OnReceivedEnd);
        m_Client.RemoveEventListener(SocketEvents.EventOnMessageReceived, OnReceived);
    }

    private async Task SendData()
    {
        for (var i = 0; i < m_SendMsgCount; i++)
        {
            var msg = $"Hello World:{i}";
            // TestContext.Progress.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] SendData: {msg}");
            m_Client.SendMessage(msg);
            await Task.Delay(m_SendDelay);
        }

        await m_TokenSource.CancelAsync();
    }
}