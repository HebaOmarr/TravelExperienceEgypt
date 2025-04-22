using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TravelExperienceEgypt.DataAccess.Models;
using TravelExperienceEgypt.DataAccess.UnitOfWork;

namespace TravelExperienceEgypt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public WishListController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost("add")]
        public async Task<IActionResult> AddToWishList([FromBody] int postid)
        {
            Post post = await _unitOfWork.Post.GetItemAsync(e=>e.ID==postid);
            if (post == null)
            {
                return NotFound(new { message = "Post not found" });
            }
            string userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
            Wishlist wishlist = new Wishlist
            {
                UserId = int.Parse(userid),
                PostId = postid,
                Date = DateTime.Now
            };
            await _unitOfWork.WishList.AddAsync(wishlist);

            return Ok(new { message = "Place added to wishlist", wishlist });
        }

        //public async Task<IActionResult> GetWishList()
        //{
        //    string userid = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier).Value;
        //    var wishlist = await _unitOfWork.WishList.GetAllAsync();
        //    if (wishlist == null)
        //    {
        //        return NotFound(new { message = "Wishlist not found" });
        //    }
        //    var wishlistPosts = wishlist.Where(w => w.UserId == int.Parse(userid)).ToList();
        //    if (wishlistPosts.Count == 0)
        //    {
        //        return NotFound(new { message = "No posts in wishlist" });
        //    }
        //    return Ok(wishlist);
        //}
    }
}
