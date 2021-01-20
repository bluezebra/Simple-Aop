using System;
using System.Threading.Tasks;
using Simple.Aop.Domain.Commands;
using Simple.Aop.Domain.Interfaces;

namespace Simple.Aop.DAL.Aspects.Command
{
    public class AuditCommandServiceDecorator<TCommand> : ICommandService<TCommand>
    {
        private readonly ITimeProvider _timeProvider;
        private readonly ICommandService<TCommand> _decoratee;
        private readonly ITicketAuditRepository _ticketAuditRepository;

        public AuditCommandServiceDecorator(
            ITimeProvider timeProvider,
            ICommandService<TCommand> decoratee, 
            ITicketAuditRepository ticketAuditRepository)
        {
            _timeProvider = timeProvider;
            _decoratee = decoratee;
            _ticketAuditRepository = ticketAuditRepository;
        }

        public async Task Execute(IUserContext userContext, TCommand command)
        {
            await _decoratee.Execute(userContext, command);

            await AppendToAuditTrail(command);
        }

        private async Task AppendToAuditTrail(TCommand command)
        {
            _ = command ?? throw new ArgumentNullException(nameof(command));

            var commandJson = Newtonsoft.Json.JsonConvert.SerializeObject(command);

            var commandType = command.GetType().Name;

            var entry = new AuditEntry(
                Guid.NewGuid(),
                _timeProvider.Now,
                commandType,
                commandJson);

            await _ticketAuditRepository.Create(entry);
        }
    }
}