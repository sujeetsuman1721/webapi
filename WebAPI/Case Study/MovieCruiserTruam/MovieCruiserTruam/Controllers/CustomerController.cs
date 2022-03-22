using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MovieCruiserTruam.Models;
using System;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MovieCruiserTruam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;

        public CustomerController(UserManager<ApplicationUser> userManager,
                              RoleManager<IdentityRole> roleManager,
                              IConfiguration configuration)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.configuration = configuration;
        }
        [HttpGet]
        public IEnumerable<Movie> Get()
        {
            string str = configuration["ConnectionStrings:con"];
            var dt = DateTime.Now;
            return MovieOparation.GetMovieLIst(str).Where(p => p.Active == true && p.DateOfLaunch <= dt);

        }

        // GET: api/Customer/5
        [HttpGet("{userid}", Name = "Get Customer")]
        public object Get(int userid)
        {
            int movieCount = 0;
            List<Movie> list = new List<Movie>(MovieOparation.FavoriteList(userid, ref movieCount));

            return new { list, movieCount };
        }

        // POST: api/Customer
        [HttpPost]
        public IActionResult Post([FromBody] List<Favarite> fav)
        {
            MovieOparation.InsertIntoFavorites(fav);
            return Ok();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{favId}")]
        public string Delete(int favId)
        {
            return MovieOparation.Delete(favId);
        }
    }
}