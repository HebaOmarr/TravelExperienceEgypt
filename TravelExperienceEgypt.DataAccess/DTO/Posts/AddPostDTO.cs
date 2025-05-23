using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperienceEgypt.DataAccess.DTO.Posts
{
    public class AddPostDTO
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public float Rate { get; set; }
        public DateTime DatePosted { get; set; } = DateTime.Now;
        public string PlaceName { get; set; }
    }
}
