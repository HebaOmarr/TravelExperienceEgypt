using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelExperienceEgypt.DataAccess.DTO.PlaceDTO;
using TravelExperienceEgypt.DataAccess.Models;

using TravelExperienceEgypt.BusinessLogic.Services;

namespace TravelExperienceEgypt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaceController : ControllerBase
    {
        private readonly PlaceService _placeService;

        public PlaceController(PlaceService placeService)
        {
            _placeService = placeService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPlaces()
        {
            try
            {
                IEnumerable<Place> result = await _placeService.GetAllPlacesRequest();
                if (result == null || !result.Any())
                    return NotFound(new { message = "No places found." });

                return Ok(result);
            }
            catch
            {
                return StatusCode(500, new { message = "An error occurred while retrieving places." });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlaceById(int id)
        {
            try
            {
                Place result = await _placeService.GetPlaceByIdRequest(id);
                if (result == null)
                    return NotFound(new { message = "Place not found." });

                return Ok(result);
            }
            catch
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the place." });
            }
        }

        [HttpGet("by-governorate/{governorateId}")]
        public async Task<IActionResult> GetPlacesByGovernorateId(int governorateId)
        {
            try
            {
                IEnumerable<Place> result = await _placeService.GetPlacesByGovernorateIdRequest(governorateId);
                if (result == null || !result.Any())
                    return NotFound(new { message = "No places found in this governorate." });

                return Ok(result);
            }
            catch
            {
                return StatusCode(500, new { message = "An error occurred while retrieving places." });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreatePlace(CreatePlaceDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _placeService.CreatePlaceRequest(dto);
                return Ok(new { message = "Place created successfully." });
            }
            catch
            {
                return StatusCode(500, new { message = "An error occurred while creating the place." });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        public async Task<IActionResult> UpdatePlace(UpdatePlaceDTO dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                bool success = await _placeService.UpdatePlaceByIdRequest(dto);
                if (success)
                    return Ok(new { message = "Place updated successfully." });

                return NotFound(new { message = "Place not found." });
            }
            catch
            {
                return StatusCode(500, new { message = "An error occurred while updating the place." });
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePlace(int id)
        {
            try
            {
                bool success = await _placeService.DeletePlaceByIdRequest(id);
                if (success)
                    return Ok(new { message = "Place deleted successfully." });

                return NotFound(new { message = "Place not found." });
            }
            catch
            {
                return StatusCode(500, new { message = "An error occurred while deleting the place." });
            }
        }
    }
}
