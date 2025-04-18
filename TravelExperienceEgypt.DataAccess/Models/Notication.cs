using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelExperienceEgypt.DataAccess.Models
{
    internal class Notication:BaseModel
    {
        //[Key]
        //public int Id { get; set; }
        public bool IsRead { get; set; }
        public string Message { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public User? User { get; set; }
    }
}
