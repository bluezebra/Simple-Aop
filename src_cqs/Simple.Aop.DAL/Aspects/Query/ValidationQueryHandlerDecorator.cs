using System.ComponentModel.DataAnnotations;
using Simple.Aop.Domain.Interfaces;

namespace Simple.Aop.DAL.Aspects.Query
{
    public class ValidationQueryHandlerDecorator<TQuery, TResult>
        : IQueryHandler<TQuery, TResult> where TQuery : IQuery<TResult>
    {
        private readonly IQueryHandler<TQuery, TResult> _decorated;
 
        public ValidationQueryHandlerDecorator(IQueryHandler<TQuery, TResult> decorated) 
            => _decorated = decorated;

        public TResult Handle(IUserContext userContext, TQuery query)
        {
            // Call to System.ComponentModel.DataAnnotations
            Validator.ValidateObject(
                query,
                new ValidationContext(query, null, null),
                validateAllProperties: true);

            return _decorated.Handle(userContext, query);
        }
    }
}
