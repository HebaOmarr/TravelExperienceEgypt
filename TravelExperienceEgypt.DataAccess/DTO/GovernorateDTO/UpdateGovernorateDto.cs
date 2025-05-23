using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperienceEgypt.DataAccess.DTO.GovernorateDTO
{
    public class UpdateGovernorateDto
    {
        //public int GovermantateId { get; set; }
        //[Required(ErrorMessage = "Governorate name is required")]
        //[StringLength(100, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 100 characters")]
        public string Title { get; set; } = null!;

        public string ImageLink { get; set; } = string.Empty;

        //[StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
        public string OverviewDescription { get; set; } = string.Empty;

        //[Range(-90, 90, ErrorMessage = "Latitude value must be between -90 and 90")]
        public double Lat { get; set; }

        //[Range(-180, 180, ErrorMessage = "Longitude value must be between -180 and 180")]
        public double Lng { get; set; }
    }
}
