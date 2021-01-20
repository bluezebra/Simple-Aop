using System.Threading.Tasks;

namespace Simple.Aop.Domain.Interfaces
{
    public interface ITicketAuditRepository
    {
        Task Create(IAuditEntry auditEntry);
    }
}