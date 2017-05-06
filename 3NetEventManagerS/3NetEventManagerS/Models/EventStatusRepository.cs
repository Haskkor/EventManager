using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EntityState = System.Data.Entity.EntityState;

namespace _3NetEventManagerS.Models
{ 
    public class EventStatusRepository : IEventStatusRepository
    {
        readonly EventManagerSContext _context = new EventManagerSContext();

        public IQueryable<EventStatus> All
        {
            get { return _context.EventStatus; }
        }

        public IQueryable<EventStatus> AllIncluding(params Expression<Func<EventStatus, object>>[] includeProperties)
        {
            return includeProperties.Aggregate<Expression<Func<EventStatus, object>>, IQueryable<EventStatus>>(_context.EventStatus, (current, includeProperty) => current.Include(includeProperty));
        }

        public EventStatus Find(int id)
        {
            return _context.EventStatus.Find(id);
        }

        public void InsertOrUpdate(EventStatus eventstatus)
        {
            if (eventstatus.EventStatusId == default(int)) {
                _context.EventStatus.Add(eventstatus);
            } else {
                _context.Entry(eventstatus).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var eventstatus = _context.EventStatus.Find(id);
            _context.EventStatus.Remove(eventstatus);
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

    public interface IEventStatusRepository : IDisposable
    {
        IQueryable<EventStatus> All { get; }
        IQueryable<EventStatus> AllIncluding(params Expression<Func<EventStatus, object>>[] includeProperties);
        EventStatus Find(int id);
        void InsertOrUpdate(EventStatus eventstatus);
        void Delete(int id);
        void Save();
    }
}