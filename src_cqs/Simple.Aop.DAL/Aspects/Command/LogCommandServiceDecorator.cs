using System;
using System.Threading.Tasks;
using Simple.Aop.Domain;
using Simple.Aop.Domain.Commands;
using Simple.Aop.Domain.Interfaces;

namespace Simple.Aop.DAL.Aspects.Command
{
    public class LogCommandServiceDecorator<TCommand> : ICommandService<TCommand>
    {
        private readonly IMyLogger _logger;
        private readonly ICommandService<TCommand> _decoratee;

        public LogCommandServiceDecorator(
            ICommandService<TCommand> decoratee, IMyLogger logger)
        {
            _decoratee = decoratee;
            _logger = logger;
        }

        public async Task Execute(IUserContext userContext, TCommand command)
        {
            _ = command ?? throw new ArgumentNullException(nameof(command));

            _logger.LogInformation(command.GetType().Name);

            await _decoratee.Execute(userContext, command);
        }
    }
}