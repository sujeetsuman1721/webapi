using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RestaurantTruam.Models;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RestaurantTruam.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IConfiguration configuration;


        public CustomerController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public IEnumerable<MenuItem> Get()
        {
            string con = configuration["ConnectionStrings.con"];
            List<MenuItem> menuItems = new List<MenuItem>();
            MenuItemOparation menuItemOparation = new MenuItemOparation();


            menuItems = menuItemOparation.GetMenuItemData(con);
            return menuItems;
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public IEnumerable<MenuItem> Get(int id)
        {
            string con = configuration["ConnectionStrings.con"];
            List<MenuItem> menuItems = new List<MenuItem>();
            MenuItemOparation menuItemOparation = new MenuItemOparation();


            menuItems = menuItemOparation.GetMenuItemByID(con,id);
            return menuItems;
        }

        // POST api/<CustomerController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
