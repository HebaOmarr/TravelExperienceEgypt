using System.Text.Json.Serialization;
using TravelExperienceEgypt.DataAccess.DTO.MockDTO;
using TravelExperienceEgypt.DataAccess.Models;

namespace TravelExperienceEgypt.API.DTOs
{
    public class UserPostPlaceDTO
    {
        public IEnumerable<PostDTO>? Posts { get; set; }
        public IEnumerable<PlaceDTO>? Places { get; set; }
    }
}
