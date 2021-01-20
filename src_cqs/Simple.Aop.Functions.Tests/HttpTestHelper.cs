using System.Collections.Generic;
using System.Net.Http;

namespace Simple.Aop.Functions.Tests
{
    public static class HttpTestHelper
    {
        public static HttpRequestMessage CreateHttpRequest(
            Dictionary<string, string> headers, string content = "")
        {
            var request = new HttpRequestMessage();

            foreach (var (key, value) in headers)
            {
                request.Headers.Add(key, value);
            }

            request.Content = new StringContent(content);

            return request;
        }
    }
}
