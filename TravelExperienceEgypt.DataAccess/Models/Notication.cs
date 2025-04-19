using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelExperienceEgypt.DataAccess.Models
{
    public class Notication:BaseModel
    {
        //[Key]
        //public int Id { get; set; }
        public bool IsRead { get; set; }
        public string Message { get; set; } = null!;
        public DateTime Date { get; set; }
        public int UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser? User { get; set; }
    }
}
