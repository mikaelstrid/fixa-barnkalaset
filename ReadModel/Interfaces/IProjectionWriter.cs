using System;

namespace Pixel.FixaBarnkalaset.ReadModel.Interfaces
{
    public interface IProjectionWriter
    {
        bool Contains<T>(Guid id);
        void Add<T>(T view) where T : class, IView;
        void Update<T>(Guid id, Action<T> updateAction) where T : class, IView;
    }
}
