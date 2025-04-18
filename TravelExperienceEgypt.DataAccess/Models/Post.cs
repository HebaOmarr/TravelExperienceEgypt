using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperienceEgypt.DataAccess.Models
{
    internal class Post:BaseModel
    {
        //[Key]
        //public int Id { get; set; }
        public string UserExperice { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public float Rate { get; set; }
        public DateTime DatePosted { get; set; }
        public int PlaceId { get; set; }

        [ForeignKey(nameof(PlaceId))]
        public Place? Place { get; set; }

        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }

        
    }
}
