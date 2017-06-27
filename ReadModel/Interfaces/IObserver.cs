using Pixel.FixaBarnkalaset.Domain.Events;

namespace Pixel.FixaBarnkalaset.ReadModel.Interfaces
{
    public interface IObserver
    {
        void Handle(IEvent @event);
    }
}