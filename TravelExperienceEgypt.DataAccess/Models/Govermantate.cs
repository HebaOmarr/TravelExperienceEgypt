using System.ComponentModel.DataAnnotations;
namespace TravelExperienceEgypt.DataAccess.Models
{
    internal class Govermantate
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public double Latitude { get; set; }
        public double longitude { get; set; }

    }
}
