

using System.ComponentModel.DataAnnotations;

namespace TravelExperienceEgypt.DataAccess.Models
{
    public class BaseModel
    {
        [Key]
        public int ID { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
