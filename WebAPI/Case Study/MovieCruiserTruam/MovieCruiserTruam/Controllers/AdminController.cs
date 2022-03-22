using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MovieCruiserTruam.Models;
using System.Collections.Generic;

namespace MovieCruiserTruam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;

        public AdminController(UserManager<ApplicationUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                              IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }
        // GET: api/<AdminController>
        [HttpGet]
        [Authorize]
        public IEnumerable<Movie> Get()
        {
           
            string con = configuration["ConnectionStrings.con"];
            List<Movie> menuItems = new List<Movie>();
            MovieOparation movieItemOparation = new MovieOparation();


            menuItems = (List<Movie>)MovieOparation.GetMovieLIst(con);
            return menuItems;
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Movie movie)
        {

            string con = configuration["ConnectionStrings.con"];
            
            if (MovieOparation.Update(id,movie)) return Ok("Data Updated");

            return BadRequest("There is some error ");

        }


    }
}