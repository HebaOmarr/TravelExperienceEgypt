using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExperienceEgypt.DataAccess.DTO.PlaceDTO
{
    public class CreatePlaceDTO
    {
        [UniquePlaceNameAttribute]
        [Required(ErrorMessage = "Place name is required")]
        [StringLength(100, ErrorMessage = "Name can't exceed 100 characters")]
        public string Title { get; set; } = null!;

        [StringLength(500, ErrorMessage = "Description can't exceed 500 characters")]
        public string OverviewDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Latitude is required")]
        public double Lat { get; set; }

        [Required(ErrorMessage = "Longitude is required")]
        public double Lng { get; set; }

        [Required(ErrorMessage = "Image link is required")]
        [Url(ErrorMessage = "Invalid image URL format")]
        public string ImageLink { get; set; } = string.Empty;

        [Required(ErrorMessage = "Category ID is required")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Governorate ID is required")]
        public int GovernorateId { get; set; }


    }
}
