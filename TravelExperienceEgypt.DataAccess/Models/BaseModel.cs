

using System.ComponentModel.DataAnnotations;

namespace TravelExperienceEgypt.DataAccess.Models
{
    internal class BaseModel
    {
        [Key]
        public int Id { get; set; }

        public bool IsDeleted { get; set; } = false;
    }
}
