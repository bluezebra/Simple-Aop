using System.Threading.Tasks;
using Simple.Aop.Domain.Events;
using Simple.Aop.Domain.Interfaces;

namespace Simple.Aop.Domain.EventHandlers
{
    public class OrderFulfillment : IEventHandler<TicketCreated>
    {        
        private readonly ILocationService locationService;
        private readonly IInventoryManagement inventoryManagement;

        public OrderFulfillment(
            ILocationService locationService, IInventoryManagement inventoryManagement)
        {
            this.locationService = locationService;
            this.inventoryManagement = inventoryManagement;
        }

        public async Task Handle(TicketCreated e)
        {
            var ticketId = e.TicketId;

            var warehouses = await locationService.FindWarehouses(ticketId);

            await inventoryManagement.NotifyWarehouses(warehouses);
        }
    }
}
