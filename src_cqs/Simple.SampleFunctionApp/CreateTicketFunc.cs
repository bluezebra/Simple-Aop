using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Simple.Aop.Domain.Commands;
using Simple.Aop.Domain.Interfaces;
using SimpleInjector;

namespace Simple.Aop.Functions
{
    public class CreateTicketFunc
    {
        private readonly Container _container;

        public CreateTicketFunc(Container container) =>
            _container = container ?? throw new ArgumentNullException(nameof(container));

        [FunctionName(nameof(CreateTickets))]
        public async Task<IActionResult> CreateTickets(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "tickets")]
            HttpRequestMessage req) =>
            await _container.GetInstance<ReceiveCreateTicket>()
                .Run(new UserContext(req), await req.Content.ReadAsAsync<CreateTicket>());
    }
    
    public class ReceiveCreateTicket
    {
        private readonly ICommandService<CreateTicket> _createTicketService;

        public ReceiveCreateTicket(ICommandService<CreateTicket> createTicketService) 
            => _createTicketService = createTicketService;

        public async Task<IActionResult> Run(IUserContext userContext, CreateTicket createTicket)
        {
            await _createTicketService.Execute(userContext, createTicket);

            return new OkObjectResult("Ticket created.");
        }
    }
}