using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelExperienceEgypt.DataAccess.Models
{
    public class Place:BaseModel
    {
        //[Key]
        //public int Id { get; set; }

        public string Name { get; set; }=null!;
        public string Description { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsVerified { get; set; } = false;// admin will change that
        public int categoryId { get; set; }

        [ForeignKey(nameof(categoryId))]
        public Category? category { get; set; }
        public string Image { get; set; } = string.Empty;

        public int GovermantateId { get; set; }

        [ForeignKey(nameof(GovermantateId))]
        public Govermantate? Govermantate { get; set; }
        public IEnumerable<Post>? Posts { get; set; }

    }
    
}
