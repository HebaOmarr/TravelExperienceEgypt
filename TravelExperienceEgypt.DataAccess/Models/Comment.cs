using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperienceEgypt.DataAccess.Models
{
    internal class Comment
    {

        public bool IsDeleted { get; set; } = false;

        public string UserId { get; set; }
        public User? User { get; set; } 
        
        public int PostId { get; set; }
        public Post? Post { get; set; }

        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
