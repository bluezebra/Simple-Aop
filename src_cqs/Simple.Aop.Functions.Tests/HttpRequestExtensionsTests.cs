using System.Collections.Generic;
using Xunit;
using static Simple.Aop.Functions.HttpRequestExtensions;
using static Simple.Aop.Functions.Tests.HttpTestHelper;

namespace Simple.Aop.Functions.Tests
{
    public class HttpRequestExtensionsTests
    {
        [Fact]
        public void GetValueOrNull_KeyDoesNotExist_ReturnsNull()
        {
            var request = CreateHttpRequest(
                new Dictionary<string, string>{
                    {
                        UserIdIdentifierKey, "123"
                    }});

            Assert.Null(request.GetValueOrNull("KeyDoesNotExist"));
        }

        [Fact]
        public void GetValueOrNull_KeyExists_ReturnsValue()
        {
            var request = CreateHttpRequest(
                new Dictionary<string, string>{
                {
                    UserIdIdentifierKey, "123"
                }});

            Assert.Equal("123", request.GetValueOrNull(UserIdIdentifierKey));
        }

        [Fact]
        public void GetValueOrNull_EmptyKey_ReturnsEmptyString()
        {
            var request = CreateHttpRequest(
                new Dictionary<string, string>{
                {
                    UserIdIdentifierKey, ""
                }});

            Assert.Equal("", request.GetValueOrNull(UserIdIdentifierKey));
        }
    }
}