using System;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence
{
    //public class InMemoryProjectionWriter<T> : IProjectionWriter<T>
    public class InMemoryProjectionWriter : IProjectionWriter
    {
        public void Add<T>(T view)
        {
            throw new NotImplementedException();
        }

        public void Update<T>(object id, Action<T> updateAction)
        {
            throw new NotImplementedException();
        }
    }
}