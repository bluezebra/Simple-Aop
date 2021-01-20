using System;
using System.Collections.Immutable;
using System.Linq;
using System.Net.Http;
using Simple.Aop.Domain;
using Simple.Aop.Domain.Interfaces;
using static Simple.Aop.Functions.HttpRequestExtensions;

namespace Simple.Aop.Functions
{
    public class UserContext : IUserContext
    {
        public UserContext(HttpRequestMessage requestMessage)
        {
            var tenantIdString = requestMessage.GetValueOrNull(TenantIdKey);
            TenantId = new TenantId(tenantIdString!);
            
            var userId = requestMessage.GetValueOrNull(UserIdIdentifierKey);
            if (string.IsNullOrWhiteSpace(userId))
                throw new ArgumentException($"{nameof(UserIdIdentifierKey)} not found.");
            UserId = userId;

            Permissions = ImmutableList.Create(Permission.Edit);

            var isAdminString = requestMessage.GetValueOrNull(IsAdminKey);
            IsAdmin = isAdminString != null && Convert.ToBoolean(isAdminString);
        }

        public static (UserContext? userContext, string validationError) 
            TryCreateOrWhy(HttpRequestMessage requestMessage)
        {
            try
            {
                return (new UserContext(requestMessage), string.Empty);
            }
            catch (ArgumentException e)
            {
                return (null, e.Message);
            }
        }

        public TenantId TenantId { get; }
        public string UserId { get; }
        public bool IsAdmin { get; }
        public IImmutableList<Permission> Permissions { get; }

        public bool HasPermission(Permission permission) => Permissions.Contains(permission);
    }
}