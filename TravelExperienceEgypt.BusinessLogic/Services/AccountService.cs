using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TravelExperienceEgypt.API.DTOs;
using TravelExperienceEgypt.DataAccess.Models;
using TravelExperienceEgypt.DataAccess.UnitOfWork;
namespace TravelExperienceEgypt.BusinessLogic.Services
{
    public class AccountService
    {
        private readonly IConfiguration config;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IUnitOfWork unitOfWork;

        public AccountService(IConfiguration config ,UserManager<ApplicationUser> userManager,IUnitOfWork unitOfWork)
        {
            this.config = config;
            this.userManager = userManager;
            this.unitOfWork = unitOfWork;
        }
        public async Task<string> GenerateToken(ApplicationUser user ,DateTime expire)
        {
            string jti = Guid.NewGuid().ToString();
            string userID=user.Id.ToString();
            var userRoles = await userManager.GetRolesAsync(user);

            List<Claim> claim = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userID),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,jti)
            };
            if (userRoles != null)
            {
                foreach (var role in userRoles)
                {
                    claim.Add(new Claim(ClaimTypes.Role, role));
                }
            }

            //----------------------------------
            SymmetricSecurityKey signKey=
                new(Encoding.UTF8.GetBytes(config["Jwt:Key"]));

            SigningCredentials signingcredential = new SigningCredentials
                (signKey,SecurityAlgorithms.HmacSha256);

            JwtSecurityToken myToken = new JwtSecurityToken(
                issuer: config["Jwt:Issuer"],
                audience: config["Jwt:Audience"],
                expires: expire,
                claims:claim,
                signingCredentials:signingcredential
                );

            return new JwtSecurityTokenHandler().WriteToken(myToken);
        }

        public async Task<UserProfileDTO> usrTOuserProfileAsync(ApplicationUser user)
        {
            var posts = await unitOfWork.Post.ReadAllAsync("Place");
            posts = posts.Where(e => e.UserId == user.Id);
            //erorr Type Place Not PlaceService
            IEnumerable<Place>? places = posts.Select(e => e.Place).Distinct();
            return new UserProfileDTO
            {
               
                FirstName = user.FirstName,
                LastName = user.LastName,
                UserName = user.UserName,
                AboutMe = user.AboutMe,
                Country = user.Country,
                City = user.City,
                EmailAddress = user.Email,
                Image = user.Image,
                CoverImage = user.CoverImage,
                Posts = posts,
                Places = places
            };
        }

        public ApplicationUser ProfileTOuser(UserProfileDTO userDTO)
        {
            return new ApplicationUser
            {
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                UserName = userDTO.UserName,
                AboutMe = userDTO.AboutMe,
                Email = userDTO.EmailAddress,
                Country = userDTO.Country,
                City = userDTO.City,
                Image = userDTO.Image,
                CoverImage = userDTO.CoverImage
            };
        }


    }
}
