using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using EntityState = System.Data.Entity.EntityState;

namespace _3NetEventManagerS.Models
{ 
    public class RoleRepository : IRoleRepository
    {
        readonly EventManagerSContext _context = new EventManagerSContext();

        public IQueryable<Role> All
        {
            get { return _context.Role; }
        }

        public IQueryable<Role> AllIncluding(params Expression<Func<Role, object>>[] includeProperties)
        {
            return includeProperties.Aggregate<Expression<Func<Role, object>>, IQueryable<Role>>(_context.Role, (current, includeProperty) => current.Include(includeProperty));
        }

        public Role Find(int id)
        {
            return _context.Role.Find(id);
        }

        public void InsertOrUpdate(Role role)
        {
            if (role.RoleId == default(int)) {
                _context.Role.Add(role);
            } else {
                _context.Entry(role).State = EntityState.Modified;
            }
        }

        public void Delete(int id)
        {
            var role = _context.Role.Find(id);
            _context.Role.Remove(role);
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

    public interface IRoleRepository : IDisposable
    {
        IQueryable<Role> All { get; }
        IQueryable<Role> AllIncluding(params Expression<Func<Role, object>>[] includeProperties);
        Role Find(int id);
        void InsertOrUpdate(Role role);
        void Delete(int id);
        void Save();
    }
}