using System.Diagnostics;
using System.Threading.Tasks;
using Simple.Aop.Domain.Interfaces;

namespace Simple.Aop.DAL
{
    public class TicketAuditRepository : ITicketAuditRepository
    {
        public async Task Create(IAuditEntry auditEntry)
        {
            await Task.Delay(300);
            Debug.WriteLine($"Audit created, {auditEntry.Operation}: {auditEntry.Data}.");
        }
    }
}
