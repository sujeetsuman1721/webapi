using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FirstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValueController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("this is get method");
        }

        // this is 
        [HttpPost]

        public IActionResult Post()
        {
            return Ok("this is get method");
        }
        [HttpPatch]

        public IActionResult Put()
        {
            return Ok("this is patch method");
        }
        [HttpDelete]

        public IActionResult Delete()
        {
            return Ok("the message is deleted");
        }
    }
}
