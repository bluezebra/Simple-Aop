using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Simple.Aop.Functions
{
    public static class HttpRequestExtensions
    {
        public const string TenantIdKey = "TenantId";
        public const string UserIdIdentifierKey = "UserIdIdentifier";
        public const string IsAdminKey = "IsAdmin";

        public static BadRequestObjectResult HandleBadRequest(string errorMessage)
        {
            var loggerFactory = new LoggerFactory();
            var log = loggerFactory.CreateLogger("test");

            log.LogError(errorMessage);

            return new BadRequestObjectResult(errorMessage);
        }

        public static UnauthorizedResult HandleUnauthorizedResult(string errorMessage)
        {
            var loggerFactory = new LoggerFactory();
            var log = loggerFactory.CreateLogger("test");

            log.LogError(errorMessage);

            return new UnauthorizedResult();
        }

        //private static (IUserContext? userContext, string validationError) 
        //    ToUserContextOrWhy(this HttpRequestMessage req) => UserContext.TryCreateOrWhy(req);

        public static string? GetValueOrNull(this HttpRequestMessage req, string key)
        {
            req.Headers.TryGetValues(key, out IEnumerable<string>? values);
            return values?.First();
        }

        public static bool Contains(this HttpRequestMessage req, string key) 
            => req.Headers.Contains(key);

        public static string Get(this HttpRequestMessage req, string key) 
            => GetValue(req, key);

        private static string GetValue(HttpRequestMessage req, string key) 
            => req.Headers.GetValues(key).First();
    }
}