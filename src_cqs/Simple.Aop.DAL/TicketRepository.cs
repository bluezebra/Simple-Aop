using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Simple.Aop.Domain;
using Simple.Aop.Domain.Interfaces;

namespace Simple.Aop.DAL
{
    public class TicketRepository : ITicketRepository
    {
        public async Task Add(Ticket ticket)
        {
            await Task.Delay(300);
            mTickets = mTickets.Add(ticket);
        }

        public async Task<Ticket?> Get(Guid id) => 
            await Task.FromResult(mTickets.SingleOrDefault(x => x.Id == id));

        public async Task<IEnumerable<Ticket>> Get() => await Task.FromResult(mTickets);

        private static IImmutableList<Ticket> mTickets =
            Enumerable
                .Range(0, 5)
                .Select(x => new Ticket(Guid.NewGuid(), $"Title {x}", $"Description {x}"))
                .ToImmutableList();
    }
}