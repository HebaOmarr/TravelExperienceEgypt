using System.ComponentModel.DataAnnotations;

namespace TravelExperienceEgypt.API.DTOs
{
    public class LoginDTO
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; } = null!;
        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; } = null!;
        public bool RememberMe { get; set; }



    }
}
