using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EntityState = System.Data.Entity.EntityState;

namespace _3NetEventManagerS.Models
{ 
    public class ContributionRepository : IContributionRepository
    {
        readonly EventManagerSContext _context = new EventManagerSContext();

        public IQueryable<Contribution> All
        {
            get { return _context.Contribution; }
        }

        public IQueryable<Contribution> AllIncluding(params Expression<Func<Contribution, object>>[] includeProperties)
        {
            return includeProperties.Aggregate<Expression<Func<Contribution, object>>, IQueryable<Contribution>>(_context.Contribution, (current, includeProperty) => current.Include(includeProperty));
        }

        public Contribution Find(int id)
        {
            return _context.Contribution.Find(id);
        }

        public void InsertOrUpdate(Contribution contribution)
        {
            if (contribution.ContributionId == default(int)) {
                _context.Contribution.Add(contribution);
            } else {
                _context.Entry(contribution).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var contribution = _context.Contribution.Find(id);
            _context.Contribution.Remove(contribution);
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

    public interface IContributionRepository : IDisposable
    {
        IQueryable<Contribution> All { get; }
        IQueryable<Contribution> AllIncluding(params Expression<Func<Contribution, object>>[] includeProperties);
        Contribution Find(int id);
        void InsertOrUpdate(Contribution contribution);
        void Delete(int id);
        void Save();
    }
}