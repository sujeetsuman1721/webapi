using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestaurantTruam.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestaurantTruam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration configuration;
      

        public AdminController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // GET: api/<AdminController>
        [HttpGet]
        [Authorize]
        public IEnumerable<MenuItem> Get()
        {
            string con = configuration["ConnectionStrings.con"];
            List<MenuItem> menuItems = new List<MenuItem>();
            MenuItemOparation menuItemOparation = new MenuItemOparation();


            menuItems = menuItemOparation.GetMenuItemData(con);
            return menuItems;
        }


        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] MenuItem menu)
        {

            string con = configuration["ConnectionStrings.con"];
            MenuItemOparation menuItemOparation = new MenuItemOparation();
            if (menuItemOparation.UpdateMenu(con, id, menu)) return Ok("Data Updated");

            return BadRequest("There i some error ");

        }


    }
}