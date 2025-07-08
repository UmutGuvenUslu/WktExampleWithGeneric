using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using PointHomeworkWithGeneric.Entities.Interfaces;

public class MapObject : IEntities
{
    public int Id { get; set; }
    public string Name { get; set; }

    private Geometry _wkb;

    [JsonIgnore]
    public Geometry Wkb
    {
        get => _wkb;
        set
        {
            _wkb = value;
            if (_wkb != null)
                _wkb.SRID = 4326; 
        }
    }

    [NotMapped]
    public string Wkt
    {
        get => Wkb != null ? _wkb.ToText() : "";
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                var geometryFactory = new GeometryFactory(new PrecisionModel(), 4326);
                var reader = new WKTReader(geometryFactory);
                Wkb = reader.Read(value);
            }
        }
    }
}
