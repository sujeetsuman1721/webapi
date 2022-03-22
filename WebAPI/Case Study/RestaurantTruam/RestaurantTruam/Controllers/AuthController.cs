using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RestaurantTruam.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantTruam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;

        public AuthController(UserManager<ApplicationUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                              IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }
        public async Task<IActionResult> GetAsync(LoginModel model)
        {
            LoginModel loginModel = new LoginModel();
            loginModel.Username = ("User1");
            loginModel.Password = ("paswword1");
            loginModel.Id = 1;
            string token = await GetTockenAsync(loginModel);
            return Ok(new { jwt = token });
        }
        private async Task<string> GetTockenAsync(LoginModel model)
        {
            ApplicationUser appUser = await userManager.FindByNameAsync(model.Username);


            bool isValid = await userManager.CheckPasswordAsync(appUser, model.Password);


            string key = configuration["JwtSettings:Key"];
            string issuer = configuration["JwtSettings:Issuer"];
            string audience = configuration["JwtSettings:Audience"];
            int durationInMinutes = int.Parse(configuration["JwtSettings:DurationInMinutes"]);

            IList<Claim> userClaims = await userManager.GetClaimsAsync(appUser);
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));
            userClaims.Add(new Claim(JwtRegisteredClaimNames.Sub, appUser.FullName));

            byte[] keyBytes = System.Text.Encoding.ASCII.GetBytes(key);
            SecurityKey securityKey = new SymmetricSecurityKey(keyBytes);
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            JwtSecurityToken jwtSecurityToken = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                expires: DateTime.Now.AddMinutes(durationInMinutes),
                signingCredentials: signingCredentials,
                claims: userClaims
                );
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            string token = tokenHandler.WriteToken(jwtSecurityToken);

            return token;
        }
    }
}
