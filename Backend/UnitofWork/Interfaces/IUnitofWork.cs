using PointHomeworkWithGeneric.Entities;
using PointHomeworkWithGeneric.Repositories.Interfaces;

namespace PointHomeworkWithGeneric.UnitofWork.Interfaces
{
    public interface IUnitofWork : IDisposable
    {
        IGenericRepository<MapObject> _mapObject { get; }

        int Commit();
    }
}
