using Pixel.FixaBarnkalaset.Domain.Events;

namespace Pixel.FixaBarnkalaset.Infrastructure.Interfaces
{
    public interface IEventPublisher
    {
        void Publish(IEvent @event);
        void CatchUp(int lastPosition);
    }
}
