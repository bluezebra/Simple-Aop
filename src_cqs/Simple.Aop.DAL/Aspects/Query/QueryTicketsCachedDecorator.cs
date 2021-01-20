using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Simple.Aop.Domain;
using Simple.Aop.Domain.Interfaces;
using Simple.Aop.Domain.Queries;

namespace Simple.Aop.DAL.Aspects.Query
{
    public class QueryTicketsCachedDecorator : IQueryTickets
    {
        private Task<IEnumerable<Ticket>> _TicketsCached = Task.FromResult(Enumerable.Empty<Ticket>());
        private readonly IQueryTickets _queryTickets;

        public QueryTicketsCachedDecorator(IQueryTickets queryTickets) => 
            _queryTickets = queryTickets;

        public Task<IEnumerable<Ticket>> Get(IUserContext userContext)
        {
            if (!userContext.IsAdmin)
                throw new UnauthorizedAccessException("Must be Admin");

            if (!_TicketsCached.Result.Any())
                _TicketsCached = _queryTickets.Get(userContext);

            return _TicketsCached;
        }
    }
}