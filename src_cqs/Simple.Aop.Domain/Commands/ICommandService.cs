using System.Threading.Tasks;
using Simple.Aop.Domain.Interfaces;

namespace Simple.Aop.Domain.Commands
{
    // ReSharper disable TypeParameterCanBeVariant
    // No 'in' keyword for TCommand. Commands are single use cases, and cannot map to multiple handlers.
    public interface ICommandService<TCommand>
    {
        Task Execute(IUserContext userContext, TCommand command);
    }

    public interface ICommandResultService<TCommand>
    {
        Task<(bool result, string error)> Execute(IUserContext userContext, TCommand command);
    }
}