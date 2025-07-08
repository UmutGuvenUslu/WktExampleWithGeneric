using PointHomeworkWithGeneric.Entities;
using PointHomeworkWithGeneric.Entities.Interfaces;

namespace PointHomeworkWithGeneric.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T: class,IEntities
    {
        public List<T> GetAll();
        public T GetById(int id);
        bool Add(T entity);
        bool  Delete(int id);
        bool Update(T entity);
        bool AddRange(List<T> entities);


    }
}
