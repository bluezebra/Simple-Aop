using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Simple.Aop.Domain.Interfaces;

namespace Simple.Aop.Domain.QueryHandlers
{
    public class GetTicketsQuery : IQuery<Task<IEnumerable<Ticket>>> { }

    public class GetTicketsQueryHandler : IQueryHandler<GetTicketsQuery, Task<IEnumerable<Ticket>>>
    {
        private readonly ITicketRepository _ticketRepository;

        public GetTicketsQueryHandler(ITicketRepository ticketRepository) =>
            _ticketRepository = ticketRepository;

        public async Task<IEnumerable<Ticket>> Handle(IUserContext userContext, GetTicketsQuery query)
        {
            if (!userContext.IsAdmin)
                throw new UnauthorizedAccessException("Must be Admin");

            return await _ticketRepository.Get();
        }
    }
}
