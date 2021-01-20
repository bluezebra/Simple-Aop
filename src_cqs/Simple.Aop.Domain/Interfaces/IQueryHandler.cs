namespace Simple.Aop.Domain.Interfaces
{
    // ReSharper disable TypeParameterCanBeVariant
    // No 'in' keyword, Queries are single use cases, and cannot map to multiple handlers.
    public interface IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        TResult Handle(IUserContext userContext, TQuery query);
    }
}