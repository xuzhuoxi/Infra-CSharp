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

        [Test]
        public async Task TestHttpGet()
        {
            using (var proxy = new HttpClientProxy())
            {
                await proxy.Get(TestHttpValues.FullUrl,
                    (bool timeout, HttpStatusCode code, byte[] bytes) =>
                    {
                        Console.WriteLine($"Get 1: ---------- ----------");
                        Console.WriteLine($"Timeout: {timeout}");
                        Console.WriteLine($"Code: {code}");
                        Console.WriteLine($"Content: {Encoding.UTF8.GetString(bytes)}");
                    });
            }

            using (var proxy = new HttpClientProxy(TestHttpValues.Url))
            {
                await proxy.Get(TestHttpValues.Pattern
                    , (bool timeout, HttpStatusCode code, string content) =>
                    {
                        Console.WriteLine($"Get 2: ---------- ----------");
                        Console.WriteLine($"Timeout: {timeout}");
                        Console.WriteLine($"Code: {code}");
                        Console.WriteLine($"Content: {content}");
                    });
            }
        }

        [Test]
        public async Task TestHttpPost()
        {
            using (var proxy = new HttpClientProxy())
            {
                await proxy.Post(TestHttpValues.FullUrl, new Dictionary<string, string>()
                    , ((bool timeout, HttpStatusCode code, byte[] bytes) =>
                    {
                        Console.WriteLine($"Post 2: ---------- ----------");
                        Console.WriteLine($"Timeout: {timeout}");
                        Console.WriteLine($"Code: {code}");
                        Console.WriteLine($"Content: {Encoding.UTF8.GetString(bytes)}");
                    }));
            }

            using (var proxy = new HttpClientProxy(TestHttpValues.Url))
            {
                await proxy.Post(TestHttpValues.Pattern, new Dictionary<string, string>()
                    , ((bool timeout, HttpStatusCode code, string content) =>
                    {
                        Console.WriteLine($"Post 1: ---------- ----------");
                        Console.WriteLine($"Timeout: {timeout}");
                        Console.WriteLine($"Code: {code}");
                        Console.WriteLine($"Content: {content}");
                    }));
            }
        }

        [Test]
        public void TestUri()
        {
            var base1 = new Uri("http://127.0.0.1:9000/api/sub");
            var base2 = new Uri("http://127.0.0.1:9000/api/sub/");

            var uri1 = new Uri(base1, "test"); // 结果: http://127.0.0.1:9000/api/test
            var uri2 = new Uri(base2, "test"); // 结果: http://127.0.0.1:9000/api/sub/test
            var uri3 = new Uri(base1, "/test"); // 结果: http://127.0.0.1:9000/test
            var uri4 = new Uri(base2, "/test"); // 结果: http://127.0.0.1:9000/test

            Console.WriteLine(uri1);
            Console.WriteLine(uri2);
            Console.WriteLine(uri3);
            Console.WriteLine(uri4);
        }

        [Test]
        public async Task TestTimeout()
        {
            using (var proxy = new HttpClientProxy(TestHttpValues.Url))
            {
                await proxy.Get(TestHttpValues.Pattern
                    , (bool timeout, HttpStatusCode code, string content) =>
                    {
                        Console.WriteLine($"Get 2: ---------- ----------");
                        Console.WriteLine($"Timeout: {timeout}");
                        Console.WriteLine($"Code: {code}");
                        Console.WriteLine($"Content: {content}");
                    }, TimeSpan.FromSeconds(2));
            }
        }
    }
}