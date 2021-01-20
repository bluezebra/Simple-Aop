using System;
using System.Threading.Tasks;

namespace Simple.Aop.Domain.Interfaces
{
    public interface ILocationService
    {
        Task<Warehouse[]> FindWarehouses(Guid ticketId);
    }

    public class LocationService : ILocationService
    {
        public async Task<Warehouse[]> FindWarehouses(Guid ticketId)
        {
            await Task.Delay(300);

            return new[] {new Warehouse()};
        }
    }
}