using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Simple.Aop.Domain.Interfaces;

namespace Simple.Aop.Domain.Queries
{
    public interface IQueryTickets
    {
        Task<IEnumerable<Ticket>> Get(IUserContext userContext);
    }

    public class QueryTickets : IQueryTickets
    {
        private readonly ITicketRepository _ticketRepository;

        public QueryTickets(ITicketRepository ticketRepository) => 
            _ticketRepository = ticketRepository;

        public Task<IEnumerable<Ticket>> Get(IUserContext userContext)
        {
            if (!userContext.IsAdmin)
                throw new UnauthorizedAccessException("Must be Admin");

            return _ticketRepository.Get();
        }
    }
}
