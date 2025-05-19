using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperienceEgypt.DataAccess.Models
{
    public class ApplicationUser: IdentityUser<int>   
    {

        //public string AboutMe { get; set; } = string.Empty;
        //public string Country { get; set; } = string.Empty;
        //public string City { get; set; } = string.Empty;
        //public string? Image { get; set; }//should has dafult value
        //public string? CoverImage { get; set; }//should has dafult value
        IEnumerable<Post>? Posts { get; set; }
        IEnumerable<Comment>? Comments { get; set; }
        IEnumerable<Wishlist>? Wishlists { get; set; }
        public IEnumerable<Notification>? Notications { get; set; }
        public String FirstName { get; set; } = null!;
        public String LastName { get; set; } = null!;

    }
}
