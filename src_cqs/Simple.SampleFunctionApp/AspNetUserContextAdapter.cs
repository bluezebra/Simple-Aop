using Microsoft.AspNetCore.Http;
using Simple.Aop.Domain;

namespace Simple.Aop.Functions
{
    //NOTE What we can do with WebApi or MVC, maybe we can do with Azure AD?
    public class AspNetUserContextAdapter
    {
        private static readonly HttpContextAccessor mAccessor = new HttpContextAccessor();

        public bool IsInRole(Permission permission) => 
            mAccessor.HttpContext.User.IsInRole(permission.ToString());
    }
}
