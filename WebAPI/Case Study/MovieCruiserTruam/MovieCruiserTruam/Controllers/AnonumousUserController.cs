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
    public class AnonumousUserController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IConfiguration configuration;

        public AnonumousUserController(UserManager<ApplicationUser> userManager,
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
            string con = configuration["ConnectionStrings:con"];

            MovieOparation movieOparation = new MovieOparation();
            List<Movie> movieList = new List<Movie>();
            movieList = (List<Movie>)MovieOparation.GetMovieLIst(con);
            return movieList;
        }
    }
}
