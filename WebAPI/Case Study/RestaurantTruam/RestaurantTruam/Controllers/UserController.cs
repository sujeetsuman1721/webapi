using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestaurantTruam.Models;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace RestaurantTruam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;

        public UserController(UserManager<ApplicationUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                              IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }
        [HttpPost("signup")]
        public async Task<IActionResult> Register(UserModel model)
        {
            //if (!ModelState.IsValid) return BadRequest(ModelState);

            //IdentityUser appUser = new IdentityUser
            ApplicationUser appUser = new ApplicationUser
            {
                UserName = model.Username,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                FullName = model.FullName,
                Age = model.Age
            };

            IdentityResult result = await userManager.CreateAsync(appUser, model.Password);

            if (!result.Succeeded) return BadRequest(result.Errors);

            if (!await roleManager.RoleExistsAsync("Customers"))
                await roleManager.CreateAsync(new IdentityRole { Name = "Customers" });

            result = await userManager.AddToRoleAsync(appUser, "Customers");

            if (!result.Succeeded) return BadRequest(result.Errors);

            return Ok();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            //IdentityUser appUser = await userManager.FindByNameAsync(model.Username);
            ApplicationUser appUser = await userManager.FindByNameAsync(model.Username);

            if (appUser == null) return BadRequest("Invalid username/password");

            bool isValid = await userManager.CheckPasswordAsync(appUser, model.Password);

            if (!isValid) return BadRequest("Invalid username/password");
            return Ok(isValid);


        }
    }
}
