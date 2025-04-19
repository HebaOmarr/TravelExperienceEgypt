using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperienceEgypt.DataAccess.Models
{
    public class Comment
    {

        public bool IsDeleted { get; set; } = false;

        public int UserId { get; set; }

        public ApplicationUser? User { get; set; } 
        
        public int PostId { get; set; }
        [ForeignKey("PostId")]

        public Post? Post { get; set; }

        public string Description { get; set; }=string.Empty;
        public DateTime Date { get; set; }
    }
}
