using System;
using System.Security;
using System.Threading.Tasks;
using Simple.Aop.Domain;
using Simple.Aop.Domain.Commands;
using Simple.Aop.Domain.Interfaces;

namespace Simple.Aop.DAL.Aspects.Command
{
    public class SecureCommandServiceDecorator<TCommand> : ICommandService<TCommand>
    {
        private readonly Permission PermittedPermission = GetPermittedPermission();

        private readonly IUserContext _userContext;
        private readonly ICommandService<TCommand> _decoratee;

        public SecureCommandServiceDecorator(
            IUserContext userContext, ICommandService<TCommand> decoratee)
        {
            _decoratee = decoratee ?? throw new ArgumentNullException(nameof(decoratee));
            _userContext = userContext ?? throw new ArgumentNullException(nameof(userContext));
        }

        public async Task Execute(IUserContext userContext, TCommand command)
        {
            CheckAuthorization();

            await _decoratee.Execute(userContext, command);
        }

        private void CheckAuthorization()
        {
            if (!_userContext.HasPermission(PermittedPermission)) throw new SecurityException();
        }

        private static Permission GetPermittedPermission()
        {
            //var attribute = typeof(TCommand)
            //    .GetCustomAttribute<PermittedPermissionAttribute>();

            //if (attribute == null)
            //    throw new InvalidOperationException($"[PermittedPermission] missing from {typeof(TCommand).Name}.");

            //return attribute.Permission;

            return Permission.Edit;
        }
    }
}