using Microsoft.EntityFrameworkCore;
using PointHomeworkWithGeneric.Entities;
using NetTopologySuite.Geometries;

namespace PointHomeworkWithGeneric.Data
{
    public class MapObjectDbContext : DbContext
    {
        public MapObjectDbContext(DbContextOptions<MapObjectDbContext> options)
            : base(options)
        {
        }
  
        public DbSet<MapObject> MapObjects { get; set; }
    }
}
