using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EntityState = System.Data.Entity.EntityState;

namespace _3NetEventManagerS.Models
{ 
    public class EventTypeRepository : IEventTypeRepository
    {
        readonly EventManagerSContext _context = new EventManagerSContext();

        public IQueryable<EventType> All
        {
            get { return _context.EventType; }
        }

        public IQueryable<EventType> AllIncluding(params Expression<Func<EventType, object>>[] includeProperties)
        {
            return includeProperties.Aggregate<Expression<Func<EventType, object>>, IQueryable<EventType>>(_context.EventType, (current, includeProperty) => current.Include(includeProperty));
        }

        public EventType Find(int id)
        {
            return _context.EventType.Find(id);
        }

        public void InsertOrUpdate(EventType eventtype)
        {
            if (eventtype.EventTypeId == default(int)) {
                _context.EventType.Add(eventtype);
            } else {
                _context.Entry(eventtype).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var eventtype = _context.EventType.Find(id);
            _context.EventType.Remove(eventtype);
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

    public interface IEventTypeRepository : IDisposable
    {
        IQueryable<EventType> All { get; }
        IQueryable<EventType> AllIncluding(params Expression<Func<EventType, object>>[] includeProperties);
        EventType Find(int id);
        void InsertOrUpdate(EventType eventtype);
        void Delete(int id);
        void Save();
    }
}