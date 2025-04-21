using TravelExperienceEgypt.DataAccess.Models;

namespace TravelExperienceEgypt.API.DTOs
{
    public class UserProfileDTO
    {
       
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? EmailAddress { get; set; } 
        public string? AboutMe { get; set; }
        public string? Country { get; set; }
        public string? City { get; set; }
        public string? Image { get; set; }
        public string? CoverImage { get; set; }
      public  IEnumerable<Post>? Posts { get; set; }
        public IEnumerable<Place>? Places { get; set; }
    }
}
