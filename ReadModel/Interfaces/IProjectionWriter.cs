using System;

namespace Pixel.FixaBarnkalaset.ReadModel.Interfaces
{
    //public interface IProjectionWriter<T>
    public interface IProjectionWriter
    {
        void Add<T>(T view);
        void Update<T>(object id, Action<T> updateAction);
    }
}
