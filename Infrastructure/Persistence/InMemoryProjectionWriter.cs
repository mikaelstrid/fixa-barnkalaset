using System;
using System.Collections.Generic;
using Pixel.FixaBarnkalaset.ReadModel;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence
{
    public class InMemoryProjectionWriter : IProjectionWriter
    {
        private readonly Dictionary<string, IView> _cache;

        public InMemoryProjectionWriter()
        {
            _cache = new Dictionary<string, IView>();
        }

        public void Add<T>(T view) where T : IView
        {
            var key = CreateCacheKey(view.GetType(), view.Id);
            _cache.Add(key, view);
        }

        public void Update<T>(object id, Action<T> updateAction) where T : IView
        {
            throw new NotImplementedException();
        }

        private static string CreateCacheKey(Type type, Guid id) => $"{type.Name}-{id}";
    }
}