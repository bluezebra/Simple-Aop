using System.Diagnostics;
using Simple.Aop.Domain;
using Simple.Aop.Domain.Interfaces;

namespace Simple.Aop.DAL.Aspects.Query
{
    public class PerformanceLogQueryHandlerDecorator<TQuery, TResult>
        : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> _decorated;
        private readonly IMyLogger _logger;
 
        public PerformanceLogQueryHandlerDecorator(
            IQueryHandler<TQuery, TResult> decorated, IMyLogger logger)
        {
            _decorated = decorated;
            _logger = logger;
        }

        public TResult Handle(IUserContext userContext, TQuery query)
        {
            var sw = new Stopwatch();

            var result = _decorated.Handle(userContext, query);

            sw.Stop();

            _logger.LogInformation($"Query {nameof(query)} elapsed:{sw.Elapsed}.");

            return result;
        }
    }
}