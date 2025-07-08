using PointHomeworkWithGeneric.Dtos;
using PointHomeworkWithGeneric.Entities;
using System.Text.RegularExpressions;

namespace PointHomeworkWithGeneric.Services.Interfaces
{
    public interface IValidatonService
    {

        public void IdValidator(int Id);

        public void NameValidator(string name);

        public void WktValidator(string Wkt);

        public void ObjectValidator(List<NameWktDto> mapObjecList);
        
    }
}
