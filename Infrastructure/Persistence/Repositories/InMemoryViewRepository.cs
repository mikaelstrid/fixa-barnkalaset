using System;
using System.Collections.Generic;
using Pixel.FixaBarnkalaset.ReadModel;
using Pixel.FixaBarnkalaset.ReadModel.Interfaces;

namespace Pixel.FixaBarnkalaset.Infrastructure.Persistence.Repositories
{
    public class InMemoryViewRepository : IViewRepository, ISlugLookup
    {
        private readonly Dictionary<string, IView> _cache;

        private readonly Dictionary<string, Guid> _slugIdDictionary;

        public InMemoryViewRepository()
        {
            _cache = new Dictionary<string, IView>();
            _slugIdDictionary = new Dictionary<string, Guid>();
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



        public Guid? GetIdBySlug(string slug)
        {
            return _slugIdDictionary.ContainsKey(slug) ? (Guid?) _slugIdDictionary[slug] : null;
        }

        public void AddSlug(string slug, Guid id)
        {
            _slugIdDictionary[slug] = id;
        }

        public void RemoveSlug(string slug)
        {
            _slugIdDictionary.Remove(slug);
        }

        //public void AddOrUpdateIdForSlug(string slug, Guid id)
        //{
        //    var key = slug.ToLower();
        //    if (_slugIdDictionary.ContainsKey(key))
        //    {
        //        _slugIdDictionary[key] = id;

        //    }
        //    else
        //        _slugIdDictionary.Add(key, id);
        //}
    }
}