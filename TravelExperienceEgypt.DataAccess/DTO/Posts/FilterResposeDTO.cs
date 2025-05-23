using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperienceEgypt.DataAccess.DTO.Posts
{
    public class FilterResposeDTO
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public float Rate { get; set; }
        public decimal Price { get; set; }
        public string PlaceName { get; set; }
        public string Description { get; set; }
        public DateTime DatePosted { get; set; }
    }
}
