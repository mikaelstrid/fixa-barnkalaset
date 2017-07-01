using System;
using System.Collections.Generic;
using Pixel.FixaBarnkalaset.ReadModel;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence
{
    public class InMemoryViewRepository : IViewRepository, ISlugDictionary
    {
        private readonly Dictionary<string, IView> _cache;

        public InMemoryViewRepository()
        {
            _cache = new Dictionary<string, IView>();
        }

        public bool Contains<T>(Guid id) where T : class, IView
        {
            var key = CreateCacheKey(typeof(T), id);
            return _cache.ContainsKey(key);
        }

        public T Get<T>(Guid id) where T : class, IView
        {
            var key = CreateCacheKey(typeof(T), id);
            if (_cache.ContainsKey(key))
                return _cache[key] as T;
            else
                return null;
        }

        public void Add<T>(T view) where T : class, IView
        {
            var key = CreateCacheKey(view.GetType(), view.Id);
            _cache.Add(key, view);
        }

        public void Update<T>(Guid id, Action<T> updateAction) where T : class, IView
        {
            var key = CreateCacheKey(typeof(T), id);
            if (!_cache.ContainsKey(key))
                throw new ArgumentException($"The cache does not contain any entry for {key}", nameof(id));

            updateAction(_cache[key] as T);
        }

        private static string CreateCacheKey(Type type, Guid id) => $"{type.Name}-{id}";



        public Guid? GetId(string slug)
        {
            throw new NotImplementedException();
        }
    }
}