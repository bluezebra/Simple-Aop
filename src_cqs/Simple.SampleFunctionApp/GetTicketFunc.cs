using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Simple.Aop.Domain;
using Simple.Aop.Domain.Interfaces;
using Simple.Aop.Domain.Queries;
using SimpleInjector;

namespace Simple.Aop.Functions
{
    public class GetTicketFunc
    {
        private readonly Container _container;

        public GetTicketFunc(Container container) =>
            _container = container ?? throw new ArgumentNullException(nameof(container));

        [FunctionName(nameof(GetTicket))]
        public async Task<IActionResult> GetTicket(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tickets/{ticketId}")]
            HttpRequestMessage req,
            Guid ticketId)
            => await _container.GetInstance<ReceiveGetTicket>()
                .Run(new UserContext(req), ticketId);
    }
    public class ReceiveGetTicket
    {
        private readonly IQueryTicket _queryTicket;

        public ReceiveGetTicket(IQueryTicket queryTicket) => _queryTicket = queryTicket;

        public async Task<IActionResult> Run(IUserContext userContext, Guid ticketId)
        {
            Ticket? ticket = await _queryTicket.Get(userContext, ticketId);

            return ticket != null
                ? (IActionResult) new OkObjectResult(ticket)
                : new NotFoundObjectResult(ticketId);
        }
    }
}