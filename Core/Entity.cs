using System;

namespace Pixel.FixaBarnkalaset.Core
{
    public abstract class Entity<T>
    {
        // ReSharper disable FieldCanBeMadeReadOnly.Local
        private DateTime _lastUpdatedUtc = DateTime.MinValue;
        private string _updatedBy = string.Empty;

        public T Id { get; set; }
        public bool IsRemoved { get; set; }

        // ReSharper disable ConvertToAutoProperty
        public DateTime LastUpdatedUtc => _lastUpdatedUtc;
        public string UpdatedBy => _updatedBy;
    }
}