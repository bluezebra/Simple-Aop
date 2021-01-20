using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Simple.Aop.Domain.Interfaces
{
    public interface IInventoryManagement
    {
        Task NotifyWarehouses(IEnumerable<Warehouse> warehouses);
    }

    public class InventoryManagement : IInventoryManagement
    {
        public async Task NotifyWarehouses(IEnumerable<Warehouse> warehouses)
        {
            await Task.Delay(300);

            Debug.WriteLine("Wharehouses notified.");
        }
    }
}