using System;
using System.Threading.Tasks;
using Simple.Aop.Domain.Events;
using Simple.Aop.Domain.Interfaces;

namespace Simple.Aop.Domain.Commands
{
    public class CreateTicketResult
    {
        public CreateTicketResult(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public string Title { get; }
        public string Description { get; }
    }

    public class CreateTicketResultService : ICommandResultService<CreateTicket>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IEventHandler<TicketCreated> _handler;

        public CreateTicketResultService(
            ITicketRepository ticketRepository, 
            IEventHandler<TicketCreated> handler)
        {
            _ticketRepository = ticketRepository;
            _handler = handler;
        }

        public async Task<(bool result, string error)> 
            Execute(IUserContext userContext, CreateTicket command)
        {
            if (!userContext.HasPermission(Permission.Edit))
                return (false, $"Does not have role {Permission.Edit}");

            var ticketId = Guid.NewGuid();

            await _ticketRepository.Add(new Ticket(ticketId, command.Title, command.Description));
            
            await _handler.Handle(new TicketCreated(ticketId));

            return (true, string.Empty);
        }
    }
}
