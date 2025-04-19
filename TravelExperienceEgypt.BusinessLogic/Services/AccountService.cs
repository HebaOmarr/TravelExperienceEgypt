using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TravelExperienceEgypt.DataAccess.Models;

namespace TravelExperienceEgypt.BusinessLogic.Services
{
    public class AccountService
    {
        private readonly IConfiguration config;
        private readonly UserManager<ApplicationUser> userManager;

        public AccountService(IConfiguration config ,UserManager<ApplicationUser> userManager)
        {
            this.config = config;
            this.userManager = userManager;
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
    }
}
