using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperienceEgypt.DataAccess.Models
{
    public class Wishlist
    {
        public bool IsDeleted { get; set; } = false;
        public int UserId { get; set; }

        public ApplicationUser? User { get; set; }
        public int PostId { get; set; }

        public Post? Post { get; set; }
        public DateTime Date { get; set; }
    }
}
