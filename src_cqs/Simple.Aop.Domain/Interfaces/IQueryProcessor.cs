namespace Simple.Aop.Domain.Interfaces
{
    public interface IQueryProcessor
    {
        TResult Process<TResult>(IUserContext userContext, IQuery<TResult> query);
    }
}
