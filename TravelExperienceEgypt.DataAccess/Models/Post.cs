using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperienceEgypt.DataAccess.Models
{
    public class Post:BaseModel
    {
        //[Key]
        //public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public float Rate { get; set; }
        public DateTime DatePosted { get; set; }
        public int PlaceId { get; set; }

        [ForeignKey(nameof(PlaceId))]
        public Place? Place { get; set; }

        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }
        IEnumerable<ImageUrl>? ImageUrls { get; set; }
        public IEnumerable<Comment>? Comments { get; set; }
        public IEnumerable<Wishlist>? Wishlists { get; set; }


    }
}
