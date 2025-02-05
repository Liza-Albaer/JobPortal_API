using JobPortal.core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.EF.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected JobPortalDbContext _context;
        public BaseRepository(JobPortalDbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
            Save();
        }

        public void Update(T entity)
        {
            _context.Set<T>().Update(entity);
            Save();
        }

        public void Delete(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                Save();
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
