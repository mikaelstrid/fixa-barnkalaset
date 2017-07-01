using System;

namespace Pixel.FixaBarnkalaset.ReadModel.Interfaces
{
    public interface IViewRepository
    {
        bool Contains<T>(Guid id) where T : class, IView;
        T Get<T>(Guid id) where T : class, IView;
        void Add<T>(T view) where T : class, IView;
        void Update<T>(Guid id, Action<T> updateAction) where T : class, IView;
    }
}
