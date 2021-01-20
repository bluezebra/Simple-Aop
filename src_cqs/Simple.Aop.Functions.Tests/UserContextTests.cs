using System;
using System.Collections.Generic;
using Xunit;
using static Simple.Aop.Functions.Tests.HttpTestHelper;

namespace Simple.Aop.Functions.Tests
{
    public class UserContextTests
    {
        [Fact]
        public void TenantId_Missing_Throws()
        {
            var request = CreateHttpRequest(
                new Dictionary<string, string>{
                    {
                        HttpRequestExtensions.UserIdIdentifierKey, "123"
                    }});

            Assert.Throws<ArgumentException>(() => new UserContext(request));
        }

        [Fact]
        public void TenantId_Null_Throws()
        {
            var request = CreateHttpRequest(
                new Dictionary<string, string>{
                    {
                        HttpRequestExtensions.TenantIdKey, null!
                    },
                    {
                        HttpRequestExtensions.UserIdIdentifierKey, "123"
                    }});

            Assert.Throws<ArgumentException>(() => new UserContext(request));
        }

        [Fact]
        public void UserIdKey_Missing_Throws()
        {
            var request = CreateHttpRequest(
                new Dictionary<string, string>{
                    {
                        HttpRequestExtensions.TenantIdKey, "123"
                    }});

            Assert.Throws<ArgumentException>(() => new UserContext(request));
        }

        [Fact]
        public void UserId_Null_Throws()
        {
            var request = CreateHttpRequest(
                new Dictionary<string, string>{
                    {
                        HttpRequestExtensions.TenantIdKey, "123"
                    },
                    {
                        HttpRequestExtensions.UserIdIdentifierKey, null!
                    }});

            Assert.Throws<ArgumentException>(() => new UserContext(request));
        }

        [Fact]
        public void HappyPath_IsAdmin_Null_False()
        {
            var request = CreateHttpRequest(
                new Dictionary<string, string>{
                    {
                        HttpRequestExtensions.TenantIdKey, "123"
                    },
                    {
                        HttpRequestExtensions.UserIdIdentifierKey, "321"
                    }});

            var userContext = new UserContext(request);

            Assert.False(userContext.IsAdmin);
        }
        
        [Fact]
        public void TryCreate_NoAdmin_HappyPath()
        {
            var request = CreateHttpRequest(
                new Dictionary<string, string>{
                    {
                        HttpRequestExtensions.TenantIdKey, "123"
                    },
                    {
                        HttpRequestExtensions.UserIdIdentifierKey, "321"
                    }});
            
            (UserContext? userContext, var error) = UserContext.TryCreateOrWhy(request);

            Assert.False(userContext!.IsAdmin);
            Assert.True(string.IsNullOrWhiteSpace(error));
        }
    }
}