using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TravelExperienceEgypt.DataAccess.DTO;
using TravelExperienceEgypt.DataAccess.Models;
using TravelExperienceEgypt.DataAccess.UnitOfWork;

namespace TravelExperienceEgypt.API.Controllers;

[ApiController]
[Route("[controller]")]
public class CommentController : ControllerBase
{
    private readonly IUnitOfWork unitOfWork;

    public CommentController(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    [HttpGet("[action]")]
    public async Task<ActionResult> GetComments()
    {
        IEnumerable<Comment> comments = await unitOfWork.Comment.ReadAllAsync();
        return Ok(comments);
    }

    [HttpPost("[action]")]
    public async Task<ActionResult> AddComment(CommentDTO commentDTO)
    {
        if (ModelState.IsValid)
        {
            Comment comment = new()
            {
                UserId = commentDTO.UserId,
                PostId = commentDTO.PostId,
                Description = commentDTO.Description,
                Date = DateTime.Now,
            };
            await unitOfWork.Comment.AddAsync(comment);
            await unitOfWork.Save();
            return Ok("Comment Added Successfully");
        }
        return BadRequest("Comment Is Not Added !!");
    }
}