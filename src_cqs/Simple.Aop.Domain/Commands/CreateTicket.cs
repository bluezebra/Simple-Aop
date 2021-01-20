using System;
using System.Threading.Tasks;
using Simple.Aop.Domain.Events;
using Simple.Aop.Domain.Interfaces;

namespace Simple.Aop.Domain.Commands
{
    public class CreateTicket
    {
        public CreateTicket(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public string Title { get; }
        public string Description { get; }
    }

    public class CreateTicketService : ICommandService<CreateTicket>
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IEventHandler<TicketCreated> _handler;

        public CreateTicketService(
            ITicketRepository ticketRepository, IEventHandler<TicketCreated> handler)
        {
            _ticketRepository = ticketRepository;
            _handler = handler;
        }

        public async Task Execute(IUserContext userContext, CreateTicket command)
        {
            EnforceUserContext(userContext);

            var ticketId = Guid.NewGuid();

            await _ticketRepository.Add(new Ticket(ticketId, command.Title, command.Description));

            await _handler.Handle(new TicketCreated(ticketId));
        }

        private static void EnforceUserContext(IUserContext userContext)
        {
            if (userContext == null)
                throw new ArgumentException(nameof(userContext));

            if (!userContext.HasPermission(Permission.Edit))
                throw new ArgumentException($"Does not have edit permission {Permission.Edit}");
        }
    }
}