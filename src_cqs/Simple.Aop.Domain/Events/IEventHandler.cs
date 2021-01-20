using System.Threading.Tasks;

namespace Simple.Aop.Domain.Events
{
    public interface IEventHandler<in TEvent>
    {
        Task Handle(TEvent e);
    }
}