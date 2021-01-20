using System;
using Simple.Aop.Domain;
using Xunit;

namespace Simple.Domain.Tests
{
    public class TenantIdTests
    {
        [Fact]
        public void TenantId_AnyInvalidChar_Throws()
        {
            var exception1 = Record.Exception(
                () => new TenantId("tenant-id/tenant-id"));

            Assert.IsType<ArgumentException>(exception1);
            Assert.Equal("TenantId must not contain /, \\, #, ?, or control characters.", exception1!.Message);

            var exception2 = Record.Exception(
                () => new TenantId("tenant-id\\tenant-id"));

            Assert.IsType<ArgumentException>(exception2);
            Assert.Equal("TenantId must not contain /, \\, #, ?, or control characters.", exception2!.Message);

            var exception3 = Record.Exception(
                () => new TenantId("tenant-id#tenant-id"));

            Assert.IsType<ArgumentException>(exception3);
            Assert.Equal("TenantId must not contain /, \\, #, ?, or control characters.", exception3!.Message);

            var exception4 = Record.Exception(
                () => new TenantId("tenant-id?tenant-id"));

            Assert.IsType<ArgumentException>(exception4);
            Assert.Equal("TenantId must not contain /, \\, #, ?, or control characters.", exception4!.Message);

            var exception5 = Record.Exception(
                () => new TenantId("tenant-id\ntenant-id"));

            Assert.IsType<ArgumentException>(exception5);
            Assert.Equal("TenantId must not contain /, \\, #, ?, or control characters.", exception5!.Message);
        }

        [Fact]
        public void TenantId_Null_Throws()
        {
            string nullInput = null!;
            var exception = Record.Exception(
                () => new TenantId(nullInput));

            Assert.IsType<ArgumentException>(exception);
            Assert.Equal("TenantId not found.", exception!.Message);
        }

        [Fact]
        public void TenantId_EmptyOrWhitespace_Throws()
        {
            var exception1 = Record.Exception(
                () => new TenantId(""));

            Assert.IsType<ArgumentException>(exception1);
            Assert.Equal("TenantId not found.", exception1!.Message);

            var exception2 = Record.Exception(
                () => new TenantId(" "));

            Assert.IsType<ArgumentException>(exception2);
            Assert.Equal("TenantId not found.", exception2!.Message);
        }
    }
}
