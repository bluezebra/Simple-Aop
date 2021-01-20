using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Simple.Aop.Domain.Interfaces;
using Simple.Aop.Domain.QueryHandlers;
using SimpleInjector;

namespace Simple.Aop.Functions
{
    public class GetTicketMediatorFunc
    {
        private readonly Container _container;

        public GetTicketMediatorFunc(Container container) =>
            _container = container ?? throw new ArgumentNullException(nameof(container));

        [FunctionName(nameof(GetTicketMediator))]
        public async Task<IActionResult> GetTicketMediator(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tickets-mediator/{ticketId}")]
            HttpRequestMessage req,
            Guid ticketId)
            => await _container.GetInstance<ReceiveGetTicketMediator>()
                .Run(new UserContext(req), ticketId);
    }

    public class ReceiveGetTicketMediator
    {
        readonly IQueryProcessor _queries;

        public ReceiveGetTicketMediator(IQueryProcessor queries) => _queries = queries;

        public async Task<IActionResult> Run(IUserContext userContext, Guid ticketId)
        {
            var query = new GetTicketByIdQuery(ticketId);

            var tickets = await _queries.Process(userContext, query);

            return new OkObjectResult(tickets);
        }
    }
}