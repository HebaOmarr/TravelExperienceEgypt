using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperienceEgypt.DataAccess.Models
{
    internal class User// inherit from Identity User
    {

        public string AboutMe { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string? Image { get; set; }//should has dafult value
        public string? CoverImage { get; set; }//should has dafult value

    }
}
