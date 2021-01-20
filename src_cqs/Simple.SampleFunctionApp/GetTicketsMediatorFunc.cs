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
    public class GetTicketsMediatorFunc
    {
        private readonly Container _container;

        public GetTicketsMediatorFunc(Container container) =>
            _container = container ?? throw new ArgumentNullException(nameof(container));

        [FunctionName(nameof(GetTicketsMediator))]
        public Task<IActionResult> GetTicketsMediator(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tickets-mediator")]
            HttpRequestMessage req)
            => _container.GetInstance<ReceiveGetTicketsMediator>()
                .Run(new UserContext(req));
    }

    public class ReceiveGetTicketsMediator
    {
        private readonly IQueryProcessor _queries;

        public ReceiveGetTicketsMediator(IQueryProcessor queries) => _queries = queries;

        public async Task<IActionResult> Run(IUserContext userContext)
        {
            var tickets = await _queries.Process(userContext, new GetTicketsQuery());

            return new OkObjectResult(tickets);
        }
    }
}