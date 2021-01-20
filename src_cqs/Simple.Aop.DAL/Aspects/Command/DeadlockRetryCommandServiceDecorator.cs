using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Simple.Aop.Domain.Commands;
using Simple.Aop.Domain.Interfaces;

namespace Simple.Aop.DAL.Aspects.Command
{
    public class DeadlockRetryCommandServiceDecorator<TCommand> : ICommandService<TCommand>
    {
        private readonly ICommandService<TCommand> _decoratee;
 
        public DeadlockRetryCommandServiceDecorator(ICommandService<TCommand> decoratee) 
            => _decoratee = decoratee;

        public Task Execute(IUserContext userContext, TCommand command) => 
            HandleWithRetry(userContext, command, retries: 5);

        private async Task HandleWithRetry(
            IUserContext userContext, TCommand command, int retries)
        {
            try
            {
                await _decoratee.Execute(userContext, command);
            }
            catch (Exception ex)
            {
                if (retries <= 0 || !IsDeadlockException(ex)) throw;
 
                Thread.Sleep(300);
 
                await HandleWithRetry(userContext, command, retries - 1);
            }
        }
 
        private static bool IsDeadlockException(Exception ex) =>
            ex is DbException && ex.Message.Contains("deadlock") 
            || ex.InnerException != null && IsDeadlockException(ex.InnerException);
    }
}