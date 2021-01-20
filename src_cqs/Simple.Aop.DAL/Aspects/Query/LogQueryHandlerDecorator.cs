using Simple.Aop.Domain;
using Simple.Aop.Domain.Interfaces;

namespace Simple.Aop.DAL.Aspects.Query
{
    public class LogQueryHandlerDecorator<TQuery, TResult>
        : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> _decorated;
        private readonly IMyLogger _logger;
 
        public LogQueryHandlerDecorator(IQueryHandler<TQuery, TResult> decorated, IMyLogger logger)
        {
            _decorated = decorated;
            _logger = logger;
        }

        public TResult Handle(IUserContext userContext, TQuery query)
        {
            _logger.LogInformation(nameof(query));

            return _decorated.Handle(userContext, query);
        }
    }
}
