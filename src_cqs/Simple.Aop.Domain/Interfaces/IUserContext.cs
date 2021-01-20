namespace Simple.Aop.Domain.Interfaces
{
    public interface IUserContext
    {
        TenantId TenantId { get; }
        string UserId { get; }
        bool IsAdmin { get; }
        bool HasPermission(Permission permission);
    }
}
