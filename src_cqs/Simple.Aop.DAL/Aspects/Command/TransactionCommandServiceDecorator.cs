using System.Threading.Tasks;
using System.Transactions;
using Simple.Aop.Domain.Commands;
using Simple.Aop.Domain.Interfaces;

namespace Simple.Aop.DAL.Aspects.Command
{
    public class TransactionCommandServiceDecorator<TCommand> : ICommandService<TCommand>
    {
        private readonly ICommandService<TCommand> _decoratee;

        public TransactionCommandServiceDecorator(ICommandService<TCommand> decoratee) 
            => _decoratee = decoratee;

        public async Task Execute(IUserContext userContext, TCommand command)
        {
            using var scope = CreateAsyncTransactionScope();

            await _decoratee.Execute(userContext, command);

            scope.Complete();
        }

        private static TransactionScope CreateAsyncTransactionScope() =>
            new TransactionScope(
                TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled);
    }
}