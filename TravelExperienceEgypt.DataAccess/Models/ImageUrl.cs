using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace TravelExperienceEgypt.DataAccess.Models
{
    internal class ImageUrl
    {
        [Key]
        public int Id { get; set; }
        public string Url { get; set; }
        public int PostId { get; set; }

        [ForeignKey(nameof(PostId))]
        public Post? Post { get; set; }
    }
}
}
