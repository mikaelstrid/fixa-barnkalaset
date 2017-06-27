using System.Collections.Generic;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;

namespace Pixel.FixaBarnkalaset.Infrastructure.Interfaces
{
    public interface IObserverRegistry
    {
        IEnumerable<IObserver> GetObservers();
    }
}