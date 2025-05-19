using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using TravelExperienceEgypt.BusinessLogic.Services;
using TravelExperienceEgypt.DataAccess.Models;
using TravelExperienceEgypt.DataAccess.UnitOfWork;

namespace TravelExperienceEgypt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public WishListController(IUnitOfWork unitOfWork   )
        {
            _unitOfWork = unitOfWork;
        }
        [HttpPost("add/{postid:int}")]

        public async Task<IActionResult> AddToWishList([FromRoute] int postid)
        {
            try
            {

                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userid))
                    return Unauthorized("Invalid user.");

                var existingWishlist = await _unitOfWork.WishList.GetItemAsync(e => e.PostId == postid && e.UserId == userid);
                if (existingWishlist != null)
                {
                    return Conflict(new { message = "Post already in wishlist" });
                }

                Wishlist wishlist = new Wishlist
                {
                    UserId = userid,
                    PostId = postid,
                    Date = DateTime.Now
                };

                await _unitOfWork.WishList.AddAsync(wishlist);
                await _unitOfWork.Save();

                return Ok(new { message = "Post added to wishlist", wishlist });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error adding to wishlist", error = ex.Message });

            }
        }

        [HttpGet("getUserWishlist")]
        public async Task<IActionResult> GetUserWishlist()
        {
            try
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userid))
                    return Unauthorized("Invalid user.");

                var wishlist = await _unitOfWork.WishList.ReadAllAsync();
                if (wishlist == null)
                {
                    return NotFound(new { message = "Wishlist is Empty" });
                }
                var wishlistPosts = wishlist.Where(w => w.UserId == userid).ToList();
                if (wishlistPosts.Count == 0)
                {
                    return NotFound(new { message = "No posts in wishlist" });
                }
                return Ok(wishlistPosts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving wishlist", error = ex.Message });


            }
        }


        [HttpDelete("delete/{postid:int}")]

        public async Task<IActionResult> DeleteFromWishList([FromRoute] int postid)
        {
            try
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userid))
                    return Unauthorized("Invalid user.");

                Wishlist? wishlist = await _unitOfWork.WishList.GetItemAsync(e => e.PostId == postid && e.UserId == userid);
                if (wishlist == null)
                    return NotFound(new { message = "Post not found in wishlist" });

                await _unitOfWork.WishList.Delete(e => e.PostId == postid && e.UserId == userid);
                await _unitOfWork.Save();

                return Ok(new { message = "Post Removed from wishlist", wishlist });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting from wishlist", error = ex.Message });
            }
        }
        [HttpDelete("deleteAll")]
        public async Task<IActionResult> DeleteAllWishList()
        {
            try
            {
                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userid))
                    return Unauthorized("Invalid user.");

                var wishlist = await _unitOfWork.WishList.ReadAllAsync();
                if (wishlist == null)
                    return NotFound(new { message = "WishList Empty" });
                foreach (var item in wishlist)
                {
                    await _unitOfWork.WishList.Delete(e => e.PostId == item.PostId && e.UserId == userid);
                }

                await _unitOfWork.WishList.Delete(e => e.UserId == userid);

                await _unitOfWork.Save();

                return Ok(new { message = "Wishlist is Clear now"});

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error deleting All wishlist", error = ex.Message });
            }


        }


        [HttpGet("IsPostinWishlist/{postid:int}")]
        public async Task<IActionResult> IsPostinWishlist([FromRoute] int postid)
        {
            try
            {

                if (!int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int userid))
                    return Unauthorized("Invalid user.");

                var wishlist = await _unitOfWork.WishList.GetItemAsync(e => e.PostId == postid && e.UserId == userid);
                if (wishlist == null)
                    return NotFound(new { isInWishlist = false});
                return Ok(new { isInWishlist = true });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error retrieving wishlist", error = ex.Message });
            }
        }




    }
}
