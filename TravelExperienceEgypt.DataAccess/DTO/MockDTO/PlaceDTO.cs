using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperienceEgypt.DataAccess.DTO.MockDTO
{
    public class PlaceDTO
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public bool IsVerified { get; set; } = false;// admin will change that
       
        public string Image { get; set; } = string.Empty;

 
    }
}
