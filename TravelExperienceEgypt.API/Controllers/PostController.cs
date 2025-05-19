using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TravelExperienceEgypt.API.Results;
using TravelExperienceEgypt.BusinessLogic.Services;
using TravelExperienceEgypt.DataAccess.DTO.Posts;
using TravelExperienceEgypt.DataAccess.Models;
using TravelExperienceEgypt.DataAccess.Repository.Contract;

namespace TravelExperienceEgypt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : ControllerBase
    {
        PostServices PostServices;
        public PostController(PostServices PostServices)
        {
            this.PostServices = PostServices;
        }

        [HttpGet("GDTF")]
        public ActionResult Filter()
        {
           return  Ok(PostServices.GetFilterOptions());
        }

        [HttpPost("get-data")]
        public async Task<ActionResult> GetFilterOptions(string RequestName)
        {
            try
            {
                List<FilterResposeDTO> filterResposes = PostServices.Filter(RequestName);
                return Ok(filterResposes);
            }
            catch
            {
                return BadRequest("An error occurred while processing your request");
            }
        }



        [HttpPost("AddPost")]
        public async Task<IActionResult> AddPostAction([FromForm] AddPostDTO dto, IFormFile imageFile)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);
                return BadRequest(Result<string>.Failure(string.Join(",", errors)));
            }

            if (imageFile == null || imageFile.Length == 0)
                return BadRequest("Image required");

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized(Result<string>.Failure("Not authorized"));


            Result<string> res = await PostServices.AddPost(dto, imageFile, int.Parse(userIdClaim.Value));

            if (res.IsSuccess)
                return Ok(res);

            return BadRequest(res);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> EditPost(int id, [FromForm] EditPostDTO dto)
        {

            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage);
                return BadRequest(Result<string>.Failure(string.Join(",", errors)));
            }

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized(Result<string>.Failure("Not authorized"));


            Result<string> res = await PostServices.UpdatePostAsync(id, dto ,int.Parse(userIdClaim.Value));
            if (res.IsSuccess)
                return Ok(res);

            return BadRequest(res);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
                return Unauthorized(Result<string>.Failure("Not authorized"));

            var userId = int.Parse(userIdClaim.Value);

            Result<string> res = await PostServices.DeletePostAsync(id, userId);
            if (res.IsSuccess)
                return Ok(res);

            return BadRequest(res);
        }
    }
}
