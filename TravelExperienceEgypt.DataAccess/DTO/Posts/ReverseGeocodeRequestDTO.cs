using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperienceEgypt.DataAccess.DTO.Posts
{
    public class ReverseGeocodeRequest
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
