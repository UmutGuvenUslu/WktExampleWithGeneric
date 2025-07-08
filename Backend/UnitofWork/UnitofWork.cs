using PointHomeworkWithGeneric.Data;
using PointHomeworkWithGeneric.Entities;
using PointHomeworkWithGeneric.Repositories;
using PointHomeworkWithGeneric.Repositories.Interfaces;
using PointHomeworkWithGeneric.UnitofWork.Interfaces;

namespace PointHomeworkWithGeneric.UnitofWork
{
    public class UnitofWork : IUnitofWork
    {
        public readonly MapObjectDbContext _context;
        public IGenericRepository<MapObject> _mapObject { get;  set; }

        public UnitofWork(MapObjectDbContext context)
        {
            _context = context;
            _mapObject = new GenericRepository<MapObject>(_context);

        }

        public int Commit()
        {
            try
            {
                return _context.SaveChanges();
            }
            catch(Exception ex)
            {
                var a = ex.InnerException.Message;
            }
            return _context.SaveChanges();
        }

        public void Dispose()
        {
          
            _context.Dispose();
        }

    }
}
