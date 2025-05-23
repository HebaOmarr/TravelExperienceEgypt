using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelExperienceEgypt.BusinessLogic.Services;
using TravelExperienceEgypt.DataAccess.DTO.GovernorateDTO;
using TravelExperienceEgypt.DataAccess.Models;
namespace TravelExperienceEgypt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GovernorateController : ControllerBase
    {
        GovernorateService _governorateService;
        public GovernorateController(GovernorateService governorateService) { 
            _governorateService = governorateService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllGovernorates()
        {
            try
            {
                IEnumerable<Govermantate> result = await _governorateService.GetAllGovermantateRequest();
                if (result == null || !result.Any())
                    return NotFound(new { message = "No governorates found." });

                return Ok(result);
            }
            catch
            {
                return StatusCode(500, new { message = "An error occurred while retrieving governorates." });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetGovernorateById(int id)
        {
            try
            {
                Govermantate result = await _governorateService.GetGovermantateByIdRequest(id);
                if (result == null)
                    return NotFound(new { message = "Governorate not found." });

                return Ok(result);
            }
            catch
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the governorate." });
            }
        }

        [HttpGet("byName/{name}")]
        public async Task<IActionResult> GetGovernorateByName(string name)
        {
            try
            {
                var result = await _governorateService.GetGovermantateByNameRequest(name);
                if (result == null)
                    return NotFound(new { message = "Governorate not found." });

                return Ok(result);
            }
            catch
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the governorate." });
            }
        }

        [HttpGet("with-places")]
        public async Task<IActionResult> GetGovernoratesWithPlaces()
        {
            try
            {
                IEnumerable<Govermantate> result = await _governorateService.GetGovermantateWithPlacesRequest();
                if (result == null || !result.Any())
                    return NotFound(new { message = "No governorates with places found." });

                return Ok(result);
            }
            catch
            {
                return StatusCode(500, new { message = "An error occurred while retrieving data." });
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateGovernorate(CreateGovernorateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _governorateService.CreateGovermantateRequest(dto);
                return Ok(new { message = "Governorate created successfully." });
            }
            catch
            {
                return StatusCode(500, new { message = "An error occurred while creating the governorate." });
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateGovernorate(int id,UpdateGovernorateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                bool success = await _governorateService.UpdateGovermantateByIdRequest(dto, id);
                if (success)
                    return Ok(new { message = "Governorate updated successfully." });

                return NotFound(new { message = "Governorate not found." });
            }
            catch
            {
                return StatusCode(500, new { message = "An error occurred while updating the governorate." });
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGovernorate(int id)
        {
            try
            {
                bool success = await _governorateService.DeleteGovermantateByIdRequest(id);
                if (success)
                    return Ok(new { message = "Governorate deleted successfully." });

                return NotFound(new { message = "Governorate not found." });
            }
            catch
            {
                return StatusCode(500, new { message = "An error occurred while deleting the governorate." });
            }
        }

    }
}
