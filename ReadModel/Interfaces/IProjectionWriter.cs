using System;

namespace Pixel.FixaBarnkalaset.ReadModel.Interfaces
{
    public interface IProjectionWriter
    {
        void Add<T>(T view) where T : IView;
        void Update<T>(object id, Action<T> updateAction) where T : IView;
    }
}
