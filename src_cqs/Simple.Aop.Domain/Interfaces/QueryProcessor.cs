using System.Diagnostics;
using SimpleInjector;

namespace Simple.Aop.Domain.Interfaces
{
    public sealed class QueryProcessor : IQueryProcessor
    {
        private readonly Container _container;

        public QueryProcessor(Container container) => _container = container;

        [DebuggerStepThrough]
        public TResult Process<TResult>(IUserContext userContext, IQuery<TResult> query)
        {
            var handlerType = typeof(IQueryHandler<,>)
                .MakeGenericType(query.GetType(), typeof(TResult));

            dynamic handler = _container.GetInstance(handlerType);

            return handler.Handle(userContext, (dynamic)query);
        }
    }
}