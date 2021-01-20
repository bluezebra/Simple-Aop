using System;
using System.Threading.Tasks;
using Simple.Aop.Domain.Interfaces;

namespace Simple.Aop.Domain.Queries
{
    public interface IQueryTicket
    {
        Task<Ticket?> Get(IUserContext userContext, Guid id);
    }

    public class QueryTicket : IQueryTicket
    {
        private readonly ITicketRepository _ticketRepository;

        public QueryTicket(ITicketRepository ticketRepository) => 
            _ticketRepository = ticketRepository;

        public Task<Ticket?> Get(IUserContext userContext, Guid id)
        {
            return userContext.HasPermission(Permission.Edit)
                ? _ticketRepository.Get(id)
                : throw new UnauthorizedAccessException();
        }
    }
}