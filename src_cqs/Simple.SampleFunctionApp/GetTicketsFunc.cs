using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Simple.Aop.Domain.Interfaces;
using Simple.Aop.Domain.Queries;
using SimpleInjector;

namespace Simple.Aop.Functions
{
    public class GetTicketsFunc
    {
        private readonly Container _container;

        public GetTicketsFunc(Container container) =>
            _container = container ?? throw new ArgumentNullException(nameof(container));

        [FunctionName(nameof(GetTickets))]
        public async Task<IActionResult> GetTickets(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tickets")]
            HttpRequestMessage req)
            => await _container.GetInstance<ReceiveGetTickets>()
                .Run(new UserContext(req));
    }

    public class ReceiveGetTickets
    {
        readonly IQueryTickets _queryTickets;

        public ReceiveGetTickets(IQueryTickets queryTickets) => _queryTickets = queryTickets;

        public async Task<IActionResult> Run(IUserContext userContext)
        {
            var tickets = await _queryTickets.Get(userContext);

            return new OkObjectResult(tickets);
        }
    }
}