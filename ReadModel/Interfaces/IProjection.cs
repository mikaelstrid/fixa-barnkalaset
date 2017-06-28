using Pixel.FixaBarnkalaset.Domain.Events;

namespace Pixel.FixaBarnkalaset.ReadModel.Interfaces
{
    public interface IProjection
    {
        void Handle(IEvent e);
    }
}