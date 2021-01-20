using System;

namespace Simple.Aop.Domain.Events
{
    public class TicketCreated
    {
        public Guid TicketId { get; }

        public TicketCreated(Guid ticketId)
        {
            TicketId = ticketId;
        }
    }
}