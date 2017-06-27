using System;

namespace Pixel.FixaBarnkalaset.ReadModel.Interfaces
{
    public interface IProjectionWriter<T>
    {
        void Add(T view);
        void Update(object id, Action<T> updateAction);
    }
}
