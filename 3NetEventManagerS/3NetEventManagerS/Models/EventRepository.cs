using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EntityState = System.Data.Entity.EntityState;

namespace _3NetEventManagerS.Models
{ 
    public class EventRepository : IEventRepository
    {
        readonly EventManagerSContext _context = new EventManagerSContext();

        public IQueryable<Event> All
        {
            get { return _context.Event; }
        }

        public IQueryable<Event> AllIncluding(params Expression<Func<Event, object>>[] includeProperties)
        {
            return includeProperties.Aggregate<Expression<Func<Event, object>>, IQueryable<Event>>(_context.Event, (current, includeProperty) => current.Include(includeProperty));
        }

        public Event Find(int id)
        {
            return _context.Event.Find(id);
        }

        public void InsertOrUpdate(Event event1)
        {
            if (event1.EventId == default(int)) {
                _context.Event.Add(event1);
            } else {
                _context.Entry(event1).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var event1 = _context.Event.Find(id);
            _context.Event.Remove(event1);
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

    public interface IEventRepository : IDisposable
    {
        IQueryable<Event> All { get; }
        IQueryable<Event> AllIncluding(params Expression<Func<Event, object>>[] includeProperties);
        Event Find(int id);
        void InsertOrUpdate(Event event1);
        void Delete(int id);
        void Save();
    }
}