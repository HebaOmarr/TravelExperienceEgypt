using System.ComponentModel.DataAnnotations;
namespace TravelExperienceEgypt.DataAccess.Models
{
    public class Govermantate:BaseModel
    {
        //[Key]
        //public int Id { get; set; }
        public string Name { get; set; }=null!;
        public string Image { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double longitude { get; set; }
        public IEnumerable<Place>? Places { get; set; } 
    }
}
