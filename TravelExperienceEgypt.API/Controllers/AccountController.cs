using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using TravelExperienceEgypt.API.DTOs;
using TravelExperienceEgypt.BusinessLogic.Services;
using TravelExperienceEgypt.DataAccess.Models;

namespace TravelExperienceEgypt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly AccountService _accountService;

        public AccountController(RoleManager<ApplicationRole> roleManager,
            UserManager<ApplicationUser> userManager, AccountService accountService   )
        {

            _roleManager = roleManager;
            _userManager = userManager;
            _accountService = accountService;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await _userManager.Users.AnyAsync(u => u.Email == model.EmailAddress))
            {
                return Conflict(new { message = "Email is already registered." });
            }
            if (await _userManager.Users.AnyAsync(u => u.UserName == model.UserName))
            {
                return Conflict(new { message = "Username is already taken." });
            }
            ApplicationUser user = new ApplicationUser()
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                UserName = model.UserName,
                Email = model.EmailAddress,
                AboutMe = model.AboutMe,
                Country = model.Country,
                City = model.City,
                Image = model.Image,
                CoverImage = model.CoverImage
            };
            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
              if(await _userManager.Users.AnyAsync())  await _userManager.AddToRoleAsync(user, "User");
              else await _userManager.AddToRoleAsync(user, "Admin");

                DateTime expired = DateTime.Now.AddHours(3);
                string token = await _accountService.GenerateToken(user , expired);
                return Created("", new
                {
                    expired = expired,
                    token = token
                });
            }
            else
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return BadRequest(new { errors });
            }

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
          if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
             ApplicationUser? user= await _userManager.FindByEmailAsync(model.EmailAddress);
            if (user == null)
            {
                return NotFound(new { message = "User not found." });
            }
            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (result) {

                DateTime expireDate = model.RememberMe ? DateTime.Now.AddDays(1) : DateTime.Now.AddHours(3);

                string token = await _accountService.GenerateToken(user, expireDate);

                return Ok(new
                {
                    expired = expireDate,
                    token = token
                });
            }
            else
            {
                return Unauthorized(new { message = "Invalid password." });
            }
        }


      

    }
}
