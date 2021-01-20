using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Simple.Aop.Domain.Interfaces;

namespace Simple.Aop.Domain.QueryHandlers
{
    public class GetTicketByIdQuery : IQuery<Task<Ticket?>>
    {
        public GetTicketByIdQuery(Guid ticketId) => TicketId = ticketId;

        [Required]
        public Guid TicketId { get; }
    }
    
    public class GetTicketByIdQueryHandler : IQueryHandler<GetTicketByIdQuery, Task<Ticket?>>
    {
        private readonly ITicketRepository _ticketRepository;

        public GetTicketByIdQueryHandler(ITicketRepository ticketRepository) 
            => _ticketRepository = ticketRepository;

        public async Task<Ticket?> Handle(IUserContext userContext, GetTicketByIdQuery query) 
            => await _ticketRepository.Get(query.TicketId);
    }
}