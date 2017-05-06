using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EntityState = System.Data.Entity.EntityState;

namespace _3NetEventManagerS.Models
{ 
    public class ContributionTypeRepository : IContributionTypeRepository
    {
        readonly EventManagerSContext _context = new EventManagerSContext();

        public IQueryable<ContributionType> All
        {
            get { return _context.ContributionType; }
        }

        public IQueryable<ContributionType> AllIncluding(params Expression<Func<ContributionType, object>>[] includeProperties)
        {
            return includeProperties.Aggregate<Expression<Func<ContributionType, object>>, IQueryable<ContributionType>>(_context.ContributionType, (current, includeProperty) => current.Include(includeProperty));
        }

        public ContributionType Find(int id)
        {
            return _context.ContributionType.Find(id);
        }

        public void InsertOrUpdate(ContributionType contributiontype)
        {
            if (contributiontype.ContributionTypeId == default(int)) {
                // New entity
                _context.ContributionType.Add(contributiontype);
            } else {
                // Existing entity
                _context.Entry(contributiontype).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var contributiontype = _context.ContributionType.Find(id);
            _context.ContributionType.Remove(contributiontype);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose() 
        {
            _context.Dispose();
        }
    }

    public interface IContributionTypeRepository : IDisposable
    {
        IQueryable<ContributionType> All { get; }
        IQueryable<ContributionType> AllIncluding(params Expression<Func<ContributionType, object>>[] includeProperties);
        ContributionType Find(int id);
        void InsertOrUpdate(ContributionType contributiontype);
        void Delete(int id);
        void Save();
    }
}