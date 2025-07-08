using NetTopologySuite.IO;
using PointHomeworkWithGeneric.Dtos;
using PointHomeworkWithGeneric.Services.Interfaces;
using PointHomeworkWithGeneric.UnitofWork.Interfaces;

namespace PointHomeworkWithGeneric.Services
{
    public class ValidationService:IValidatonService
    {
        public IUnitofWork _unitofWork;

        public ValidationService(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }


        public void IdValidator(int Id)
        {
            if (Id <= 0)
            {
                throw new ArgumentException("Id 0 veya daha küçük olamaz!");
            }

            if (_unitofWork._mapObject.GetAll().FirstOrDefault(m => m.Id == Id) == null)
            {
                throw new ArgumentException("Id Veritabanındaki bir id ile eşleşmiyor!");
            }
        }

        public void NameValidator(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                throw new ArgumentException("İsim boş olamaz");
            }

            if (name.Length < 3 || name.Length > 200)
            {
                throw new ArgumentException("İsim uzunluğu geçersiz");
            }
        }

        public void WktValidator(string Wkt)
        {
            var reader = new WKTReader();
            var wkt = reader.Read(Wkt);

            if (String.IsNullOrEmpty(Wkt)) {
                throw new ArgumentException("Wkt boş olamaz");
            }
            if (!wkt.IsValid)
            {
                throw new ArgumentException("Wkt Geçerli Bir Formatta Değil!");
            }
        }

        public void ObjectValidator(List<NameWktDto> mapObjectList)
        {
            foreach(var mapObject in mapObjectList){ 
            NameValidator(mapObject.Name);
            WktValidator(mapObject.Wkt);
            }
        }
    }
}
