using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TravelExperienceEgypt.API.DTOs;
using TravelExperienceEgypt.DataAccess.Models;

namespace TravelExperienceEgypt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AccountController(RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager
            )
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _userManager.Users.AnyAsync(u => u.Email == model.Email))
            {
                return Conflict(new { message = "Email is already registered." });
            }
            ApplicationUser user = new ApplicationUser()
            {
               // FirstName = model.FirstName,
                // LastName = model.LastName,
                UserName = model.UserName,
                Email = model.Email,
                AboutMe = model.AboutMe,
                Country = model.Country,
                City = model.City,
                Image = model.Image,
                CoverImage = model.CoverImage
            };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
              if(await _userManager.Users.AnyAsync())  await _userManager.AddToRoleAsync(user, "Admin");
              else await _userManager.AddToRoleAsync(user, "User");
               
                //create token
                await _signInManager.SignInAsync(user, isPersistent: false);
                return Ok(new { message = "User registered successfully." });
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest(new { errors });
            }

        }
    }
}
