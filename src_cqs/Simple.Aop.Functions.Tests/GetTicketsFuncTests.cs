using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Simple.Aop.Domain;
using Simple.Aop.Domain.Interfaces;
using Simple.Aop.Domain.Queries;
using Xunit;

namespace Simple.Aop.Functions.Tests
{
    public class GetTicketsFuncTests
    {
        private readonly Mock<IQueryTickets> _queryTickets;

        public GetTicketsFuncTests()
        {
            _queryTickets = new Mock<IQueryTickets>();
        }

        [Fact]
        public void GetTickets_HappyPath_ReturnsOkObjectResult()
        {
            IEnumerable<Ticket> tickets = Enumerable
                .Range(0, 5)
                .Select(x => new Ticket(Guid.NewGuid(), $"Title {x}", $"Description {x}"))
                .ToImmutableList();

            _queryTickets
                .Setup(x => x.Get(It.IsAny<IUserContext>()))
                .Returns(Task.FromResult(tickets));

            var userContext = new Mock<IUserContext>();

            var response = new ReceiveGetTickets(_queryTickets.Object)
                .Run(userContext.Object).Result;

            Assert.IsType<OkObjectResult>(response);
            var result = (OkObjectResult)response;

            Assert.Equal(tickets, result.Value);
        }
    }
}