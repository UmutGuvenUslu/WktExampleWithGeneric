using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using PointHomeworkWithGeneric.Dtos;
using PointHomeworkWithGeneric.Entities;
using PointHomeworkWithGeneric.Services.Interfaces;
using PointHomeworkWithGeneric.UnitofWork.Interfaces;

namespace PointHomeworkWithGeneric.Services
{
    public class MapObjectService
    {
        public readonly IUnitofWork _unitofWork;
        public readonly IValidatonService _validatonService;


        public MapObjectService(IUnitofWork unitofWork, IValidatonService validatonService)
        {
            _unitofWork = unitofWork;
            _validatonService = validatonService;
        }


        #region GetAll
        public List<MapObject> GetAll()
        {
            return _unitofWork._mapObject.GetAll();

        }
        #endregion

        #region GetById
        public Result GetbyId(int id) {
            var result = new Result();
            try
            {
                _validatonService.IdValidator(id);
                result.Data = _unitofWork._mapObject.GetById(id);
                result.IsSuccess = true;
                result.Message = "Başarılı";
                return result;

            }
            catch (Exception ex) {
                result.Message = ex.Message;
                result.IsSuccess = false;
                return result;
            }
        }
        #endregion

        #region Add
        public Result Add(string Name, string Wkt)
        {
            var result = new Result();
            try
            {
                _validatonService.NameValidator(Name);
                _validatonService.WktValidator(Wkt);

                var geometryFactory = new GeometryFactory(new PrecisionModel(), 4326);
                var reader = new WKTReader(geometryFactory);

                Geometry wkb = reader.Read(Wkt);
                wkb.SRID = 4326;

                var mapObject = new MapObject
                {
                    Name = Name,
                    Wkt = Wkt,
                    Wkb = wkb
                };
                _unitofWork._mapObject.Add(mapObject);
                _unitofWork.Commit();

                result.IsSuccess = true;
                result.Message = "Başarılı";
                result.Data = mapObject;
                return result;
            }
            catch (Exception ex)
            {
                _unitofWork.Dispose();
                result.IsSuccess = false;
                return result;
            }
        }
        #endregion

        #region Delete
        public Result Delete(int id)
        {
            var result = new Result();
            try
            {
                _validatonService.IdValidator(id);
                _unitofWork._mapObject.Delete(id);
                _unitofWork.Commit();
                result.IsSuccess = true;
                result.Message = "Başarılı";
                return result;
            }
            catch (Exception ex)
            {
                _unitofWork.Dispose();
                result.Message = ex.Message;
                result.IsSuccess = false;
                return result;
            }

        }
        #endregion

        #region Update
        public Result Update(int id, string Name, string Wkt) {

            var result = new Result();
            try
            {
                _validatonService.IdValidator(id);
                _validatonService.NameValidator(Name);
                _validatonService.WktValidator(Wkt);
                var geometryFactory = new GeometryFactory(new PrecisionModel(), 4326);
                var reader = new WKTReader(geometryFactory);
                Geometry wkb = reader.Read(Wkt);
                wkb.SRID = 4326;
                var SelectedObject = _unitofWork._mapObject.GetById(id);
                SelectedObject.Name = Name;
                SelectedObject.Wkb = wkb;
                _unitofWork.Commit();
                result.IsSuccess = true;
                result.Message = "Başarılı";
                return result;
            }
            catch (Exception ex) {
                _unitofWork.Dispose();
                result.Message = ex.Message;
                result.IsSuccess = false;
                return result;

            }

        }
        #endregion

        #region AddRange
        public Result AddRange(List<NameWktDto> mapObjectList)
        {
            var result = new Result();
            var addedList = new List<MapObject>();
            try
            {
                _validatonService.ObjectValidator(mapObjectList);
                var geometryFactory = new GeometryFactory(new PrecisionModel(), 4326);
                var reader = new WKTReader(geometryFactory);

                foreach (var item in mapObjectList) {
                    Geometry wkb = reader.Read(item.Wkt);
                    wkb.SRID = 4326;
                    var mapObject = new MapObject
                    {
                        Name = item.Name,
                        Wkb = wkb,
                    };
                    addedList.Add(mapObject);
                }
                _unitofWork._mapObject.AddRange(addedList);
                _unitofWork.Commit();
                result.IsSuccess = true;
                result.Message = "Başarılı";
                result.Data = mapObjectList;
                return result;
            }
            catch (Exception ex) {
                _unitofWork.Dispose();
                result.Message = ex.Message;
                result.IsSuccess = false;
                return result;
            }

        }
        #endregion

    }
}


