using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Simple.Aop.Domain.Interfaces
{
    public interface ITicketRepository
    {
        Task Add(Ticket ticket);
        Task<Ticket?> Get(Guid id);
        Task<IEnumerable<Ticket>> Get();
    }
}