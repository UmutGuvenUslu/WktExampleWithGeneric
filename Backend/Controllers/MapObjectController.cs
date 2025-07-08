using Microsoft.AspNetCore.Mvc;
using PointHomeworkWithGeneric.Dtos;
using PointHomeworkWithGeneric.Entities;        
using PointHomeworkWithGeneric.Services;         

namespace PointHomeworkWithGeneric.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController : ControllerBase
    {
        private readonly MapObjectService _mapObjectService;

        public UserController(MapObjectService mapObjectService)
        {
            _mapObjectService = mapObjectService;
        }

        [HttpGet]
        public List<MapObject> GetAll()
        {

            return _mapObjectService.GetAll();
        }
        [HttpGet]
        public MapObject GetById(int id)
        {
            var result = _mapObjectService.GetbyId(id);
            if (result.IsSuccess)
            {
                return (MapObject)result.Data;
            }
            else
            {
                return null;
            }
            

        }

        [HttpPost]
        public bool Add(string Name, string Wkt)
        {
            var result = _mapObjectService.Add(Name, Wkt);
            if (result.IsSuccess)
            {
                return true;
            }
            else
            {
                return false;
            }
            

        }

        [HttpPost]
        public bool AddRange(List<NameWktDto> mapObjects)
        {
            var result = _mapObjectService.AddRange(mapObjects);
            if (result.IsSuccess)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        [HttpPut]
        public bool Update(int Id,string Name,string Wkt) { 
            var result = _mapObjectService.Update(Id, Name, Wkt);
            if (result.IsSuccess)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }

        [HttpDelete]
        public bool Delete(int id) { 
            var result = _mapObjectService.Delete(id);
            if (result.IsSuccess)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }



    }
}
