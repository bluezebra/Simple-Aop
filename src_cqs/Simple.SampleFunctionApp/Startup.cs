using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Simple.Aop.DAL;
using Simple.Aop.DAL.Aspects.Command;
using Simple.Aop.DAL.Aspects.Query;
using Simple.Aop.Domain;
using Simple.Aop.Domain.Commands;
using Simple.Aop.Domain.Events;
using Simple.Aop.Domain.Interfaces;
using Simple.Aop.Domain.Queries;
using Simple.Aop.Functions;
using SimpleInjector;
using SimpleInjectorContainer = SimpleInjector.Container;

[assembly: FunctionsStartup(typeof(Startup))]
namespace Simple.Aop.Functions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var configuration = builder.GetContext().Configuration;
            builder.Services.AddSingleton(Setup(configuration));
        }

        public SimpleInjectorContainer Setup(IConfiguration configuration)
        {
            var container = new SimpleInjectorContainer();

            container.Register<IMyLogger, MyLogger>(Lifestyle.Singleton);

            // Humble Objects replacing Functions
            container.Register<ReceiveCreateTicket>();
            container.Register<ReceiveGetTicket>();
            container.Register<ReceiveGetTickets>();
            container.Register<ReceiveGetTicketMediator>();
            container.Register<ReceiveGetTicketsMediator>();

            container.Register<ITicketRepository, TicketRepository>(Lifestyle.Singleton);
            container.Register<ITicketAuditRepository, TicketAuditRepository>(Lifestyle.Singleton);

            // Inject time to remove impurity
            container.Register<ITimeProvider, TimeProvider>(Lifestyle.Singleton);

            // Cannot inject IUserContext in Function App

            var assembly = typeof(ITimeProvider).Assembly;

            // Commands
            container.Register(typeof(ICommandService<>), assembly);
            // Decorators can be applied conditionally with type constraints https://simpleinjector.readthedocs.io/en/latest/aop.html#decoration
            container.RegisterDecorator(typeof(ICommandService<>), typeof(AuditCommandServiceDecorator<>));
            container.RegisterDecorator(typeof(ICommandService<>), typeof(DeadlockRetryCommandServiceDecorator<>));
            container.RegisterDecorator(typeof(ICommandService<>), typeof(TransactionCommandServiceDecorator<>));
            container.RegisterDecorator(typeof(ICommandService<>), typeof(LogCommandServiceDecorator<>));

            // Events
            container.Register<ILocationService, LocationService>(Lifestyle.Singleton);
            container.Register<IInventoryManagement, InventoryManagement>(Lifestyle.Singleton);
            container.Register(typeof(IEventHandler<>), typeof(IEventHandler<>).Assembly);

            // Simple Queries
            container.Register<IQueryTicket, QueryTicket>(Lifestyle.Singleton);
            container.Register<IQueryTickets, QueryTickets>(Lifestyle.Singleton);
            container.RegisterDecorator(typeof(IQueryTickets), typeof(QueryTicketsCachedDecorator), Lifestyle.Singleton);

            // Query Mediator
            container.Register<IQueryProcessor, QueryProcessor>(Lifestyle.Singleton);
            container.Register(typeof(IQueryHandler<,>), typeof(IQueryHandler<,>).Assembly);
            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(PerformanceLogQueryHandlerDecorator<,>));
            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(ValidationQueryHandlerDecorator<,>));
            container.RegisterDecorator(typeof(IQueryHandler<,>), typeof(LogQueryHandlerDecorator<,>));

            container.Verify();

            return container;
        }
    }
}