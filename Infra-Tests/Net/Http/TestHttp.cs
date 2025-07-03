using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JLGames.Infra.Net;

namespace JLGames.InfraTests.Http
{
    [TestFixture]
    public class TestHttp
    {
        private static class TestHttpValues
        {
            public const string Url = "http://127.0.0.1:9000";
            public const string Pattern = "/test";
            public const string FullUrl = Url + Pattern;
        }

        [Test, Category("RunOnlyThis")]
        public async Task TestHttpGet()
        {
            using (var proxy = new HttpClientProxy())
            {
                var result = await proxy.GetBytesAsync(TestHttpValues.FullUrl);
                TestContext.Progress.WriteLine($"Get 1: ---------- ----------");
                TestContext.Progress.WriteLine($"Timeout: {result.Timeout}");
                TestContext.Progress.WriteLine($"Code: {result.StatusCode}");
                TestContext.Progress.WriteLine($"Content: {Encoding.UTF8.GetString(result.Content)}");
            }

            using (var proxy = new HttpClientProxy(TestHttpValues.Url))
            {
                var result = await proxy.GetStringAsync(TestHttpValues.Pattern);
                TestContext.Progress.WriteLine($"Get 2: ---------- ----------");
                TestContext.Progress.WriteLine($"Timeout: {result.Timeout}");
                TestContext.Progress.WriteLine($"Code: {result.StatusCode}");
                TestContext.Progress.WriteLine($"Content: {result.Content}");
            }
        }

        [Test, Category("RunOnlyThis")]
        public async Task TestHttpPost()
        {
            using (var proxy = new HttpClientProxy())
            {
                var result = await proxy.PostBytesAsync(TestHttpValues.FullUrl, null);
                TestContext.Progress.WriteLine($"Post 2: ---------- ----------");
                TestContext.Progress.WriteLine($"Timeout: {result.Timeout}");
                TestContext.Progress.WriteLine($"Code: {result.StatusCode}");
                TestContext.Progress.WriteLine($"Content: {Encoding.UTF8.GetString(result.Content)}");
            }

            using (var proxy = new HttpClientProxy(TestHttpValues.Url))
            {
                var result = await proxy.PostStringAsync(TestHttpValues.Pattern, null);
                TestContext.Progress.WriteLine($"Post 1: ---------- ----------");
                TestContext.Progress.WriteLine($"Timeout: {result.Timeout}");
                TestContext.Progress.WriteLine($"Code: {result.StatusCode}");
                TestContext.Progress.WriteLine($"Content: {result.Content}");
            }
        }

        [Test, Category("RunOnlyThis")]
        public void TestUri()
        {
            var base1 = new Uri("http://127.0.0.1:9000/api/sub");
            var base2 = new Uri("http://127.0.0.1:9000/api/sub/");

            var uri1 = new Uri(base1, "test"); // 结果: http://127.0.0.1:9000/api/test
            var uri2 = new Uri(base2, "test"); // 结果: http://127.0.0.1:9000/api/sub/test
            var uri3 = new Uri(base1, "/test"); // 结果: http://127.0.0.1:9000/test
            var uri4 = new Uri(base2, "/test"); // 结果: http://127.0.0.1:9000/test

            TestContext.Progress.WriteLine(uri1);
            TestContext.Progress.WriteLine(uri2);
            TestContext.Progress.WriteLine(uri3);
            TestContext.Progress.WriteLine(uri4);
        }

        [Test, Category("RunOnlyThis")]
        public async Task TestTimeout()
        {
            using (var proxy = new HttpClientProxy(TestHttpValues.Url))
            {
                var result = await proxy.GetStringAsync(TestHttpValues.Pattern, TimeSpan.FromSeconds(2));
                TestContext.Progress.WriteLine($"Get 2: ---------- ----------");
                TestContext.Progress.WriteLine($"Timeout: {result.Timeout}");
                TestContext.Progress.WriteLine($"Code: {result.StatusCode}");
                TestContext.Progress.WriteLine($"Content: {result.Content}");
            }
        }
    }
}