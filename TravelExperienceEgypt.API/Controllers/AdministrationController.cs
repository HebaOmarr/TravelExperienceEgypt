using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using TravelExperienceEgypt.API.DTOs;
using TravelExperienceEgypt.DataAccess.Models;

namespace TravelExperienceEgypt.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdministrationController : ControllerBase
    {
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        public AdministrationController(RoleManager<ApplicationRole> roleManager, UserManager<ApplicationUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }
        [HttpPost("createrole")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return BadRequest("Role name cannot be empty.");

            }
            bool exit = await _roleManager.RoleExistsAsync(roleName);
            if (exit)
            {
                return Conflict($"Role '{roleName}' already exists.");
            }
            ApplicationRole newRole = new ApplicationRole
            {
                Name = roleName,
                NormalizedName = roleName.ToUpper()
            };
            IdentityResult result = await _roleManager.CreateAsync(newRole);
            if (result.Succeeded)
                return Ok($"Role '{roleName}' created successfully.");
            return BadRequest("Failed to create role.");
        }
        [HttpPost("deleteRole")]
        public async Task<IActionResult> DeleteRole([FromBody] string roleName)
        {
            if (string.IsNullOrEmpty(roleName))
            {
                return BadRequest("Role name cannot be empty.");
            }
            ApplicationRole? role = await _roleManager.FindByNameAsync(roleName);
            if (role == null)
            {
                return NotFound($"Role '{roleName}' not found.");
            }
            IdentityResult result = await _roleManager.DeleteAsync(role);
            if (result.Succeeded)
            {
                return Ok($"Role '{roleName}' deleted successfully.");
            }
            return BadRequest("Failed to delete role.");
        }


        [HttpPost("asssignRolesToUser")]
        public async Task<IActionResult> AssignRolesToUser([FromBody] UserRoleDTO model)
        {
            if (string.IsNullOrEmpty(model.RoleName) || string.IsNullOrEmpty(model.UserName))
            {
                return BadRequest("Role name and User name cannot be empty.");
            }
           bool exit = await _roleManager.RoleExistsAsync(model.RoleName);
            ApplicationUser? user = await _userManager.FindByNameAsync(model.UserName);
            if (!exit  || user==null)
            {
                return NotFound($"not found.");
            }
            IdentityResult result = await _userManager.AddToRoleAsync(user, model.RoleName);
            if (result.Succeeded)
            {
                return Ok($"Role '{model.RoleName}' assigned to user '{model.UserName}' successfully.");
            }
            return BadRequest("Failed to assign role to user.");
        }
        [HttpPost("removeRoleFromUser")]
        public async Task<IActionResult> RemoveRoleFromUser([FromBody] UserRoleDTO model)
        {
            if (string.IsNullOrEmpty(model.RoleName) || string.IsNullOrEmpty(model.UserName))
            {
                return BadRequest("Role name and User name cannot be empty.");
            }
            bool exit = await _roleManager.RoleExistsAsync(model.RoleName);
            ApplicationUser? user = await _userManager.FindByNameAsync(model.UserName);
            if (!exit || user == null)
            {
                return NotFound($"not found.");
            }
            IdentityResult result = await _userManager.RemoveFromRoleAsync(user, model.RoleName);
            if (result.Succeeded)
            {
                return Ok($"Role '{model.RoleName}' removed from user '{model.UserName}' successfully.");
            }
            return BadRequest("Failed to remove role from user.");
        }

        [HttpGet("getAllRoles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            if (roles == null || roles.Count == 0)
            {
                return NotFound("No roles found.");
            }
            return Ok(roles);
        }


    }
}
