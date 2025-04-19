using System.ComponentModel.DataAnnotations;

namespace TravelExperienceEgypt.API.DTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage = "First Name Is Required")]

        public string FirstName { get; set; }=null!;
        [Required(ErrorMessage = "Last Name Is Required")]

        public string LastName { get; set; } = null!;
        [Required(ErrorMessage = "Email Address Is Required")]
        [EmailAddress]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Password Is Required")]
        [DataType(DataType.Password)]
        
        public string Password { get; set; } = null!;

        [Required(ErrorMessage = "Confirmation Password Is Required")]
        [Compare("Password", ErrorMessage = "Confirmation Password Not Match")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;

        [Required(ErrorMessage = "User Name Is Required")]
        [MinLength(6, ErrorMessage = "Must More Than 6 Charachters")]
        public string UserName { get; set; } = null!;

        public string? AboutMe { get; set; } = string.Empty;
        public string? Country { get; set; } = string.Empty;
        public string? City { get; set; } = string.Empty;
        public string? Image { get; set; } = string.Empty;
        public string? CoverImage { get; set; } = string.Empty;

    }
}
