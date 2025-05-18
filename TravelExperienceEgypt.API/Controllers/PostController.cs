using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelExperienceEgypt.BusinessLogic.Services;
using TravelExperienceEgypt.DataAccess.DTO.Posts;
using TravelExperienceEgypt.DataAccess.Repository.Contract;

namespace TravelExperienceEgypt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        PostServices PostServices;
        public PostController(PostServices PostServices)
        {
            this.PostServices = PostServices;
        }

        [HttpGet]
        public ActionResult Filter()
        {
           return  Ok(PostServices.GetFilterOptions());
        }

        [HttpPost("get-data")]
        public async Task<ActionResult> GetFilterOptions(FilterRequestDTO requestDTO)
        {
            try
            {
                List<FilterResposeDTO> filterResposes =await PostServices.FilterRequestAsync(requestDTO);
                return Ok(filterResposes);
            }
            catch
            {
                return BadRequest("An error occurred while processing your request");
            }
        }

    }
}
