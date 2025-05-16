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
                await proxy.Get(TestHttpValues.FullUrl,
                    (bool timeout, HttpStatusCode code, byte[] bytes) =>
                    {
                        TestContext.Progress.WriteLine($"Get 1: ---------- ----------");
                        TestContext.Progress.WriteLine($"Timeout: {timeout}");
                        TestContext.Progress.WriteLine($"Code: {code}");
                        TestContext.Progress.WriteLine($"Content: {Encoding.UTF8.GetString(bytes)}");
                    });
            }

            using (var proxy = new HttpClientProxy(TestHttpValues.Url))
            {
                await proxy.Get(TestHttpValues.Pattern
                    , (bool timeout, HttpStatusCode code, string content) =>
                    {
                        TestContext.Progress.WriteLine($"Get 2: ---------- ----------");
                        TestContext.Progress.WriteLine($"Timeout: {timeout}");
                        TestContext.Progress.WriteLine($"Code: {code}");
                        TestContext.Progress.WriteLine($"Content: {content}");
                    });
            }
        }

        [Test, Category("RunOnlyThis")]
        public async Task TestHttpPost()
        {
            using (var proxy = new HttpClientProxy())
            {
                await proxy.Post(TestHttpValues.FullUrl, new Dictionary<string, string>()
                    , ((bool timeout, HttpStatusCode code, byte[] bytes) =>
                    {
                        TestContext.Progress.WriteLine($"Post 2: ---------- ----------");
                        TestContext.Progress.WriteLine($"Timeout: {timeout}");
                        TestContext.Progress.WriteLine($"Code: {code}");
                        TestContext.Progress.WriteLine($"Content: {Encoding.UTF8.GetString(bytes)}");
                    }));
            }

            using (var proxy = new HttpClientProxy(TestHttpValues.Url))
            {
                await proxy.Post(TestHttpValues.Pattern, new Dictionary<string, string>()
                    , ((bool timeout, HttpStatusCode code, string content) =>
                    {
                        TestContext.Progress.WriteLine($"Post 1: ---------- ----------");
                        TestContext.Progress.WriteLine($"Timeout: {timeout}");
                        TestContext.Progress.WriteLine($"Code: {code}");
                        TestContext.Progress.WriteLine($"Content: {content}");
                    }));
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
                await proxy.Get(TestHttpValues.Pattern
                    , (bool timeout, HttpStatusCode code, string content) =>
                    {
                        TestContext.Progress.WriteLine($"Get 2: ---------- ----------");
                        TestContext.Progress.WriteLine($"Timeout: {timeout}");
                        TestContext.Progress.WriteLine($"Code: {code}");
                        TestContext.Progress.WriteLine($"Content: {content}");
                    }, TimeSpan.FromSeconds(2));
            }
        }
    }
}