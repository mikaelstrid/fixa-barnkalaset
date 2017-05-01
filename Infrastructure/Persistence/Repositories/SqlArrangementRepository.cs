using System;
using System.Collections.Generic;
using System.Linq;
using Pixel.Kidsparties.Core;
using Pixel.Kidsparties.Core.Interfaces;
using Pixel.Kidsparties.Infrastructure.Persistence.EntityFramework;

namespace Pixel.Kidsparties.Infrastructure.Persistence.Repositories
{
    public class SqlArrangementRepository : IArrangementRepository
    {
        private readonly KidsPartiesContext _dbContext;

        public SqlArrangementRepository(KidsPartiesContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Arrangement> GetAll()
        {
            return _dbContext.Arrangements.AsEnumerable();
        }

        public IEnumerable<Arrangement> GetByCitySlug(string citySlug)
        {
            return _dbContext
                .Cities
                .Find(citySlug)
                .Arrangements;
        }

        public Arrangement GetBySlug(string citySlug, string arrangementSlug)
        {
            return _dbContext
                .Cities
                .Find(citySlug)
                .Arrangements
                .SingleOrDefault(a =>
                    a.Slug.Equals(arrangementSlug, StringComparison.CurrentCultureIgnoreCase)
                );
        }

        public Arrangement GetById(int id)
        {
            return _dbContext
                .Arrangements
                .Find(id);
        }

        public void AddOrUpdate(Arrangement arrangement)
        {
            if (arrangement.Id == default(int))
            {
                _dbContext.Arrangements.Add(arrangement);
            }
            else
            {
                _dbContext.Update(arrangement);
            }
            _dbContext.SaveChanges();
        }

        public void Remove(int id)
        {
            var arrangement = GetById(id);
            if (arrangement == null) return;
            _dbContext.Arrangements.Remove(arrangement);
            _dbContext.SaveChanges();
        }
    }
}
