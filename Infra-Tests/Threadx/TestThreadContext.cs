using System;
using System.Threading;
using System.Threading.Tasks;
using JLGames.Infra.Threadx;

namespace JLGames.InfraTests.Threadx;

[TestFixture]
public class TestThreadContext
{
    private CancellationTokenSource m_TokenSource;
    private FixedThreadContext m_Context;

    [SetUp]
    public void SetUp()
    {
        m_TokenSource = new CancellationTokenSource();
        m_Context = new FixedThreadContext(Thread.CurrentThread.ManagedThreadId);
    }

    [TearDown]
    public void TearDown()
    {
        m_Context.Dispose();
        m_TokenSource.Dispose();
    }

    [Test]
    public async Task TestSingle()
    {
        TestContext.Progress.WriteLine($"TestSingle[{Thread.CurrentThread.ManagedThreadId}]");

        m_Context.StartExec();
        await Invoke();

        try
        {
            await Task.Delay(Timeout.Infinite, m_TokenSource.Token);
        }
        catch (Exception)
        {
            // ignored
        }
    }

    private async Task Invoke()
    {
        for (var i = 0; i < 5; i++)
        {
            m_Context.Post(PostCall, null);
            m_Context.Send(SendCall, null);
            // Thread.Sleep(100);
            await Task.Delay(100);
        }

        await m_TokenSource.CancelAsync();
    }

    private void PostCall(object state)
    {
        TestContext.Progress.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] PostCall");
    }

    private void SendCall(object state)
    {
        TestContext.Progress.WriteLine($"[{Thread.CurrentThread.ManagedThreadId}] SendCall");
    }
}