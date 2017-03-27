using System;
using System.Collections.Generic;
using System.Linq;
using Pixel.Kidsparties.Core;
using Pixel.Kidsparties.Core.Interfaces;

namespace Pixel.Kidsparties.Infrastructure
{
    public class DummyArrangementRepository : IArrangementRepository
    {
        private readonly List<Arrangement> _arrangements;

        public DummyArrangementRepository()
        {
            _arrangements = new List<Arrangement>
            {
                new Arrangement
                {
                    Id = 1,
                    Name = "Laserdome",
                    Latitude = 56.689355,
                    Longitude = 12.8682093,
                    CategoryCity = "Halmstad",
                    CategoryCounty = "Halland"
                },
                new Arrangement
                {
                    Id = 2,
                    Name = "Parkour",
                    Latitude = 57.6906169,
                    Longitude = 11.9152949,
                    CategoryCity = "Göteborg",
                    CategoryCounty = "Göteborg"
                },
                new Arrangement
                {
                    Id = 3,
                    Name = "Laserdome",
                    Latitude = 57.6006462,
                    Longitude = 11.9772345,
                    CategoryCity = "Göteborg",
                    CategoryCounty = "Göteborg"
                },

            };
        }

        public IEnumerable<Arrangement> GetAll()
        {
            return _arrangements;
        }

        public Arrangement GetById(int id)
        {
            return _arrangements.FirstOrDefault(a => a.Id == id);
        }

        public void AddOrUpdate(Arrangement arrangement)
        {
            var existigArrangement = GetById(arrangement.Id);
            if (existigArrangement != null)
                _arrangements.Remove(existigArrangement);
            _arrangements.Add(arrangement);
        }

        public void Remove(int id)
        {
            _arrangements.RemoveAll(a => a.Id == id);
        }
    }
}
