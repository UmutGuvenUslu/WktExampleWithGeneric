using Microsoft.EntityFrameworkCore;
using PointHomeworkWithGeneric.Entities.Interfaces;
using PointHomeworkWithGeneric.Repositories.Interfaces;

namespace PointHomeworkWithGeneric.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, IEntities
    {
        public readonly DbContext _context;
        public readonly DbSet<T> _dbSet;

        public GenericRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public List<T> GetAll()
        {

            return _dbSet.ToList();
        }

        public T GetById(int id)
        {
            return _dbSet.SingleOrDefault(t => t.Id == id);
        }

        public bool Delete(int id)
        {
            var selectedEntity = _dbSet.SingleOrDefault(e => e.Id == id);
            if (selectedEntity == null)
                return false;

            _dbSet.Remove(selectedEntity);
            return true;
        }

        public bool Add(T entity)
        {
            _dbSet.Add(entity);
            return true;
        }

        public bool AddRange(List<T> entities)
        {
            _dbSet.AddRange(entities);
            return true;
        }

        public bool Update(T entity)
        {
            _dbSet.Update(entity);
            return true;
        }
    }
}
