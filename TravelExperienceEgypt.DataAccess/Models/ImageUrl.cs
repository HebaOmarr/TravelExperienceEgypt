using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TravelExperienceEgypt.DataAccess.Models
{
    public class ImageUrl:BaseModel
    {
        //[Key]
        //public int Id { get; set; }
        public string Url { get; set; } = string.Empty;
        public int PostId { get; set; }

        [ForeignKey(nameof(PostId))]
        public Post? Post { get; set; }
    }
}

