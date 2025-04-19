
using System.ComponentModel.DataAnnotations;

namespace TravelExperienceEgypt.DataAccess.Models
{
    public class Category:BaseModel
    {
    //    [Key]
    //    public int Id { get; set; }

        [Required]
        public string Name { get; set; } = null!;

        // category from Admin is true User Can enter other name make it false until Revision from admin
        public bool IsVerified { get; set; }

        #region possible values
        //Tourism,
        //Entertainment,
        //Restaurants,
        //Cafes,
        //Shopping,
        //Services,
        //Education,
        //Medical,
        //Sports,
        //Parks,
        //Transportation,
        //Accommodation,
        //Religious,
        //PersonalCare,
        //Nightlife,
        //Family,
        //Kids,
        #endregion
    }
}
