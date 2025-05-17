using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperienceEgypt.DataAccess.DTO.MockDTO
{
    public class PostDTO
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public float Rate { get; set; }
        public DateTime DatePosted { get; set; }
 

    }
}
