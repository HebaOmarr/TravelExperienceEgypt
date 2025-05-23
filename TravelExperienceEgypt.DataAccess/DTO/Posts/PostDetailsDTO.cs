using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperienceEgypt.DataAccess.DTO.Posts
{
    public class PostDetailsDTO
    {
        public int Id { get; set; }
        public List<string>? ImageURLs { get; set; } = new List<string>();
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public float Rate { get; set; }
        public DateTime DatePosted { get; set; }
        public string? PlaceName { get; set; } = null!;
        public string? PlaceDescription { get; set; } = string.Empty;
        public String ?UserFirstName { get; set; } = null!;
        public String? UserLastName { get; set; } = null!;
        public string? CategoryName { get; set; } = null!;
    }
}
