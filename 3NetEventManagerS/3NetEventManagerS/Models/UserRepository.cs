using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EntityState = System.Data.Entity.EntityState;

namespace _3NetEventManagerS.Models
{ 
    public class UserRepository : IUserRepository
    {
        readonly EventManagerSContext _context = new EventManagerSContext();

        public IQueryable<User> All
        {
            get { return _context.User; }
        }

        public IQueryable<User> AllIncluding(params Expression<Func<User, object>>[] includeProperties)
        {
            return includeProperties.Aggregate<Expression<Func<User, object>>, IQueryable<User>>(_context.User, (current, includeProperty) => current.Include(includeProperty));
        }

        public User Find(int id)
        {
            return _context.User.Find(id);
        }

        public void InsertOrUpdate(User user)
        {
            if (user.UserId == default(int)) {
                _context.User.Add(user);
            } else {
                _context.Entry(user).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var user = _context.User.Find(id);
            _context.User.Remove(user);
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

    public interface IUserRepository : IDisposable
    {
        IQueryable<User> All { get; }
        IQueryable<User> AllIncluding(params Expression<Func<User, object>>[] includeProperties);
        User Find(int id);
        void InsertOrUpdate(User user);
        void Delete(int id);
        void Save();
    }
}